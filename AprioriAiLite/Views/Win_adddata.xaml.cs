using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AprioriAiLite.Controlerss;

namespace AprioriAiLite.Views
{
    /// <summary>
    /// Interaction logic for Win_adddata.xaml
    /// </summary>
    public partial class Win_adddata : Window
    {
        private ctrl_winAdddata winadd;
        public Win_adddata(String namadata, page_DataApriori page)
        {
            InitializeComponent();
            textBox_namadata.Text = namadata;
            winadd = new ctrl_winAdddata(page, this);
            inisialize();

        }


        public void inisialize()
        {
            button_simpan.Click += winadd.Eonclick_Tambahdata;
        }
    }
}
