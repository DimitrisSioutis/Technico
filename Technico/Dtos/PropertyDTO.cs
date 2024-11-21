namespace Technico.Dtos;

public class PropertyDTO
{
        public Guid PropertyId { get; set; }
        public string Address { get; set; } = string.Empty;
        public int YearOfConstruction { get; set; }
        public Guid OwnerID { get; set; }
        public virtual List<RepairDTO> Repairs { get; set; } = new List<RepairDTO>();
}

public class SimplePropertyDTO
{   
    public Guid PropertyId{ get; set; }
    public string Address { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
}