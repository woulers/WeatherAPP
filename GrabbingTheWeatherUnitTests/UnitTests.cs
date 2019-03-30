using System;
using GrabbingTheWeather;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GrabbingTheWeatherUnitTests
{
    [TestClass]
    public class UnitTests
    {
        string formMessageResponse;
        APIResponce validResponse;
        APIResponce invalidResponse;
        string url;



        [TestInitialize]
        public void Setup()
        {
            formMessageResponse = "Chicago weather: 40.11 degrees Fahrenheit";

            validResponse = new APIResponce();
            validResponse.Main = new main() { Temp = 40.11 };
            validResponse.Name = "Chicago";
            validResponse.IsSuccessful = true;

            invalidResponse = new APIResponce();
            invalidResponse.Main = new main() { Temp = 40.11 };
            invalidResponse.Name = "Chicago";
            invalidResponse.IsSuccessful = false;


            url = "http://api.openweathermap.org/data/2.5/weather?units=imperial&APPID=3e6309d288961c6cfb49323f8515da06&q=Chicago";


        }

        [TestMethod]
        public void GetWeather_ValidLocation_AreEqual()
        {
            //Arrange
            var mock = new Mock<OpenWeatherMap>();
            mock.Setup(owp => owp.FormMessage(validResponse)).Returns(formMessageResponse);
            mock.Setup(owp => owp.GETRequest(url)).Returns(validResponse);
            var expected = "Chicago weather: 40.11 degrees Fahrenheit";

            //Act
            string result = mock.Object.GetWeather("Chicago");

            //Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void GetWeather_InvalidLocation_AreEqual()
        {
            //Arrage
            var mock = new Mock<OpenWeatherMap>();
            mock.Setup(owp => owp.FormMessage(invalidResponse)).Returns(formMessageResponse);
            mock.Setup(owp => owp.GETRequest(url)).Returns(invalidResponse);
            var expected = "Invalid location";
            

            //Act
            string result = mock.Object.GetWeather("Chicago");

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FormMessage_CorrectMessage_AreEquel()
        {
            //Arrage
            var owp = new OpenWeatherMap();
            var expected = "Chicago weather: 40.11 degrees Fahrenheit";
     
            //Act
            string result = owp.FormMessage(validResponse);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
