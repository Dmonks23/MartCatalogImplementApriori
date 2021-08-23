using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AprioriAiLite.Views;
using System.Data;
using System.Data.SQLite;
using AprioriAiLite.Models;
using System.Windows.Forms;

namespace AprioriAiLite.Controlerss
{
    partial class ctrl_pDataApriori
    {
        private page_DataApriori _page_DataApriori;
        private ctrl_dbsqlite _ctrl_dbsqlite;
        private DataSet _dataset_ditemsdb;
        private int _jumlahtransaksi, _mode, _jmlkombinasi, _value_min;
        private String _items_perrow, _items_untukkombinasi;
        private String _richtextbox_allhasil;
        private List<getset_itemcountsupportconfidenc> _list_adapter = new List<getset_itemcountsupportconfidenc>();
        /// <summary>
        /// Construktor
        /// </summary>
        public ctrl_pDataApriori(page_DataApriori p)
        {
            this._page_DataApriori = p;
            _ctrl_dbsqlite = new ctrl_dbsqlite();
        }

        /// <summary>
        /// Function biasa
        /// </summary>
       
        //Menmpilkan data list di combo box
        public void _get_coboboxlist()
        {
            SQLiteDataReader dr = _ctrl_dbsqlite.sql_datareader("select * from 'namadata'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    _page_DataApriori.combobox_listdata.Items.Add(dr[0].ToString());
                }
            }
            _ctrl_dbsqlite.sql_disconnect();
        }

        //Menampilkan ke data grid
        public void _get_datagriddata()
        {
            _dataset_ditemsdb = _ctrl_dbsqlite.sql_datasetfill("select id_data,data from data where namadata = '" + _page_DataApriori.combobox_listdata.Text + "'");
            _page_DataApriori.dataGrid_dataitem.ItemsSource = _dataset_ditemsdb.Tables[0].DefaultView;
            _jumlahtransaksi = _dataset_ditemsdb.Tables[0].Rows.Count;
        }

        //Menampilkan item yang di saring ke listview
        public void _get_listviewitem()
        {
            //mendapatkan item satu"
            _mode = 1;
            _getcollect_items();
            String items = "";
            items = _items_perrow.Replace('@', ',');
            items = items.Substring(0, items.Length - 1);
            String[] item = items.Split(',');
            int count = 0;
            double sup = 0;
            for (int i = 0; i < item.Length; i++)
            {
                for (int n = i + 1; n < item.Length; n++)
                {
                    if (item[i] != "")
                    {
                        if (item[i] == item[n])
                        {
                            item[n] = "";
                        }
                    }

                }
                if (item[i] != "")
                {
                    count = _getcount_item(item[i]);
                    sup = count*100/_jumlahtransaksi;
                    _list_adapter.Add(new getset_itemcountsupportconfidenc { item = item[i], count = count, support = sup });
                }
                item[i] = "";
            }
            _page_DataApriori.listView_items.ItemsSource = _list_adapter;
        }

        //Memulai perhitungan
        public void _run_countingApriori()
        {
            //disortir
            _getsortir_items();
            //dikombinasi
            _getkombinasi_item();
            //disortirlagi
        }

        //Menghapus data
        public void _db_deletedata()
        {
            String id_data = "";
            DataRowView drv = _page_DataApriori.dataGrid_dataitem.SelectedItem as DataRowView;
            if(drv != null)
            {
                id_data = drv[0].ToString();   
            }
            DialogResult ds = MessageBox.Show("Ingin Menghapusnya "+ id_data+"?", "Delete data", MessageBoxButtons.OKCancel);
            if(ds == DialogResult.OK)
            {
                try
                {
                    String query = "delete from data where id_data = '" + id_data + "'";
                    _ctrl_dbsqlite.sql_executenonquery(query);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        }
    }
}
