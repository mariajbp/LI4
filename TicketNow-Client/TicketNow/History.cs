using System;
namespace TicketNow
{
    public class History
    {

        public DateTime date;
        public string lunch;
        public string dinner;

        public History()
        {
        }

        public History(DateTime date, string lunch, string d)
        {
            this.date = date;
            this.lunch = lunch;
            this.dinner = d;
        }
        
    }
}
