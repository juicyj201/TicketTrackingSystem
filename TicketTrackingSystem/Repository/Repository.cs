using TicketTrackingSystem.Models;
using System.Data.SQLite;
using System.Configuration;

namespace TicketTrackingSystem.Repository;

public class Repository{
    public virtual List<TicketsViewModel> GetTickets(){ return new();}
    public virtual void CreateTicket(TicketsViewModel ticket){}
    public virtual void EditTicket(TicketsViewModel ticket){}
    public virtual void DeleteTicket(int ID){}
    public virtual void ResolveTicket(int ID){}
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

    public override void CreateTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ({ticket.Ticket_ID}, {ticket.Summary}, {ticket.Description}, {ticket.Ticket_Type}, {ticket.Severity}, {ticket.Priority}, {ticket.Status});";
                
                switch(cmd.ExecuteNonQuery()){
                    case 0: 
                        Console.WriteLine("Nothing was changed");
                        break;
                    case 1:
                        Console.WriteLine($"The ticket: {ticket.Ticket_ID} has been added");
                        break;        
                    default:
                        Console.WriteLine("An error occured");
                        break;     
                }
            }    
        }
    }

    public override void EditTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ({ticket.Ticket_ID}, {ticket.Summary}, {ticket.Description}, {ticket.Ticket_Type}, {ticket.Severity}, {ticket.Priority}, {ticket.Status});";
                
                switch(cmd.ExecuteNonQuery()){
                    case 0: 
                        Console.WriteLine("Nothing was changed");
                        break;
                    case 1:
                        Console.WriteLine($"The ticket: {ticket.Ticket_ID} has been updated");
                        break;        
                    default:
                        Console.WriteLine("An error occured");
                        break;          
                }
            }    
        }
    }

    public override void DeleteTicket(int ID)
    {
        if(ID == 0) Console.WriteLine("The Delete procedure has failed.");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"DELETE from Tickets where Ticket_ID = '{ID}';";
                
                switch(cmd.ExecuteNonQuery()){
                    case 0: 
                        Console.WriteLine("Nothing was changed");
                        break;
                    case 1:
                        Console.WriteLine("The ticket: {ticket.Ticket_ID} has been deleted");
                        break;  
                    default:
                        Console.WriteLine("An error occured");
                        break;       
                }
            }    
        }
    }

    public override void ResolveTicket(int ID){
        if(ID == 0) Console.WriteLine("The ticket has not been resolved");
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand()){
                conn.Open();
                cmd.CommandText = $"UPDATE Tickets SET Status = 'Closed' WHERE Ticket_ID = '{ID}';";
                
                switch(cmd.ExecuteNonQuery()){
                    case 0: 
                        Console.WriteLine("Nothing was changed");
                        break;
                    case 1:
                        Console.WriteLine("The ticket: {ticket.Ticket_ID} has been resolved");
                        break;
                    default:
                        Console.WriteLine("An error occured");
                        break;         
                }
            }
        }
    }
}