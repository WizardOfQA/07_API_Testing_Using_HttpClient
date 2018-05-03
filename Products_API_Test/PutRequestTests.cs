using HttpClient_API_TestFramework;
using HttpClient_API_TestFramework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace Products_API_Test
{
    [TestClass]
    public class PutRequestTests
    {
        static string _baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        string endPoint = "";

        [TestCategory("PUT")]
        [TestMethod]
        public void Can_Update_Existing_Student_Info()
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

            Student updatedData = new Student()
            {
                FirstName = "xxxFirstName",
                LastName = "xxxLastName",
                Email = "xxx@test.com",
                Phone = "0000000000",
                isActive = false
            };

            updatedData.StudentId = DB_Helper.GetTop1Student("desc").StudentId;
            endPoint = _baseUrl + "/" + updatedData.StudentId;

            var jsonData = JsonConvert.SerializeObject(updatedData);
            headers.Add("content-type", "application/json");

            // Update the test data
            var response = API_Helper.PutRequest(endPoint, jsonData).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Get the updated data from DB
            var fromDB = DB_Helper.GetStudentById(updatedData.StudentId);

            Assert.IsTrue(updatedData.stEquals(fromDB));
        }

    }
}
