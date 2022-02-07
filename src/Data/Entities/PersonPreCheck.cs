namespace Brandaris.Data.Entities;

public class PersonPreCheck : Person, IPreCheck
{
    public int? ParentId { get; set; }
}
