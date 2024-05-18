
namespace ArtBooking.Model.Auth;

public class Role
{
    public int RoleId { get; set; }
    public required string RoleName { get; set; }
    public required string RoleDescription { get; set; }
    // MasterAdmin - może zarządzać wszystkimi organizacjami w tym dodawać lub usuwać użytkowników.
    // OrganizationAdmin - może zarządzać jedną konkretną organizacją w tym dodawać lub usuwać użytkowników.
    // Cashier - może sprzedawać bilety w jednej konkretnej organizacji nie może zarządzać kontami użytkowników ani zmieniać wydarzeń.
    // ArtDirector - może definiować i zmieniać wydarzenia w danej organizacji nie może zarządzać kontami użytkowników.    
}