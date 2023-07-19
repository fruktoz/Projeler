using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelefonKayıtları;

namespace TelefonKayıtları
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string dialButton;
        
        
        private void buttonContacts_Click(object sender, EventArgs e)
        {
            
            Contacts contacts = new Contacts();
            contacts.Show();
            this.Hide();
            

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            //textBox2.Text=textBox2.Text.Length[0]="";
            //textBox2.Text = textBox2.Text[(textBox2.Text.Length)+1].ToString();
        }

        private void buttonNr1_Click(object sender, EventArgs e)
        {
            textBox2.Text+=buttonNr1.Text;
        }

        private void buttonNr2_Click(object sender, EventArgs e)
        {
            textBox2.Text +=buttonNr2.Text;
        }

        private void buttonNr3_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr3.Text;
        }

        private void buttonNr4_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr4.Text;
        }

        private void buttonNr5_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr5.Text;
        }

        private void buttonNr6_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr6.Text;
        }

        private void buttonNr7_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr7.Text;
        }

        private void buttonNr8_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr8.Text;
        }

        private void buttonNr9_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr9.Text;
        }

        private void buttonNr0_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonNr0.Text;
        }

        private void buttonStar_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonStar.Text;
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonSquare.Text;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            textBox2.Text += buttonPlus.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = dialButton;
        }

        private void buttonAddToContacts_Click(object sender, EventArgs e)
        {
            Contacts cnt =new Contacts();
            cnt.addNew = textBox2.Text;
            this.Hide();
            cnt.Show();
        }
    }
}
