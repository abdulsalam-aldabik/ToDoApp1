using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp1.Data.Moudels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public static Category GetCategoryByName(string name)
        {
            using (var db = new SQLiteConnection(DbContext.ConnectionString))
            {
                db.Open();

                // Use Dapper to query the database
                var category = db.QueryFirstOrDefault<Category>(
                    "SELECT * FROM Category WHERE LOWER(CategoryName) = @Name",
                    new { Name = name.ToLower().Trim() });

                return category;
            }
        }

    }
}
