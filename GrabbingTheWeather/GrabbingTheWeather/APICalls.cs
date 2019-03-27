using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace GrabbingTheWeather
{
    public class APIResponce
    {
        public main main {get;set;}
        public string name { get; set; }
    }

    public class main
    {
        public double temp { get; set; }
    }

    public class OpenWeatherMap
    {
        //pull key from app.config
        //public static string OpenWeatherKey = "3e6309d288961c6cfb49323f8515da06";
        public static string OpenWeatherKey = ConfigurationManager.AppSettings["OpenWeatherMapKey"];

        public static string GetWeather(string Location)
        {
            //construct the API call
            string Url = string.Format("http://api.openweathermap.org/data/2.5/weather?units=imperial&APPID={0}&q={1}", OpenWeatherKey, Location);

            HttpClient client = new HttpClient();
            string message = null;
            HttpResponseMessage Response;

            try
            {
                //GET request
                Response = client.GetAsync(Url).Result;

                //check to see if the call successful
                if (Response.IsSuccessStatusCode)
                {
                    //use JSON to parse the message
                    string tempMessage = Response.Content.ReadAsStringAsync().Result;

                    //Fill temp with Json
                    APIResponce temp = JsonConvert.DeserializeObject<APIResponce>(tempMessage);
                    message = temp.name + " weather: " + temp.main.temp.ToString() + " degrees Fahrenheit";
                }
                else
                {
                    //location was not found
                    message = "Invalid location";
                }
            
                return message;
                }

            catch(Exception ex1)
            {
                Exception ex2 = new Exception("An error in the API call occured:" + ex1.Message);
                throw ex2;
            }
            
            
            
        } 
    }
}
