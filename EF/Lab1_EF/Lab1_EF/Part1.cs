using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_EF_P1
{
    public class FlightTicket
    {
        public int TicketId { get; set; }
        public string PassengerName { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime FlightTime { get; set; }
        public bool IsDelayed { get; set; }

        public FlightTicket(int ticketId, string passengerName, decimal basePrice, DateTime flightTime, bool isDelayed)
        {
            TicketId = ticketId;
            PassengerName = passengerName;
            BasePrice = basePrice;
            FlightTime = flightTime;
            IsDelayed = isDelayed;
        }
    }

    public delegate decimal DiscountCalculator(FlightTicket ticket);

    public class DiscountRules
    {
        public static decimal DelayedFlightDiscount(FlightTicket ticket)
        {
            if (ticket.IsDelayed)
                return ticket.BasePrice * 0.80m;

            return ticket.BasePrice;
        }

        public decimal VipPassengerDiscount(FlightTicket ticket)
        {
            return ticket.BasePrice * 0.85m;
        }
    }

    class Program
    {
        public static void ProcessTickets(List<FlightTicket> tickets, DiscountCalculator calculator)
        {
            foreach (var ticket in tickets)
            {
                decimal finalPrice = calculator.Invoke(ticket);
                Console.WriteLine(
                    $"Passenger: {ticket.PassengerName,-12} | Original: {ticket.BasePrice,8:C} | Final: {finalPrice,8:C}");
            }
        }

        static void Main(string[] args)
        {
            List<FlightTicket> tickets = new List<FlightTicket>
            {
                new FlightTicket(1, "Jana Ahmed", 1440.00m, new DateTime(2026, 8, 1, 9, 30, 0), false),
                new FlightTicket(2, "Bary Salem", 1200.00m, new DateTime(2026, 8, 1, 14, 0, 0), true),
                new FlightTicket(3, "Sameh Hamdy", 1780.50m, new DateTime(2026, 8, 2, 7, 45, 0), true),
                new FlightTicket(4, "Tarik Ali", 1500.00m, new DateTime(2026, 8, 2, 18, 15, 0), true),
                new FlightTicket(5, "Sara Saeed", 1320.75m, new DateTime(2026, 8, 3, 11, 0, 0), false),
            };

            Console.WriteLine("Named Methods");

            Console.WriteLine("Delayed Flight Discount");
            DiscountCalculator delayedDiscount = DiscountRules.DelayedFlightDiscount;
            ProcessTickets(tickets, delayedDiscount);

            Console.WriteLine("VIP Passenger Discount");
            DiscountRules rules = new DiscountRules();
            DiscountCalculator vipDiscount = rules.VipPassengerDiscount;
            ProcessTickets(tickets, vipDiscount);

            Console.WriteLine("Anonymous Method");
            ProcessTickets(tickets, delegate (FlightTicket t)
            {
                return t.BasePrice * 0.90m;
            });

            Console.WriteLine("Lambda Expressions");

            Console.WriteLine("Morning Flight Discount");
            ProcessTickets(tickets, t =>
                t.FlightTime.Hour < 12 ? t.BasePrice * 0.95m : t.BasePrice);

            Console.WriteLine("Premium Ticket Discount");
            ProcessTickets(tickets, t =>
                t.BasePrice > 1000m ? t.BasePrice - 50m : t.BasePrice);

            Console.WriteLine("Multicast Delegate Notifications");
            RunNotification(tickets[0]);
        }


        public delegate void NotificationSender(FlightTicket ticket);

        public static void SendEmail(FlightTicket ticket)
        {
            Console.WriteLine($"[Email] Dear {ticket.PassengerName}, your ticket #{ticket.TicketId} is confirmed.");
        }

        public static void SendSMS(FlightTicket ticket)
        {
            Console.WriteLine($"[SMS] Hi {ticket.PassengerName}, flight update for ticket #{ticket.TicketId}.");
        }

        public static void RunNotification(FlightTicket ticket)
        {
            NotificationSender notifier = SendEmail;
            notifier += SendSMS;

            notifier.Invoke(ticket);
        }
    }

}