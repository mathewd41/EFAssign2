using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Program
{
    public static void Main(string[] args)
    {
         
         TestDatabase();
         
    }
    public static void TestDatabase()
    {
        var repo = new Repo();
        while (true)
        {
            Console.WriteLine("List of all Users");
            UserList();
            Console.WriteLine("Select 1 to insert new user, 2 to update a user, 3 to delete a user,4 to exit");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.WriteLine("Enter the details for new User");
                var user = new User()
                {
                    Name = ReadInput("Name:", null),
                    PhoneNumber = ReadInput("Phone Number:", null),
                    EmailAddress = ReadInput("Email Address:", null),
                };
                repo.Create(user);
               

            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter the id of the user to update");
                int id = int.Parse(Console.ReadLine());
                var user1Updated = repo.Read<User>(id);
                if (user1Updated != null)
                {
                    user1Updated.Name = ReadInput("Name:", user1Updated.Name);
                    user1Updated.PhoneNumber = ReadInput("Phone Number:", user1Updated.PhoneNumber);
                    user1Updated.EmailAddress = ReadInput("Email Address:", user1Updated.EmailAddress);
                    repo.Update(user1Updated);
                   
                }

            }
            else if (choice == "3")
            {
                Console.WriteLine("Enter the id of the user to delete");
                int id = int.Parse(Console.ReadLine());
                repo.Delete<User>(id);
               
            }
            else if (choice == "4")
            {
                break; 
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    
    }
    public static string ReadInput(String writeLine,string defaultData)
    {
        Console.Write(writeLine);
        var input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
            return input.Trim();
        }
        else
        {
            return defaultData;
        }
       
    }
    public static void UserList()
    {
        var dbContext = new DataContext();
        var users = dbContext.Users.Where(u => !u.IsDeleted).ToList();
        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.EmailAddress}, Phone: {user.PhoneNumber}");
            }
        }
    }

   
}