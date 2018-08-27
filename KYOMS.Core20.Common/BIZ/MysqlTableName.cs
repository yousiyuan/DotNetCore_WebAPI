using System;

namespace KYOMS.Core20.Common.BIZ
{
    public class MysqlTableName
    {
        /// <summary>
        /// 获取MySql的订单表名
        /// 生成规则是：T_MySql_Order_yyyyMMdd
        /// </summary>
        /// <returns></returns>
        public static string GetMySqlOrderTableName()
        {
            return string.Format("T_MySql_Order_{0}", DateTime.Now.ToString("yyyyMMdd"));
            //return "T_MySql_Order_20171008";
        }

        /// <summary>
        /// 获取MySql的订单表名
        /// 生成规则是：T_MySql_Order_yyyyMMdd
        /// </summary>
        /// <returns></returns>
        public static string GetMySqlOrderTableName(string EndTag)
        {
            return string.Format("T_MySql_Order_{0}", EndTag);
        }

    }
}
