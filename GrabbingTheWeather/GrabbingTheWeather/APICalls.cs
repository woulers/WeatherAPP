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
        public main Main {get;set;}
        public string Name { get; set; }
        public bool IsSuccessful { get; set; }
    }

    public class main
    {
        public double Temp { get; set; }
    }

    public class OpenWeatherMap
    {
        //pull key from app.config
        //public static string OpenWeatherKey = "3e6309d288961c6cfb49323f8515da06";
        public string OpenWeatherKey = ConfigurationManager.AppSettings["OpenWeatherMapKey"];

        public string GetWeather(string location)
        {
            //construct the API call
            string Url = string.Format("http://api.openweathermap.org/data/2.5/weather?units=imperial&APPID={0}&q={1}", OpenWeatherKey, location);
            string message = null; 

            try
            {
                //GET request
                var Response = GETRequest(Url); 

                //check to see if the call successful
                if (Response.IsSuccessful)
                {
                    //use JSON to parse the message
                    message = FormMessage(Response);
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

        public virtual string FormMessage(APIResponce response)
        {
            var message = response.Name + " weather: " + response.Main.Temp.ToString() + " degrees Fahrenheit";
            return message;
        }

        public virtual APIResponce GETRequest(string url)
        {
            //make API call
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync(url).Result;

            //Fill temp with Json
            string JsonMessage = responseMessage.Content.ReadAsStringAsync().Result;
            var returnedResponce = JsonConvert.DeserializeObject<APIResponce>(JsonMessage);
            if(responseMessage.IsSuccessStatusCode)
            {
                returnedResponce.IsSuccessful = true;
            }
            else
            {
                returnedResponce.IsSuccessful = false;
            }
            return returnedResponce;
        }

        
    }
}
