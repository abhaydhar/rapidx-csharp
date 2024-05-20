using System;
using System.Data.SqlClient;
using System.Text;

class DatabaseOperations
{
    static string connectionString = "your_connection_string_here";

    static void Main()
    {
        ReadEmployees();
        ReadDepartment(2); // Example: Read department with ID 2
        InsertLog("Sample log entry.");
    }

    static void ReadEmployees()
    {
        string query = "SELECT EmployeeId, Name, DepartmentId FROM Employees";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["EmployeeId"]}, Name: {reader["Name"]}, Department ID: {reader["DepartmentId"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ReadEmployees: " + ex.Message);
            }
        }
    }

    static void ReadDepartment(int departmentId)
    {
        string query = "SELECT DepartmentId, DepartmentName FROM Departments WHERE DepartmentId = @DepartmentId";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine($"Department ID: {reader["DepartmentId"]}, Name: {reader["DepartmentName"]}");
                }
                else
                {
                    Console.WriteLine("No department found with the specified ID.");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ReadDepartment: " + ex.Message);
            }
        }
    }

    static void InsertLog(string message)
    {
        string query = "INSERT INTO Logs (Message) VALUES (@Message)";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Message", message);
            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Log entry added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add log entry.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in InsertLog: " + ex.Message);
            }
        }
    }
}