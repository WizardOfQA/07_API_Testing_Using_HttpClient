using HttpClient_API_TestFramework;
using HttpClient_API_TestFramework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace Products_API_Test
{
    [TestClass]
    public class DeleteRequestTests
    {
        static string _baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string endPoint = "";

        [TestCategory("DELETE")]
        [TestMethod]
        public void Can_Delete_A_Student()
        {
            Student postData = new Student()
            {
                FirstName = "testFirstName",
                LastName = "testLastName",
                Email = "test@test.com",
                Phone = "3939992222",
                isActive = true
            };

            // Add a test data to DB
            DB_Helper.InsertStudent(postData);
            postData.StudentId = DB_Helper.GetTop1Student("desc").StudentId;

            endPoint = _baseUrl + "/" + postData.StudentId;


            // Delete the test data
            var response = API_Helper.DeleteRequest(endPoint).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var fromDB = DB_Helper.GetStudentById(postData.StudentId);

            Assert.IsNull(fromDB); //Doesn't exist in DB
        }
    }
}
