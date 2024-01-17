using JwtPractica.Models;

namespace JwtPractica.Constants
{
    public class EmployeeConstants
    {
        public static List<EmployeeModel> Employees = new List<EmployeeModel>()
        {
            new EmployeeModel(){FirstName = "Pedro", LastName = "Perez", Email = "pedroperez@gmail.com"},
            new EmployeeModel(){FirstName = "Marcos", LastName = "Gonzalez", Email = "marcosgonzalez@gmail.com"},
        };
    }
}
