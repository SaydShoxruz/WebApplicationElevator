using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationElevator.Models;

public class ElevatorRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CurrentFloor { get; set; }
    public int TargetFloor { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
}
