using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem;

public class User {
    public int EMP_ID {get;set;}

    [Required]
    [StringLength(50)]
    public string? Name {get;set;}

    [StringLength(20)]
    public string? Emp_Type {get;set;}

    [Required]
    [StringLength(50)]
    public string? Password {get;set;}
}