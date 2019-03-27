using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrabbingTheWeather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            weather(textBox1.Text);
        }

        private void weather(string Location)
        {
            try
            {
                label2.Text = "loading...";
                label2.Update();
                label2.Text = OpenWeatherMap.GetWeather(Location);
            }
            catch (Exception ex)
            {
                label2.Text = ex.Message;
            }
            label2.Visible = true;
            label2.Update();
        }
    }
}
