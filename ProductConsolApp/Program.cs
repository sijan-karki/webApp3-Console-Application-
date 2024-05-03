using System;
using System.Data.SqlClient;

namespace ProductConsolApp
{
    class Program
    {
        static string connectionString = @"Server=LAPTOP-9A4GKMF2\SQLEXPRESS02;Database=LabNCC;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            Console.WriteLine("Student CRUD Operations");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InsertRecord();
                    break;
                case "2":
                    UpdateRecord();
                    break;
                case "3":
                    DeleteRecord();
                    break;
                case "4":
                    DisplayRecords();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void InsertRecord()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connection Established Successfully");

                    Console.Write("Enter student Id : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter student name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter student address: ");
                    string address = Console.ReadLine();
                    Console.Write("Enter student phone no : ");
                    int phone = Convert.ToInt32(Console.ReadLine());

                    string query = "INSERT INTO AddressBook (Id, Name, Address, Phone) VALUES (@Id, @Name, @Address, @Phone)";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    sqlCommand.Parameters.AddWithValue("@Name", name);
                    sqlCommand.Parameters.AddWithValue("@Address", address);
                    sqlCommand.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " Record(s) Inserted Successfully");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Oops! Something went wrong during insert operation. " + ex.Message);
            }
        }

        static void UpdateRecord()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connection Established Successfully");

                    Console.Write("Enter student Id to update: ");
                    int idToUpdate = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter new student name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Enter new student address: ");
                    string newAddress = Console.ReadLine();

                    string query = "UPDATE AddressBook SET Name = @Name, Address = @Address WHERE Id = @Id";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.Parameters.AddWithValue("@Name", newName);
                    sqlCommand.Parameters.AddWithValue("@Address", newAddress);
                    sqlCommand.Parameters.AddWithValue("@Id", idToUpdate);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " Record(s) Updated Successfully");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Oops! Something went wrong during update operation. " + ex.Message);
            }
        }

        static void DeleteRecord()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connection Established Successfully");

                    Console.Write("Enter student Id to delete: ");
                    int idToDelete = Convert.ToInt32(Console.ReadLine());

                    string query = "DELETE FROM AddressBook WHERE Id = @Id";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.Parameters.AddWithValue("@Id", idToDelete);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " Record(s) Deleted Successfully");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Oops! Something went wrong during delete operation. " + ex.Message);
            }
        }

        static void DisplayRecords()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connection Established Successfully");

                    string query = "SELECT * FROM AddressBook";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        Console.WriteLine("Id\tName\tAddress\tPhone");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]}\t{reader["Name"]}\t{reader["Address"]}\t{reader["Phone"]}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Oops! Something went wrong during display operation. " + ex.Message);
            }
        }
    }
}
