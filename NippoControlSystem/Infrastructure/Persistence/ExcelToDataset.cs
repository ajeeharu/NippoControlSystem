using System.Data;
using System.Data.OleDb;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class ExcelToDataset
    {
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //GetInstance
        private static ExcelToDataset m_instance = null;
        public static ExcelToDataset GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ExcelToDataset();
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public ExcelToDataset()
        {
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~ExcelToDataset()
        {
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        public DataSet Read(String path)
        {
            //string conString = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;Imex=1"";", path);
            //string conString = String.Format(@"provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=""Excel 12.0;MAXSCANROWS=16;HDR=Yes;Imex=1"";", path);
            string conString = String.Format(@"provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=""Excel 12.0;HDR=Yes;Imex=1"";", path);
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            DataTable schemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            con.Close();
            DataSet ds = new DataSet();
            foreach (DataRow row in schemaTable.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                if (tableName == "PLC→PC$" || tableName == "PC→PLC$")
                {
                    string sql = String.Format("SELECT * FROM [{0}]", tableName);
                    OleDbDataAdapter oda = new OleDbDataAdapter(sql, con);
                    DataTable table = new DataTable(tableName);
                    oda.Fill(table);
                    ds.Tables.Add(table);
                }
            }
            return ds;
        }
    }
}

