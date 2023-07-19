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
using System.Security.Cryptography;

namespace TelefonKayıtları
{
    public partial class Contacts : Form
    {
        public Contacts()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.ContactsTableAdapter ds = new DataSet1TableAdapters.ContactsTableAdapter();
        //DataSet1TableAdapters.Favotites1TableAdapter ds1 = new DataSet1TableAdapters.Favotites1TableAdapter();
        public string addNew;

        private void Contacts_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.GetData();
            dataGridView1.Columns[0].Visible = false;
            textBoxPhone.Text = addNew;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ds.UpdateQuery(textBoxPhone.Text, textBoxName.Text, textBoxSurname.Text, bool.Parse(labelBlocked.Text), byte.Parse(label6.Text));
            labelBlocked.Text = "False";
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = ds.UpdateQuery(textBox3.Text, textBox1.Text, textBox2.Text, Convert.ToBoolean(label5.Text), dataGridView1.SelectedCells[0].ToString());
            dataGridView1.DataSource = ds.GetData();
            labelBlocked.Text = "False";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ds.DeleteQuery(byte.Parse(label6.Text));
            labelBlocked.Text = "False";
        }

        private void buttonBlock_Click(object sender, EventArgs e)
        {
            if (buttonBlock.Text=="Unblock")
            {
                buttonBlock.Text = "Block";
                labelBlocked.Text = "False";
            }
            else
            {
                buttonBlock.Text = "Unblock";
                labelBlocked.Text = "True";
            }
            ds.UpdateQuery(textBoxPhone.Text, textBoxName.Text, textBoxSurname.Text, bool.Parse(labelBlocked.Text), byte.Parse(label6.Text));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label6.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                labelBlocked.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Unaccepted action. Please try again.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            if (labelBlocked.Text=="True")
            {
                buttonBlock.Text = "Unblock";
            }
            if (labelBlocked.Text=="False")
            {
                buttonBlock.Text = "Block";
            }
           
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            ds.InsertQuery(textBoxPhone.Text, textBoxName.Text, textBoxSurname.Text, bool.Parse(labelBlocked.Text));
        }

        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            textBoxPhone.Text = "";
            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();
        }

        private void buttonDial_Click(object sender, EventArgs e)
        {
            Form1 frm=new Form1();
            frm.dialButton = textBoxPhone.Text;
            this.Hide();
            frm.Show();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxPhone.Text = "";
            textBoxName.Text = "";
            textBoxSurname.Text = "";
        }
    }
}
