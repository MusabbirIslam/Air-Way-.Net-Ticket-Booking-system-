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
    public partial class Form4 : Form
    {
        //enum ticketState { OneWay, Return, Done };
        List<Plane> DepartingPlaneList;
        List<Plane> returnPlaneList;
        Plane destinationPlane;
        Plane nextDestinationPlane;
        TicketState state;

        public Form4(List<Plane> DepartingPlaneList)
        {
            InitializeComponent();
            this.DepartingPlaneList = DepartingPlaneList;
            state = TicketState.OneWay;
        }

        public Form4(List<Plane> DepartingPlaneList, List<Plane> returnPlaneList) : this(DepartingPlaneList)
        {
            this.returnPlaneList = returnPlaneList;
            state = TicketState.Return;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            //taking value to data grid view

            bunifuCustomDataGrid1.DataSource = null;
            bunifuCustomDataGrid1.DataSource = DepartingPlaneList;

            //making full screen
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            //Form5 f5 = new Form5();
            //f5.Show();
            //this.Hide();

        }

        private void bunifuFlatButton1_Click_2(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
            this.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
            this.Close();

        }

        private void bunifuCustomDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (state == TicketState.OneWay)
            {
                //marking selected plane and sending to next form
                destinationPlane = (Plane)bunifuCustomDataGrid1.CurrentRow.DataBoundItem;
                Form5 f5 = new Form5(destinationPlane ,state);
                this.Hide();
                f5.ShowDialog();
                Close();
            }
            else if (state == TicketState.Return)
            {
                //marking selected plane for dsetination and giving new source to grid view
                if (returnPlaneList != null)
                {
                    destinationPlane = (Plane)bunifuCustomDataGrid1.CurrentRow.DataBoundItem;
                    bunifuCustomDataGrid1.DataSource = null;
                    bunifuCustomDataGrid1.DataSource = returnPlaneList;
                    bunifuCustomDataGrid1.Refresh();
                    state = TicketState.Done;
                }

            }
            else
            {
                //marking next selected plane and open new form
                nextDestinationPlane = (Plane)bunifuCustomDataGrid1.CurrentRow.DataBoundItem;
                state = TicketState.Return;
                Form5 f5 = new Form5(destinationPlane, nextDestinationPlane, state);//destinationPlane, nextDestinationPlane);
                this.Hide();
                f5.ShowDialog();
                Close();
            }
        }
    }
}
