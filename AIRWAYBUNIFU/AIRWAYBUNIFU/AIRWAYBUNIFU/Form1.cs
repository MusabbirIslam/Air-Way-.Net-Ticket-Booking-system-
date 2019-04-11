using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AIRWAYBUNIFU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bunifuTextbox2._TextBox.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string name = bunifuTextbox1.text;
            string pass = bunifuTextbox2.text;
            if (name.Equals("user") && pass.Equals("user"))
            {
                Form2 f2 = new Form2();
                this.Hide();
                f2.ShowDialog();
                this.Show();
            }
            else if(name.Equals("admin") && pass.Equals("admin"))
            {
                Form3 f3 = new Form3();
                this.Hide();
                f3.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Wrong user name or Password");
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form3 f3 = new Form3();
            //f3.ShowDialog();
            //f3.Show();
            //this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.Transparent;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.BackColor = System.Drawing.Color.Transparent;
        }
    }
}
