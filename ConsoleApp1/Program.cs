//using Newtonsoft.Json.Linq;
//using RestSharp;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JWT;
using System.Threading;

namespace GetTokenDemo
{
    class Program
    {
        static JObject token=getToken();
        static void Main(string[] args)
        {
            string old = token["access_token"].ToString();

            Console.WriteLine(old);
            Thread.Sleep(16);
            string newold = tokenValied();
            Console.WriteLine(newold);
            Console.WriteLine(newold==old);
        }
        public static string tokenValied()
        {
            DateTime nowtime = DateTime.Now;
            DateTime tokentime = DateTime.Parse(token["time"].ToString());
            TimeSpan time = nowtime - tokentime;
            if (time.TotalSeconds>15)
            {
                token = getToken();
                string Newtoken = token["access_token"].ToString();
                Console.WriteLine(Newtoken);
                return Newtoken;
            }
            else
            {
                return token["access_token"].ToString();
            }
           
        }
        public static string getResultbytoken(string res)
        {
            //获取token
            var loginClient = new RestClient("http://127.0.0.1:8000");
            var loginRequest = new RestRequest("/Outpatient/mz/his", Method.GET);
            loginRequest.AddHeader("Authorization", "Bearer "+res);
         
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            return loginContent;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public static JObject getToken()
        {
            //获取token
            var loginClient = new RestClient("http://127.0.0.1:9000");
            var loginRequest = new RestRequest("/connect/token", Method.POST);
            loginRequest.AddParameter("username", "hjw123");
            loginRequest.AddParameter("password", "123456");
            loginRequest.AddParameter("client_id", "hjw");
            loginRequest.AddParameter("grant_type", "password");
            loginRequest.AddParameter("client_secret", "123456");
            //或用用户名密码查询对应角色
            loginRequest.AddParameter("role", "admin");
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            
            JObject token = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(loginContent);
            token.Add("time", DateTime.Now);
            return token;
        }
       
      
    }
}
