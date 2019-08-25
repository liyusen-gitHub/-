using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace MZ_MES_MAIN
{
    public class XmlUtil
    {
        public String Serializer(Object obj)
        {
            String result = String.Empty;
            MemoryStream ms = null;
            XmlSerializer mySerializer = null;
            ResultWrap rw = null;
            XmlSerializerNamespaces xmlSerializerNamespaces = null;
            XmlWriterSettings setting = null;
            try
            {
                rw = VerificationRequestData(obj);
                if (rw.SIGN != EnumResultFlagType.Y)
                {
                    throw new ArgumentException("obj参数校验失败: " + rw.MESSAGE);
                }
                xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                obj = FormatRequestObjData(obj);
                setting = new XmlWriterSettings();
                setting.Encoding = new UTF8Encoding(false);
                setting.Indent = false;
                mySerializer = new XmlSerializer(obj.GetType());
                using (ms = new MemoryStream())
                {
                    using (XmlWriter writer = XmlWriter.Create(ms, setting))
                    {
                        mySerializer.Serialize(writer, obj, xmlSerializerNamespaces);
                    }
                    result = Encoding.UTF8.GetString(ms.ToArray());
                    //result = formatRequestStrData(result);
                    ms.Close();
                }
            }
            catch (Exception exc)
            {
                result = String.Empty;
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
                throw exc;
            }
            return result;
        }

        public Object Deserialize(String data, Type returnObjType)
        {
            Object obj = null;
            MemoryStream ms = null;
            XmlSerializer mySerializer = null;
            if (String.IsNullOrEmpty(data))
            {
                return obj;
            }
            try
            {
                obj = new Object();
                mySerializer = new XmlSerializer(returnObjType);
                ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
                obj = mySerializer.Deserialize(ms);
                ms.Close();
            }
            catch (Exception exc)
            {
                obj = null;
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
                throw exc;
            }
            return obj;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        public ResultWrap VerificationRequestData(Object requestObj)
        {
            ResultWrap rw = null;
            Type objType = null;
            FieldInfo[] fieldInfos = null;
            PropertyInfo[] propertyInfos = null;
            FieldVerificationAttribute[] fieldVerifiers = null;
            FieldVerificationAttribute fieldVerifier = null;
            String str = null;
            Object[] tmpAttrs = null;
            try
            {
                if (requestObj == null)
                {
                    throw new ArgumentNullException("requestObj对象不能为空!");
                }
                objType = requestObj.GetType();
                propertyInfos = objType.GetProperties();
                fieldInfos = objType.GetFields();
                if ((fieldInfos == null || fieldInfos.Length <= 0) && (propertyInfos == null || propertyInfos.Length <= 0))
                {
                    throw new ArgumentException("没有给业务对象添加字段!(" + requestObj.ToString() + ")");
                }
                if (fieldInfos != null)
                {
                    foreach (FieldInfo field in fieldInfos)
                    {
                        if (field.FieldType.BaseType == typeof(Array))
                        {
                            //数组
                            Object[] tmpObjs = (Object[])field.GetValue(requestObj);
                            foreach (Object tmpObj in tmpObjs)
                            {
                                rw = VerificationRequestData(tmpObj);
                                if (rw.SIGN == EnumResultFlagType.N)
                                {
                                    return rw;
                                }
                            }
                        }
                        else
                        {
                            //非数组
                            //XmlIgnore属性不判断
                            tmpAttrs = field.GetCustomAttributes(typeof(XmlIgnoreAttribute), false);
                            if (tmpAttrs != null && tmpAttrs.Length > 0)
                            {
                                continue;
                            }
                            //非数组
                            if (field.FieldType.BaseType == typeof(System.Enum))
                            {
                                //类型为枚举的不判断
                                continue;
                            }


                            if (field.FieldType.BaseType == typeof(System.Enum))
                            {
                                //类型为枚举的不判断
                                continue;
                            }
                            fieldVerifiers = (FieldVerificationAttribute[])field.GetCustomAttributes(typeof(FieldVerificationAttribute), false);
                            if (fieldVerifiers == null || fieldVerifiers.Length <= 0)
                            {
                                throw new ArgumentException("没有给业务对象添加属性!" + requestObj.ToString() + "");
                            }
                            fieldVerifier = fieldVerifiers[0];
                            str = field.GetValue(requestObj).ToString();
                            rw = checkData(str, fieldVerifier, field);
                            if (rw.SIGN == EnumResultFlagType.N)
                            {
                                return rw;
                            }
                        }
                    }
                }
                if (propertyInfos != null)
                {
                    foreach (PropertyInfo property in propertyInfos)
                    {
                        if (property.PropertyType.BaseType == typeof(Array))
                        {
                            //数组
                            Object[] tmpObjs = (Object[])property.GetValue(requestObj, null);
                            foreach (Object tmpObj in tmpObjs)
                            {
                                rw = VerificationRequestData(tmpObj);
                                if (rw.SIGN == EnumResultFlagType.N)
                                {
                                    return rw;
                                }
                            }
                        }
                        else
                        {
                            //非数组
                            //XmlIgnore属性不判断
                            tmpAttrs = property.GetCustomAttributes(typeof(XmlIgnoreAttribute), false);
                            if (tmpAttrs != null && tmpAttrs.Length > 0)
                            {
                                continue;
                            }
                            //非数组
                            if (property.PropertyType.BaseType == typeof(System.Enum))
                            {
                                //类型为枚举的不判断
                                continue;
                            }
                            fieldVerifiers = (FieldVerificationAttribute[])property.GetCustomAttributes(typeof(FieldVerificationAttribute), false);
                            if (fieldVerifiers == null || fieldVerifiers.Length <= 0)
                            {
                                throw new ArgumentException("没有给业务对象添加属性!" + requestObj.ToString() + "");
                            }
                            fieldVerifier = fieldVerifiers[0];
                            str = property.GetValue(requestObj, null).ToString();
                            rw = checkData(str, fieldVerifier, property);
                            if (rw.SIGN == EnumResultFlagType.N)
                            {
                                return rw;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                rw = null;

                throw exc;
            }
            return rw;
        }

        /// <summary>
        /// 检查属性上的验证规则是否符合
        /// </summary>
        /// <param name="str">要验证的字符串</param>
        /// <param name="fieldVerifier">验证规则对象</param>
        /// <returns></returns>
        private ResultWrap checkData(String str, FieldVerificationAttribute fieldVerifier, FieldInfo field)
        {
            ResultWrap rw = null;
            Object[] tmpAttrs = null;
            try
            {
                tmpAttrs = field.GetCustomAttributes(typeof(XmlIgnoreAttribute), false);
                if (tmpAttrs != null && tmpAttrs.Length > 0)
                {
                    rw = new ResultWrap(EnumResultFlagType.Y, "");
                    return rw;
                }
                int pos = -1;
                //判断是否是必填字段
                if (fieldVerifier.IsRequired)
                {
                    if (String.IsNullOrEmpty(str))
                    {
                        //空
                        rw = new ResultWrap(EnumResultFlagType.N, field.Name + "字段验证失败: 字段不能为空!");
                        return rw;
                    }
                }
                //判断长度是否超长
                if (str.Length > fieldVerifier.FieldLength)
                {
                    rw = new ResultWrap(EnumResultFlagType.N, field.Name + "字段验证失败: 字段长度超过规则长度!");
                    return rw;
                }
                //判断是否是数字
                if (fieldVerifier.IsNumberic && !String.IsNullOrEmpty(str))
                {
                    if (Regex.IsMatch(str, @"[^-0-9.]"))
                    {
                        //含有非数字
                        rw = new ResultWrap(EnumResultFlagType.N, field.Name + "字段验证失败: 字段不符合数字规则!");
                        return rw;
                    }
                }
                //判断是否是金额
                if (fieldVerifier.IsDecimalDigit && !String.IsNullOrEmpty(str))
                {
                    pos = str.IndexOf('.');
                    if ((pos <= 0) || (str.Length - (pos + 1) != fieldVerifier.DecimalDigitsLength))
                    {
                        //不符合金额
                        rw = new ResultWrap(EnumResultFlagType.N, field.Name + "字段验证失败: 字段不符合金额规则!");
                        return rw;
                    }
                }
                rw = new ResultWrap(EnumResultFlagType.Y, "");
            }
            catch (Exception exc)
            {
                rw = null;
               
                throw exc;
            }
            return rw;
        }
        private ResultWrap checkData(String str, FieldVerificationAttribute fieldVerifier, PropertyInfo property)
        {
            ResultWrap rw = null;
            Object[] tmpAttrs = null;
            try
            {
                tmpAttrs = property.GetCustomAttributes(typeof(XmlIgnoreAttribute), false);
                if (tmpAttrs != null && tmpAttrs.Length > 0)
                {
                    rw = new ResultWrap(EnumResultFlagType.Y, "");
                    return rw;
                }

                int pos = -1;
                //判断是否是必填字段
                if (fieldVerifier.IsRequired)
                {
                    if (String.IsNullOrEmpty(str))
                    {
                        //空
                        rw = new ResultWrap(EnumResultFlagType.N, property.Name + "字段验证失败: 字段不能为空!");
                        return rw;
                    }
                }
                //判断长度是否超长
                if (str.Length > fieldVerifier.FieldLength)
                {
                    rw = new ResultWrap(EnumResultFlagType.N, property.Name + "字段验证失败: 字段长度超过规则长度!");
                    return rw;
                }
                //判断是否是数字
                if (fieldVerifier.IsNumberic && !String.IsNullOrEmpty(str))
                {
                    if (Regex.IsMatch(str, @"[^-0-9.]"))
                    {
                        //含有非数字
                        rw = new ResultWrap(EnumResultFlagType.N, property.Name + "字段验证失败: 字段不符合数字规则!");
                        return rw;
                    }
                }
                //判断是否是金额
                if (fieldVerifier.IsDecimalDigit && !String.IsNullOrEmpty(str))
                {
                    pos = str.IndexOf('.');
                    if ((pos <= 0) || (str.Length - (pos + 1) != fieldVerifier.DecimalDigitsLength))
                    {
                        //不符合金额
                        rw = new ResultWrap(EnumResultFlagType.N, property.Name + "字段验证失败: 字段不符合数字规则!");
                        return rw;
                    }
                }
                rw = new ResultWrap(EnumResultFlagType.Y, "");
            }
            catch (Exception exc)
            {
                rw = null;
                throw exc;
            }
            return rw;
        }

        /// <summary>
        /// 格式化数据
        /// </summary>
        /// <param name="data">要格式化数据</param>
        /// <returns>返回格式化后的数据</returns>
        public String FormatRequestStrData(String data)
        {
            String result = data.Trim();
            return result;
        }

        /// <summary>
        /// 将实体对象字段内的空格替换掉
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Object FormatRequestObjData(Object obj)
        {
            Type objType = obj.GetType();
            String str = String.Empty;
            string pattern = @"\s+";
            Regex rgx = new Regex(pattern);
            FieldInfo[] fis = objType.GetFields();
            PropertyInfo[] pis = objType.GetProperties();
            try
            {
                for (int i = 0; i < fis.Length; i++)
                {
                    if (rgx.IsMatch(str))
                    {
                        str = fis[i].GetValue(obj).ToString();
                        str = rgx.Replace(str, "");
                        fis[i].SetValue(obj, str);
                    }
                }
                for (int i = 0; i < pis.Length; i++)
                {
                    if (rgx.IsMatch(str))
                    {
                        str = pis[i].GetValue(obj, null).ToString();
                        str = rgx.Replace(str, "");
                        pis[i].SetValue(obj, str, null);
                    }
                }
            }
            catch (Exception exc)
            {
                obj = null;
               
                throw exc;
            }
            return obj;
        }
    }
}
