using TicketTrackingSystem.Models;
using Microsoft.Data.SQLite;
using System.Data.SQLite;

namespace TicketTrackingSystem.Repository;

public interface Repository{
    public virtual List<TicketsViewModel> GetTickets() => return null;
    public virtual void CreateTicket(TicketsViewModel ticket);
    public virtual void EditTicket(TicketsViewModel ticket);
    public virtual void DeleteTicket();
    public virtual void ResolveTicket(int ID);
}

public class RepositoryImpl : Repository{
    private const string ConnectionString = @"Data Source=/.TicketTracking.db;";
    public override virtual List<TicketsViewModel> GetTickets()
    {
        using (var conn = new SQLiteConnection()); 
        using (var cmd = conn.CreateCommand())
        {
            conn.Open();
            cmd.CommandText = "SELECT * FROM Tickets";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string myreader = reader.GetString(0);
                    Console.WriteLine(myreader);
                }
            }
        }    
    }

    public override void CreateTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            using (var conn = new SQLiteConnection()); 
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ({ticket.Ticket_ID}, {ticket.Summary}, {ticket.Description}, {ticket.Ticket_type}, {ticket.Serverity}, {ticket.Priority});";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string myreader = reader.GetString(0);
                        Console.WriteLine(myreader);
                    }
                }
            }    
        }
    }

    public override void EditTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            using (var conn = new SQLiteConnection()); 
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ({ticket.Ticket_ID}, {ticket.Summary}, {ticket.Description}, {ticket.Ticket_type}, {ticket.Serverity}, {ticket.Priority});";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string myreader = reader.GetString(0);
                        Console.WriteLine(myreader);
                    }
                }
            }    
        }
    }

    public override void DeleteTicket(int ID)
    {
        if(ID == 0) Console.WriteLine("The Delete procedure has failed.");
        else{
            using (var conn = new SQLiteConnection()); 
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"DELETE from Tickets where Ticket_ID = '{ticket.Ticket_ID}';";
                using (var reader = cmd.Execute())
                {
                    while (reader.Read())
                    {
                        string myreader = reader.GetString(0);
                        Console.WriteLine(myreader);
                    }
                }
            }    
        }
    }
}