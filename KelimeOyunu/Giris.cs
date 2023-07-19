using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeOyunu
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oyun frmOyun = new Oyun();
            frmOyun.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Başlat butonuna tıkladıktan sonra oyun başlar.\nHarf sayısının 100 katı kadar puan alabilirsiniz.\n"+
               "Aldığınız her harf sorudan alabileceğiniz puanı düşürür.\nCevap vermek istediğinizde süreyi durdurarak "+
               "cevap kutusunu aktif hale getirebilirsiniz.\nCevap hakkında bir fikriniz yoksa harf alarak ya da "+
               "süreyi durdurduktan sonra kontrol et butonuna bastıktan sonra sıradaki soruya geçebilirsiniz.\n"+
               "Yazmak istediğiniz cevabın tüm harflerinin büyük olmasına özellikle dikkat ediniz."+
               "\nHazırsanız başlayalım. Bol şans...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
