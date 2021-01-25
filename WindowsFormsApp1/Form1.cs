using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wordDoc=Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Collections;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        LinkedList<string> list = new LinkedList<string>();// satır satır tüm word verileri
        List<int> id_ler = new List<int>();// genele []id ler
        List<int> kose = new List<int>();//kaynakça []id leri
        List<string> sekillerTbl1 = new List<string>();
        List<string> sekillerTbl2 = new List<string>();
        List<string> Genl_Sekiller = new List<string>();
        Dictionary<string, int> basliklar = new Dictionary<string, int>();
        List<string> tabloLst = new List<string>();//İçindekiler tablo kontrol
        List<string> Genl_Tablolar = new List<string>();//Giriş'ten sonra metnin içinde tablo isminin kontrolü
        Sonuc sonuc = new Sonuc();
        string TezYazarı = null;//Tez yazarının adı
        object fileName;
        OpenFileDialog ofd;

        /*
         *TabloKontrol()
         *
         *Tez metni içerisinde verilen tablolar listesinde yer alan tabloların bulundukları sayfa numaraları ilgili tabloların kullanıldığı
         *sayfalar tutarlı olmak zorundadır.
         **/
        void TabloKontrol()
        {
            //Sonuc sonuc =new Sonuc();

            int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string tablolar = (richTextBox1.Lines[i].Trim()).ToUpper();
                if ("TABLOLAR LİSTESİ".Equals(tablolar) || "TABLO".Equals(tablolar) || "\tTABLOLAR LİSTESİ".Equals(tablolar) || "\nTABLOLAR LİSTESİ".Equals(tablolar) || "\rTABLOLAR LİSTESİ".Equals(tablolar))
                {
                    baslangic = i;
                }

            }
            for (int i = baslangic; i < richTextBox1.Lines.Length; i++)
            {
                string tablolar = (richTextBox1.Lines[i].Trim()).ToUpper();
                // string ekler= richTextBox1.Lines[i].Trim();
                if ("EKLER LİSTESİ".Equals(tablolar) || "EKLER".Equals(tablolar) || "\tEKLER LİSTESİ".Equals(tablolar) || "\tEKLER".Equals(tablolar))
                {
                    bitis = i;
                    break;
                }
                else if ("SİMGELER VE KISALTMALAR".Equals(tablolar) || "SİMGELER".Equals(tablolar) || "\tSİMGELER VE KISALTMALAR".Equals(tablolar) || "\tSİMGELER".Equals(tablolar))
                {
                    bitis = i;
                    break;
                }

            }

            tabloLst.Clear();

            for (int i = baslangic + 1; i < bitis; i++)
            {
                //sekillerTbl1 = richTextBox1.Lines[i].Split('Ş').Where(x => x.Contains(".")).Select(x => new string(x.TakeWhile(c => c != '.').ToArray())).ToArray();
                if (richTextBox1.Lines[i].IndexOf('T') == 0)
                {
                    int ilknokta = richTextBox1.Lines[i].IndexOf('.');//satırda geçen ilk noktanın indisini alır.
                    int ikincinokta = richTextBox1.Lines[i].IndexOf('.', ilknokta + 1);//satırın ilk noktadan sonraki boşluğunun tespitini yapar
                    tabloLst.Add(richTextBox1.Lines[i].Substring(0, ikincinokta + 1));//satırın ilk karakteriyle geçen ilk noktadan sonraki boşluğun arasını alır.
                }
            }



            int giris = 0;
            for (int i = bitis; i < richTextBox1.Lines.Length; i++)
            {
                string tablolar = (richTextBox1.Lines[i].Trim()).ToUpper();
                tablolar = tablolar.Trim(' ', '1', '.');

                if ("\tGİRİŞ".Equals(tablolar) || "1.\tGİRİŞ".Equals(tablolar) || "1. GİRİŞ".Equals(tablolar) || "GİRİŞ".Equals(tablolar))
                {
                    giris = i;
                    break;
                }
            }
            int oneriler = 0;
            bool kontrol_onerilenler = false;
            for (int i = giris; i < richTextBox1.Lines.Length; i++)
            {
                string tablolar = (richTextBox1.Lines[i].Trim()).ToUpper();

                if ("ÖNERİLER".Equals(tablolar) || "\tÖNERİLER".Equals(tablolar) || "1.\tÖNERİLER".Equals(tablolar))
                {
                    oneriler = i;
                    kontrol_onerilenler = true;
                    break;
                }
            }
            if (!kontrol_onerilenler)
            {
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Tez içerisinde Öneriler kısmı eksiktir!");
                sonuc.listBox2.Items.Add("Tez içerisinde Öneriler kısmı eksiktir!");
            }

            int[] deger_kontrol = new int[tabloLst.Count];//sekillerTbl1 nin değerlerinin varlık durumuna göre her bulduğunda bulunana değerin bulunduğu indisi deger_kontrol arry indeki indisini bir attırır
            for (int i = giris; i < oneriler; i++)
            {
                string tablolar = (richTextBox1.Lines[i]);
                int deger = 0;

                for (int j = 0; j < tabloLst.Count; j++)
                {
                    deger = tablolar.IndexOf(tabloLst[j]);
                    if (deger >= 0)
                    {
                        deger_kontrol[j] += 1;
                    }
                }
            }
            sonuc.listBox1.Items.Clear();
            sonuc.listBox1.Items.Add("Tablo Kontrolleri");
            sonuc.listBox2.Items.Add("Tablo Kontrolleri");
            bool kontrol = false;//eğer hiç sorun yoksa ihitmalinin tespit etmek için kullanılacak, listbox a sorun yok yazdıra bilmek için 
            for (int i = 0; i < deger_kontrol.Length; i++)
            {
                if (deger_kontrol[i] != 0)
                {
                    //MessageBox.Show("Bu var" + tabloLst[i]);
                }
                else
                {
                    kontrol = true;//olumsuz durum tespiti
                    string tabloNotFound = tabloLst[i] + " no'lu tablo, ana metin içerisinde bulunamadı!";
                    //MessageBox.Show("Bu yok" + tabloLst[i]);
                    sonuc.listBox1.Items.Add(tabloNotFound);
                    sonuc.listBox2.Items.Add(tabloNotFound);
                }
            }
            if (kontrol==false)
            {
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Tablolar Listesinde herhangi bir sıkıntı bulunmamaktadır!!");
            }
            sonuc.listBox1.Items.Add(" ");
            sonuc.listBox1.Items.Add(" ");
            sonuc.listBox2.Items.Add(" ");
            sonuc.listBox2.Items.Add(" ");
        }

        /*
         * KoseliParantez()
         * Kaynakca()
         * Tez metni içerisinde atıf yapılan her kaynağın Kaynaklar bölümünde yer alması zorunlu olduğu gibi
         * Kaynaklar bölümünde bulunan her kaynağa da metin içinde mutlaka değinilmiş (atıf yapılmış) olmalıdır.
         */
        void KoseliParantez()
        {
            //int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string kaynakca = richTextBox1.Lines[i].Trim().ToLower();
                if ("kaynakça".Equals(kaynakca) || "kaynaklar".Equals(kaynakca) || "\tkaynakça".Equals(kaynakca) || "\tkaynaklar".Equals(kaynakca))
                {
                    bitis = i;
                }

            }
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                list.AddLast(richTextBox1.Lines[i]);
            }
            for (int i = 0; i < bitis; i++)//bitise kadar gitmesinin sebebi kaynakların içine tekrardan kontrol etmesin diye
            {
                string dokuman = richTextBox1.Lines[i];
                string[] tirnak = dokuman.Split('[').Where(x => x.Contains("]")).Select(x => new string(x.TakeWhile(c => c != ']').ToArray())).ToArray();
                for (int j = 0; j < tirnak.Length; j++)
                {
                    int kontrol_blok= tirnak[j].IndexOf("-");
                    int kontrol = tirnak[j].IndexOf(",");
                    if (kontrol>=0)
                    {
                        if (kontrol_blok >= 0)
                        {
                            string[] spl1 = tirnak[j].Split('-');
                            if (spl1[0].Length < spl1[1].Length)
                            {
                                id_ler.Add(int.Parse(spl1[0].Trim()));
                                string[] spl2 = spl1[1].Split(',');
                                for (int k = 0; k < spl2.Length; k++)
                                {
                                    string sayi = spl2[k].Trim();
                                    id_ler.Add(int.Parse(sayi));
                                }
                                //id_ler.Add(int.Parse(spl2[0].Trim()));
                                //id_ler.Add(int.Parse(spl2[1].Trim()));
                            }
                            else {

                                id_ler.Add(int.Parse(spl1[1].Trim()));
                                string[] spl2 = spl1[0].Split(',');
                                for (int k = 0; k < spl2.Length; k++)
                                {
                                    string sayi = spl2[k].Trim();
                                    id_ler.Add(int.Parse(sayi));
                                }
                            }
                           
                        }
                        else {
                            string[] spl = tirnak[j].Split(',');
                            for (int k = 0; k < spl.Length; k++)
                            {
                                string sayi = spl[k].Trim();
                                id_ler.Add(int.Parse(sayi));
                                //MessageBox.Show(sayi);
                            }
                        }
                        
                    }
                    else if (kontrol_blok >= 0)
                    {
                        string[] spl = tirnak[j].Split('-');
                        for (int k = 0; k < spl.Length; k++)
                        {
                            string sayi = spl[k].Trim();
                            id_ler.Add(int.Parse(sayi));
                            //MessageBox.Show(sayi);
                        }
                    }
                    else {
                        // MessageBox.Show(tirnak[j]);
                        try
                        {
<<<<<<< HEAD
                           
                          id_ler.Add(int.Parse(tirnak[j]));

=======
                            if (tirnak[j] != null || tirnak[j] != "")
                            {
                                id_ler.Add(int.Parse(tirnak[j]));

                            }
>>>>>>> 2921aa48963feecaac9b3b69afd8c17df27269b6
                        }
                        catch (Exception)
                        {

                           
                        }
                        
                    }
                }


               
            }

           
        }
        void Kaynakca()
        {

            int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string kaynakca = richTextBox1.Lines[i].Trim().ToLower();
                if ("kaynakça".Equals(kaynakca) || "kaynaklar".Equals(kaynakca))
                {
                    baslangic = i;
                }

            }
            for (int i = baslangic; i < richTextBox1.Lines.Length; i++)
            {
                string ekler = richTextBox1.Lines[i].Trim().ToLower();
                if ("ekler".Equals(ekler))
                {
                    bitis = i;
                    break;
                }
                else if ("özgeçmiş".Equals(ekler))
                {
                    bitis = i;
                    break;
                }

            }
            


            for (int i = baslangic; i < bitis; i++)//bitise kadar gitmesinin sebebi kaynakların içine tekrardan kontrol etmesin diye
            {
                string dokuman = richTextBox1.Lines[i];
                string[] tirnak = dokuman.Split('[').Where(x => x.Contains("]")).Select(x => new string(x.TakeWhile(c => c != ']').ToArray())).ToArray();
                for (int j = 0; j < tirnak.Length; j++)
                {

                    int kontrol = tirnak[j].IndexOf(",");
                    if (kontrol >= 0)
                    {
                        string[] spl = tirnak[j].Split(',');
                        for (int k = 0; k < spl.Length; k++)
                        {
                            string sayi = spl[k].Trim();
                            kose.Add(int.Parse(sayi));
                            //MessageBox.Show(sayi);
                        }
                    }
                    else
                    {
                        // MessageBox.Show(tirnak[j]);
                        kose.Add(int.Parse(tirnak[j]));
                    }
                }

            }
         
        }

        /*
         *TirnakKontrol()
         *Tez metninde bir başka kaynaktan alınmış bir paragraf (cümle) anlam ve yazım bakımından değiştirilmeden aynen aktarılmak isteniyorsa, 
         *bu alıntının tamamı tırnak işareti “....” içinde yazılır. 
         *Tez içerisinde alıntıların kelime adedi tezin özgünlüğü açısından önem taşımaktadır, 
         *bu sebeple çift tırmak içerisinde kullanılan kelime adedi elliden fazla olmamalıdır.
         */
        void TirnakKontrol()
        {
            //Sonuc sonuc = new Sonuc();

            string dokuman = null;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                dokuman = dokuman + richTextBox1.Lines[i] + " ";
            }

            string[] tirnak = dokuman.Split('“').Where(x => x.Contains("”")).Select(x => new string(x.TakeWhile(c => c != '”').ToArray())).ToArray();

            for (int i = 0; i < tirnak.Length; i++)
            {
                string[] spl_adet = tirnak[i].Split(' ');
                if (spl_adet.Length > 50)
                {
                    sonuc.listBox1.Items.Clear();
                    sonuc.listBox1.Items.Add("Alıntı sınırı aşılmıştır!");
                    sonuc.listBox1.Items.Add("Alıntı adedi:" + spl_adet.Length);
                    sonuc.listBox2.Items.Add("\nAlıntı sınırı aşılmıştır!");
                    sonuc.listBox2.Items.Add("Alıntı adedi:" + spl_adet.Length+"\n");

                    //MessageBox.Show("Kardeş bu alıntı değil resmen copy past olmuş ayıptır günahtır !!!");
                }
<<<<<<< HEAD
                //else
                //{
                //    // MessageBox.Show("Alıntı adededi:" + spl_adet.Length);
                //   // sonuc.listBox1.Items.Clear();
                //   // sonuc.listBox1.Items.Add("Alıntı sınırı aşılmamıştır!");
                //}
=======
                else
                {
                    // MessageBox.Show("Alıntı adededi:" + spl_adet.Length);
                    sonuc.listBox1.Items.Clear();
                    sonuc.listBox1.Items.Add("Alıntı sınırı aşılmamıştır!");
                }
>>>>>>> 2921aa48963feecaac9b3b69afd8c17df27269b6
            }
        }

        /*
         *SekilKontrol()
         *GenelSekilKontrolu()
         *Tez metni içerisinde verilen şekiller listesinde yer alan şekillerin bulundukları sayfa numaraları ilgili şekillerin kullanıldığı
         *sayfalar tutarlı olmak zorundadır.
         **/
        void SekilKontrol()
        {
           // Sonuc sonuc = new Sonuc();

            int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string sekiller = (richTextBox1.Lines[i].Trim()).ToUpper();
                if ("ŞEKİLLER LİSTESİ".Equals(sekiller) || "ŞEKİLLER".Equals(sekiller) || "\tŞEKİLLER LİSTESİ".Equals(sekiller) || "\tŞEKİLLER".Equals(sekiller))
                {
                    baslangic = i;
                }

            }
            for (int i = baslangic; i < richTextBox1.Lines.Length; i++)
            {
                string tablolar = (richTextBox1.Lines[i].Trim()).ToUpper();
                // string ekler= richTextBox1.Lines[i].Trim();
                if ("TABLOLAR LİSTESİ".Equals(tablolar) || "TABLOLAR".Equals(tablolar) || "\tTABLOLAR LİSTESİ".Equals(tablolar) || "\tTABLOLAR".Equals(tablolar))
                {
                    bitis = i;
                    break;
                }
                else if ("EKLER LİSTESİ".Equals(tablolar) || "EKLER".Equals(tablolar) || "\tEKLER LİSTESİ".Equals(tablolar) || "\tEKLER".Equals(tablolar))
                {
                    bitis = i;
                    break;
                }

            }

            sekillerTbl1.Clear();

            for (int i = baslangic + 1; i < bitis; i++)
            {
                //sekillerTbl1 = richTextBox1.Lines[i].Split('Ş').Where(x => x.Contains(".")).Select(x => new string(x.TakeWhile(c => c != '.').ToArray())).ToArray();
                if (richTextBox1.Lines[i].IndexOf('Ş') == 0)
                {
                    int ilknokta = richTextBox1.Lines[i].IndexOf('.');//satırda geçen ilk noktanın indisini alır.
                    int ikincinokta = richTextBox1.Lines[i].IndexOf('.', ilknokta + 1);//satırın ilk noktadan sonraki boşluğunun tespitini yapar
                    sekillerTbl1.Add(richTextBox1.Lines[i].Substring(0, ikincinokta + 1));//satırın ilk karakteriyle geçen ilk noktadan sonraki boşluğun arasını alır.
                }
            }



            int giris = 0;
            for (int i = bitis; i < richTextBox1.Lines.Length; i++)
            {
                string sekiller = (richTextBox1.Lines[i].Trim()).ToUpper();
                sekiller = sekiller.Trim(' ', '1', '.');

                if ("\tGİRİŞ".Equals(sekiller) || "1.\tGİRİŞ".Equals(sekiller) || "1. GİRİŞ".Equals(sekiller) || "GİRİŞ".Equals(sekiller))
                {
                    giris = i;
                    break;
                }
            }
            int oneriler = 0;
            bool kontrol_onerilenler = false;
            for (int i = giris; i < richTextBox1.Lines.Length; i++)
            {
                string sekiller = (richTextBox1.Lines[i].Trim()).ToUpper();

                if ("ÖNERİLER".Equals(sekiller) || "\tÖNERİLER".Equals(sekiller) || "1.\tÖNERİLER".Equals(sekiller))
                {
                    oneriler = i;
                    kontrol_onerilenler = true;
                    break;
                }
            }
            if (!kontrol_onerilenler)
            {
                // MessageBox.Show("Kardeş bu tez eksik içinde Öneriler yok.\n sen git tamamla gel");
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Tez içerisinde Öneriler kısmı eksiktir!");
                sonuc.listBox2.Items.Add("Tez içerisinde Öneriler kısmı eksiktir!");
            }

            int[] deger_kontrol = new int[sekillerTbl1.Count];//sekillerTbl1 nin değerlerinin varlık durumuna göre her bulduğunda bulunana değerin bulunduğu indisi deger_kontrol arry indeki indisini bir attırır
            for (int i = giris; i < oneriler; i++)
            {
                string tablolar = (richTextBox1.Lines[i]);
                int deger = 0;

                for (int j = 0; j < sekillerTbl1.Count; j++)
                {
                    deger = tablolar.IndexOf(sekillerTbl1[j]);
                    if (deger >= 0)
                    {
                        deger_kontrol[j] += 1;
                    }
                }
            }

            sonuc.listBox1.Items.Clear();
            sonuc.listBox1.Items.Add("Şekil Kontrolleri");
            sonuc.listBox2.Items.Add("Şekil Kontrolleri");
            bool kontrol = false;//eğer hiç sorun yoksa ihitmalinin tespit etmek için kullanılacak, listbox a sorun yok yazdıra bilmek için 
            for (int i = 0; i < deger_kontrol.Length; i++)
            {
                if (deger_kontrol[i] != 0)
                {
                    //MessageBox.Show("Bu var" + tabloLst[i]);
                }
                else
                {
                    kontrol = true;//sorun var
                    string sekilNotFound = sekillerTbl1[i] + " no'lu şekil, ana metin içerisinde bulunamadı!";
                    //MessageBox.Show("Bu yok" + tabloLst[i]);
                    sonuc.listBox1.Items.Add(sekilNotFound);
                    sonuc.listBox2.Items.Add(sekilNotFound);

                }
            }
            if (kontrol==false)
            {
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Şekiller Listesinde herhangi bir sıkıntı bulunmamaktadır!!");
            }
            sonuc.listBox1.Items.Add(" ");
            sonuc.listBox1.Items.Add(" ");
            sonuc.listBox2.Items.Add(" ");
            sonuc.listBox2.Items.Add(" ");
        }
        void GenelSekilKontrolu() {
            int baslangic = 0;

            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string kaynakca = richTextBox1.Lines[i].Trim().ToUpper();
                if ("1. GİRİŞ".Equals(kaynakca))
                {
                    baslangic = i;
                }

            }
            int ilk_nokta = 0;
            int ikinci_nokta = 0;
            for (int i = baslangic; i < richTextBox1.Lines.Length; i++)
            {
                string sekiller = (richTextBox1.Lines[i]);

                ilk_nokta = sekiller.IndexOf("Şekil ");
                if (ilk_nokta >= 0)
                {
                    string parca = sekiller.Substring(ilk_nokta + 7);
                    if ('.' == parca[0])
                    {
                        ikinci_nokta = sekiller.IndexOf('.', ilk_nokta + 8);
                        if (ikinci_nokta - (ilk_nokta + 8) > 4)
                        {
                            ikinci_nokta = sekiller.IndexOf('’', ilk_nokta + 8);
                            if (ikinci_nokta - (ilk_nokta + 8) > 4)
                            {
                                ikinci_nokta = sekiller.IndexOf(' ', ilk_nokta + 8);
                            }
                        }
                    }
                }
                if (ilk_nokta >= 0 && ikinci_nokta >= 0)
                {
                    if (sekiller[ilk_nokta + 7] == '.' || sekiller[ilk_nokta + 8] == '.' || sekiller[ilk_nokta + 9] == '.')
                    {
                        string ssss = "Şekil ";
                        ssss = ssss + sekiller.Substring(ilk_nokta + 6, 1 + ikinci_nokta - (ilk_nokta + 6));
                        if (ssss[ssss.Length - 1] == '.')
                        {
                            int d = ssss.IndexOf(")");
                            if (d >= 0)
                            {
                                ssss = ssss.Remove(d, 1);
                            }
                            // MessageBox.Show(ssss);
                            Genl_Sekiller.Add(ssss);

                        }
                        if ((ssss[ssss.Length - 1] == '’') || (ssss[ssss.Length - 1] == ' '))
                        {
                            ssss = ssss.Remove(ssss.Length - 1);
                            ssss = ssss.Insert(ssss.Length, ".");
                            int d = ssss.IndexOf(")");
                            if (d >= 0)
                            {
                                ssss = ssss.Remove(d, 1);
                            }
                            //MessageBox.Show(ssss);
                            Genl_Sekiller.Add(ssss);


                        }
                    }
                }
            }

            foreach (var item in Genl_Sekiller)
            {
                MessageBox.Show(item);

            }
        }

        /**
         * IcindekilerBaslikKontrolu()
         * IcindekilerBaslikKontroluDevamıBtn3()
         * içindekiler kısmında verilen başlıkların sayfa numaraları ile 
         * tez içerisindeki başlıkların geçtiği sayfa numaraları tutarlı olmak zorundadır.
         */
        void IcindekilerBaslikKontrolu() {
            basliklar.Clear();
            #region
            //    int kelime = 0;

            //    //for (int i = 0; i < richTextBox1.Lines.Length; i++)
            //    {
            //        bool kontrol = false;
            //        string spl = richTextBox1.Lines[i];
            //        kelime = kelime + spl.Length;
            //        int index = 0;
            //        for (richTextBox1.SelectionStart = kelime; richTextBox1.SelectionStart < richTextBox1.Text.Length - 1; richTextBox1.SelectionStart++)
            //        {
            //            char bosluk = richTextBox1.Text[richTextBox1.SelectionStart];

            //            var s = richTextBox1.SelectionFont.Style;
            //            if (index <= spl.Length)
            //            {
            //                if ((s & FontStyle.Bold) != 0 || bosluk==' ')
            //                {

            //                    //MessageBox.Show(s.ToString());
            //                    kontrol = true;
            //                }
            //                else
            //                {
            //                    kontrol = false;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                break;
            //            }
            //            index++;
            //        }
            //        if (kontrol == true)
            //        {
            //            MessageBox.Show(richTextBox1.Lines[i]);
            //        }
            //    }
            #endregion
            bool durum = false;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string ddd = richTextBox1.Lines[i];
                if (ddd.Length > 5)
                {
                    if (ddd[1] == '.')
                    {
                        int index = ddd.IndexOf(' ');
                        if (index > 1)
                        {
                            for (int j = index; j < ddd.Length; j++)
                            {
                                if (ddd[j] == '.' || ddd[j] == ',' || ddd[j] == '!' || ddd[j] == '?')
                                {
                                    durum = false;
                                    break;
                                }
                                else
                                {
                                    durum = true;
                                }
                            }
                            if (durum)
                            {
                                //  MessageBox.Show(ddd);
                            }
                        }
                    }
                }
            }
            int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                if (richTextBox1.Lines[i].Trim().ToUpper() == "İÇİNDEKİLER")
                {
                    baslangic = i;
                }
                else if (richTextBox1.Lines[i].Trim().ToUpper() == "ÖZET")
                {
                    bitis = i;
                }
            }

            for (int i = baslangic; i < bitis; i++)
            {
                string baslik = richTextBox1.Lines[i];
                if (baslik.Length > 5)
                {
                    if (baslik[1] == '.')
                    {
                        int index = baslik.IndexOf(' ');
                        if (index > 1)
                        {
                            for (int j = index; j < baslik.Length; j++)
                            {
                                if (baslik[j] == '.' || baslik[j] == ',' || baslik[j] == '!' || baslik[j] == '?')
                                {
                                    durum = false;
                                    break;
                                }
                                else
                                {
                                    durum = true;
                                }
                            }
                            if (durum)
                            {
                                #region
                                //  MessageBox.Show(ddd);
                                //int bosluk = baslik.IndexOf(' ');
                                //int nokta = baslik.IndexOf('.', bosluk + 1);
                                //MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(nokta+1));

                                //if (baslik[baslik.Length - 1] != '.' && baslik[baslik.Length - 2] != '.')
                                //{
                                //    MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length - 2)+"üçüncü hane:"+baslik[baslik.Length-3]);
                                //}
                                //else if (baslik[baslik.Length - 1] != '.')
                                //{
                                //    MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length - 1));
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length));
                                //}



                                //try
                                //{
                                //    int ucuncu_basamak = baslik[baslik.Length - 3];
                                //    MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length - 3));

                                //}
                                //catch (Exception)
                                //{
                                //    if (baslik[baslik.Length - 1] != '.' && baslik[baslik.Length - 2] != '.')
                                //    {
                                //        MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length - 2));
                                //    }
                                //    else if (baslik[baslik.Length - 1] != '.')
                                //    {
                                //        MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length - 1));
                                //    }
                                //    else
                                //    {
                                //        MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length));
                                //    }
                                //}

                                //MessageBox.Show("Başlık:" + baslik + "\nSayfa Numarası:" + baslik.Substring(baslik.Length - 3) + "\nuzunluğu:" + baslik.Substring(baslik.Length - 3).Length);
                                //for (int j = 1; j < 9; j++)
                                //{

                                //    int [] nummber = [1, 2, 3, 4, 5, 6, 7, 8, 9];
                                //    nummber.;
                                //    if (baslik.Substring(baslik.Length - 3)[0] == j)
                                //    {
                                //       MessageBox.Show(baslik.Substring(baslik.Length - 3));

                                //    }//912

                                //    else if (kontrol_basamak)
                                //    {

                                //    }
                                //}
                                //bütün emeklerin zayi olduğu an, alttaki 4 satırlık method bulundu
                                #endregion                             
                                var result_v = Regex.Match(baslik, @"\d+$").Value;
                                int result = int.Parse(result_v);
                                int son = baslik.IndexOf(result_v);
                                int nokta = baslik.LastIndexOf('.');
                                if (nokta > 0)
                                {
                                    baslik = baslik.Substring(nokta + 1, son - nokta - 1);
                                    basliklar.Add(baslik.Trim(), result);
                                }
                            }
                        }
                    }
                }
            }
        }
        void IcindekilerBaslikKontroluDevamıBtn3()
        {
          //  Sonuc sonuc = new Sonuc();

            //bool durum = true;
            sonuc.listBox1.Items.Clear();
            sonuc.listBox1.Items.Add("İçindekiler başlık sayfa numarası kontrolü ");
            sonuc.listBox2.Items.Add("İçindekiler başlık sayfa numarası kontrolü ");
            foreach (Microsoft.Office.Interop.Word.Paragraph c in document.Paragraphs)
            {
                foreach (var item in basliklar)
                {
                    if ((c.Range.Text.Trim()) == (item.Key).Trim())
                    {
                        int page = c.Range.Information[Microsoft.Office.Interop.Word.WdInformation.wdActiveEndAdjustedPageNumber];
                        //  MessageBox.Show(c.Range.Text + "is on page " + page.ToString());                      
                        if (item.Value == page)
                        {
                            // MessageBox.Show("Doğru olanlar\n" + item.Key + " : " + item.Value);
                        }
                        else
                        {
                            sonuc.listBox1.Items.Add("'" + item.Key + "' başlığın\n" + item.Value + ". sayfada bulunmamaktadır!");
                            sonuc.listBox2.Items.Add("'" + item.Key + "' başlığın\n" + item.Value + ". sayfada bulunmamaktadır!");
                            // durum = false;
                            break;
                        }
                    }

                }
                // if (!durum)
                // {
                //    sonuc.listBox1.Items.Add("Lütfen   ");
                //    break;
                // }
            }

            sonuc.listBox2.Items.Add("");
            sonuc.listBox2.Items.Add("");

        }

        /*
         *önsöz bölümünün ilk paragrafında tez konusunun önemi, zorlukları, 
         *sınırları ve isteklendirme (motivasyon) faktörleri hakkında bilgi verilmelidir. 
         *Önsöz bölümünün ilk paragrafında paragrafında, tez çalışmalarına protokol numaralı 
         *proje ile maddi destek sağlayan ve/veya yazılı olur müsaadesi veren kurum/kuruluşlara 
         *ilgili yazı ve protokol numaraları belirtilerek teşekkür edilmemelidir.
         */
        void OnsozTesekkurKontrol() {
            //Sonuc sonuc = new Sonuc();

            Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;

            // Count number of paragraphs in the file

            long parCount = DocPar.Count;

            // Step through the paragraphs
            int i = 0;
            bool kontrol = false;
            int paragrafDeger = 0;
            while (i < parCount)
            {
                i++;
                string a = DocPar[i].Range.Text;
                if (kontrol)
                {
                    if (a.Length > 10)
                    {
                        paragrafDeger = i;
                        break;
                    }
                }

                if (a == "ÖNSÖZ\r")
                {
                    kontrol = true;
                }
            }
            string b = DocPar[paragrafDeger].Range.Text.Trim().ToLower();

            int deger = b.IndexOf("teşekkür");
            if (deger >= 0)
            {
                //MessageBox.Show("Önsözün ilk paragrafın da teşekkür ifadesi kullanılmaz!");
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Önsözün ilk paragrafın da teşekkür ifadesi kullanılmaz!");
                sonuc.listBox2.Items.Add("\nÖnsözün ilk paragrafın da teşekkür ifadesi kullanılmaz!\n");
            }
            else {
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Herhangi bir sorun bulunamadı");
                sonuc.listBox1.Items.Add("Önsözün ilk paragrafın da teşekkür ifadesi kullanılmamıştır!");
            }
        }

        /**
         * Tez Onay Kontrolü
         */
        void TezOnayKontrolu()
        {
         //   Sonuc sonuc = new Sonuc();

            //WordAc();
            Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;

            long parCount = DocPar.Count;

            int i = 0;

            int baslangic = 0;
            while (i < parCount)
            {
                i++;
                string tezonay = DocPar[i].Range.Text.Trim();
                if (tezonay== "TEZ ONAYI\r" || tezonay == "TEZ ONAYI")
                {
                    baslangic = i;
                    break;
                }
                
            }
            int bitis = 0;
            i = 0;
            while (i < parCount)
            {
                i++;
                 string beyan = DocPar[i].Range.Text.Trim();
                if (beyan== "BEYAN\r" || beyan == "BEYAN")
                {
                    bitis = i;
                    break;
                }
               
            }
            bool onayKontrol = false;
            while (baslangic < bitis)
            {
                baslangic++;
               
                int onaylarım = DocPar[baslangic].Range.Text.ToLower().IndexOf("oybirliği");
                if (onaylarım > 0)
                {

                    sonuc.listBox1.Items.Clear();
                    sonuc.listBox1.Items.Add("Tez Onayı Alınmıştır!");
                    sonuc.listBox2.Items.Add("Tez Onayı");
                    sonuc.listBox2.Items.Add("Tez Onayı Alınmıştır!\n");

                    onayKontrol = true;
                    break;
                }             
            }
            if (onayKontrol==false)
            {
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Tez Onayı Alınmamıştır!");
                sonuc.listBox2.Items.Add("Tez Onayı");
                sonuc.listBox2.Items.Add("Tez Onayı Alınmamıştır!\n");


            }

        }

        /*
         * Önsöz kısmında yazar adı ve tarih varmı
         
         */
        void OnsozTarihveAdKontrolu() {
            

            Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;
            long parCount = DocPar.Count;

            int i = 0;

            int baslangic = 0;
            while (i < parCount)
            {
                i++;
                if (DocPar[i].Range.Text == "ÖNSÖZ\r")
                {
                    baslangic = i;
                    break;
                }
            }
            int bitis = 0;
            i = 0;
            while (i < parCount)
            {
                i++;
                if (DocPar[i].Range.Text == "İÇİNDEKİLER\r")
                {
                    bitis = i;
                    break;
                }
            }
            bool tarih_kontrol = false;
            while (bitis >= baslangic)
            {
                string tarihKontrol = DocPar[bitis].Range.Text.ToLower();
                int tarih = tarihKontrol.IndexOf("20");
                if (tarih > 0)
                {
                    tarih_kontrol = true;
                    if (DocPar[bitis - 1].Range.Text.ToLower() != "\r")
                    {
                        string yazar = DocPar[bitis - 1].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            // MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            //  MessageBox.Show(DocPar[bitis - 1].Range.Text.ToLower());
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmimiştir!");
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\n\nÖnsöz Kontrolü\nYazar adı belirtilmemiştir!\n\n");
                            break;
                        }
                    }
                    else if (DocPar[bitis - 2].Range.Text.ToLower() != "\r")
                    {
                        string yazar = DocPar[bitis - 2].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            //MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            // MessageBox.Show(DocPar[bitis - 2].Range.Text.ToLower());
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmimiştir!");
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\n\nÖnsöz Kontrolü\nYazar adı belirtilmemiştir!\n\n");

                            break;
                        }
                    }
                    else
                    {

                        string yazar = DocPar[bitis - 3].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            // MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            //MessageBox.Show(DocPar[bitis - 3].Range.Text.ToLower());
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmimiştir!");
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\n\nÖnsöz Kontrolü\nYazar adı belirtilmemiştir!\n\n");
                            break;
                        }
                    }
                }
               
                bitis--;
            }
            if (tarih_kontrol == false)
            {
                sonuc.listBox1.Items.Clear();
                sonuc.listBox1.Items.Add("Önsöz Kontrolü\nTarih belirtilmemiştir!\n");
                sonuc.listBox2.Items.Add("\n\nÖnsöz Kontrolü\nTarih belirtilmemiştir!\n\n");

            }
            else {
                sonuc.listBox1.Items.Clear();
<<<<<<< HEAD
                sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmiştir!");
=======
                sonuc.listBox1.Items.Add("Önsöz Kontrolü\nYazar adı belirtilmimiştir!");
>>>>>>> 2921aa48963feecaac9b3b69afd8c17df27269b6
            }

        }
        
        /*
         * Beyan kısmında yazar adı ve tarih varmı      
         */
        void BeyanTarihveAdKontrolu(){
          //  Sonuc sonuc = new Sonuc();
             Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;
             long parCount = DocPar.Count;
             int i = 0;
             int baslangic = 0;
                while (i<parCount)
                {
                    i++;
                    if (DocPar[i].Range.Text == "BEYAN\r")
                    {
                        baslangic = i;
                        break;
                    }
                }
                int bitis = 0;
                i = 0;
                while (i < parCount)
                {
                  i++;
                  if (DocPar[i].Range.Text == "ÖNSÖZ\r")
                  {
                    bitis = i;
                    break;
                  }
                }

            while (bitis >= baslangic)
            {
                string tarihKontrol = DocPar[bitis].Range.Text;
                int tarih = tarihKontrol.IndexOf("20");
                int YazarAd = tarihKontrol.IndexOf(TezYazarı);
                if (tarih >= 0)// önce ad sonra tarih yazılmışsa buraya gir
                {
                    if (DocPar[bitis - 1].Range.Text.ToLower() != "\r")
                    {
                        string yazar = DocPar[bitis - 1].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add(DocPar[bitis].Range.Text.ToLower() + " " + DocPar[bitis - 1].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add("Yazar adı belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\nBeyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox2.Items.Add("Yazar adı belirtilmemiştir!\n");
                            break;
                        }
                    }
                    else if (DocPar[bitis - 2].Range.Text.ToLower() != "\r")
                    {
                        string yazar = DocPar[bitis - 2].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add(DocPar[bitis].Range.Text.ToLower()+" "+ DocPar[bitis - 2].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add("Yazar adı belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\nBeyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox2.Items.Add("Yazar adı belirtilmemiştir!\n");
                            break;
                        }
                    }
                    else
                    {
                        string yazar = DocPar[bitis - 3].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add(DocPar[bitis].Range.Text.ToLower() + " " + DocPar[bitis - 3].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add("Yazar adı belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\nBeyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox2.Items.Add("Yazar adı belirtilmemiştir!\n");
                            break;
                        }
                    }
                }

                else if (YazarAd >= 0)// önce tarih sonra ad yazılmışsa buraya gir
                {
                    if (DocPar[bitis - 1].Range.Text.ToLower() != "\r")
                    {
                        tarih = DocPar[bitis - 1].Range.Text.IndexOf("20");
                        if (tarih > 0)
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add(DocPar[bitis].Range.Text.ToLower() + " " + DocPar[bitis - 1].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add("Tarih belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\nBeyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox2.Items.Add("Tarih belirtilmemiştir!\n");
                            break;
                        }
                    }
                    else if (DocPar[bitis - 2].Range.Text.ToLower() != "\r")
                    {
                        tarih = DocPar[bitis - 2].Range.Text.IndexOf("20");

                        if (tarih > 0)
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add(DocPar[bitis].Range.Text.ToLower() + " " + DocPar[bitis - 2].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add("Tarih belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\nBeyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox2.Items.Add("Tarih belirtilmemiştir!\n");
                            break;
                        }
                    }
                    else
                    {
                        tarih = DocPar[bitis - 3].Range.Text.IndexOf("20");

                        if (tarih > 0)
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add(DocPar[bitis].Range.Text.ToLower() + " " + DocPar[bitis - 3].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            sonuc.listBox1.Items.Clear();
                            sonuc.listBox1.Items.Add("Beyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox1.Items.Add("Tarih belirtilmemiştir!");
                            sonuc.listBox2.Items.Add("\nBeyan tarih ve yazar adı kontrolü ");
                            sonuc.listBox2.Items.Add("Tarih belirtilmemiştir!\n");
                            sonuc.listBox2.Items.Add("");

                            break;
                        }
                    }
                }
                bitis--;
            }
         }

        void YazarAdi() {
           
            Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;
            long parCount = DocPar.Count;
            int i = 0;
            int baslangic = 0;
            while (i < parCount)
            {
                i++;
                if (DocPar[i].Range.Text == "Tez Yazarı\r")
                {
                    baslangic = i;
                    break;
                }
            }
            TezYazarı = DocPar[i + 1].Range.Text;
        }

        public void WordAc()
        {
            using (ofd = new OpenFileDialog() { ValidateNames = true, Multiselect = false, Filter = "Word 97-2003|*.doc|Word Document|*.docx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    object readOnly = false;
                    object visible = true;
                    object save = false;
                    fileName = ofd.FileName;
                    object newTemplate = false;
                    object docType = 0;
                    object missing = Type.Missing;
                    // Microsoft.Office.Interop.Word._Document document;
                    application = new Microsoft.Office.Interop.Word.Application() { Visible = false }; document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing); document.ActiveWindow.Selection.WholeStory();
                    document.ActiveWindow.Selection.Copy();
                    IDataObject dataObject = Clipboard.GetDataObject();
                    richTextBox1.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();

                    //application.Quit(ref missing, ref missing, ref missing);

                    Microsoft.Office.Interop.Word.Range rng = document.Content;
                    Microsoft.Office.Interop.Word.Find find = rng.Find;
                     //Microsoft.Office.Interop.Word.Selection Selection;
                    // Microsoft.Office.Interop.Word.WdLanguageID lid;
                   //document.Close();
                  //application.Quit(ref missing, ref missing, ref missing);
                }
            }
        }
      
       public void WordAc2()
        {
            object readOnly = false;
            object visible = true;
            object save = false;
            fileName = ofd.FileName;
            object newTemplate = false;
            object docType = 0;
            object missing = Type.Missing;


            // Microsoft.Office.Interop.Word.Range rng = document.Content;
            // Microsoft.Office.Interop.Word.Find find = rng.Find;
          //  Microsoft.Office.Interop.Word.Selection Selection;
          //  Microsoft.Office.Interop.Word.WdLanguageID lid;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            //toolTip2.Active = true;
            groupBox1.Enabled = false;
        }
       
        Microsoft.Office.Interop.Word._Document document;
        Microsoft.Office.Interop.Word._Application application;

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { ValidateNames = true, Multiselect = false, Filter = "Word 97-2003|*.doc|Word Document|*.docx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    object readOnly = false;
                    object visible = true;
                    object save = false;
                    object fileName = ofd.FileName;
                    object newTemplate = false;
                    object docType = 0;
                    object missing = Type.Missing;
                    //Microsoft.Office.Interop.Word._Document document;
                   // Microsoft.Office.Interop.Word._Application application = new Microsoft.Office.Interop.Word.Application() { Visible = false };
                    application = new Microsoft.Office.Interop.Word.Application() { Visible = false };
                    document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing);
                    document.ActiveWindow.Selection.WholeStory();
                    document.ActiveWindow.Selection.Copy();
                    IDataObject dataObject = Clipboard.GetDataObject();
                    richTextBox1.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
                    application.Quit(ref missing, ref missing, ref missing);
                }
            }

        }       
        private void button2_Click(object sender, EventArgs e)
        {
                          
        }
        private void button3_Click(object sender, EventArgs e)
        {
            WordAc();
            Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;

            long parCount = DocPar.Count;

            int i = 0;

            int baslangic = 0;
            while (i < parCount)
            {
                i++;
                if (DocPar[i].Range.Text == "Tez Yazarı\r")
                {
                    baslangic = i;
                    break;
                }
            }
            MessageBox.Show(DocPar[i+1].Range.Text);
            document.Close();
            application.Quit();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("WINWORD");
            foreach (Process p in ps)
                p.Kill();

            timer1.Enabled = true;
            WordAc();
            groupBox1.Enabled = true;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("WINWORD");
            foreach (Process p in ps)
                  p.Kill();
        }


        private void AlintiKontrol_Click(object sender, EventArgs e)
        {
            //Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            TirnakKontrol();
            sonuc.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 1;
            progressBar1.Step = 1;
            timer1.Interval = 100;

            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Enabled = false;
                return;
            }

            progressBar1.Value += 1;
        }

        private void SekillerListesi_Click(object sender, EventArgs e)
        {
            Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            timer1.Enabled = true;
            SekilKontrol();
            sonuc.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void BaslikSayfaNumaralari_Click(object sender, EventArgs e)
        {
            //Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            IcindekilerBaslikKontrolu();
            IcindekilerBaslikKontroluDevamıBtn3();
            sonuc.Show();
        }

        private void KaynakcaKontrol_Click(object sender, EventArgs e)
        {
           // Sonuc sonuc =new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            KoseliParantez();
            Kaynakca();
            sonuc.listBox1.Items.Clear();
            sonuc.listBox2.Items.Add("");
<<<<<<< HEAD
            sonuc.listBox2.Items.Add("KAYNAKÇA KONTROL SONUÇLARI");
=======
>>>>>>> 2921aa48963feecaac9b3b69afd8c17df27269b6
            //Metin içerisinde olan atıflar kaynakçada mevcut mu kontrolü.
            bool Genelvarlık = false;
            for (int i = 0; i < id_ler.Count; i++)
            {
                //  KoseliParantez düzeltildi ve şimdi kaynakçda düzgünse karşılaştırma yapılacak sadece sonra bitiyor
                for (int j = 0; j < kose.Count; j++)
                {
                    if (id_ler[i] == kose[j])
                    {
                        Genelvarlık = true;
                        break;
                    }
                }
                if (Genelvarlık == false)
                {
                    sonuc.listBox2.Items.Add(id_ler[i] + "no lu atıf yapılan kaynak kaynakçada belirtilmemiştir!");
                    sonuc.listBox1.Items.Add(id_ler[i] + "no lu atıf yapılan kaynak kaynakçada belirtilmemiştir!");
                }//Metin içerisinde olan atıflar kaynakçada bulunmuyor mu? Kontrolü
                else
                {
                  //  sonuc.listBox2.Items.Add(id_ler[i] + "no lu atıf yapılan kaynak kaynakçada belirtilmiştir!");
                  //  sonuc.listBox1.Items.Add(id_ler[i] + "no lu atıf yapılan kaynak kaynakçada belirtilmiştir!");
                    Genelvarlık = false; 
                }//Metin içerisinde olan atıflar kaynakçada bulunuyor mu? Kontrolü
            }

            sonuc.listBox2.Items.Add("");
            //Kaynakçada bulunan her kaynak numarasına metin içerisinde atıf yapılmış mı kontrolü.
            bool kaynakcaVarlik = false;
            for (int i = 0; i < kose.Count; i++)
            {
                //  KoseliParantez düzeltildi ve şimdi kaynakçda düzgünse karşılaştırma yapılacak sadece sonra bitiyor
                for (int j = 0; j < id_ler.Count; j++)
                {
                    if (id_ler[j] == kose[i])
                    {
                        kaynakcaVarlik = true;
                        break;
                    }
                }
                if (kaynakcaVarlik == false)
                {
                    sonuc.listBox2.Items.Add(kose[i] + "'no lu kaynakça maddesine metin içerisinde atıf yapılmamıştır!");
                    sonuc.listBox1.Items.Add(kose[i] + "'no lu kaynakça maddesine metin içerisinde atıf yapılmamıştır!");
                }//Metin içerisinde olan atıflar kaynakçada bulunmuyor mu? Kontrolü
                else
                {
                   // sonuc.listBox2.Items.Add(kose[i] + "'no lu kaynakça maddesine metin içerisinde atıf yapılmıştır!");
                    //sonuc.listBox1.Items.Add(kose[i] + "'no lu kaynakça maddesine metin içerisinde atıf yapılmıştır!");
                    kaynakcaVarlik = false;
                }//Metin içerisinde olan atıflar kaynakçada bulunuyor mu? Kontrolü
            }
            sonuc.listBox2.Items.Add("");
            sonuc.Show();
        }

        private void sonuclar_Click(object sender, EventArgs e)
        {
         //   Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = false;
            sonuc.listBox2.Visible = true;
            sonuc.Show();

        }

        private void TezOnay_Click(object sender, EventArgs e)
        {
            //Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            TezOnayKontrolu();
            sonuc.Show();
        }

        private void TablolarListesi_Click(object sender, EventArgs e)
        {
          //  Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            TabloKontrol();
            sonuc.Show();
        }

        private void OnsozTesekkur_Click(object sender, EventArgs e)
        {
       // Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            OnsozTesekkurKontrol();
            sonuc.Show();
        }

        private void OnsozTarihAd_Click(object sender, EventArgs e)
        { 
        //   Sonuc sonuc =new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            YazarAdi();
            OnsozTarihveAdKontrolu();
            sonuc.Show();
        }

        private void BeyanTarihAd_Click(object sender, EventArgs e)
        {
       //     Sonuc sonuc = new Sonuc();

            sonuc.listBox1.Visible = true;
            sonuc.listBox2.Visible = false;
            YazarAdi();
            BeyanTarihveAdKontrolu();       
            sonuc.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
            //toolTip1.SetToolTip(this.groupBox1, "Lütfen önce bir tez dosyası seçip yükleyiniz!");

        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            //if (groupBox1.Enabled == false)
            //{
            //    toolTip2.Active = true;
            //}
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        void progres_bar()
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 1000;
            progressBar1.Maximum = 50000;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (progressBar1.Value != 100)
            {
                progressBar1.Value++;
            }
            else
            {
                timer1.Stop();
            }
        }
    }
    } 
