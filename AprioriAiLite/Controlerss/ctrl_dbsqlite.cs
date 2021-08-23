using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace AprioriAiLite.Controlerss
{
    class ctrl_dbsqlite
    {
        private String sqlitecons = "Data Source=aprioridb.db;Version=3";
        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private DataSet ds;
        private SQLiteDataAdapter da;

        private void sql_intro()
        {
            con = new SQLiteConnection(sqlitecons);
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            con.Open();
        }

        public void sql_executenonquery(String query)
        {
            //insert,update,delete
            sql_intro();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            sql_disconnect();
        }

        public SQLiteDataReader sql_datareader(String query)
        {
            //baca file
            sql_intro();
            cmd.CommandText = query;
            SQLiteDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public DataSet sql_datasetfill(String query)
        {
            //read table 
            sql_intro();
            cmd.CommandText = query;
            ds = new DataSet();
            da = new SQLiteDataAdapter(query, con);
            da.Fill(ds);
            sql_disconnect();
            return ds;
        }

        public object sql_executescalar(String query)
        {
            //mengeluarkan satuan
            sql_intro();
            cmd.CommandText = query;
            object result = cmd.ExecuteScalar();
            sql_disconnect();
            return result;
        }

        public void sql_disconnect()
        {
            //disconek
            con.Close();
        }
    }
}
