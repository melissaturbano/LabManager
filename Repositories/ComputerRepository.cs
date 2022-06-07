using LabManager.Models;
using LabManager.Database;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;


    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public List<Computer> GetAll()
    {
        var computers = new List<Computer>();
        
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";

        var reader = command.ExecuteReader(); // quando precisa devolver uma resposta do banco

        while(reader.Read())
        {
            //var computer = readerToComputer(reader);

            computers.Add(readerToComputer(reader));
        }

        reader.Close();
        connection.Close(); 

        return computers;
    }


    public Computer Save (Computer computer)    //se passa o obj como parâmetro, pois eram muitos atributos
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);";
            command.Parameters.AddWithValue("$id", computer.Id);
            command.Parameters.AddWithValue("$ram", computer.Ram);
            command.Parameters.AddWithValue("$processor", computer.Processor);

            command.ExecuteNonQuery();
            connection.Close();  

            return computer; 
    }

    public void Delete (int id) 
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery(); //quando não precisa de uma resposta do banco 

        connection.Close();
    }

    public Computer GetById (int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

    
        command.CommandText = "SELECT * FROM Computers WHERE id = $id ;"; //nunca concatenar sql com a variável, por isso do "$id"
        command.Parameters.AddWithValue("$id", id);


        var reader = command.ExecuteReader();
        reader.Read(); //faz a leitura de linha por linha
        var computer = readerToComputer(reader);


        reader.Close();
        connection.Close(); 

        return computer;
    }

    public Computer Update (Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        
        command.CommandText = "UPDATE Computers SET ram = $ram, processor = $processor WHERE id = $id;";  
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor); 

        command.ExecuteNonQuery();
        connection.Close(); 

        return computer;
    }

    private Computer readerToComputer(SqliteDataReader reader)
    {
        var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

        return computer;

    }


    public bool existById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = "SELECT count(id) FROM Computers WHERE id = $id;"; 
        command.Parameters.AddWithValue("$id", id);


        bool result = Convert.ToBoolean(command.ExecuteScalar()); // devolve um obj, que seria o valor


        return result;
    }

}