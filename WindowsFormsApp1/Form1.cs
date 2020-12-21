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

        List<string> Genl_Sekiller= new List<string>();


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
        #region
        private void Form1_Load(object sender, EventArgs e)
        {
            //wordDoc.Application oWord;
            //wordDoc.Document oDoc;
            //oWord = new wordDoc.Application();
            //oWord.Visible = true;
            //oDoc = oWord.Documents.Add();
            //Clipboard.SetText(richTextBox1.Text);
            //oDoc.ActiveWindow.Selection.Paste();
            //string fileName = @"deneme.docx";
            //oWord.Application.ActiveDocument.SaveAs2(fileName);
            //oDoc.Close();


            //richTextBox1.LoadFile(@"deneme.docx", RichTextBoxStreamType.PlainText);

            //Microsoft.Office.Interop.Word.Application WordObj;
            //WordObj = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            //for (int i = 0; i < WordObj.Windows.Count; i++)
            //{
            //    object idx = i + 1;
            //    Window WinObj = WordObj.Windows.get_Item(ref idx);
            //    doc_list.Add(WinObj.Document.FullName);
            //}        
        }

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
                    Microsoft.Office.Interop.Word._Document document;
                    Microsoft.Office.Interop.Word._Application application = new Microsoft.Office.Interop.Word.Application() { Visible = false }; document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing); document.ActiveWindow.Selection.WholeStory();
                    document.ActiveWindow.Selection.Copy();
                    IDataObject dataObject = Clipboard.GetDataObject();
                    richTextBox1.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();

                    application.Quit(ref missing, ref missing, ref missing);
                }
            }

        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
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
                            if (ikinci_nokta - (ilk_nokta + 8) >4)
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
    }

}