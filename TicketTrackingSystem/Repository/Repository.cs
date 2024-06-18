using TicketTrackingSystem.Models;
using System.Data.SQLite;
using System.Text;

namespace TicketTrackingSystem.Repository;

public class Repository{

    #region Tickets
    public virtual List<TicketsViewModel> GetTickets() => new();
    public virtual TicketsViewModel GetTicketFromID(int ID) => new();
    public virtual string CreateTicket(TicketsViewModel ticket) => String.Empty;
    public virtual string EditTicket(TicketsViewModel ticket) => String.Empty;
    public virtual string DeleteTicket(int ID) => String.Empty;
    public virtual string ResolveTicket(int ID) => String.Empty;
    #endregion

    #region Users
    public virtual User Login(User user) => new User();
    #endregion
}

public class RepositoryImpl : Repository
{
    private const string ConnectionString = @"Data Source=.\TicketTracking.db;Version=3;";

    #region Tickets
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

    public override TicketsViewModel GetTicketFromID(int ID)
    {
        TicketsViewModel ticket = new();

        using (var conn = new SQLiteConnection(ConnectionString)) 
        using (var cmd = conn.CreateCommand())
        {
            conn.Open();
            cmd.CommandText = $"SELECT * FROM Tickets where Ticket_ID = '{ID}'";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ticket = new TicketsViewModel();
                    ticket.Ticket_ID = Convert.ToInt32(reader["Ticket_ID"]);
                    ticket.Summary = reader["Summary"].ToString();
                    ticket.Description = reader["Description"].ToString();
                    ticket.Ticket_Type = reader["Ticket_Type"].ToString();
                    ticket.Severity = reader["Severity"].ToString();
                    ticket.Priority = reader["Priority"].ToString();
                    ticket.Status = reader["Status"].ToString();
                    return ticket;
                }
            }
        }    

        return new();
    }

    public override string CreateTicket(TicketsViewModel ticket)
    {
        if(ticket.Ticket_ID == 0 && ticket.Summary.Equals(null) && ticket.Description.Equals(null)) Console.WriteLine("The Create procedure has failed");
        else{
            var existingTicket = GetTicketFromID(ticket.Ticket_ID);
            if(existingTicket.Ticket_ID != 0){
                return GetResult(-1, ticket.Ticket_ID, "created");
            }

            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT into Tickets values ('{ticket.Ticket_ID}', '{ticket.Summary}', '{ticket.Description}', '{ticket.Ticket_Type}', '{ticket.Severity}', '{ticket.Priority}', '{ticket.Status}');";
                
                return GetResult(cmd.ExecuteNonQuery(), ticket.Ticket_ID, "created");
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
                cmd.CommandText = $"UPDATE Tickets SET Ticket_ID = '{ticket.Ticket_ID}', Summary = '{ticket.Summary}', Description = '{ticket.Description}', Ticket_Type = '{ticket.Ticket_Type}', Severity = '{ticket.Severity}', Priority = '{ticket.Priority}', Status = '{ticket.Status}' WHERE Ticket_ID = '{ticket.Ticket_ID}';";
                
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
    }
    #endregion

    #region Users

    public override User Login(User user)
    {
        var checkuser = new User();

        if(user.EMP_ID == 0 && user.Name == null && user.Password == null && user.Emp_Type == null) {
            Console.WriteLine("The user name or password entered was not valid");
            return checkuser;
        }
        else{
            using (var conn = new SQLiteConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"SELECT * FROM Users WHERE Name = '{user.Name}';";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        checkuser.EMP_ID = Convert.ToInt32(reader["EMP_ID"]);
                        checkuser.Name = reader["Name"].ToString();
                        checkuser.Emp_Type = reader["Emp_Type"].ToString();
                        checkuser.Password = reader["Password"].ToString();
                        if(user.Name.Equals(checkuser.Name) && user.Password.Equals(checkuser.Password)){
                            return checkuser;
                        }
                    }
                }
            }    
        }

        return checkuser;
    }
    #endregion
}