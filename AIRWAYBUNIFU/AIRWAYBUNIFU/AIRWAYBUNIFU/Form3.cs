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
using Airway.Entity;
namespace AIRWAYBUNIFU
{
    public partial class Form3 : Form
    {
        private AdminService service;
        public Form3()
        {
            InitializeComponent();
            service = new AdminService();
            //auto getting plane id from flight
            AutoCompleteStringCollection dataCollection = new AutoCompleteStringCollection();
            dataCollection = service.placName();

            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = dataCollection;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;        
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            UPDATE.Visible = true;
            VIEW.Visible = false;
            CANCEL.Visible = false;
            
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            VIEW.Visible = true;
            UPDATE.Visible = false;
            CANCEL.Visible = false;
           
            viewPassenger();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            CANCEL.Visible = true;
            VIEW.Visible = false;
            UPDATE.Visible = false;
            
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
           
            CANCEL.Visible = false;
            VIEW.Visible = false;
            UPDATE.Visible = false;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            Form6 f6 = new Form6();
            this.Hide();
            f6.ShowDialog();
            this.Show();

        }

        private void slide_Click(object sender, EventArgs e)
        {
            
            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            string planeId = textBox1.Text;
            string fieldName = comboBox1.SelectedItem.ToString();
            string value = textBox2.Text;
            try
            {
                service.updateSchedule(fieldName, value, planeId);
                MessageBox.Show("Successfully Updated");
            }
            catch (Exception ex) { MessageBox.Show("Wrong Plane ID or invalid value" + ex.ToString()); }
        }

        private void viewPassenger()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = service.getPlane();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get passenger list fr selected plane
            Plane select = (Plane)dataGridView1.CurrentRow.DataBoundItem;
            dataGridView1.DataSource = null;

            dataGridView1.DataSource = service.getPassengerList(select.planeID); ;
            dataGridView1.Refresh();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string ticketId = textBox3.Text;
            service.cancelTicket(ticketId);
            this.Refresh();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
