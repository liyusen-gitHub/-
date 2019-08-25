using System;
using System.Collections;
using System.Reflection;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Web;

namespace Dynamic_Quote_Service
{
    /// <summary>
    /// WebServiceProxy 的摘要说明。
    /// </summary>
    public sealed class WebServiceProxy
    {
        private static Hashtable _assenblyCache = null;  //缓存提高效率
        public WebServiceProxy()
        {
        }
        //url:服务地址
        //methodname：方法名字
        //args:方法的参数
        public static object InvokeWebservice(string url, string nsClassName, string methodname, params object[] args)
        {
            return InvokeWebservice(url, nsClassName, methodname, 100000, args);
        }
        public static object InvokeWebservice(string url, string nsClassName, string methodname, int timeout, params object[] args)
        {
            if (args.Length == 1 && args[0] is ArrayList)
            {
                args = (args[0] as ArrayList).ToArray();
            }
            try
            {
                int li = nsClassName.LastIndexOf('.');
                string @namespace = (li == -1 ? "" : nsClassName.Substring(0, li));

                Assembly assembly;
                if (_assenblyCache == null)
                {
                    _assenblyCache = new Hashtable();
                }
                if (_assenblyCache.ContainsKey(url.ToUpper()))
                {
                    assembly = (Assembly)_assenblyCache[url.ToUpper()];
                }
                else
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    System.IO.Stream stream = wc.OpenRead(url + "?WSDL");
                    //Configuration.SoapEnvelopeProcessingElement se = new Configuration.SoapEnvelopeProcessingElement();
                    //se.ReadTimeout = 15000;

                    ServiceDescription sd = ServiceDescription.Read(stream);
                    ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                    sdi.AddServiceDescription(sd, "", "");
                    CodeNamespace cn = new CodeNamespace(@namespace);
                    CodeCompileUnit ccu = new CodeCompileUnit();
                    ccu.Namespaces.Add(cn);
                    sdi.Import(cn, ccu);

                    Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();
                    ICodeCompiler icc = csc.CreateCompiler();
                    CompilerParameters cplist = new CompilerParameters();
                    cplist.GenerateExecutable = false;
                    cplist.GenerateInMemory = true;
                    cplist.ReferencedAssemblies.Add("System.dll");
                    cplist.ReferencedAssemblies.Add("System.XML.dll");
                    cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                    cplist.ReferencedAssemblies.Add("System.Data.dll");
                    CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                    if (true == cr.Errors.HasErrors)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        foreach (CompilerError ce in cr.Errors)
                        {
                            sb.Append(ce.ToString());
                            sb.Append(System.Environment.NewLine);
                        }
                        throw new Exception(sb.ToString());
                    }
                    assembly = cr.CompiledAssembly;
                    _assenblyCache[url.ToUpper()] = assembly;
                }
                Type t = null;
                if (String.IsNullOrEmpty(nsClassName))
                {
                    t = assembly.GetTypes()[0];
                }
                else
                {
                    t = assembly.GetType(nsClassName, true, true);
                }
                MethodInfo mi = null;
                if (String.IsNullOrEmpty(methodname))
                {
                    mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)[0];
                }
                else
                {
                    mi = t.GetMethod(methodname);
                }
                SoapHttpClientProtocol obj = Activator.CreateInstance(t) as SoapHttpClientProtocol;
                SetCookie(url, obj);
                //obj.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                obj.Timeout = timeout;
                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }

        /// <summary>
        /// 传入Cookie，使对方可以使用当前Session
        /// By 黄正 2009-12-6
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        private static void SetCookie(string url, SoapHttpClientProtocol obj)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                CookieContainer cc = new CookieContainer();
                foreach (string cookieName in ctx.Request.Cookies.AllKeys)
                {
                    cc.SetCookies(new Uri(url), cookieName + "=" + ctx.Request.Cookies[cookieName].Value);
                }
                //req.Headers.Add(HttpRequestHeader.Cookie, Request.Headers["Cookie"]);
                obj.CookieContainer = cc;
            }
        }
    }
}