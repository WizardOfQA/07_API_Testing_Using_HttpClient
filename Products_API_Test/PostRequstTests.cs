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
    public class PostRequstTests
    {
        static string _baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        string endPoint = "";

        [TestMethod]
        public void Can_Create_A_New_Student()
        {
            Student postData = new Student()
            {
                FirstName = "testFirstName",
                LastName = "testLastName",
                Email = "test@test.com",
                Phone = "3939992222",
                isActive = true
            };
            var jsonData = JsonConvert.SerializeObject(postData);
            headers.Add("content-type", "application/json");

            endPoint = _baseUrl;

            var response = API_Helper.PostRequest(endPoint, jsonData).Result;
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var fromDB = DB_Helper.GetTop1Student("desc");
            postData.StudentId = fromDB.StudentId;

            Assert.IsTrue(API_Helper.Compare2Students(postData, fromDB));

            // Clean up test data
            DB_Helper.DeleteStudents("test.com");
        }
    }
}
