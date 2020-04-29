using System;
namespace TicketNow
{
    public class Ticket
    {

        public string id_ticket;
        public string id_user;
        public int type; // 1:simple meal ; 2:complete meal
        public bool used; 

        public Ticket()
        {
        }
    }
}
