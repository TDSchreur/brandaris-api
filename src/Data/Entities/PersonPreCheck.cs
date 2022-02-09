namespace Brandaris.Data.Entities;

public class PersonPreCheck : PersonBase, IPreCheck
{
    public int? ParentId { get; set; }
}
