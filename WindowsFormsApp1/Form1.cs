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
        LinkedList<int> id_ler = new LinkedList<int>();// genele []id ler
        List<int> kose = new List<int>();//kaynakça []id leri
        List<string> sekillerTbl1 = new List<string>();
        List<string> sekillerTbl2 = new List<string>();
        List<string> Genl_Sekiller = new List<string>();
        Dictionary<string, int> basliklar = new Dictionary<string, int>();

        string TezYazarı = null;//Tez yazarının adı
        object fileName;
        OpenFileDialog ofd;
        /*
         * KoseliParantez()
         * Kaynakca()
         * Tez metni içerisinde atıf yapılan her kaynağın Kaynaklar bölümünde yer alması zorunlu olduğu gibi
         * Kaynaklar bölümünde bulunan her kaynağa da metin içinde mutlaka değinilmiş (atıf yapılmış) olmalıdır.
         */
        void KoseliParantez()
        {
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                list.AddLast(richTextBox1.Lines[i]);
            }
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string[] spl = richTextBox1.Lines[i].Split('[');

                for (int j = 1; j < spl.Length; j = j + 2)
                {
                    // iid_ler.AddLast(spl[j].Substring(0));
                    //spl[j].Split(']');
                    id_ler.AddLast(int.Parse(spl[j].Split(']')[0]));
                }
            }

            foreach (var item in id_ler)
            {
                MessageBox.Show((item).ToString());
            }
        }
        void Kaynakca()
        {

            int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string kaynakca = richTextBox1.Lines[i].Trim();
                if ("Kaynakça".Equals(kaynakca))
                {
                    baslangic = i;
                }

            }
            for (int i = baslangic; i < richTextBox1.Lines.Length; i++)
            {
                string ekler = richTextBox1.Lines[i].Trim();
                if ("Ekler".Equals(ekler))
                {
                    bitis = i;
                    break;
                }
                else if ("Özgeçmiş".Equals(ekler))
                {
                    bitis = i;
                    break;
                }

            }

            for (int i = baslangic + 1; i < bitis; i++)
            {
                string[] spl = richTextBox1.Lines[i].Split('[');//köseli parantezlerin başlangıcına göre böldük

                for (int j = 1; j < spl.Length; j = j + 2)//sağ tarafa ] ler sol tarafa rakamlar düştüğü için herzamana tekleri almaya çalıştık 
                {
                    kose.Add(int.Parse(spl[j].Split(']')[0]));//köseli parantezin bitişinden böldük ve herzaman elimizdeki ilk elaman sayı oluyor
                }
            }

            foreach (var item in kose)
            {
                MessageBox.Show((item).ToString());

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
            string dokuman = null;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                dokuman = dokuman + richTextBox1.Lines[i] + " ";
            }

            string[] tirnak = dokuman.Split('“').Where(x => x.Contains("”")).Select(x => new string(x.TakeWhile(c => c != '”').ToArray())).ToArray();
            for (int i = 0; i < tirnak.Length; i++)
            {
                MessageBox.Show(tirnak[i]);
            }

            for (int i = 0; i < tirnak.Length; i++)
            {
                string[] spl_adet = tirnak[i].Split(' ');
                if (spl_adet.Length > 50)
                {
                    MessageBox.Show("Kardeş bu alıntı değil resmen copy past olmuş ayıptır güahtır !!!");
                }
                else
                {
                    MessageBox.Show("Alıntı adededi:" + spl_adet.Length);
                }
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
            int baslangic = 0;
            int bitis = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string sekiller = (richTextBox1.Lines[i].Trim()).ToUpper();
                if ("ŞEKİLLER LİSTESİ".Equals(sekiller) || "ŞEKİLLER".Equals(sekiller))
                {
                    baslangic = i;
                }

            }
            for (int i = baslangic; i < richTextBox1.Lines.Length; i++)
            {
                string tablolar = (richTextBox1.Lines[i].Trim()).ToUpper();
                // string ekler= richTextBox1.Lines[i].Trim();
                if ("TABLOLAR LİSTESİ".Equals(tablolar) || "TABLOLAR".Equals(tablolar))
                {
                    bitis = i;
                    break;
                }
                else if ("EKLER LİSTESİ".Equals(tablolar) || "EKLER".Equals(tablolar))
                {
                    bitis = i;
                    break;
                }

            }// burdan konuşucuam caddcvc tama yok 

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

            foreach (string item in sekillerTbl1)
            {
                MessageBox.Show((item).ToString());
            }

            int giris = 0;
            for (int i = bitis; i < richTextBox1.Lines.Length; i++)
            {
                string sekiller = (richTextBox1.Lines[i].Trim()).ToUpper();
                sekiller = sekiller.Trim(' ', '1', '.');

                if ("1.GİRİŞ".Equals(sekiller) || "GİRİŞ".Equals(sekiller))
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

                if ("ÖNERİLER".Equals(sekiller))
                {
                    oneriler = i;
                    kontrol_onerilenler = true;
                    break;
                }
            }
            if (!kontrol_onerilenler)
            {
                MessageBox.Show("Kardeş bu tez eksik içinde Öneriler yok.\n sen git tamamla gel");
            }

            int[] deger_kontrol = new int[sekillerTbl1.Count];//sekillerTbl1 nin değerlerinin varlık durumuna göre her bulduğunda bulunana değerin bulunduğu indisi deger_kontrol arry indeki indisini bir attırır
            for (int i = giris; i < oneriler; i++)
            {
                string sekiller = (richTextBox1.Lines[i]);
                int deger = 0;

                for (int j = 0; j < sekillerTbl1.Count; j++)
                {
                    deger = sekiller.IndexOf(sekillerTbl1[j]);
                    if (deger >= 0)
                    {
                        deger_kontrol[j] += 1;
                    }
                }
            }

            for (int i = 0; i < deger_kontrol.Length; i++)
            {
                if (deger_kontrol[i] != 0)
                {
                    MessageBox.Show("Bu var" + sekillerTbl1[i]);
                }
                else
                {
                    MessageBox.Show("Bu yok" + sekillerTbl1[i]);
                }
            }
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
        void IcindekilerBaslikKontroluDevamıBtn3() {
            object readOnly = false;
            object visible = true;
            object save = false;
            object fileName = @"C:\Users\mertb\OneDrive\Masaüstü\progdiller-22.12.2020\dosya2.docx";
            object newTemplate = false;
            object docType = 0;
            object missing = Type.Missing;

            application = new Microsoft.Office.Interop.Word.Application() { Visible = false };
            document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing);
            document.ActiveWindow.Selection.WholeStory();
            document.ActiveWindow.Selection.Copy();
            IDataObject dataObject = Clipboard.GetDataObject();

            bool durum = true;
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
                            MessageBox.Show("Doğru olanlar\n" + item.Key + " : " + item.Value);
                        }
                        else
                        {
                            MessageBox.Show("Kardeş içindekilerde belirtiğin '" + item.Key + "' başlığın\n" + item.Value + " numaralı sayfada bulunmamaktadır!!!");
                            durum = false;
                            break;

                        }
                    }
                }
                if (!durum)
                {
                    MessageBox.Show("Şimdi git düzelt gel!");
                    break;
                }
            }
        }

        /*
         *önsöz bölümünün ilk paragrafında tez konusunun önemi, zorlukları, 
         *sınırları ve isteklendirme (motivasyon) faktörleri hakkında bilgi verilmelidir. 
         *Önsöz bölümünün ilk paragrafında paragrafında, tez çalışmalarına protokol numaralı 
         *proje ile maddi destek sağlayan ve/veya yazılı olur müsaadesi veren kurum/kuruluşlara 
         *ilgili yazı ve protokol numaraları belirtilerek teşekkür edilmemelidir.
         */
        void OnsozTesekkurKontrol() {
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
                MessageBox.Show("Önsözün ilk paragrafın da teşekkür ifadesi kullanılmaz!");
            }
        }

        /**
         * Tez Onay Kontrolü
         */
        void TezOnayKontrolu()
        {
            //WordAc();
            Microsoft.Office.Interop.Word.Paragraphs DocPar = document.Paragraphs;

            long parCount = DocPar.Count;

            int i = 0;

            int baslangic = 0;
            while (i < parCount)
            {
                i++;
                if (DocPar[i].Range.Text == "TEZ ONAYI\r")
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
                if (DocPar[i].Range.Text == "BEYAN\r")
                {
                    bitis = i;
                    break;
                }
            }
            while (baslangic < bitis)
            {
                string tezonay = DocPar[baslangic].Range.Text.ToLower();
                int onaylarım = tezonay.IndexOf("oybirliği");
                if (onaylarım > 0)
                {
                    MessageBox.Show("Test");
                    break;
                }
                baslangic++;

            }
            //document.Close();
            //application.Quit();
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
            while (bitis >= baslangic)
            {
                string tarihKontrol = DocPar[bitis].Range.Text.ToLower();
                int tarih = tarihKontrol.IndexOf("20");
                if (tarih > 0)
                {
                    if (DocPar[bitis - 1].Range.Text.ToLower() != "\r")
                    {
                        string yazar = DocPar[bitis - 1].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 1].Range.Text.ToLower());                         
                            break;
                        }
                        else {
                            MessageBox.Show("Yazar adı belirtilmemiştir");
                            break;
                        }
                    }
                    else if (DocPar[bitis - 2].Range.Text.ToLower() != "\r") {
                        string yazar = DocPar[bitis - 2].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 2].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir");
                            break;
                        }
                    }
                    else {

                        string yazar = DocPar[bitis - 3].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 3].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir");
                            break;
                        }
                    }
                }
                bitis--;
            }

        }
        
        /*
         * Beyan kısmında yazar adı ve tarih varmı      
         */
        void BeyanTarihveAdKontrolu(){
             YazarAdi();
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
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 1].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir!");
                            break;
                        }
                    }
                    else if (DocPar[bitis - 2].Range.Text.ToLower() != "\r")
                    {
                        string yazar = DocPar[bitis - 2].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 2].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir!");
                            break;
                        }
                    }
                    else
                    {

                        string yazar = DocPar[bitis - 3].Range.Text;
                        if (yazar == TezYazarı)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 3].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir!");
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
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 1].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir!");
                            break;
                        }
                    }
                    else if (DocPar[bitis - 2].Range.Text.ToLower() != "\r")
                    {
                        tarih = DocPar[bitis - 2].Range.Text.IndexOf("20");

                        if (tarih > 0)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 2].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir!");
                            break;
                        }
                    }
                    else
                    {
                        tarih = DocPar[bitis - 3].Range.Text.IndexOf("20");

                        if (tarih > 0)
                        {
                            MessageBox.Show(DocPar[bitis].Range.Text.ToLower());
                            MessageBox.Show(DocPar[bitis - 3].Range.Text.ToLower());
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı belirtilmemiştir!");
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
            WordAc();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("WINWORD");
            foreach (Process p in ps)
                  p.Kill();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("WINWORD");
            foreach (Process p in ps)
                p.Kill();
        }

        private void KaynakcaKontrol_Click(object sender, EventArgs e)
        {

        }

        private void BaslikSayfaNumaralari_Click(object sender, EventArgs e)
        {

        }
    } 
}