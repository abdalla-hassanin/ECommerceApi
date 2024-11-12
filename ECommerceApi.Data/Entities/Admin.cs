namespace ECommerceApi.Data.Entities;

public class Admin
{
    public  string AdminId { get; set; }= Ulid.NewUlid().ToString();
    public  string ApplicationUserId { get; set; }
    public  ApplicationUser ApplicationUser { get; set; } = null!;
    public required string Position { get; set; }
}