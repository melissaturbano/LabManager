using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig) // configuração p fazer tabela
    {
        this.databaseConfig = databaseConfig;
        CreateTableComputer();
        CreateTableLab();
    }


    private void CreateTableComputer()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key,
                ram varchar(100) not null,
                processador varchar(100) not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close(); 

    }

    private void CreateTableLab()
    {
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
    }
}