using System;
using System.Collections.Generic;
using System.Text;
using KYOMS.Core20.Common.BIZ;
using KYOMS.Core20.Common.Log4NetCore;
using KYOMS.Core20.Entity.MySqlDB;
using ZQOMS.Core20.Repository;
using ZQOMS.Core20.Repository.Abstractions;

namespace KYOMS.Core20.Application.Interface
{
    public abstract class AppService : IAppService
    {
        //protected LoggerHandle Logger;
        protected IUnitOfWork MysqlScope;
        protected IUnitOfWork OracleScope;
        protected IUnitOfWork SqlServerScope;

        protected AppService(string dbConfig, DbConfigType dbType)
        {
            Initialize(dbConfig, dbType);
        }

        protected AppService(string mySqlConfig = null, string oracleConfig = null, string sqlServerConfig = null)
        {
            if (!string.IsNullOrWhiteSpace(mySqlConfig))
                Initialize(mySqlConfig, DbConfigType.MySql);

            if (!string.IsNullOrWhiteSpace(oracleConfig))
                Initialize(oracleConfig, DbConfigType.Oracle);

            if (!string.IsNullOrWhiteSpace(sqlServerConfig))
                Initialize(sqlServerConfig, DbConfigType.SqlServer);
        }

        public virtual void Dispose()
        {
            //若对象非空，则销毁
            MysqlScope?.Dispose();
            OracleScope?.Dispose();
            SqlServerScope?.Dispose();

            GC.SuppressFinalize(this);
        }

        private void Initialize(string dbConfig, DbConfigType dbType)
        {
            switch (dbType)
            {
                case DbConfigType.MySql:
                    MysqlScope = new UnitOfMySqlWork(dbConfig);
                    break;
                case DbConfigType.Oracle:
                    OracleScope = new UnitOfOracleWork(dbConfig);
                    break;
                case DbConfigType.SqlServer:
                    SqlServerScope = new UnitOfSqlServerWork(dbConfig);
                    break;
                default:
                    throw new ArgumentNullException(nameof(dbType), "未确认创建何种数据库对象");
            }
        }

        #region Generate

        protected virtual object CreateMysqlOrderParams(T_MySql_Order mysqlOrder)
        {
            #region 拼接动态类型的对象作为参数传递
            ////获取动态表名
            //var tableName = MysqlTableName.GetMySqlOrderTableName();
            ////声明动态对象
            //dynamic dyObj = new System.Dynamic.ExpandoObject();
            //var jObj = JObject.FromObject(mySqlOrder);
            //var jTokenPair = jObj.Children().ToArray();
            //foreach (var jToken in jTokenPair)
            //{
            //    var pair = (JProperty)jToken;
            //    ((IDictionary<string, object>)dyObj).Add(pair.Name, pair.Value);
            //}
            //((IDictionary<string, object>)dyObj).Add("tableName", tableName);
            //var json = JsonConvert.SerializeObject((object)dyObj);
            //var parameters = JsonConvert.DeserializeObject(json);
            #endregion

            var tableName = MysqlTableName.GetMySqlOrderTableName();
            var parameters = new
            {
                tableName,
                mysqlOrder.OUTSYS_ORDER_NO,
                mysqlOrder.OUTSYS_BILL_CODE,
                mysqlOrder.ORDER_SOURCE,
                mysqlOrder.MSG_CONTENT,
                mysqlOrder.MSG_TYPE,
                mysqlOrder.REMARK,
                mysqlOrder.IS_SYNC_SUCCESS,
                mysqlOrder.CREATE_BY,
                mysqlOrder.CREATE_TIME,
                C1 = "",
                C2 = "",
                C3 = ""
            };
            return parameters;
        }

        #endregion
    }

    /// <summary>
    /// 用于创建哪一种数据库连接的对象
    /// </summary>
    public enum DbConfigType : ushort
    {
        MySql = 0XDB01,
        Oracle = 0XDB02,
        SqlServer = 0XDB03
    }
}
