using System;
using System.Windows.Forms;

namespace ConsoleApp.Forms
{
    partial class Forms
    {
        TextBox txtCity;
        Button btnGetWeather;
        Label introTxt,lblCity, lblTemp, lblDesc, lblIcon, lblStatus;

        private void InitializeComponent()
        {
            this.introTxt = new Label { Top = 15, Left = 20, Width = 300 };
            this.introTxt.Text = "Вкажіть місто або натисніть пошук";
            this.txtCity = new TextBox { Top = 40, Left = 20, Width = 200 };
            this.btnGetWeather = new Button { Text = "Пошук", Top = 40, Left = 230 };
            this.lblCity = new Label { Top = 80, Left = 20, Width = 300 };
            this.lblTemp = new Label { Top = 110, Left = 20, Width = 300 };
            this.lblDesc = new Label { Top = 140, Left = 20, Width = 300 };
            this.lblIcon = new Label { Top = 170, Left = 20, Font = new System.Drawing.Font("Segoe UI Emoji", 8F) };
            this.lblStatus = new Label { Top = 220, Left = 20, Width = 300 };

            this.btnGetWeather.Click += new EventHandler(this.btnGetWeather_Click);

            this.Controls.AddRange(new Control[] {
                introTxt, txtCity, btnGetWeather, lblCity, lblTemp, lblDesc, lblIcon, lblStatus });

            this.Text = "Погода — wttr.in";
            this.Width = 400;
            this.Height = 300;
            
        }
    }
}