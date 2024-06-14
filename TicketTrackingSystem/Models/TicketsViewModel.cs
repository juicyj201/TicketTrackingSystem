namespace TicketTrackingSystem.Models;

public class TicketsViewModel{
    public int Ticket_ID  {get;set;} = 0;
    public string? Summary {get;set;}
    public string? Description {get;set;}
    public string? Ticket_Type {get;set;} 
    public string? Severity {get;set;}
    public string? Priority {get;set;}
    public string? Status {get;set;}
}
