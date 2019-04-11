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
    public partial class Form2 : Form
    {
        internal static UserService serviceClass;
        Form4 form4;
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            serviceClass = new UserService();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            AutoCompleteStringCollection dataCollection = new AutoCompleteStringCollection();
            dataCollection = serviceClass.placName("destination");

            AutoCompleteStringCollection dataCollection2 = new AutoCompleteStringCollection();
            dataCollection2 = serviceClass.placName("start");

            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = dataCollection2;

            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = dataCollection;

            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteCustomSource = dataCollection2;

            textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteCustomSource = dataCollection;

            textBox6.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox6.AutoCompleteCustomSource = dataCollection;

            textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox5.AutoCompleteCustomSource = dataCollection2;

            textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteCustomSource = dataCollection2;

            textBox8.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox8.AutoCompleteCustomSource = dataCollection;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            checkWhichTabOpen();
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }

        private void checkWhichTabOpen()
        {
            try
            {
                //input of passenger number

                Byte adult = Byte.Parse(comboBox1.SelectedItem.ToString());
                Byte child = Byte.Parse(comboBox4.SelectedItem.ToString());
                Byte infant = Byte.Parse(comboBox3.SelectedItem.ToString());
                Byte seat = (Byte)(adult + child + infant);
                String seatClass = comboBox2.SelectedItem.ToString();

                serviceClass.setPassengers(adult, child, infant, seat, seatClass);

                //taking data according to tab selected
                if (tabControl1.SelectedTab == tabPage1)
                {
                    string from = textBox1.Text;

                    string to = textBox2.Text;
                    DateTime departing = bunifuDatepicker1.Value;
                    List<Plane> departingPlaneList = serviceClass.checkOneway(from, to, departing, seat);
                    if (departingPlaneList.Count == 0) { MessageBox.Show("destination  Plane not Available"); }
                    else { form4 = new Form4(departingPlaneList); this.Hide(); form4.ShowDialog(); Close(); }
                }
                else if (tabControl1.SelectedTab == tabPage2)
                {
                    string from = textBox3.Text;
                    string to = textBox4.Text;
                    DateTime departing = bunifuDatepicker2.Value;
                    DateTime returning = bunifuDatepicker3.Value;

                    //checking plane
                    List<Plane> returnList = new List<Plane>();
                    List<Plane> departingPlaneList = serviceClass.checkReturn(out returnList, from, to, departing, returning, seat);

                    //if no return plane available massage show
                    if (returnList.Count == 0 && departingPlaneList.Count == 0) { MessageBox.Show("NO Plane Available"); }
                    else if (returnList.Count == 0) { MessageBox.Show("NO Return Plane Available"); }
                    else if (departingPlaneList.Count == 0) { MessageBox.Show("destination  Plane not Available"); }
                    else { form4 = new Form4(departingPlaneList, returnList); this.Hide(); form4.ShowDialog(); Close(); }

                }
                else
                {
                    string from1 = textBox5.Text;
                    string to1 = textBox6.Text;
                    string from2 = textBox7.Text;
                    string to2 = textBox8.Text;
                    DateTime departing1 = bunifuDatepicker4.Value;
                    DateTime departing2 = bunifuDatepicker5.Value;

                    //checking plane
                    List<Plane> nextTravelList = new List<Plane>();
                    List<Plane> departingPlaneList = serviceClass.checkMultyCity(out nextTravelList, from1, to1, departing1, from2, to2, departing2, seat);

                    //if no return plane available massage show
                    if (nextTravelList.Count == 0 && departingPlaneList.Count == 0) { MessageBox.Show("NO Plane Available"); }
                    else if (nextTravelList.Count == 0) { MessageBox.Show("Next destination Plane not Available"); }
                    else if (departingPlaneList.Count == 0) { MessageBox.Show("destination  Plane not Available"); }
                    else { form4 = new Form4(departingPlaneList, nextTravelList); this.Hide(); form4.ShowDialog(); Close(); }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Wrong number of passengers or Empty field " + e.ToString());
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            book1.Visible = true;
        }
    }
}
