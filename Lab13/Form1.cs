using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart.ChartAreas[0].AxisX.IsMarginVisible = false;
        }

        const double k = 0.05;
        const double drift = 0.005;
        const double volatility = 0.05;
        double rateEuro, rateDollar;
        double BoxMuller = 0;
        int days = 0;
        Random random = new Random();

        private void timer1_Tick(object sender, EventArgs e)
        {

            BoxMuller = Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Cos(Math.PI * random.NextDouble());
            rateEuro = rateEuro * Math.Exp((drift-((double)(volatility*volatility)/(double)2)) + volatility * BoxMuller);

            BoxMuller = Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Cos(Math.PI * random.NextDouble());
            rateDollar = rateDollar * Math.Exp((drift - ((double)(volatility * volatility) / (double)2)) + volatility * BoxMuller); 

            rateEuro = Math.Round(rateEuro,2);
            rateDollar = Math.Round(rateDollar,2);

            chart.Series[0].Points.AddXY(days, rateEuro);
            chart.Series[1].Points.AddXY(days, rateDollar);
            days++;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Stop")
            {
                startButton.Text = "Predict again";
                timer1.Stop();
                days = 0;
            }
            else
            {
                rateEuro = (double)editEuro.Value;
                rateDollar = (double)editDollar.Value;
                ;

                chart.Series[0].Points.Clear();
                chart.Series[0].Points.AddXY(0, rateEuro);

                chart.Series[1].Points.Clear();
                chart.Series[1].Points.AddXY(0, rateDollar);

                startButton.Text = "Stop";
                timer1.Start();
            }
        }
    }
}
