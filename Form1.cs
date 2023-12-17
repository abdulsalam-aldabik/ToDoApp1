using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using ToDoApp1.Data;
using ToDoApp1.Data.Moudels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ToDoApp1
{
    public partial class Form1 : Form
    {
        private string All = "All";
        private string date = "10/10/2023";

        public Form1()
        {
            InitializeComponent();
            // Initialize the database and tables
            DbContext.InitializeDatabase();




            // Use the database connection
            using (var db = new SQLiteConnection(DbContext.ConnectionString))
            {
                db.Open();

                // Fetch categories
                List<string> catItems = new List<string>();


                var categories = db.Query<Category>("SELECT * FROM Category");

                foreach (var category in categories)
                    catItems.Add(category.CategoryName);

                comboBox2.Items.AddRange(catItems.ToArray());


            }






        }

        private void RelodData()
        {
            using (var db = new SQLiteConnection(DbContext.ConnectionString))
            {
                db.Open();

                // Construct the base query
                var query = "SELECT * FROM ToDoItem WHERE StartDate >= @StartDate AND EndDate <= @EndDate";

                // Use Dapper to query the database
                var data = db.Query<ToDoItem>(query, new
                {
                    StartDate = this.date,
                    EndDate = this.date,
                    CategoryId = this.All
                }).ToList();

                // Apply category filter if not "All"
                if (this.All != "All")
                {
                    var categoryId = Category.GetCategoryByName(this.All)?.CategoryId;

                    if (categoryId.HasValue)
                    {
                        data = data.Where(r => r.CategoryId == categoryId.Value).ToList();
                    }
                }

                string sqlQuery = "SELECT * FROM ToDoItem";

                List<ToDoItem> toDoItems = db.Query<ToDoItem>(sqlQuery).ToList();
                var data1 = toDoItems;

                grid.DataSource = data1;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.All = comboBox2.SelectedItem?.ToString() ?? "All";
            RelodData();
        }

        private void add_Click(object sender, EventArgs e)

        {
            // Retrieve values from TextBoxes
            string description = box1.Text;
            int categoryId = int.Parse(box2.Text); // Assuming CategoryId is an integer
            string startDate = box3.Text;
            string endDate = box4.Text;

            ToDoItem newItem = new ToDoItem
            {
                Description = description,
                CategoryId = categoryId,
                StartDate = startDate,
                EndDate = endDate,
                //Done = true,
                // You might set other properties accordingly
            };

            // Insert into the database using Dapper
            using (var db = new SQLiteConnection(DbContext.ConnectionString))
            {
                db.Open();

                // Assuming your TodoItem table has an auto-incremented Id
                string insertQuery = "INSERT INTO TodoItem (Description, CategoryId, StartDate, EndDate) VALUES (@Description, @CategoryId, @StartDate, @EndDate)";
                db.Execute(insertQuery, newItem);

                MessageBox.Show("Item inserted successfully!");

            }
            RelodData();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            using (var db = new SQLiteConnection(DbContext.ConnectionString))
            {
                db.Open();

                // Specify the table name in the DELETE statement
                string deleteQuery = "DELETE FROM ToDoItem";

                // Execute the query using Dapper
                db.Execute(deleteQuery);
                MessageBox.Show("All items deleted successfully!");
            }
        }
    }
}
