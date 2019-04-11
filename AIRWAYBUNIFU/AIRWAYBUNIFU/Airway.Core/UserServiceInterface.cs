using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airway.Entity;
namespace Airway.Core
{
    internal interface UserServiceInterface
    {
        List<Plane> checkOneway(String from, String to, DateTime date, byte seat);
        List<Plane> checkReturn(out List<Plane> returnList, String from, String to, DateTime traveldate, DateTime retunDate, byte seat);
        List<Plane> checkMultyCity(out List<Plane> returnList, String from1, String to1, DateTime date1, String from2, String to2, DateTime date2, byte seat);
        void setPassengers(Byte adult, Byte child, Byte infant, Byte seat, string seatClass);
        double getTicketPrice(Plane selected);
        void getTicketIdAndBookTicket(Plane destinationPlane, string name,
            string passportNo, string email, string mobile, List<string> ticketNumber);
    }
}
