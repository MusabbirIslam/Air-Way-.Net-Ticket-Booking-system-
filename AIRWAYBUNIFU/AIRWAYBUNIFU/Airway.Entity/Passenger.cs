using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airway.Entity
{
    public enum TicketState { OneWay, Return, Done};
    sealed public class Passenger
    {
        private string name;
        private string contactNo;
        private string passportNo;
        private string email;
        private string ticketID;
        private byte seatBooked;
        private DateTime DepartingDate;
        private string Starting;
        private string destination;

        //property
        public string Name { get { return name; } }
        public string ContactNo { get { return contactNo; } }
        public string PassportNo { get { return passportNo; } }
        public string Email { get { return email; } }
        public string TicketID { get { return ticketID; } }
        public byte SeatBooked { get { return seatBooked; } }
        public DateTime Departing_Date { get { return DepartingDate; }}
        public string Strating_From { get { return Starting; } }
        public string Destination { get { return destination; } }

        public Passenger(string name, string contactNo,
            string passportNo, string email, string ticketID, byte seatBooked,DateTime DepartingDate,string Starting,string destination)
        {
            this.name = name;
            this.contactNo = contactNo;
            this.passportNo = passportNo;
            this.email = email;
            this.ticketID = ticketID;
            this.seatBooked = seatBooked;
            this.DepartingDate= DepartingDate;
            this.Starting= Starting;
            this.destination= destination;
    }
    }
}
