using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace KelimeOyunu
{
    public partial class Oyun : Form
    {
        public Oyun()
        {
            InitializeComponent();
        }
        SqlConnection baglantiKelimeOyunu = new SqlConnection("Data Source=DESKTOP-4TDPNH4\\MSSQLSERVER19;Initial Catalog=KelimeOyunu;Integrated Security=True");
        //Initial Catalog=KelimeOyunu;
        Random rnd = new Random();
        int top = 340;
        int left = 105;
        //yeni baglanti adresi 
        //Data Source=DESKTOP-4TDPNH4\MSSQLSERVER19;Initial Catalog=KelimeOyunu;Integrated Security=True

        Button[] button = new Button[Convert.ToInt16(10)];
        private void Oyun_Load(object sender, EventArgs e)
        {
            //BAŞLANGIÇTA AYARLANAN PUAN DEĞERİ
            lblPuan.Text = (Convert.ToInt16(lblButonSayaci.Text) * 100).ToString();

            baglantiKelimeOyunu.Open();
            //4 HARFLİ CEVAPLARIN MAKSİMUM SAYISINI VERİ TABANINDAN AL VE MAKSİMUM SAYISINA KADAR RASTGELE SAYI ÜRET
            SqlCommand komut2 = new SqlCommand("select max(SoruNoId) from SoruCevap4", baglantiKelimeOyunu);
            SqlDataReader dr = komut2.ExecuteReader();
            int maxSoruSayisi;//soru sayısının maksimum değerini tutan değişken

            while (dr.Read())
            {
                maxSoruSayisi = Convert.ToInt16(dr[0]);
                int random = rnd.Next(1, maxSoruSayisi + 1);
                lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
            }
            baglantiKelimeOyunu.Close();

            //4 HARFLİ CEVAPLARIN SORULARININ ID DEĞERİNE GÖRE AL
            baglantiKelimeOyunu.Open();
            SqlCommand komut = new SqlCommand("select Soru,Cevap from SoruCevap4 where SoruNoId=@s1", baglantiKelimeOyunu);
            komut.Parameters.AddWithValue("@s1", lblRandom.Text);
            SqlDataReader dr2 = komut.ExecuteReader();
            while (dr2.Read())
            {
                rchSoruKutusu.Text = dr2[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                txtCevap.Text = dr2[1].ToString();
            }
            baglantiKelimeOyunu.Close();


            //BAŞLANGIÇTA 4 ADET BUTON OLUŞTURUR
            //int labelSecilen = Convert.ToInt16(lblButonSayaci.Text);
            timer1.Start();
            lblSure.Visible = true;
            lblSure2.Visible = true;
            label3.Visible = true;
            //Button[] button = new Button[Convert.ToInt16(lblButonSayaci.Text)];//105,280 sayfanın en üstünde belirtildi

            for (int i = 0; i <= button.GetUpperBound(0); i++) //buttons.GetUpperBound(0) 0. boyutun alabileceği en yüksek değer(4(labelSecilen))
            {                                                  //0. indexten 4. indexe kadar (4 dahil değil)
                button[i] = new Button();
                button[i].Width = 55;
                button[i].Height = 55;
                button[i].Left = left;
                button[i].Top = top;
                button[i].ForeColor = Color.White;
                button[i].BackColor = Color.White;
                button[i].TextAlign = ContentAlignment.MiddleCenter;
                button[i].Name = (i).ToString();
                //button[i].Text = button[i].Name;
                //button[i].te;

                left += 60;
                this.Controls.Add(button[i]);
                button[i].Enabled = false;

            }
            button[4].Visible = false;
            button[5].Visible = false;
            button[6].Visible = false;
            button[7].Visible = false;
            button[8].Visible = false;
            button[9].Visible = false;

        }

        int sayac = 3;
        int sayac2 = 59;
        int butonSayaci = 4;

        //TIMER OLAYLARI
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac2 -= 1;
            lblSure2.Text = sayac2.ToString();
            lblSure.Text = sayac.ToString();

            if (sayac == 0 && sayac2 == 0)
            {
                timer1.Stop();
                lblSure.ForeColor = Color.Red;
                lblSure2.ForeColor = Color.Red;
                label3.ForeColor = Color.Red;
            }
            if (sayac2 == 0)
            {
                sayac -= 1;
                sayac2 = 59;
            }
        }

        //SÜREYİ DURDUR
        private void btnDurdur_Click(object sender, EventArgs e)
        {
            timer1.Stop(); 
            btnHarfAl.Enabled = false;
            btnkontrolEt.Enabled = true;
            btnDurdur.Enabled = false;
            mskCevap.Visible = true;
            label10.Visible= true;

        }

        //SORU SAYACI
        int sayacSoru = 1;

        //SONRAKİ SORUYA GEÇME-----SORU SAYISINA GÖRE YENİ BUTON EKLEME
        private void btnSonrakiSoru_Click(object sender, EventArgs e)
        {
            timer1.Start(); //SÜREYİ DEVAM ETTİR
            btnHarfAl.Enabled = true; //HARF ALMA BUTONUNU AKTİF HALE GETİR
            btnkontrolEt.Enabled = false; //KONTROL ET BUTONUNU PASİF HALE GETİR
            btnDurdur.Enabled = true; //DURDUR BUTONUNU AKTİF HALE GETİR 
            mskCevap.Visible = false; //CEVAP KUTUSUNU GİZLE           
            mskCevap.Text = ""; //CEVAP KUTUSUNU TEMİZLE
            label10.Visible = false; //label10'U GİZLE
            
            pictureTick.Visible = false;
            pictureTick2.Visible = false;
            pictureTick3.Visible = false;
            pictureCross.Visible = false;
            pictureCross2.Visible = false;
            pictureCross3.Visible = false;

            

            //BUTON TEXTLERİNİ NORMALE DÖNDÜRÜR
            for (int j = 0; j < txtCevap.Text.Length; j++)
            {
                button[j].Text = "";
            }

            //HER 2 SORUDDA BİR VISIBLE ÖZELLİĞİNİ DEĞİŞTİR VE YEN BUTONLARI GÖRÜNTÜLE
            sayacSoru += 1;
            lblSoruSayaci.Text = sayacSoru.ToString();
            if (sayacSoru == 3 || sayacSoru == 5 || sayacSoru == 7 || sayacSoru == 9 || sayacSoru == 11 || sayacSoru == 13)
            {
                butonSayaci += 1;
                lblButonSayaci.Text = butonSayaci.ToString();//swich case yapılarına bağlı olarak visible özelliğini değiştirmek için gerekli
            }

            //PUANI BUTON SAYISINA GÖRE YENİDEN DÜZENLE
            lblPuan.Text = (Convert.ToInt16(lblButonSayaci.Text) * 100).ToString();

            //SORU SAYACINA GÖRE VERİTABANINDAN SORU AL
            switch (lblButonSayaci.Text)
            {
                //SORUCEVAP4 TABLOSUNA KAYITLI MAKSİMUM SORU SAYISINI ALIR VE BUNA GÖRE RANDOM SAYI ÜRETİR
                case "4":
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut4 = new SqlCommand("select max(SoruNoId) from SoruCevap4", baglantiKelimeOyunu);
                    SqlDataReader dr4 = komut4.ExecuteReader();
                    int maxSoruSayisi;
                    while (dr4.Read())
                    {
                        maxSoruSayisi = Convert.ToInt16(dr4[0]);
                        int random = rnd.Next(1, maxSoruSayisi + 1 );
                        label9.Text = random.ToString();
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();
                    //SORUCEVAP4 TABLOSUNDAN SORU ID'SİNE GÖRE SORUYU ALIR
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut44 = new SqlCommand("select Soru,Cevap from SoruCevap4 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut44.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr44 = komut44.ExecuteReader();
                    while (dr44.Read())
                    {
                        rchSoruKutusu.Text = dr44[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text = dr44[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;

                case "5":
                    button[4].Visible = true;
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut5 = new SqlCommand("select max(SoruNoId) from SoruCevap5", baglantiKelimeOyunu);
                    SqlDataReader dr5 = komut5.ExecuteReader();
                    int maxSoruSayisi5;
                    while (dr5.Read())
                    {
                        maxSoruSayisi5 = Convert.ToInt16(dr5[0]);
                        int random = rnd.Next(1, maxSoruSayisi5 + 1);
                        label9.Text = random.ToString();
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();

                    baglantiKelimeOyunu.Open();
                    SqlCommand komut55 = new SqlCommand("select Soru,Cevap from SoruCevap5 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut55.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr55 = komut55.ExecuteReader();
                    while (dr55.Read())
                    {
                        rchSoruKutusu.Text = dr55[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text= dr55[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;

                case "6":
                    button[5].Visible = true;
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut6 = new SqlCommand("select max(SoruNoId) from SoruCevap6", baglantiKelimeOyunu);
                    SqlDataReader dr6 = komut6.ExecuteReader();
                    int maxSoruSayisi6;
                    while (dr6.Read())
                    {
                        maxSoruSayisi6 = Convert.ToInt16(dr6[0]);
                        int random = rnd.Next(1, maxSoruSayisi6 + 1);
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();

                    baglantiKelimeOyunu.Open();
                    SqlCommand komut66 = new SqlCommand("select Soru,Cevap from SoruCevap6 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut66.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr66 = komut66.ExecuteReader();
                    while (dr66.Read())
                    {
                        rchSoruKutusu.Text = dr66[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text = dr66[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;

                case "7":
                    button[6].Visible = true;
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut7 = new SqlCommand("select max(SoruNoId) from SoruCevap7", baglantiKelimeOyunu);
                    SqlDataReader dr7 = komut7.ExecuteReader();
                    int maxSoruSayisi7;
                    while (dr7.Read())
                    {
                        maxSoruSayisi7 = Convert.ToInt16(dr7[0]);
                        int random = rnd.Next(1, maxSoruSayisi7 + 1);
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();

                    baglantiKelimeOyunu.Open();
                    SqlCommand komut77 = new SqlCommand("select Soru,Cevap from SoruCevap7 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut77.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr77 = komut77.ExecuteReader();
                    while (dr77.Read())
                    {
                        rchSoruKutusu.Text = dr77[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text= dr77[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;

                case "8":
                    button[7].Visible = true;
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut8 = new SqlCommand("select max(SoruNoId) from SoruCevap8", baglantiKelimeOyunu);
                    SqlDataReader dr8 = komut8.ExecuteReader();
                    int maxSoruSayisi8;
                    while (dr8.Read())
                    {
                        maxSoruSayisi8 = Convert.ToInt16(dr8[0]);
                        int random = rnd.Next(1, maxSoruSayisi8 + 1);
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();

                    baglantiKelimeOyunu.Open();
                    SqlCommand komut88 = new SqlCommand("select Soru,Cevap from SoruCevap8 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut88.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr88 = komut88.ExecuteReader();
                    while (dr88.Read())
                    {
                        rchSoruKutusu.Text = dr88[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text= dr88[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;

                case "9":
                    button[8].Visible = true;
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut9 = new SqlCommand("select max(SoruNoId) from SoruCevap9", baglantiKelimeOyunu);
                    SqlDataReader dr9 = komut9.ExecuteReader();
                    int maxSoruSayisi9;
                    while (dr9.Read())
                    {
                        maxSoruSayisi9 = Convert.ToInt16(dr9[0]);
                        int random = rnd.Next(1, maxSoruSayisi9 + 1);
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();

                    baglantiKelimeOyunu.Open();
                    SqlCommand komut99 = new SqlCommand("select Soru,Cevap from SoruCevap9 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut99.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr99 = komut99.ExecuteReader();
                    while (dr99.Read())
                    {
                        rchSoruKutusu.Text = dr99[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text= dr99[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;

                case "10":
                    button[9].Visible = true;
                    rchSoruKutusu.Text = "";
                    baglantiKelimeOyunu.Open();
                    SqlCommand komut10 = new SqlCommand("select max(SoruNoId) from SoruCevap10", baglantiKelimeOyunu);
                    SqlDataReader dr10 = komut10.ExecuteReader();
                    int maxSoruSayisi10;
                    while (dr10.Read())
                    {
                        maxSoruSayisi10 = Convert.ToInt16(dr10[0]);
                        int random = rnd.Next(1, maxSoruSayisi10 + 1);
                        lblRandom.Text = random.ToString();//RANDOM SAYISINI lblRandom'a ata
                    }
                    baglantiKelimeOyunu.Close();

                    baglantiKelimeOyunu.Open();
                    SqlCommand komut100 = new SqlCommand("select Soru,Cevap from SoruCevap10 where SoruNoId=@s1", baglantiKelimeOyunu);
                    komut100.Parameters.AddWithValue("@s1", lblRandom.Text);
                    SqlDataReader dr100 = komut100.ExecuteReader();
                    while (dr100.Read())
                    {
                        rchSoruKutusu.Text = dr100[0].ToString();//dr2 sorgusundan gelen 0. indexi richTextBox'a ata
                        txtCevap.Text = dr100[1].ToString();
                    }
                    baglantiKelimeOyunu.Close();
                    break;
            }

            if (Convert.ToInt16(lblSoruSayaci.Text)>= 14)//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                btnSonrakiSoru.Enabled = false;
            }
            else
            {
                btnSonrakiSoru.Enabled = false;
            }
        }

        private void btnHarfAl_Click(object sender, EventArgs e)
        {
            lblPuan.Text=(Convert.ToInt16(lblPuan.Text) -100).ToString();

            bool test = false;
            int randomHarf;
            
            switch (lblButonSayaci.Text)
            {
                case "4":
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text == "0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled= false;
                        btnSonrakiSoru.Enabled= true;
                        timer1.Stop();
                    }
                    //bool test = false;
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    break;

                case "5":
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text=="0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled = false;
                        btnSonrakiSoru.Enabled = true;
                        timer1.Stop();
                    }
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    break;

                case "6":
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text == "0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled = false;
                        btnSonrakiSoru.Enabled = true;
                        timer1.Stop();
                    }
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    break;

                case "7":
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text == "0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled = false;
                        btnSonrakiSoru.Enabled = true;
                        timer1.Stop();
                    }
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    break;

                case "8":
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text == "0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled = false;
                        btnSonrakiSoru.Enabled = true;
                        timer1.Stop();
                    }
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    break;

                case "9":
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text == "0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled = false;
                        btnSonrakiSoru.Enabled = true;
                        timer1.Stop();
                    }
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    break;

                case "10":
                    
                    //EN FAZLA BUTON SAYACININ DEĞERİ KADAR HARF ALINABİLMESİ İÇİN KONTROL EDİLİR
                    if (lblPuan.Text == "0")
                    {
                        btnHarfAl.Enabled = false;
                        btnDurdur.Enabled = false;
                        if (lblSoruSayaci.Text=="14")
                        {
                            btnSonrakiSoru.Enabled = false;
                        }
                        else
                        {
                            btnSonrakiSoru.Enabled = true;
                        }
                        
                        timer1.Stop();
                    }
                    while (test == false)
                    {
                        randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                                                                                       //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));
                        char[] tekHarf = new char[txtCevap.Text.Length];
                        for (int j = 0; j < txtCevap.Text.Length; j++)
                        {
                            //randomHarf = rnd.Next(0, Convert.ToInt16(lblButonSayaci.Text));//cevabın harf sayısı kadar sayı üret
                            tekHarf[j] = txtCevap.Text[j];
                            //label9.Text = randomHarf.ToString();
                            if (randomHarf.ToString() == button[j].Name)
                            {
                                if (button[j].Text == "")
                                {
                                    button[j].Text = tekHarf[j].ToString();
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                        }
                    }
                    
                    break;
            }
        }

        private void btnkontrolEt_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(lblSoruSayaci.Text)>13)
            {
                btnSonrakiSoru.Enabled = false;
                btnkontrolEt.Enabled = false;
            }
            else
            {
                btnSonrakiSoru.Enabled = true;
                btnkontrolEt.Enabled = false;
            }
           
            if (mskCevap.Text.ToUpper()==txtCevap.Text)
            {
                lblKasa.Text=(Convert.ToInt16(lblKasa.Text) + Convert.ToUInt16(lblPuan.Text)).ToString();
                pictureTick.Visible = true;
                pictureTick2.Visible = true;
                pictureTick3.Visible = true;
            }
            else
            {
                lblKasa.Text = (Convert.ToInt16(lblKasa.Text) - Convert.ToUInt16(lblPuan.Text)).ToString();
                pictureCross.Visible = true;
                pictureCross2.Visible = true;
                pictureCross3.Visible = true;
            }
        }
    }
}





