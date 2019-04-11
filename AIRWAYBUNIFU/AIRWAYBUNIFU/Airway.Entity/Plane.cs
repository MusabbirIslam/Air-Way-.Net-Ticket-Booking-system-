using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airway.Entity
{
    public enum SeatClass { Economics,Business,Delux };
    sealed public class Plane
    {
        public string planeName;
        public string planeID;
        public string destination;
        public string start;
        public DateTime departingDate;
        public byte CountBusinessClass;
        public byte CountEconomicClass;
        public byte CountDeluxClass;
        public byte maximumBusinessClass;
        public byte maximumEconomicClass;
        public byte maximumDeluxClass;
        public double BusinessTicketPrice;
        public double EconomicTicketPrice;
        public double DeluxTicketPrice;

        //property


        public string Plane_Name { get { return planeName; } }
        public string Destination { get { return destination; } }
        public string Start { get { return start; } }
        public string Date { get { DateTime date = departingDate.Date; return date.ToString("d"); } }
        public string Time { get { return departingDate.TimeOfDay.ToString(); } }


        public Plane(string planeID, string planeName, string destination, string start, DateTime departingDate,
            byte maximumBusinessClass, byte maximumEconomicClass, byte maximumDeluxClass,
            double BusinessTicketPrice, double EconomicTicketPrice,
            double DeluxTicketPrice, byte CountBusinessClass = 0,
            byte CountEconomicClass = 0, byte CountDeluxClass = 0)
        {
            this.planeID = planeID;
            this.planeName = planeName;
            this.destination = destination;
            this.start = start;
            this.departingDate = departingDate;
            this.maximumBusinessClass = maximumBusinessClass;
            this.maximumEconomicClass = maximumEconomicClass;
            this.maximumDeluxClass = maximumDeluxClass;
            this.BusinessTicketPrice = BusinessTicketPrice;
            this.EconomicTicketPrice = EconomicTicketPrice;
            this.DeluxTicketPrice = DeluxTicketPrice;
            this.CountBusinessClass = CountBusinessClass;
            this.CountEconomicClass = CountEconomicClass;
            this.CountDeluxClass = CountDeluxClass;
        }



    }
}
