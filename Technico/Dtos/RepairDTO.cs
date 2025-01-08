using Technico.Models;

namespace Technico.Dtos;

public class RepairDTO
{
    public Guid Id { get; set; }
    public DateTime ScheduledDate { get; set; }
    public Repair.RepairType Type { get; set; }
    public Repair.Status CurrentStatus { get; set; }
    public string? Description { get; set; } 
    public string? Address { get; set; } 
    public decimal Cost { get; set; }
    public Guid PropertyId { get; set; }
}
