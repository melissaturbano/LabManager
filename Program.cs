using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);




//Routing 
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer") 
{
        var computerRepository = new ComputerRepository(databaseConfig);
        
        if(modelAction == "List")
        {
                Console.WriteLine("Computer List");
                foreach (var computer in computerRepository.GetAll())
                {
                        Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
                } 
        }

        if(modelAction == "New")
        {
                Console.WriteLine("ComputerNew");
                int id = Convert.ToInt32(args[2]);
                var ram = args[3];
                var processor = args[4];

                var computer = new Computer(id, ram, processor);
                computerRepository.Save(computer);
        }

        if (modelAction == "Delete") 
        {
                Console.Write("Computer Delete");

                int id = Convert.ToInt32(args[2]);
                computerRepository.Delete(id);
        
        }

        if (modelAction == "Show") 
        {
            Console.Write("Computer Show ");
            int id = Convert.ToInt32(args[2]);

            if(computerRepository.existById(id))
            {
                var computer = computerRepository.GetById(id);
                Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
            } else {
                Console.WriteLine($"O computador {id} não existe");
            }


            foreach (var computer in computerRepository.GetAll())
            {
                if(computer.Id == id)
                {
                    computerRepository.GetById(id);
                    Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
                }
            }


        }

        if(modelAction == "Update")
        {
                Console.WriteLine("Computer Update");

                int id = Convert.ToInt32(args[2]);
                var ram = args[3];
                var processor = args[4];

                var computer = new Computer(id, ram, processor);
                computerRepository.Update(computer);

                Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
 
        }
}
