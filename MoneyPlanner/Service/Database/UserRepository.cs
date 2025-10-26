using MoneyPlanner.Service.DTO;

namespace MoneyPlanner.Service.Database
{
    public class UserRepository
    {
        UserDTO user = new UserDTO();
        public void AddUser(string name, string surname, string birthNumber)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Uzivatele (Name, Surname, BirthNumber)
                    VALUES (@Name, @Surname, @BirthNumber)";

                if (name != null && surname != null && birthNumber != null)
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@BirthNumber", birthNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public UserDTO FindUserByBirthNumber(string birthNumber)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT * FROM Uzivatele WHERE BirthNumber = @BirthNumber";

                command.Parameters.AddWithValue("@BirthNumber", birthNumber);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader["BirthNumber"] != null)
                {
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
