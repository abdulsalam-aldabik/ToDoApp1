using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using ToDoApp1.Data.Moudels;

namespace ToDoApp1.Data
{
    public class DbContext
    {
        public const string ConnectionString = "Data Source=./Database/ToDoApp.db;Version=3;";



        public static void InitializeDatabase()
        {

   

            try
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    dbConnection.Open();

                    // Explicitly create Tasks table
                    //dbConnection.Execute("CREATE TABLE IF NOT EXISTS Category (Id INTEGER PRIMARY KEY, CatagoryName TEXT)");

                    // Explicitly create TodoItem table
                    //dbConnection.Execute("CREATE TABLE IF NOT EXISTS ToDoItem (Id INTEGER PRIMARY KEY AUTOINCREMENT, Description TEXT, CategoryId INTEGER, StartDate DATETIME, EndDate DATETIME, Done BOOL)");

                    // Add default Categories
                    //var categoryQuery = "INSERT INTO Category (CatagoryName) VALUES (@CatagoryName);";
                    //dbConnection.Execute(categoryQuery, new { Description = "Personal" });
                    //dbConnection.Execute(categoryQuery, new { Description = "Shopping" });
                    //dbConnection.Execute(categoryQuery, new { Description = "Work" });
                    //dbConnection.Execute(categoryQuery, new { Description = "Errands" });
                    //dbConnection.Execute(categoryQuery, new { Description = "Projects" });

                    // Add default ToDoItems
                    //var todoItemQuery = "INSERT INTO ToDoItem (CategoryId, Description) VALUES (@CategoryId, @Description);";
                    //dbConnection.Execute(todoItemQuery, new { CategoryId = 1, Description = "Check the todo item to mark it complete" });
                    //dbConnection.Execute(todoItemQuery, new { CategoryId = 1, Description = "Filter using the calendar date" });
                    //dbConnection.Execute(todoItemQuery, new { CategoryId = 1, Description = "Group tasks using categories" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        }



}

