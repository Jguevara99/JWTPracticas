using JwtPractica.Models;

namespace JwtPractica.Constants
{
    public class ProductConstants
    {
        public static List<ProductModel> Products = new List<ProductModel>()
        {
            new ProductModel(){Name = "Coca Cola", Description = "Bebida Refrescante"},
            new ProductModel(){Name = "Celular Samsung", Description = "Telefono celular A54 8gb 254 gb"}
        };
    }
}
