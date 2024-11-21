using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

public class Repair
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    public RepairType Type { get; set; }

    [Required]
    public Status CurrentStatus { get; set; } = Status.Pending;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Cost { get; set; }
    
    [Required]
    public Guid PropertyId { get; set; }
    public Property RepairingProperty { get; set; }

    public Repair() { }

    public Repair(Guid id, DateTime scheduledDate, RepairType type, string description, string address, decimal cost, Guid propertyId)
    {
        Id = id;
        ScheduledDate = scheduledDate;
        Type = type;
        Description = description;
        Address = address;
        Cost = cost;
        PropertyId = propertyId;
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public enum RepairType
    {
        Plumbing,
        Electrical,
        Painting,
        Other
    }
}
