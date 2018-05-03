using HttpClient_API_TestFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient_API_TestFramework
{
    public class API_Helper
    {
        public async static Task<string> GetRequest(string endPoint)
        {
            string contents = "";
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.UseDefaultCredentials = true;

                using (HttpClient _httpClient = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await _httpClient.GetAsync(endPoint))
                    {
                        try
                        {
                            contents = await response.Content.ReadAsStringAsync();
                        }
                        catch (Exception ex)
                        {
                           contents =  ex.Message;
                        }                        
                        return contents;
                    }
                }
            }
        }

        public async static Task<HttpResponseMessage> PostRequest(string endPoint, string jsonData)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.UseDefaultCredentials = true;                

                using (HttpClient _httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage _response = new HttpResponseMessage();
                    try
                    {
                        _response = await _httpClient.PostAsync(endPoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                    }
                    catch (Exception ex)
                    {
                         _response.ReasonPhrase = ex.Message;
                    }
                    return _response;
                }
            }
        }

        public async static Task<HttpResponseMessage> PutRequest(string endPoint, string jsonData)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.UseDefaultCredentials = true;

                using (HttpClient _httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage _response = new HttpResponseMessage();
                    try
                    {
                        _response = await _httpClient.PutAsync(endPoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                    }
                    catch (Exception ex)
                    {
                        _response.ReasonPhrase = ex.Message;
                    }
                    return _response;
                }
            }
        }


        public async static Task<HttpResponseMessage> DeleteRequest(string endPoint)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.UseDefaultCredentials = true;

                using (HttpClient _httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage _response = new HttpResponseMessage();
                    try
                    {
                        _response = await _httpClient.DeleteAsync(endPoint);
                    }
                    catch (Exception ex)
                    {
                        _response.ReasonPhrase = ex.Message;
                    }
                    return _response;
                }
            }
        }
        public static bool Check3Spots(List<Student> first, List<Student> second)
        {
            if (first.Count() != second.Count())
            {
                return false;
            }
            return (first.First().stEquals(second.First()) &&
                    (first[first.Count / 2].stEquals(second[second.Count / 2])) &&
                    (first.Last().stEquals(second.Last())));
        }

        public static bool Compare2Students(Student first, Student second)
        {
            return first.stEquals(second);
        }
    }    
}
