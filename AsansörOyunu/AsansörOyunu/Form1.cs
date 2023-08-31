using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsansörOyunu
{
    public partial class Form1 : Form
    {
        public void oyunSonu()
        {
            
            label3.Enabled = false; 
            label4.Enabled = false; 
            label5.Enabled = false; 
            label6.Enabled = false; 
            label7.Enabled = false; 
            label8.Enabled = false; 
            label9.Enabled = false; 
            label10.Enabled = false; 
            label11.Enabled = false; 
            label12.Enabled = false; 
            label16.Enabled = false; 
            label18.Enabled = false; 
            label20.Enabled = false; 
            label22.Enabled = false; 
            label23.Enabled = false; 
            label21.Enabled = false; 
            label19.Enabled = false; 
            label17.Enabled = false; 
            label13.Enabled = false; 
            label1.Enabled = false;
            katilKatbilgisi.Stop();
            timerBeklemeSayaci.Stop();
            
        }

        public static int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
        public int sayac, sayacKatil, kalanSure = 3, gosterilen;
        Random rndm = new Random(seed);
        List<string> list = new List<string>();
        List<int> sifre = new List<int>();
        public int katNumarasi = 0, katilKatNumarasi = 0;
        public int sifreCevap, random, randomsifre, randomKatil;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list.Insert(0, "");
            for (int i = 1; i < 10; i++)
            {
                int random = rndm.Next(1, 21);
                list.Add(random.ToString());
                //int floorNumber = rndm.Next(1, 21);

            }
            label15.Text = list[1];
            label50.Text = list[2];
            label52.Text = list[3];
            label51.Text = list[4];
            label49.Text = list[5];
            label53.Text = list[6];
            label55.Text = list[7];
            label57.Text = list[8];
            label56.Text = list[9];
            // label61.Text = list[10];


            //tek haneli 3 rakamla şifre oluştur 
            for (int i = 0; i < 4; i++)
            {
                int randomSifre = rndm.Next(1, list.Count);//belirtilen sayılar arasından rastgele sayılar seçip şifreyi kaydetme
                //List<int> sifre= new List<int>();
                sifre.Add(randomSifre);
            }
            label58.Text = sifre[0].ToString();
            label59.Text = sifre[1].ToString();
            label60.Text = sifre[2].ToString();
            label62.Text = sifre[3].ToString();

            //sifreCevap = Convert.ToInt32(sifre[0]+sifre[1]+sifre[2].ToString());

            //int murdererFloorNumber=rndm.Next(1, 21);
            //label14.Text=murdererFloorNumber.ToString();
            //MessageBox.Show("You have to go to floors in the floor list until ground floor is fixed and than you can escape (if you want to escape)");

            //listedeki sayılarla basılan kat numarası kontrolü


        }

        private void label3_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label3.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //gidilen kat numarası belirlenen katlardan rastgele seçilmiş olan
            //biriyle eşleşirse şifrenin rastgele bir sayısını ver

            //katilin gittiği kat ile oyuncunun gittiği katın eşleşip eşleşmediğini kontrol eden kod
            if (label14.Text==label24.Text)
            {
                oyunSonu();
                button2.Enabled= false;
                MessageBox.Show("Katil ile aynı kata geldin. 💀");
            }
            if (label24.Text == list[int.Parse(label58.Text)].ToString())
            {
                label58.ForeColor = Color.Green;
                label58.Visible = true;

            }
            if (label24.Text == list[int.Parse(label59.Text)].ToString())
            {
                label59.ForeColor = Color.Green;
                label59.Visible = true;

            }
            if (label24.Text == list[int.Parse(label60.Text)].ToString())
            {
                label60.ForeColor = Color.Green;
                label60.Visible = true;

            }
            if (label24.Text == list[int.Parse(label62.Text)].ToString())
            {
                label62.ForeColor = Color.Green;
                label62.Visible = true;
            }
            if (label58.Visible == true && label59.Visible == true && label60.Visible == true && label62.Visible == true)
            {
                label2.ForeColor= Color.Green;
                label2.Enabled = true;
            }
            if (label24.Text == label2.Text)
            {

                katilKatbilgisi.Stop();
                MessageBox.Show("Tebrikler! Şifreleri bulup katile yakalanmadan giriş katından kaçmayı başardınız.");
                button2.Enabled = false;
            }
        }

        //kat bilgisini tutan ve belirlenen label'a yazan kod
        private void timerKatSayaci_Tick(object sender, EventArgs e)
        {
            if (katNumarasi < sayac)
            {
                button2.Enabled = false;
                //label24.Text = katNumarasi.ToString();
                katNumarasi += 1;
                label24.Text = katNumarasi.ToString();

            }
            else if (katNumarasi > sayac)
            {
                button2.Enabled = false;
                //label24.Text = katNumarasi.ToString();
                katNumarasi -= 1;
                label24.Text = katNumarasi.ToString();

            }
            else
            {
                button2.Enabled = true;
                button2.ForeColor = Color.Blue;
                if (katNumarasi!=0)
                {
                    timerBeklemeSayaci.Start();
                }
                
                timerKatSayaci.Stop();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label4.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatilKatSayaci.Start();
            timerKatSayaci.Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label5.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label6.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label7.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label8.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label9.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label10.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label11.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label12.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label16.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label18.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label20.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label22.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label23.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label21.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        //çıkış katı
        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
            sayac = int.Parse(label2.Text);
            timerKatSayaci.Start();
            timerBeklemeSayaci.Stop();
            timerKatilKatSayaci.Stop();
            oyunSonu();
        }

        //uygulamayı yeniden başat
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //katil kat bilgisini anlık olarak gösteren kod bloğu
        private void katilKatbilgisi_Tick(object sender, EventArgs e)
        {
            //int gosterilen = 2;

            label14.Visible = true;
            gosterilen = int.Parse(label65.Text);
            gosterilen -= 1;
            label65.Text = gosterilen.ToString();
            if (gosterilen <= 0)
            {
                katilKatbilgisi.Stop();
                label14.Visible = false;
                //gosterilen = 2;
                //label14.Text = gosterilen.ToString();
            }

        }

        //bir katta bekleme süresini tutan kod bloğu
        private void timerBeklemeSayaci_Tick(object sender, EventArgs e)
        {
            kalanSure = int.Parse(label54.Text);
            kalanSure -= 1;
            label54.Text = kalanSure.ToString();
            if (kalanSure == 0)
            {
                MessageBox.Show("Çok bekledin. Bulunduğun kat anlaşıldı! 💀");
                button2.Visible=false;
                oyunSonu();
            }

        }

        private void label19_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label19.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label17.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label13.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            timerBeklemeSayaci.Stop();
            label65.Text = "2";
            kalanSure = 3;
            label54.Text = kalanSure.ToString();
            sayac = int.Parse(label1.Text);
            randomKatil = rndm.Next(1, 10);
            sayacKatil = int.Parse(list[randomKatil]);
            label63.Text = sayacKatil.ToString();
            timerKatSayaci.Start();
            timerKatilKatSayaci.Start();
        }

        //katilin gittiği katı tutan kod bloğu
        private void timerKatilKatSayaci_Tick(object sender, EventArgs e)
        {
            if (katilKatNumarasi < int.Parse(label63.Text))
            {
                //label24.Text = katNumarasi.ToString();
                katilKatNumarasi += 1;
                label14.Text = katilKatNumarasi.ToString();

            }
            else if (katilKatNumarasi > int.Parse(label63.Text))
            {
                //label24.Text = katNumarasi.ToString();
                katilKatNumarasi -= 1;
                label14.Text = katilKatNumarasi.ToString();

            }
            else
            {
                katilKatbilgisi.Start();
            }
        }
    }
}
