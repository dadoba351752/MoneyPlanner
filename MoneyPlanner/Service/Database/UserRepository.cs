using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;
using System;
using System.Windows;

namespace MoneyPlanner.Service.Database
{
    public class UserRepository
    {
        private static readonly UserDTO user = new UserDTO();
        private static readonly MessageService messageService = new MessageService();
        public void AddUser(string name, string surname, string birthNumber)
        {
            //Spojení s databází
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Uzivatele (Name, Surname, BirthNumber)
                    VALUES (@Name, @Surname, @BirthNumber)";

                //Validace, že jsou vyplněné všechny hodnoty
                if (name != null && surname != null && birthNumber != null)
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@BirthNumber", birthNumber);
                    try
                    {
                        command.ExecuteNonQuery();
                        messageService.ShowConfirmation($"Uživatel {name} {surname} byl úspěšně přidán!");                    }
                    catch (Exception ex)
                    {
                        messageService.ShowError(ex.Message);
                    }
                }
            }
        }

        //Metoda vyhledající uživatele pomocí rodného čísla
        public UserDTO FindUserByBirthNumber(string birthNumber)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT * FROM Uzivatele WHERE BirthNumber = @BirthNumber";

                command.Parameters.AddWithValue("@BirthNumber", birthNumber);

                var reader = command.ExecuteReader();

                //Pokud existují nějaké záznamy ve výsledcích, vrátí daného uživatele, jinak vrátí null
                if (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.Name = reader["Name"] as string;
                    user.Surname = reader["Surname"] as string;
                    user.BirthNumber = reader["BirthNumber"] as string;

                    return user;
                }
                else return null;
            }
        }
    }
}
