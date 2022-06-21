using LabManager.Models;
using LabManager.Database;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;


    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        //Read
        var computers = connection.Query<Computer>("SELECT * FROM Computers");
 
        connection.Close(); 

        return computers;
    }


    public Computer Save (Computer computer)    //se passa o obj como parâmetro, pois eram muitos atributos
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        //Create
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer);

        connection.Close();  
        return computer; 
    }

    public void Delete (int id) 
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        // new {Id = id}  -> encapsulando propriedade de um obj, sem precisar criar uma classe para isso
        connection.Execute("DELETE FROM Computers WHERE id = @Id", new {Id = id} );

        connection.Close();
    }

    public Computer GetById (int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();


        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id", new {Id = id});
 
        connection.Close(); 

        return computer;

    }

    public Computer Update (Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id = @Id", computer);
        
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

        var count = connection.ExecuteScalar("SELECT count(id) FROM Computers WHERE id = @Id", new {Id = id});

        bool result = Convert.ToBoolean(count); // devolve um obj, que seria o valor

        return result;
    }

}