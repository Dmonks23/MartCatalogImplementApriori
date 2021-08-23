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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AprioriAiLite.Controlerss;

namespace AprioriAiLite.Views
{
    /// <summary>
    /// Interaction logic for page_DataApriori.xaml
    /// </summary>
    public partial class page_DataApriori : Page
    {
        private ctrl_pDataApriori _ctrl_pDataApriori;
        public page_DataApriori()
        {
            InitializeComponent();
            _ctrl_pDataApriori = new ctrl_pDataApriori(this);
            onEvent();
        }

        private void onEvent()
        {
            this.Loaded += _ctrl_pDataApriori.Evonload_pdataapriori;
            button_choose.Click += _ctrl_pDataApriori.Eonclick_butonchoose;
            buton_getitem.Click += _ctrl_pDataApriori.Eonclick_buttongetitem;
            button_process.Click += _ctrl_pDataApriori.Eonclick_buttonprocess;
            btnpds_delete.Click += _ctrl_pDataApriori.Eonclick_buttondeletedata;
            btnpds_add.Click += _ctrl_pDataApriori.Eonclick_buttonAdddata;
            btnpds_refresh.Click += _ctrl_pDataApriori.Eonclick_butonrefresh;
        }
    }
}
