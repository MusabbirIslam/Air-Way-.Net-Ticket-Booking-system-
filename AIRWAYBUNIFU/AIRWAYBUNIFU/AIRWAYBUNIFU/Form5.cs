using Bunifu.Framework.UI;
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
    public partial class Form5 : Form
    {

        private List<BunifuCheckbox> economyClass;
        private List<BunifuCheckbox> BusinessClass;
        private List<BunifuCheckbox> deluxClass;
        private List<string> ticketNumber;
        private List<bool> seatsettings;
        private UserService searvice;

        private SeatClass sClas;
        private TicketState tstate;

        private Double totalPrice;
        private Byte checkboxSelected;
        private Byte countTicketSelection;
        private Plane destinationPlane;
        private Plane nextPlane;

        public Form5(Plane destinationPlane, TicketState tstate)
        {
            InitializeComponent();
            //initializig necessary list and seat state and ticket state
            seatsettings = new List<bool>();
            ticketNumber = new List<string>();
            this.tstate = tstate;
            searvice = Form2.serviceClass;
            
            this.destinationPlane = destinationPlane;
            this.countTicketSelection = 0;
            totalPrice = 0;
            //for not allowing more click of check box then seat number
            checkboxSelected = searvice.TotalSeat;
            
            //getting ticket class
            sClas = searvice.SeatClassProp;
            loadSeat(destinationPlane.planeID);
        }

        public Form5(Plane destinationPlane, Plane nextPlane, TicketState tstate) :this(destinationPlane,tstate)
        {
            this.nextPlane = nextPlane;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadSeat(string planeID)
        {
            if(sClas==SeatClass.Business)
            {
                bunifuGradientPanel2.Visible = true;
                bunifuGradientPanel1.Visible = false;
                bunifuGradientPanel3.Visible = false;
                panel1.Visible = false;
                loadBusSeat(planeID);
            }
            else if(sClas == SeatClass.Economics)
            {
                bunifuGradientPanel1.Visible = true;
                bunifuGradientPanel2.Visible = false;
                bunifuGradientPanel3.Visible = false;
                panel1.Visible = false;
                loadEcoSeat(planeID);
            }
            else if(sClas == SeatClass.Delux)
            {

                bunifuGradientPanel1.Visible = false;
                bunifuGradientPanel2.Visible = false;
                bunifuGradientPanel3.Visible = true;
                panel1.Visible = false;
                loadDelSeat(planeID);
            }
        }

        // loding delux seat represntation from panel and database
        private void loadDelSeat(string planeID)
        {
            deluxClass = new List<BunifuCheckbox>();

            deluxClass.Add(bunifuCheckbox63);
            deluxClass.Add(bunifuCheckbox64);
            deluxClass.Add(bunifuCheckbox65);
            deluxClass.Add(bunifuCheckbox66);
            deluxClass.Add(bunifuCheckbox67);
            deluxClass.Add(bunifuCheckbox68);
            deluxClass.Add(bunifuCheckbox69);
            deluxClass.Add(bunifuCheckbox70);

            deluxClass.Add(bunifuCheckbox71);
            deluxClass.Add(bunifuCheckbox72);
            deluxClass.Add(bunifuCheckbox73);
            deluxClass.Add(bunifuCheckbox74);

            seatsettings = searvice.getDelSeat(planeID);

            int i = 0;
            foreach (bool a in seatsettings)
            {
                if (a == false)
                {
                    deluxClass[i].CheckedOnColor = Color.Red;
                    deluxClass[i].Enabled = false;
                }
                else
                    deluxClass[i].Checked = a;
                i++;
            }
        }


        // loding Business seat represntation from panel and database
        private void loadBusSeat(string planeID)
        {
            BusinessClass = new List<BunifuCheckbox>();
            BusinessClass.Clear();

            BusinessClass.Add(bunifuCheckbox43);
            BusinessClass.Add(bunifuCheckbox44);
            BusinessClass.Add(bunifuCheckbox45);
            BusinessClass.Add(bunifuCheckbox46);
            BusinessClass.Add(bunifuCheckbox47);
            BusinessClass.Add(bunifuCheckbox48);
            BusinessClass.Add(bunifuCheckbox49);
            BusinessClass.Add(bunifuCheckbox50);

            BusinessClass.Add(bunifuCheckbox51);
            BusinessClass.Add(bunifuCheckbox52);
            BusinessClass.Add(bunifuCheckbox53);
            BusinessClass.Add(bunifuCheckbox54);
            BusinessClass.Add(bunifuCheckbox55);
            BusinessClass.Add(bunifuCheckbox56);
            BusinessClass.Add(bunifuCheckbox57);
            BusinessClass.Add(bunifuCheckbox58);
            BusinessClass.Add(bunifuCheckbox59);
            BusinessClass.Add(bunifuCheckbox60);

            BusinessClass.Add(bunifuCheckbox61);
            BusinessClass.Add(bunifuCheckbox62);

            seatsettings = searvice.getBusSeat(planeID);

            int i = 0;
            foreach (bool a in seatsettings)
            {
                if (a == false)
                {
                    BusinessClass[i].CheckedOnColor = Color.Red;
                    BusinessClass[i].Enabled = false;

                }
                else
                    BusinessClass[i].Checked = a;
                i++;
            }

        }


        // loding economics seat represntation from panel and database
        private void loadEcoSeat(string planeID)
        {
            economyClass = new List<BunifuCheckbox>();
            economyClass.Add(bunifuCheckbox1);
            economyClass.Add(bunifuCheckbox2);
            economyClass.Add(bunifuCheckbox3);
            economyClass.Add(bunifuCheckbox4);
            economyClass.Add(bunifuCheckbox5);
            economyClass.Add(bunifuCheckbox6);
            economyClass.Add(bunifuCheckbox7);
            economyClass.Add(bunifuCheckbox8);
            economyClass.Add(bunifuCheckbox9);
            economyClass.Add(bunifuCheckbox10);

            economyClass.Add(bunifuCheckbox11);
            economyClass.Add(bunifuCheckbox12);
            economyClass.Add(bunifuCheckbox13);
            economyClass.Add(bunifuCheckbox14);
            economyClass.Add(bunifuCheckbox15);
            economyClass.Add(bunifuCheckbox16);
            economyClass.Add(bunifuCheckbox17);
            economyClass.Add(bunifuCheckbox18);
            economyClass.Add(bunifuCheckbox19);
            economyClass.Add(bunifuCheckbox20);

            economyClass.Add(bunifuCheckbox21);
            economyClass.Add(bunifuCheckbox22);
            economyClass.Add(bunifuCheckbox23);
            economyClass.Add(bunifuCheckbox24);
            economyClass.Add(bunifuCheckbox25);
            economyClass.Add(bunifuCheckbox26);
            economyClass.Add(bunifuCheckbox27);
            economyClass.Add(bunifuCheckbox28);
            economyClass.Add(bunifuCheckbox29);
            economyClass.Add(bunifuCheckbox30);

            economyClass.Add(bunifuCheckbox31);
            economyClass.Add(bunifuCheckbox32);
            economyClass.Add(bunifuCheckbox33);
            economyClass.Add(bunifuCheckbox34);
            economyClass.Add(bunifuCheckbox35);
            economyClass.Add(bunifuCheckbox36);
            economyClass.Add(bunifuCheckbox37);
            economyClass.Add(bunifuCheckbox38);
            economyClass.Add(bunifuCheckbox39);
            economyClass.Add(bunifuCheckbox40);

            economyClass.Add(bunifuCheckbox41);
            economyClass.Add(bunifuCheckbox42);

            seatsettings = searvice.getEcoSeat(planeID);

            int i = 0;
            foreach (bool a in seatsettings)
            {
                if (a == false)
                {
                    economyClass[i].CheckedOnColor = Color.Red;
                    economyClass[i].Enabled = false;
                }
                else
                    economyClass[i].Checked = a;
                i++;
            }
        }


        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {

            //getting id of selecting checkbox
            if (sClas == SeatClass.Business)
            {
                getBusTicketNumber();
            }
            else if (sClas == SeatClass.Economics)
            {
                getEcoTicketNumber();
            }
            else if (sClas == SeatClass.Delux)
            {
                getDelTicketNumber();
            }

            //check exact seat been selected or not
            if (countTicketSelection == checkboxSelected)
            {
                //code for oneway passenger
                if (tstate == TicketState.OneWay)
                {
                    getPrice();
                    bunifuGradientPanel1.Visible = false;
                    bunifuGradientPanel2.Visible = false;
                    bunifuGradientPanel3.Visible = false;
                    panel1.Visible = true;
                }
                //code for return and multicity passenger
                else if (tstate == TicketState.Return)
                {
                    loadSeat(nextPlane.planeID);
                    tstate = TicketState.Done;
                    countTicketSelection = 0;
                    this.Invalidate();
                    this.Refresh();
                    this.Update();
                    MessageBox.Show("destination plane seat selected . \n Select next plane seat");
                }
                else
                {
                    getPrice();
                    bunifuGradientPanel1.Visible = false;
                    bunifuGradientPanel2.Visible = false;
                    bunifuGradientPanel3.Visible = false;
                    panel1.Visible = true;
                }
            }
            else
            {
                //if more or less check box being selected 
                MessageBox.Show("selected " + countTicketSelection + " seat Insted of selecting " + checkboxSelected + " seat");
                countTicketSelection = 0;
                ticketNumber.Clear();
            }
        }

        private void getEcoTicketNumber()
        {
            int count = 0;
            char ch = 'A';
            foreach (BunifuCheckbox c in this.economyClass)
            {

                if (count == 7)
                {
                    ch = (char)(ch + 1);
                    count = 0;
                }
                count++;

                //getting seat Number according to first name of classs and seat number
                if (c.Checked == false)
                {
                    ticketNumber.Add("E" + ch + count);
                    countTicketSelection++;
                }
            }

        }

        private void getBusTicketNumber()
        {
            int count = 0;
            char ch = 'A';
            foreach (BunifuCheckbox c in this.BusinessClass)
            {

                if (count == 5)
                {
                    ch = (char)(ch + 1);
                    count = 0;
                }
                count++;
                
                if (c.Checked == false)
                {
                    ticketNumber.Add("B" + ch + count);
                    countTicketSelection++;
                }
            }

        }

        private void getDelTicketNumber()
        {
            int count = 0;
            char ch = 'A';
            foreach (BunifuCheckbox c in this.deluxClass)
            {

                if (count == 4)
                {
                    ch = (char)(ch + 1);
                    count = 0;
                }
                count++;

                //incrementing count if checkbox selected
                if (c.Checked == false)
                {
                    ticketNumber.Add("B" + ch + count);
                    countTicketSelection++;
                }
            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string name = bunifuMaterialTextbox1.Text;
            string passport = bunifuMaterialTextbox2.Text;
            string email = bunifuMaterialTextbox3.Text;
            string mobile = bunifuMaterialTextbox4.Text;
            
            if(tstate == TicketState.OneWay)
            {

                searvice.getTicketIdAndBookTicket(destinationPlane, name, passport, email, mobile,ticketNumber);
                MessageBox.Show("Ticket purchased");
            }
            if (tstate == TicketState.Done)
            {
                List<string> destinationTicket = new List<string>();
                List<string> nextdestinationTicket = new List<string>();

                for(int i=0;i< checkboxSelected;i++)
                {
                    destinationTicket.Add(ticketNumber[i]);
                }
                

                for (int i = 0; i < checkboxSelected; i++)
                {
                    nextdestinationTicket.Add(ticketNumber[checkboxSelected+i]);
                }

                searvice.getTicketIdAndBookTicket(destinationPlane, name, passport, email, mobile, destinationTicket);
                MessageBox.Show("1st Ticket purchased");
                searvice.getTicketIdAndBookTicket(nextPlane, name, passport, email, mobile, nextdestinationTicket);
                MessageBox.Show("2nd Ticket purchased");
            }
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Close();

        }

        private void getPrice()
        {
            totalPrice = searvice.getTicketPrice(destinationPlane);
            if (tstate == TicketState.Done)
            {
                totalPrice += searvice.getTicketPrice(nextPlane);
            }
            label6.Text += totalPrice.ToString()+" BDT";
        }
    }
}