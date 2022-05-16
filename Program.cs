using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Laboratories(
                id int not null primary key,
                number int not null,
                name varchar(100) not null, 
                block int not null
        );
";

command.ExecuteNonQuery();

connection.Close(); 



//Routing 
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Lab") 
{
        if(modelAction == "List")
        {
                Console.WriteLine("Lab List");
                connection = new SqliteConnection("Data Source=database.db");
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Laboratories;";

                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                        Console.WriteLine("{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                }

                reader.Close();
                connection.Close();  
        }

        if(modelAction == "New")
        {
                int id = Convert.ToInt32(args[2]);
                int number = Convert.ToInt32(args[3]);
                var name = args[4];
                int block = Convert.ToInt32(args[5]);

                connection = new SqliteConnection("Data Source=database.db");
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Laboratories VALUES($id, $number, $name, $block);";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$number", number);
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$block", block);


                command.ExecuteNonQuery();
                connection.Close();  
        }
}
