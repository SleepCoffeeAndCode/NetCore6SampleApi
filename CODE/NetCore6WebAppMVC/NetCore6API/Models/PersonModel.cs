namespace NetCore6API.Models;

public class PersonModel
{
    public int? ID { get; set; }
    public string? FIRST_NAME { get; set; }
    public string? LAST_NAME { get; set; }
    public string? EMAIL { get; set; }
    public string? GENDER { get; set; }
    public DateTime? CREATED_DATE { get; set; }
}