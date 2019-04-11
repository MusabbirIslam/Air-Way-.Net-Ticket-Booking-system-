using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Airway.Core;
namespace AIRWAYBUNIFU
{
    public partial class Form6 : Form
    {
        private AdminService service = new AdminService();
        public Form6()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                string name = bunifuCustomTextbox1.Text;
                string destination = bunifuCustomTextbox2.Text;
                string start = bunifuCustomTextbox3.Text;
                float busPrice = float.Parse(textBox1.Text);
                float ecoPrice = float.Parse(textBox2.Text);
                float delPrice = float.Parse(textBox3.Text);

                service.createSchedule(name, destination, start, dateTimePicker2.Value, busPrice, ecoPrice, delPrice);
                MessageBox.Show("Successfully Added");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Empty Text Field or Wrong Value" + ex.ToString()); }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
