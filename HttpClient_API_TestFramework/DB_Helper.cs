using Dapper;
using HttpClient_API_TestFramework.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HttpClient_API_TestFramework
{
    public class DB_Helper
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
        private static IDbConnection _dx = new SqlConnection(_connectionString);
        public static string myConnectionString()
        {
            return _connectionString;
        }
        public static List<Student> GetAllStudents()
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                List<Student> students = conn.Query<Student>(@"SELECT * FROM [Students]").ToList();
                return students;
            }
        }

        public static List<int> GetAllStudentIds()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                List<int> Ids = conn.Query<int>(@"SELECT StudentId FROM [Students]").ToList();
                return Ids;
            }
        }

        // Get a student by ID
        public static Student GetStudentById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Student>("SELECT * FROM [Students] WHERE StudentId = " + id).SingleOrDefault();
            }
        }

        // Get top 1 student
        public  static Student GetTop1Student(string sortType)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Student>("SELECT TOP (1) * FROM [Students] ORDER BY StudentId " + sortType).SingleOrDefault();
            }            
        }

        // Delete students
        public static void DeleteStudents(string email)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Execute("DELETE FROM [Students] WHERE Email LIKE @Email", new { Email = "%" + email + "%"});
            }
        }

        public static void InsertStudent(Student student)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Execute("INSERT INTO [Students](Email, FirstName, LastName, Phone, isActive)  VALUES (@Email, @FirstName, @LastName, @Phone, @isActive)",
                    new { student.Email, student.FirstName, student.LastName, student.Phone, student.isActive });
            }
        }


    }
}