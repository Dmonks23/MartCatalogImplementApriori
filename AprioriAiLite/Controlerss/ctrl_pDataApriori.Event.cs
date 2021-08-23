using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AprioriAiLite.Views;

namespace AprioriAiLite.Controlerss
{
    partial class ctrl_pDataApriori
    {
        public void Evonload_pdataapriori(object sender, EventArgs e)
        {
            _page_DataApriori.button_process.IsEnabled = false;
            _page_DataApriori.buton_getitem.IsEnabled = false;
            _get_coboboxlist();
        }

        public void Eonclick_butonchoose(object sender, EventArgs e)
        {
            _richtextbox_allhasil = "";
            _list_adapter.Clear();
            _items_untukkombinasi = "";
            _items_perrow = "";
            _page_DataApriori.dataGrid_dataitem.ItemsSource = null;
            _page_DataApriori.listView_items.ItemsSource = null;
            _page_DataApriori.richTextBox_allresult.Document.Blocks.Clear();
            _page_DataApriori.buton_getitem.IsEnabled = true;
            if(_page_DataApriori.combobox_listdata.Text != "")
            {
                _get_datagriddata();
            }
            else
            {
                MessageBox.Show("Pilih data dahulu");
            }


        }

        public void Eonclick_buttongetitem(object sender, EventArgs e)
        {
            _page_DataApriori.button_process.IsEnabled = true;
            _list_adapter.Clear();
            _get_listviewitem();
        }

        public void Eonclick_buttonprocess(object sender, EventArgs e)
        {
            if (_page_DataApriori.r_mincounting.IsChecked == false || _page_DataApriori.r_minsupport.IsChecked == false)
            {
                if(_page_DataApriori.textbox_kombinasi.Text == "" || _page_DataApriori.textbox_valuemin.Text == "")
                {
                    MessageBox.Show("Tolong Isi Value Kombinasi dan Value Min");
                }
                else
                {
                    try
                    {
                        if (_page_DataApriori.r_mincounting.IsChecked == true)
                        {
                            _mode = 1;
                        }
                        else if (_page_DataApriori.r_minsupport.IsChecked == true)
                        {
                            _mode = 2;
                        }
                        _jmlkombinasi = Convert.ToInt32(_page_DataApriori.textbox_kombinasi.Text);
                        _value_min = Convert.ToInt32(_page_DataApriori.textbox_valuemin.Text);
                        _run_countingApriori();
                        _page_DataApriori.richTextBox_allresult.AppendText(_richtextbox_allhasil);
                        _page_DataApriori.button_process.IsEnabled = false;

                   }
                    catch (Exception ex)
                   {
                       MessageBox.Show("Perhitungan Error " + ex.Message);
                   }
                    
                }
            }
            else
            {
                MessageBox.Show("Pilih Salah Satu RadioButton");
            }
            
        }

        public void Eonclick_buttondeletedata(object sender, EventArgs e)
        {
            _db_deletedata();
            _get_datagriddata();
        }

        public void Eonclick_buttonAdddata(object sender, EventArgs e)
        {
            if (_page_DataApriori.combobox_listdata.SelectedIndex != -1)
            {
                Win_adddata winadd = new Win_adddata(_page_DataApriori.combobox_listdata.Text, null);
                winadd.Show();
                winadd.Topmost = true;
            }
            else
            {
                MessageBox.Show("Pilih data dahulu");
            }
            
        }

        public void Eonclick_butonrefresh(object sneder,EventArgs e)
        {
            if(_page_DataApriori.combobox_listdata.SelectedIndex != -1)
            {
                _get_datagriddata();
            }
        }
    }
}
