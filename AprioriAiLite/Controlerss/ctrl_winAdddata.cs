using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AprioriAiLite.Views;
using System.Windows.Forms;

namespace AprioriAiLite.Controlerss
{
    class ctrl_winAdddata
    {
        private page_DataApriori _pagedataapriori;
        private Win_adddata _winadddata;
        private ctrl_dbsqlite _dbsqlite;
        public ctrl_winAdddata(page_DataApriori page, Win_adddata win)
        {
            this._pagedataapriori = page;
            this._winadddata = win;
            _dbsqlite = new ctrl_dbsqlite();
        }

        public void Eonclick_Tambahdata(object sender, EventArgs ex)
        {
            String query = "insert into data (namadata,data) values('"+_winadddata.textBox_namadata.Text+"','"+_winadddata.textbox_items.Text+"')";
            if (_winadddata.textbox_items.Text != "")
            {
                //try
                //{
                    _dbsqlite.sql_executenonquery(query);
                    MessageBox.Show("Data ditambahkan Silahkan Refresh DataGrid");
                /*} catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }*/
            }
            else
            {
                MessageBox.Show("Data item diisi dulu");
            }
        }
    }
}
