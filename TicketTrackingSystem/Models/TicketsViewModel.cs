using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace TicketTrackingSystem.Models;

public class TicketsViewModel{
    [Required]
    public int Ticket_ID  {get;set;} = 0;
    
    [Required]
    [StringLength(300)]
    public string? Summary {get;set;}
    
    [Required]
    [StringLength(300)]
    public string? Description {get;set;}
    
    [Required]
    [StringLength(50)]
    public string? Ticket_Type {get;set;} 
    
    [StringLength(50)]
    public string? Severity {get;set;}
    
    [StringLength(20)]
    public string? Priority {get;set;}
    
    [StringLength(20)]
    public string? Status {get;set;}
}
