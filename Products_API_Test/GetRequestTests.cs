using System;
using System.Collections.Generic;
using System.Configuration;
using HttpClient_API_TestFramework;
using HttpClient_API_TestFramework.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Students_API_Test
{
    [TestClass]
    public class GetRequestTests
    {
        
        static string _baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");
       
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string endPoint = "";

        [TestMethod]
        public void Can_Retrieve_All_Students()
        {
            endPoint = _baseUrl;
            
            string conn = DB_Helper.myConnectionString();
            Console.WriteLine(conn);
            var response = API_Helper.GetRequest(endPoint);
            var fromAPI = JsonConvert.DeserializeObject<List<Student>>(response.Result);
            var fromDB = DB_Helper.GetAllStudents();
            Assert.IsTrue(API_Helper.Check3Spots(fromAPI, fromDB));
        }

        [TestMethod]
        public void Can_Retrieve_A_Specific_Students()
        {            
            // Get all the StudentIds
            var AllIds = DB_Helper.GetAllStudentIds();

            // Get a random Id
            Random rnd = new Random();
            int randomID = (int)AllIds[rnd.Next(AllIds.Count)];

            int studentPicked;
            if (AllIds.Count >= 1)
                studentPicked = randomID;
            else
                throw new Exception("There is no data to test in DB");

            endPoint = _baseUrl + "/" + studentPicked;

            var response = API_Helper.GetRequest(endPoint);
            var fromAPI = JsonConvert.DeserializeObject<Student>(response.Result);

            var fromDB = DB_Helper.GetStudentById(studentPicked);
            Assert.IsTrue(API_Helper.Compare2Students(fromAPI, fromDB));
        }
    }
}
