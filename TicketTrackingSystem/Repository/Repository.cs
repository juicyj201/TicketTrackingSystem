using TicketTrackingSystem.Models;
using System.Data.SQLite;
using System.Configuration;
using System.Text;

namespace TicketTrackingSystem.Repository;

public class Repository{
    public virtual List<TicketsViewModel> GetTickets() => new();
    public virtual string CreateTicket(TicketsViewModel ticket) => String.Empty;
    public virtual string EditTicket(TicketsViewModel ticket) => String.Empty;
    public virtual string DeleteTicket(int ID) => String.Empty;
    public virtual string ResolveTicket(int ID) => String.Empty;
}

public class RepositoryImpl : Repository
{
    private const string ConnectionString = @"Data Source=.\TicketTracking.db;Version=3;";

    public override List<TicketsViewModel> GetTickets()
    {
        List<TicketsViewModel> list = new();

        using (var conn = new SQLiteConnection(ConnectionString)) 
        using (var cmd = conn.CreateCommand())
        {
            conn.Open();
            cmd.CommandText = "SELECT * FROM Tickets";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var ticket = new TicketsViewModel();
                    ticket.Ticket_ID = Convert.ToInt32(reader["Ticket_ID"]);
                    ticket.Summary = reader["Summary"].ToString();
                    ticket.Description = reader["Description"].ToString();
                    ticket.Ticket_Type = reader["Ticket_Type"].ToString();
                    ticket.Severity = reader["Severity"].ToString();
                    ticket.Priority = reader["Priority"].ToString();
                    ticket.Status = reader["Status"].ToString();
                    list.Add(ticket);
                }
            }
        }    

        return list;
    }

    public override string CreateTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ({ticket.Ticket_ID}, {ticket.Summary}, {ticket.Description}, {ticket.Ticket_Type}, {ticket.Severity}, {ticket.Priority}, {ticket.Status});";
                
                return GetResult(cmd.ExecuteNonQuery(), ticket.Ticket_ID, "updated");
            }    
        }

        return String.Empty;
    }

    public override string EditTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ({ticket.Ticket_ID}, {ticket.Summary}, {ticket.Description}, {ticket.Ticket_Type}, {ticket.Severity}, {ticket.Priority}, {ticket.Status});";
                
                return GetResult(cmd.ExecuteNonQuery(), ticket.Ticket_ID, "updated");
            }    
        }

        return String.Empty;
    }

    public override string DeleteTicket(int ID)
    {
        if(ID == 0) Console.WriteLine("The Delete procedure has failed.");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"DELETE from Tickets where Ticket_ID = '{ID}';";
                
                return GetResult(cmd.ExecuteNonQuery(), ID, "deleted");
            }    
        }

        return String.Empty;
    }

    public override string ResolveTicket(int ID){
        if(ID == 0) Console.WriteLine("The ticket has not been resolved");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand()){
                conn.Open();
                cmd.CommandText = $"UPDATE Tickets SET Status = 'Closed' WHERE Ticket_ID = '{ID}';";
                
                return GetResult(cmd.ExecuteNonQuery(), ID, "resolved"); 
            }
        }

        return String.Empty;
    }

    internal string GetResult(int result, int ticketId, string action)
    {
        StringBuilder builder = new();
        switch(result)
        {
            case 0: 
                Console.WriteLine("Nothing was changed");
                return "Nothing was changed";
            case 1:
                builder.Append("The ticket: ");
                builder.Append(ticketId);
                builder.Append(" has been ");
                builder.Append(action);
                Console.WriteLine(builder.ToString());
                return builder.ToString();
            default:
                Console.WriteLine("An error occured");
                return "An error occured";
        }

        return String.Empty;
    }
}