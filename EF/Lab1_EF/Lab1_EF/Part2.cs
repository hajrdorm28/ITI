using System;
using System.Collections.Generic;
using System.Text;


namespace Lab1_EF_P2
{
    public class FlightTicket
    {
        public int TicketId { get; set; }
        public string PassengerName { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsDelayed { get; set; }

        public override string ToString()
        {
            return $"[Ticket #{TicketId}] {PassengerName} | Flight {FlightNumber} | " +
                   $"${Price} | Delayed: {IsDelayed}";
        }
    }

    class Program
    {
        static void Notification(FlightTicket ticket, Action<FlightTicket> notificationAction)
        {
            notificationAction(ticket);
        }

        static List<FlightTicket> FilterTickets(List<FlightTicket> tickets, Predicate<FlightTicket> condition)
        {
            List<FlightTicket> result = new List<FlightTicket>();

            foreach (var ticket in tickets)
            {
                if (condition(ticket))
                {
                    result.Add(ticket);
                }
            }

            return result;
        }

        static void ProcessTickets(List<FlightTicket> tickets, Func<FlightTicket, decimal> discountCalculator)
        {
            foreach (var ticket in tickets)
            {
                decimal discountedPrice = discountCalculator(ticket);
                Console.WriteLine($"Processed Ticket #{ticket.TicketId} - {ticket.PassengerName}: " +
                                   $"Original ${ticket.Price} -> Discounted ${discountedPrice}");
            }
        }

        static List<string> GetTicketSummaries(List<FlightTicket> tickets, Func<FlightTicket, string> formatter)
        {
            List<string> summaries = new List<string>();

            foreach (var ticket in tickets)
            {
                summaries.Add(formatter(ticket));
            }

            return summaries;
        }

        static void Main(string[] args)
        {
            List<FlightTicket> tickets = new List<FlightTicket>
            {
                new FlightTicket { TicketId = 1, PassengerName = "Jana Ahmed", FlightNumber = "AV101", Price = 250m, IsDelayed = false },
                new FlightTicket { TicketId = 2, PassengerName = "Bary Salem", FlightNumber = "AV202", Price = 400m, IsDelayed = true  },
                new FlightTicket { TicketId = 3, PassengerName = "Sara Saeed", FlightNumber = "AV303", Price = 180m, IsDelayed = true  },
                new FlightTicket { TicketId = 4, PassengerName = "Tarik Shaaban", FlightNumber = "AV404", Price = 320m, IsDelayed = false }
            };

            Console.WriteLine("Notifications");
            foreach (var ticket in tickets)
            {
                Notification(ticket, t => Console.WriteLine($"[System Log] Processing ticket for {t.PassengerName}."));
            }

            Console.WriteLine("Filteration");
            List<FlightTicket> delayedFlights = FilterTickets(tickets, t => t.IsDelayed);

            foreach (var ticket in delayedFlights)
            {
                Console.WriteLine(ticket);
            }

            Console.WriteLine("Discount Processing");
            ProcessTickets(tickets, t => t.IsDelayed ? t.Price * 0.80m : t.Price * 0.95m);

            Console.WriteLine("Ticket Summaries");
            List<string> summaries = GetTicketSummaries(tickets, t => $"{t.TicketId} - {t.PassengerName}");

            foreach (var summary in summaries)
            {
                Console.WriteLine(summary);
            }
        }
    }

}
