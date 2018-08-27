using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace KYOMS.Core20.Respository.Core
{
    public sealed class DBConnectionSingletion : ObjectPool
    {
        private DBConnectionSingletion() { }

        public static readonly DBConnectionSingletion Instance = new DBConnectionSingletion();

        private static string connectionString = @"server=(local);Trusted Connection=yes;database=northwind";

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        protected override object Create()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                MySqlConnection conn = (MySqlConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                MySqlConnection conn = (MySqlConnection)o;
                conn.Close();
            }
            catch (MySqlException) { }
        }

        public MySqlConnection BorrowDBConnection()
        {
            try
            {
                return (MySqlConnection)base.GetObjectFromPool();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReturnDBConnection(MySqlConnection conn)
        {
            base.ReturnObjectToPool(conn);
        }
    }
}
