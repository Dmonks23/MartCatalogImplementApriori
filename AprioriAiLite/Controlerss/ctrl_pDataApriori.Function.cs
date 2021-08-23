using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AprioriAiLite.Models;

namespace AprioriAiLite.Controlerss
{
    partial class ctrl_pDataApriori
    {
        //mendapatkan jml item per baris
        public int _getcountperrow_items(String pattern)
        {
            String[] rowset = _items_perrow.Split('@');
            int jml = 0;
            for (int i = 0; i < rowset.Length; i++)
            {
                if (Regex.Matches(rowset[i], pattern).Count >= _jmlkombinasi)
                {
                    jml++;
                }
            }
            return jml;
        }

        //untuk menyaring per baris
        public void _getcollect_items()
        {
            _items_perrow = "";
            int i = 0;
            int n = _jumlahtransaksi;
            switch(_mode)
            {
                case 1:
                    //dari dataset per item
                    while(n-- > 0)
                    {
                        _items_perrow += _dataset_ditemsdb.Tables[0].Rows[i++]["Data"].ToString() + "@";
                    }
                    _items_perrow = _items_perrow.Replace(" ", String.Empty);
                    //_items_perrow = _items_perrow.Substring(0, _items_perrow.Length - 1);
                    break;
                case 2:

                    break;
            }
        }

        //function get count regex
        public int _getcount_item(String pattern)
        {
            return Regex.Matches(_items_perrow, pattern).Count;
        }

        //untuk sortir berdasarkan cont atau minsuppor
        public void _getsortir_items()
        {
            string jmode = "";
            if (_mode == 1)
                jmode = "mincount";
            else
                jmode = "minsupport";
            _richtextbox_allhasil += "Sortir berdasarkan " + jmode + " dengan minValue : " + _value_min+"\n";
            _richtextbox_allhasil += "\tCount\t\tSupport\t\tItemset\n";
            foreach(getset_itemcountsupportconfidenc gti in _list_adapter)
            {
                switch(_mode)
                {
                    case 1:
                        if (gti.count >= _value_min)
                        {
                            _richtextbox_allhasil += "\t"+gti.count+"\t\t"+gti.support+"\t\t"+gti.item+"\n";
                            _items_untukkombinasi += gti.item + ",";
                        }
                        break;
                    case 2:
                        if (gti.support >= _value_min)
                        {
                            _richtextbox_allhasil += "\t"+gti.count + "\t\t" + gti.support + "\t\t" + gti.item+"\n";
                            _items_untukkombinasi += gti.item + ",";
                        }
                        break;
                }
            }
            //_items_untukkombinasi = _items_untukkombinasi.Substring(0, _items_untukkombinasi.Length - 1);
        }

        //untuk mendapatkan kombinasi item
        public void _getkombinasi_item()
        {
            _list_adapter.Clear();
            int count = 0, sup=0, confidenc=0,sxc =0;

            String[] item = _items_untukkombinasi.Split(',');
            _richtextbox_allhasil += "Kombinasi berdasarkan "+ _jmlkombinasi+ " item \n";
            _richtextbox_allhasil += "\tCount\t\tSupport(%)\t\tConfidence(%)\t\tSup*Conf(%)\t\tItemset\n";
            switch (_jmlkombinasi)
            {
                case 2:
                    for (int i = 0; i < item.Length; i++)
                    {
                        for (int n = i + 1; n < item.Length; n++)
                        {
                            if (item[i] != "" && item[n] != "")
                            {
                                count = _getcountperrow_items(item[i] + "|" + item[n]);
                                sup = count * 100 / _jumlahtransaksi;
                                confidenc = count * 100 / _getcount_item(item[i]);
                                sxc = sup * confidenc / 100;
                                _list_adapter.Add(new getset_itemcountsupportconfidenc { count = count, support = sup, confidents = confidenc, item = item[i] + "," + item[n], supxconf = sxc });
                                _richtextbox_allhasil += "\t" + count + "\t\t\t" + sup + "\t\t\t" + confidenc + "\t\t\t"+sxc+"\t\t"+ item[i] + "," + item[n] + "\n";
                            }

                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < item.Length; i++)
                    {
                        for (int n = i + 1; n < item.Length; n++)
                        {
                            for(int o =n+1; o<item.Length; o++)
                            {
                                if (item[i] != "" && item[n] != "" && item[o] != "")
                                {
                                    count = _getcountperrow_items(item[i] + "|" + item[n]+ "|" + item[o]);
                                    sup = count * 100 / _jumlahtransaksi;
                                    confidenc = count * 100 / _getcount_item(item[i]);
                                    sxc = sup * confidenc / 100;
                                    _list_adapter.Add(new getset_itemcountsupportconfidenc { count = count, support = sup, confidents = confidenc, item = item[i] + "," + item[n] + "," + item[o], supxconf = sxc });
                                    _richtextbox_allhasil += "\t" + count + "\t\t\t" + sup + "\t\t\t" + confidenc + "\t\t\t" + sxc + "\t\t" + item[i] + "," + item[n] + "," + item[o] + "\n";
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < item.Length; i++)
                    {
                        for (int n = i + 1; n < item.Length; n++)
                        {
                            for (int o = n + 1; o < item.Length; o++)
                            {
                                for(int p = o+1; p < item.Length; p++)
                                {
                                    if (item[i] != "" && item[n] != "" && item[o] != "" && item[p] != "")
                                    {
                                        count = _getcountperrow_items(item[i] + "|" + item[n] + "|" + item[o] + "|" + item[p]);
                                        sup = count * 100 / _jumlahtransaksi;
                                        confidenc = count * 100 / _getcount_item(item[i]);
                                        sxc = sup * confidenc / 100;
                                        _list_adapter.Add(new getset_itemcountsupportconfidenc { count = count, support = sup, confidents = confidenc, item = item[i] + "," + item[n] + "," + item[o] + "," + item[p], supxconf = sxc });
                                        _richtextbox_allhasil += "\t" + count + "\t\t\t" + sup + "\t\t\t" + confidenc + "\t\t\t" + sxc + "\t\t" + item[i] + "," + item[n] + "," + item[o]+ "," + item[p] + "\n";
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    
                    break;
            }
        }
    }
}
