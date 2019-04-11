using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airway.Entity;
namespace Airway.Core
{
    internal interface AdminInterface
    {
        void createSchedule(string planeName, string destination, string start,
            DateTime departingDate, float BusinessTicketPrice, float EconomicTicketPrice,
            float DeluxTicketPrice);

        void updateSchedule(string input, string val, string planeID);
        List<Passenger> getPassengerList(string planeId);
        void cancelTicket(String ticketId);
    }
}
