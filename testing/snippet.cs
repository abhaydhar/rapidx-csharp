using System;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.UseCdn = true;

            StyleBundle styleBundle = new StyleBundle("~/bundles/artstyle");
            styleBundle.Include("~/Css/bootstrap.css",
                                //"~/Css/bootstrap-theme.css",
                                "~/Css/customstyle.css",
                                "~/Css/responsive-style.css");

            bundles.Add(styleBundle);

            //StyleBundle styleFontBundle1 = new StyleBundle("~/bundles/artstylebootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css");
            //bundles.Add(styleFontBundle1);
            //styleFontBundle1.CdnPath = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css";

            StyleBundle styleFontBundle = new StyleBundle("~/bundles/font", "https://netdna.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css");
            bundles.Add(styleFontBundle);

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/ARTScript/corerest.js",
                      "~/Scripts/ARTScript/tether.js",
                      "~/Scripts/ARTScript/bootstrap.js",
                      "~/Scripts/ARTScript/custom.js",
                      "~/Scripts/ARTScript/layout.js"));
            
            var fontCdnPath = "https://fonts.googleapis.com/css?family=Roboto:400,900,700,500,300,100";
            bundles.Add(new StyleBundle("~/bundles/goofonts", fontCdnPath));

            BundleTable.EnableOptimizations = true;
        }
    }
	
[Table("People")]
public class Person
{ 
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int Age { get; set; }

    // Navigation property
    public virtual ICollection<Order> Orders { get; set; }
	
}

class DatabaseOperations
{
    static string connectionString = "your_connection_string_here";

    static void Main()
    {
        ReadEmployees();
        ReadDepartment(2); // Example: Read department with ID 2
        InsertLog("Sample log entry.");
		double premium =0.0;
		
		premium = (sum_assured * present_value_future_benefits) / present_value_annuity;
		double number = 25.0;
            double squareRoot = Math.Sqrt(number);
            Console.WriteLine($"Square root of {number} is {squareRoot}");

            // Trigonometric operations
            double angle = Math.PI / 4; // 45 degrees
            double sine = Math.Sin(angle);
            double cosine = Math.Cos(angle);
            double tangent = Math.Tan(angle);
            Console.WriteLine($"For an angle of {angle} radians:");
            Console.WriteLine($"Sine: {sine}, Cosine: {cosine}, Tangent: {tangent}");
    }
	
	static void CalculatePremium(double sum_assured, double present_value_future_benefits, double present_value_annuity)
	{
		double premium =0.0;
		
		premium = (sum_assured * present_value_future_benefits) / present_value_annuity;
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
                if(true)
                {
                    Console.WriteLine("No department found with the specified ID.");
                }
				if(false)
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