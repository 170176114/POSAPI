using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace mobileAPI.ODBCDB
{
    public class db
    {

        #region ODBCSetUp
        private string GetConnectionString(string DBType)
        {
            string UserID = "sontecdba";//System.Configuration.ConfigurationManager.AppSettings["userid"];
            string Password = "sontecdb";//System.Configuration.ConfigurationManager.AppSettings["userpw"];
            string Server = System.Configuration.ConfigurationManager.AppSettings["servername"];
            string ConnectionString;

            switch (DBType)
            {
                case "Firebird": ConnectionString = "DSN=" + Server; break;
                case "Oracle": ConnectionString = "Driver={Microsoft ODBC for Oracle};Server=" + Server + ";UID=" + UserID + ";PWD=" + Password + ";"; break;
                //Default Firebird Connection
                default: ConnectionString = "DSN=" + Server; break;
            }

            return ConnectionString;
        }

        protected bool OpenConnection(out OdbcConnection odbc)
        {
            string ConnectionString = GetConnectionString(System.Configuration.ConfigurationManager.AppSettings["servertype"]);
            OdbcConnection DbConnection;

            try
            {
                DbConnection = new OdbcConnection(ConnectionString);
                DbConnection.ConnectionTimeout = 5;
                //log.writeLog("Start to conncet server [" + gv.Server + "].");
                DbConnection.Open();
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                //log.writeLog("Can't conncet server [" + gv.Server + "] : Error Message [" + e.Message.ToString() + "]");
                odbc = null;
                return false;
            }

            //log.writeLog("Server [" + gv.Server + "] was opened.");
            odbc = DbConnection;
            return true;
        }

        protected short CloseConnection(OdbcConnection odbc)
        {
            try
            {
                odbc.Close();
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                //log.writeLog("Can't close server!! : Error Message [" + e.Message.ToString() + "]");
                return -2;
            }

            //log.writeLog("Server was closed.");
            return 1;
        }

        protected class GetDBData
        {
            public OdbcDataReader dr;
            public GetDBData(OdbcDataReader DbReader) { this.dr = DbReader; }
            private bool IsNotNull(string columnName) { return (!dr.IsDBNull(dr.GetOrdinal(columnName))); }
            public string GetString(string columnName) { return ((IsNotNull(columnName)) ? dr.GetString(dr.GetOrdinal(columnName)) : ""); }
            public int GetInteger(string columnName) { return ((IsNotNull(columnName)) ? dr.GetInt16(dr.GetOrdinal(columnName)) : 0); }
            public decimal GetDecimal(string columnName) { return ((IsNotNull(columnName)) ? dr.GetDecimal(dr.GetOrdinal(columnName)) : 0); }
            public DateTime GetDateTime(string columnName) { return ((IsNotNull(columnName)) ? dr.GetDateTime(dr.GetOrdinal(columnName)) : DateTime.Now); }

        }
        #endregion

    }
}