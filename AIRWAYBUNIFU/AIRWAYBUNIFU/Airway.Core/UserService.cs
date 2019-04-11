using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airway.Entity;
using Airway.Data;
using System.Windows.Forms;

namespace Airway.Core
{
    public class UserService : UserServiceInterface
    {
        DataBaseConnection database;
        Byte adult;
        Byte child;
        Byte infant;
        Byte seatNumber;
        static Byte totalSeat;
        static SeatClass seatClass;
        Ticket printTicket;
        public Byte TotalSeat{ get {return totalSeat; }}
        public SeatClass SeatClassProp { get { return seatClass; } }

        String sClas;

        public UserService()
        {
            this.database = new DataBaseConnection();
            this.adult = 0;
            this.child = 0;
            this.infant = 0;
            totalSeat = 0;
            sClas = "Business";
        }

        //check one way going avaiable plane
        public List<Plane> checkOneway(string from, string to, DateTime date, byte seat)
        {
            string departingDate = date.Date.ToString("MM/dd/yyyy");
            string quary = "select * from Flight where destination='" + to +
                "' and start='" + from + "' and  departingDate between  '" + departingDate +
                " 12:00:00 AM' and '" + departingDate + " 11:59:59 PM' and Count" + sClas
                + "Class + " + seatNumber + " < Maximum" + sClas + "Class";
            return database.getPlane(quary);

        }


        //check multicity going avaiable plane

        public List<Plane> checkMultyCity(out List<Plane> nextTravel, string from1, string to1, DateTime date1, string from2, string to2, DateTime date2, byte seat)
        {
            nextTravel = checkOneway(from2, to2, date2, seat);
            return checkOneway(from1, to1, date1, seat);
        }

        //check avaiable plane with return ticket

        public List<Plane> checkReturn(out List<Plane> returnList, string from, string to, DateTime traveldate, DateTime retunDate, byte seat)
        {
            returnList = checkOneway(to, from, retunDate, seat);
            return checkOneway(from, to, traveldate, seat);
        }

        //set passenger seat according to adult , child infant set class of seat 
        public void setPassengers(Byte adult, Byte child, Byte infant, Byte seat, String clas)
        {
            this.adult = adult;
            this.child = child;
            this.infant = infant;
            totalSeat = seat;
            this.sClas = clas;
            if (clas == "Business")
                seatClass = SeatClass.Business;
            else if (clas == "Economic")
                seatClass = SeatClass.Economics;
            else
                seatClass = SeatClass.Delux;
        }

        //return ticket price according to class and passenger number
        public double getTicketPrice(Plane selected)
        {
            double price = 0;
            double totalPrice = 0;
            if (seatClass == SeatClass.Business)
            {
                price = selected.BusinessTicketPrice;
                seatNumber = selected.CountBusinessClass;
            }
            else if (seatClass == SeatClass.Economics)
            {
                price = selected.EconomicTicketPrice;
                seatNumber = selected.CountEconomicClass;
            }
            else
            {
                price = selected.DeluxTicketPrice;
                seatNumber = selected.CountDeluxClass;
            }
            totalPrice = (adult * price) + (child * price * .5) + (infant * price * .75);
            return totalPrice;
        }

        //generate ticket Id in format of 
        // Plane Name-MONTH-DAY-last two digit of YEAR-class name -seat no - seat no
        //example BangladeshBiman-12-26-16-Bus-3-5

        public void getTicketIdAndBookTicket(Plane destinationPlane, string name,
            string passportNo, string email, string mobile,List<string> ticketNumber)
        {
            //plane id format = plane naame-month-day-year
            string planeID = (destinationPlane.planeName) + "-"
                              + (destinationPlane.departingDate.Date.ToString("MM-dd-yy"));

            //ticket formate = planeId-Month-day-year-seat class-seat number1-seat number2-....
            string ticket = ticketNumber[0]+"='false' ";
            string ticketId = planeID + "-"+ticketNumber[0];
            for (int i=1;i<totalSeat;i++)
            {
                ticket += "," + ticketNumber[i] + "='false' ";
                ticketId += "-" + ticketNumber[i];
            }

            //booking ticket
            buyTicket(planeID, name, mobile, passportNo, email, ticket, ticketId,
                destinationPlane.departingDate, totalSeat,
                destinationPlane.Start, destinationPlane.Destination);

        }

        //booke ticket and add passenger to the plane list
        private void buyTicket(string planeID, string name, string contactNo,
            string passportNo, string email, string ticket, string ticketId,
            DateTime departingDate, byte seatBooked, string from, string to)
        {


            //inserting passenger information in passenger table
            string quaryForPassenger = "insert into PassengerTable (planeid,ticketID,name,contact," +
                "passport,email,seatBooked,DepartingDate,Starting,Destination) Values ('" + planeID + "','"+ ticketId + "',"
                +"'" + name + "','" + contactNo + "','"+ passportNo + "','" + email + "'," + seatBooked + ",'"+ departingDate+"','"+from+"','"+to+"') ";
            database.executeQuarry(quaryForPassenger);

            //updating ticket on flight table
            string quaryforTicket = "update flight set Count" + sClas + "Class = " +
                "Count" + sClas + "Class +" + totalSeat + " WHERE Planeid='" + planeID + "'";
            database.executeQuarry(quaryforTicket);

            //updating plane seat available to booked
            string planeSeatQuary = "Update planeseat set " + ticket + "where planeid ='" + planeID + "' ";
            database.executeQuarry(planeSeatQuary);

            //print ticket
            printTicket = new Ticket(ticketId,departingDate,from,to,contactNo,name);
            printTicket.print();
        }

        public AutoCompleteStringCollection placName(string a)
        {
            return database.getName("select "+a+" from Flight");
        }


        public List<bool> getBusSeat(string planeID)
        {
            string quary = "select BA1";
            char ch = 'A';
            int count = 1;
            while (count < 6 && ch < 'E')
            {
                count++;
                quary += ",B" + ch + count;
                if (count == 5)
                {
                    ch = (char)(ch + 1);
                    count = 0;
                }
            }

            quary += " from PlaneSeat where planeid = '" + planeID + "'";
            return database.getSeatData(quary, 5, 'E', 'B');
        }

        public List<bool> getEcoSeat(string planeID)
        {
            string quary = "select EA1";
            char ch = 'A';
            int count = 1;
            while (count < 8 && ch < 'G')
            {
                count++;
                quary += ",E" + ch + count;
                if (count == 7)
                {
                    ch = (char)(ch + 1);
                    count = 0;
                }
            }

            quary += " from PlaneSeat where planeid = '" + planeID + "'";

            return database.getSeatData(quary, 7, 'G', 'E');
        }

        public List<bool> getDelSeat(string planeID)
        {
            string quary = "select DA1";
            char ch = 'A';
            int count = 1;
            while (count < 5 && ch < 'D')
            {
                count++;
                quary += ",D" + ch + count;
                if (count == 4)
                {
                    ch = (char)(ch + 1);
                    count = 0;
                }
            }

            quary += " from PlaneSeat where planeid = '" + planeID + "'";

            return database.getSeatData(quary, 4, 'D', 'D');
        }
    }
}
