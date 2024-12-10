using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationElevator.Models;

public class ElevatorState
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CurrentFloor { get; set; } = 0;
    public int TargetFloor { get; set; } = 1;
    public string Direction { get; set; } = "Idle";
}
