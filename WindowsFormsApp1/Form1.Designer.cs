namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.KaynakcaKontrol = new System.Windows.Forms.Button();
            this.AlintiKontrol = new System.Windows.Forms.Button();
            this.SekillerListesi = new System.Windows.Forms.Button();
            this.TablolarListesi = new System.Windows.Forms.Button();
            this.BaslikSayfaNumaralari = new System.Windows.Forms.Button();
            this.OnsozTesekkur = new System.Windows.Forms.Button();
            this.BeyanTarihAd = new System.Windows.Forms.Button();
            this.OnsozTarihAd = new System.Windows.Forms.Button();
            this.TezOnay = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sonuclar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(457, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(617, 643);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Lime;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button4.Location = new System.Drawing.Point(35, 37);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 52);
            this.button4.TabIndex = 4;
            this.button4.Text = "Tez Aç";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Red;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button5.Location = new System.Drawing.Point(142, 37);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(101, 52);
            this.button5.TabIndex = 5;
            this.button5.Text = "Tez Kapat";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // KaynakcaKontrol
            // 
            this.KaynakcaKontrol.BackColor = System.Drawing.Color.White;
            this.KaynakcaKontrol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.KaynakcaKontrol.ForeColor = System.Drawing.Color.Black;
            this.KaynakcaKontrol.Location = new System.Drawing.Point(26, 103);
            this.KaynakcaKontrol.Name = "KaynakcaKontrol";
            this.KaynakcaKontrol.Size = new System.Drawing.Size(101, 52);
            this.KaynakcaKontrol.TabIndex = 6;
            this.KaynakcaKontrol.Text = "Kaynakça";
            this.toolTip1.SetToolTip(this.KaynakcaKontrol, "Kaynakça standartları kontrolü");
            this.KaynakcaKontrol.UseVisualStyleBackColor = false;
            this.KaynakcaKontrol.Click += new System.EventHandler(this.KaynakcaKontrol_Click);
            // 
            // AlintiKontrol
            // 
            this.AlintiKontrol.BackColor = System.Drawing.Color.White;
            this.AlintiKontrol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.AlintiKontrol.ForeColor = System.Drawing.Color.Black;
            this.AlintiKontrol.Location = new System.Drawing.Point(26, 32);
            this.AlintiKontrol.Name = "AlintiKontrol";
            this.AlintiKontrol.Size = new System.Drawing.Size(101, 52);
            this.AlintiKontrol.TabIndex = 7;
            this.AlintiKontrol.Text = "Alıntı";
            this.toolTip1.SetToolTip(this.AlintiKontrol, "Alıntı (\"...\") kelime adedi kontrolü");
            this.AlintiKontrol.UseVisualStyleBackColor = false;
            this.AlintiKontrol.Click += new System.EventHandler(this.AlintiKontrol_Click);
            // 
            // SekillerListesi
            // 
            this.SekillerListesi.BackColor = System.Drawing.Color.White;
            this.SekillerListesi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.SekillerListesi.ForeColor = System.Drawing.Color.Black;
            this.SekillerListesi.Location = new System.Drawing.Point(143, 32);
            this.SekillerListesi.Name = "SekillerListesi";
            this.SekillerListesi.Size = new System.Drawing.Size(101, 52);
            this.SekillerListesi.TabIndex = 8;
            this.SekillerListesi.Text = "Şekiller Listesi";
            this.toolTip1.SetToolTip(this.SekillerListesi, "Şekiller listesi metin \r\niçerisi uyumluluk kontrolü");
            this.SekillerListesi.UseVisualStyleBackColor = false;
            this.SekillerListesi.Click += new System.EventHandler(this.SekillerListesi_Click);
            // 
            // TablolarListesi
            // 
            this.TablolarListesi.BackColor = System.Drawing.Color.White;
            this.TablolarListesi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TablolarListesi.ForeColor = System.Drawing.Color.Black;
            this.TablolarListesi.Location = new System.Drawing.Point(143, 103);
            this.TablolarListesi.Name = "TablolarListesi";
            this.TablolarListesi.Size = new System.Drawing.Size(101, 52);
            this.TablolarListesi.TabIndex = 9;
            this.TablolarListesi.Text = "Tablolar Listesi";
            this.toolTip1.SetToolTip(this.TablolarListesi, " Tablolar listesi metin \r\niçerisi uyumluluk kontrolü");
            this.TablolarListesi.UseVisualStyleBackColor = false;
            this.TablolarListesi.Click += new System.EventHandler(this.TablolarListesi_Click);
            // 
            // BaslikSayfaNumaralari
            // 
            this.BaslikSayfaNumaralari.BackColor = System.Drawing.Color.White;
            this.BaslikSayfaNumaralari.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BaslikSayfaNumaralari.ForeColor = System.Drawing.Color.Black;
            this.BaslikSayfaNumaralari.Location = new System.Drawing.Point(262, 32);
            this.BaslikSayfaNumaralari.Name = "BaslikSayfaNumaralari";
            this.BaslikSayfaNumaralari.Size = new System.Drawing.Size(101, 52);
            this.BaslikSayfaNumaralari.TabIndex = 10;
            this.BaslikSayfaNumaralari.Text = "Sayfa Numaraları";
            this.toolTip1.SetToolTip(this.BaslikSayfaNumaralari, "İçindekiler başlık \r\nsayfa numarası kontrolü");
            this.BaslikSayfaNumaralari.UseVisualStyleBackColor = false;
            this.BaslikSayfaNumaralari.Click += new System.EventHandler(this.BaslikSayfaNumaralari_Click);
            // 
            // OnsozTesekkur
            // 
            this.OnsozTesekkur.BackColor = System.Drawing.Color.White;
            this.OnsozTesekkur.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.OnsozTesekkur.ForeColor = System.Drawing.Color.Black;
            this.OnsozTesekkur.Location = new System.Drawing.Point(26, 173);
            this.OnsozTesekkur.Name = "OnsozTesekkur";
            this.OnsozTesekkur.Size = new System.Drawing.Size(101, 52);
            this.OnsozTesekkur.TabIndex = 12;
            this.OnsozTesekkur.Text = "Önsöz Teşekkür";
            this.toolTip1.SetToolTip(this.OnsozTesekkur, "Önsöz ilk paragraf \r\nteşekkür ibaresi kontrolü");
            this.OnsozTesekkur.UseVisualStyleBackColor = false;
            this.OnsozTesekkur.Click += new System.EventHandler(this.OnsozTesekkur_Click);
            // 
            // BeyanTarihAd
            // 
            this.BeyanTarihAd.BackColor = System.Drawing.Color.White;
            this.BeyanTarihAd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BeyanTarihAd.ForeColor = System.Drawing.Color.Black;
            this.BeyanTarihAd.Location = new System.Drawing.Point(262, 173);
            this.BeyanTarihAd.Name = "BeyanTarihAd";
            this.BeyanTarihAd.Size = new System.Drawing.Size(101, 52);
            this.BeyanTarihAd.TabIndex = 13;
            this.BeyanTarihAd.Text = "Beyan Tarih ve Ad ";
            this.toolTip1.SetToolTip(this.BeyanTarihAd, "Beyan yazar ve tarih kotrolü");
            this.BeyanTarihAd.UseVisualStyleBackColor = false;
            this.BeyanTarihAd.Click += new System.EventHandler(this.BeyanTarihAd_Click);
            // 
            // OnsozTarihAd
            // 
            this.OnsozTarihAd.BackColor = System.Drawing.Color.White;
            this.OnsozTarihAd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.OnsozTarihAd.ForeColor = System.Drawing.Color.Black;
            this.OnsozTarihAd.Location = new System.Drawing.Point(143, 173);
            this.OnsozTarihAd.Name = "OnsozTarihAd";
            this.OnsozTarihAd.Size = new System.Drawing.Size(101, 52);
            this.OnsozTarihAd.TabIndex = 14;
            this.OnsozTarihAd.Text = "Önsöz Tarih ve Ad";
            this.toolTip1.SetToolTip(this.OnsozTarihAd, "Önsöz yazar ve tarih kotrolü");
            this.OnsozTarihAd.UseVisualStyleBackColor = false;
            this.OnsozTarihAd.Click += new System.EventHandler(this.OnsozTarihAd_Click);
            // 
            // TezOnay
            // 
            this.TezOnay.BackColor = System.Drawing.Color.White;
            this.TezOnay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TezOnay.ForeColor = System.Drawing.Color.Black;
            this.TezOnay.Location = new System.Drawing.Point(262, 103);
            this.TezOnay.Name = "TezOnay";
            this.TezOnay.Size = new System.Drawing.Size(101, 52);
            this.TezOnay.TabIndex = 15;
            this.TezOnay.Text = "Tez Onay";
            this.toolTip1.SetToolTip(this.TezOnay, "Tez için gerekli onay\r\nimzaları alıdı mı kontrolü");
            this.TezOnay.UseVisualStyleBackColor = false;
            this.TezOnay.Click += new System.EventHandler(this.TezOnay_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(35, 110);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(308, 27);
            this.progressBar1.TabIndex = 25;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(47, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 26);
            this.label1.TabIndex = 26;
            this.label1.Text = "PLAGIARISM CONTROL PROGRAM";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar2);
            this.groupBox1.Controls.Add(this.BeyanTarihAd);
            this.groupBox1.Controls.Add(this.KaynakcaKontrol);
            this.groupBox1.Controls.Add(this.AlintiKontrol);
            this.groupBox1.Controls.Add(this.SekillerListesi);
            this.groupBox1.Controls.Add(this.TezOnay);
            this.groupBox1.Controls.Add(this.TablolarListesi);
            this.groupBox1.Controls.Add(this.OnsozTarihAd);
            this.groupBox1.Controls.Add(this.BaslikSayfaNumaralari);
            this.groupBox1.Controls.Add(this.OnsozTesekkur);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(35, 336);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(393, 311);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "KONTROLLER";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            this.groupBox1.MouseHover += new System.EventHandler(this.groupBox1_MouseHover);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(36, 252);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(308, 28);
            this.progressBar2.TabIndex = 26;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.sonuclar);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(36, 130);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(392, 162);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DOSYA İŞLEMLERİ";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // sonuclar
            // 
            this.sonuclar.BackColor = System.Drawing.Color.White;
            this.sonuclar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sonuclar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sonuclar.Location = new System.Drawing.Point(250, 37);
            this.sonuclar.Name = "sonuclar";
            this.sonuclar.Size = new System.Drawing.Size(101, 52);
            this.sonuclar.TabIndex = 26;
            this.sonuclar.Text = "Hata Listesi";
            this.sonuclar.UseVisualStyleBackColor = false;
            this.sonuclar.Click += new System.EventHandler(this.sonuclar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(74)))), ((int)(((byte)(131)))));
            this.ClientSize = new System.Drawing.Size(1092, 688);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button KaynakcaKontrol;
        private System.Windows.Forms.Button AlintiKontrol;
        private System.Windows.Forms.Button SekillerListesi;
        private System.Windows.Forms.Button TablolarListesi;
        private System.Windows.Forms.Button BaslikSayfaNumaralari;
        private System.Windows.Forms.Button OnsozTesekkur;
        private System.Windows.Forms.Button BeyanTarihAd;
        private System.Windows.Forms.Button OnsozTarihAd;
        private System.Windows.Forms.Button TezOnay;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button sonuclar;
        private System.Windows.Forms.ToolTip toolTip2;
    }
}

