namespace FreelanceProjectControl.Models;
public class Project
{
    public string? Id { get; set; }
    public string? Customer { get; set; }  
    public string? Name { get; set; }
    public int WorkedHours { get; set; }
    public decimal FlatRateAmount { get; set; }
    public decimal HourlyRateAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; }
}

