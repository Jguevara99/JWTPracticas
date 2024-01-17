using WebApplication1.Models;

namespace JwtPractica.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "jguevara", Password = "admin123", Rol = "Administrador", EmailAddress = "guevarajairo294@gmail.com", FirstName = "Jairo", LastName = "Guevara"},
            new UserModel() { Username = "jampie", Password = "admin123", Rol = "Vendedor", EmailAddress = "jampie@gmail.com", FirstName = "Antonio", LastName = "Ampie"},
        };

    }
}
