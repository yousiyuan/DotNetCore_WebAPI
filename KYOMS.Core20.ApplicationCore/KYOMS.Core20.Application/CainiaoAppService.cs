using KYOMS.Core20.Application.Interface;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Entity.MySqlDB;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.Application
{
    public class CainiaoAppService : AppService
    {
        public CainiaoAppService(string dbConfig, DbConfigType dbType)
            : base(dbConfig, dbType)
        {
            //Logger = new LoggerHandle(null, logfilename);
        }

        public CainiaoAppService(string mySqlConfig = null, string oracleConfig = null, string sqlServerConfig = null)
            : base(mySqlConfig, oracleConfig, sqlServerConfig)
        {
            //Logger = new LoggerHandle(null, logfilename);
        }

        public override void Dispose()
        {
            //Logger = null;
            GC.SuppressFinalize(this);

            base.Dispose();
        }

        /// <summary>
        /// 写入到数据库Order-插入前检查是否重复，重复数据不插入，按天分表，表名动态传入
        /// </summary>
        /// <param name="mySqlOrder"></param>
        /// <returns></returns>
        public async Task<bool> AddOrder(T_MySql_Order mySqlOrder)
        {
            try
            {
                var parameters = CreateMysqlOrderParams(mySqlOrder);
                var eftRows = await MysqlScope.Repository.InsertAsync<T_MySql_Order>("T_MySql_Order.Insert", parameters);
                return eftRows > 0;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                var message = new StringBuilder();
                message.Append("新增订单写入时数据库发生错误:\n" + "订单号：" + mySqlOrder.OUTSYS_BILL_CODE + "运单号：" + mySqlOrder.OUTSYS_ORDER_NO);
                message.Append(msg);
                msg = message.ToString();

                msg += string.Format("\n JSON:\n<MySqlOrderData>{0}</MySqlOrderData>\n", mySqlOrder.MSG_CONTENT);
                msg.WriteToLog(LogerType.Error);

                return false;
            }
        }
    }
}
