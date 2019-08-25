using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MZ_MES_MAIN
{
    [XmlRootAttribute("DATA", Namespace = "", IsNullable = false)]
    public class MaterialSyncRequestWrap
    {
        private DateTime _开始日期 = DateTime.MinValue;
        /// <summary>
        /// 开始日期
        /// </summary>
        [XmlElementAttribute("FROM_DATE", IsNullable = false)]
        [FieldVerificationAttribute(FieldLength = 8, IsRequired = true)]
        public String 开始日期
        {
            get
            {
                return _开始日期.ToString("yyyyMMdd");
            }
            set
            { }
        }

        private DateTime _结束日期 = DateTime.MinValue;
        /// <summary>
        /// 结束日期
        /// </summary>
        [XmlElementAttribute("TO_DATE", IsNullable = false)]
        [FieldVerificationAttribute(FieldLength = 8, IsRequired = true)]
        public String 结束日期
        {
            get
            {
                return _结束日期.ToString("yyyyMMdd");
            }
            set
            { }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        [XmlElementAttribute("FROM_TIME", IsNullable = false)]
        [FieldVerificationAttribute(FieldLength = 6, IsRequired = true)]
        public String 开始时间
        {
            get
            {
                return _开始日期.ToString("HHmmss");
            }
            set
            { }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        [XmlElementAttribute("TO_TIME", IsNullable = false)]
        [FieldVerificationAttribute(FieldLength = 6, IsRequired = true)]
        public String 结束时间
        {
            get
            {
                return _结束日期.ToString("HHmmss");
            }
            set
            { }
        }

        public void SetStartDatetime(DateTime startDt)
        {
            this._开始日期 = startDt;
        }
        public void SetEndDatetime(DateTime endDt)
        {
            this._结束日期 = endDt;
        }
        public DateTime GetStartDatetime()
        {
            return this._开始日期;
        }
        public DateTime GetEndDatetime()
        {
            return this._结束日期;
        }
    }
}
