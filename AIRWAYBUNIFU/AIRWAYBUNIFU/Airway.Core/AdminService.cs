using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Airway.Data;
using Airway.Entity;
namespace Airway.Core
{
    public class AdminService : AdminInterface
    {
        private DataBaseConnection database;

        public AdminService()
        {
            database = new DataBaseConnection();
        }
        public void cancelTicket(string ticketId)
        {
            try
            {
                string[] seatNumber = ticketId.Split('-');
                int c = seatNumber.Length;

                //getting plane id from the ticket
                string planeId = seatNumber[0] + "-" + seatNumber[1] + "-" + seatNumber[2] + "-" + seatNumber[3];

                //getting the seat column name and setting them to true for quary
                string seatUpdate = seatNumber[4] + "='true' ";
                int i = 5;
                for (; i < c; i++)
                {
                    seatUpdate += "," + seatNumber[i] + "='true' ";
                }
                // quary for updating planeseat booked to available
                string quaryForUpdateSeat = "Update planeseat set " + seatUpdate + "where planeid='" + planeId + "' ";

                // quary for deleting passenger
                string quaryForDeletingPassenger = "delete from passengerTable where ticketid='" + ticketId + "'";

                // quary for update flight counting seat
                string quaryForLessingSeatCount = "update flight set ";
                if (seatNumber[4].StartsWith("E"))
                {
                    quaryForLessingSeatCount += "CountEconomicClass = CountEconomicClass -";
                }
                else if (seatNumber[4].StartsWith("B"))
                {
                    quaryForLessingSeatCount += "CountBusinessClass = CountBusinessClass -";
                }
                else
                {
                    quaryForLessingSeatCount += "CountDeluxClass = CountDeluxClass -";
                }


                int seat = i - 4;
                quaryForLessingSeatCount += seat + " where planeid='" + planeId + "' ";

                //executing quary for lessing the count from flight
                database.executeQuarry(quaryForLessingSeatCount);
                //executing quary for lessing the count from flight
                database.executeQuarry(quaryForUpdateSeat);
                //executing quary for lessing the count from flight
                database.executeQuarry(quaryForDeletingPassenger);
                MessageBox.Show(" Ticket Succefully deleted ");
            }
            catch(Exception ex) { MessageBox.Show("Invalid ticket ID"); }
        }

        //create a new schedule in flight table and a new table with plane name and date
        public void createSchedule(string planeName, string destination, string start,
            DateTime departingDate, float BusinessTicketPrice, float EconomicTicketPrice,
            float DeluxTicketPrice)
        {

            byte maximumBusinessClass = 20;
            byte maximumEconomicClass = 42;
            byte maximumDeluxClas = 12;
            byte CountBusinessClass = 0;
            byte CountEconomicClass = 0;
            byte CountDeluxClass = 0;
            string Planeid = (planeName) + "-"
                             + (departingDate.Date.ToString("MM-dd-yy"));
            //inserting new plane shadule in flight
            string quary = "insert into Flight (Planeid, PlaneName,destination,departingDate,CountBusinessClass,CountEconomicClass,CountDeluxClass,maximumBusinessClass,maximumEconomicClass,maximumDeluxClass,BusinessTicketPrice,EconomicTicketPrice,DeluxTicketPrice,start) Values (" +
                "'" + Planeid + "','" + planeName + "','" + destination + "','" + departingDate +
                "'," + CountBusinessClass + "," + CountEconomicClass + "," + CountDeluxClass +
                "," + maximumBusinessClass + "," + maximumEconomicClass + "," +
                maximumDeluxClas + "," + BusinessTicketPrice + "," +
                EconomicTicketPrice + "," + DeluxTicketPrice + ",'" + start + "') ";

            database.executeQuarry(quary);

            string quaryForPlaneSeat= " insert into PLANESEAT (PlaneId,EA1,EA2,EA3,EA4,EA5,EA6,EA7,EB1,EB2,EB3,EB4,EB5,EB6,EB7,EC1,EC2,EC3,EC4,EC5,EC6,EC7,ED1,ED2,ED3,ED4,ED5,ED6,ED7,EE1,EE2,EE3,EE4,EE5,EE6,EE7,EF1,EF2,EF3,EF4,EF5,EF6,EF7,BA1,BA2,BA3,BA4,BA5,BB1,BB2,BB3,BB4,BB5,BC1,BC2,BC3,BC4,BC5,BD1,BD2,BD3,BD4,BD5,DA1,DA2,DA3,DA4,DB1,DB2,DB3,DB4,DC1,DC2,DC3,DC4) Values('" + Planeid+ "','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE')";
            database.executeQuarry(quaryForPlaneSeat);
        }

        public List<Passenger> getPassengerList(string planeId)
        {
            return database.getPassenger("select * from PassengerTable where planeid='"+ planeId+"' ");
        }

        public List<Plane> getPlane()
        {
            return database.getPlane("select * from flight");
        }
        public AutoCompleteStringCollection placName()
        {
            return database.getName("select Planeid from Flight");
        }
        public void updateSchedule(string input, string val, string planeID)
        {
            string quary;
            if (input.Equals("maximumBusinessClass") || input.Equals("maximumEconomicClass") || input.Equals("maximumDeluxClass") ||
                    input.Equals("CountBusinessClass") || input.Equals("CountEconomicClass") || input.Equals("CountDeluxClass"))
            {
                Byte value = Byte.Parse(val);
                quary="Update flight set " + input + " = " + value + " where planeid = '" + planeID + "' ";
            }
            else if (input.Equals("DeluxTicketPrice") || input.Equals("EconomicTicketPrice") || input.Equals("BusinessTicketPrice"))
            {
                float value = float.Parse(val);
                quary = "Update flight set " + input + " = " + value + " where planeid = '" + planeID + "' ";
            }
            else
            {
                quary = "Update flight set " + input + " = '" + val + "' where planeid = '" + planeID + "' ";

            }
            database.executeQuarry(quary);
        }
    }
}
