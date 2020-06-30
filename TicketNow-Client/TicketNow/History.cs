using System;
namespace TicketNow
{
    public class History
    {

        public string date;
        public string lunch;
        public string dinner;

        public History()
        {
        }

        public History(string date, string lunch, string d)
        {
            this.date = date;
            this.lunch = lunch;
            this.dinner = d;
        }
        
    }
}
