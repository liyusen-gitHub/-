using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MZ_MES_MAIN
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FieldVerificationAttribute : Attribute
    {
        private int fieldLength = 0;
        /// <summary>
        /// 字段长度,如果是数字或金额包括小数位数和小数点比如 17.77对于这一个数值来说 FieldLength=5
        /// </summary>
        public int FieldLength
        {
            get { return fieldLength; }
            set { fieldLength = value; }
        }

        private Boolean isNumberic = false;

        /// <summary>
        /// 是否是数字,true:数字,false:非数字
        /// </summary>
        public Boolean IsNumberic
        {
            get { return isNumberic; }
            set { isNumberic = value; }
        }
        private int decimalDigits = 2;

        /// <summary>
        /// 小数位(默认2位)
        /// </summary>
        public int DecimalDigitsLength
        {
            get { return decimalDigits; }
            set { decimalDigits = value; }
        }



        private Boolean isMoney = false;
        /// <summary>
        /// 是否带小数,true:带小数,false:不带小数
        /// </summary>
        public Boolean IsDecimalDigit
        {
            get { return isMoney; }
            set
            {
                if (value == true)
                {
                    this.isNumberic = true;
                }
                isMoney = value;

            }
        }

        private Boolean isRequiredField = false;

        /// <summary>
        /// 是否是必填字段,true:必填,false:非必填,默认false
        /// </summary>
        public Boolean IsRequired
        {
            get { return isRequiredField; }
            set { isRequiredField = value; }
        }


        /// <summary>
        /// 字段验证属性
        /// </summary>
        public FieldVerificationAttribute()
        {

        }
    }
}
