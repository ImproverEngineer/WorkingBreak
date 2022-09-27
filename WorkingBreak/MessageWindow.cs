using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingBreak
{
    public partial class MessageWindow : Form
    {
        int DecayInterval = 0;
        int widthWindowDeflection = 0;
        int heightWindowDeflection = 0;
        public MessageWindow(int DecayInterval, int widtWindowDeflection, int heightWindowDeflection)
        {
            InitializeComponent();
            this.DecayInterval = DecayInterval;
            this.widthWindowDeflection = widtWindowDeflection;
            this.heightWindowDeflection = heightWindowDeflection;
        }        
        private void MessageWindow_Load(object sender, EventArgs e)
        {
            this.Size = new Size(186,242);//Размер окна возможно нужно регулировать?
            Rectangle scrinSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds; //определим размер экрана основного.            
            int width = scrinSize.Size.Width;   // задать ширину
            int height = scrinSize.Size.Height; // задать длину
            int new_width = width - this.Size.Width - this.widthWindowDeflection; //Размер Width возможно нужно регулировать?
            int new_higth = height - this.Size.Height   - this.heightWindowDeflection; //Размер Height возможно нужно регулировать?
            this.Location = new System.Drawing.Point(new_width,new_higth);
            Timer timer = new Timer(); //Cоздать таймер чтобы окно само закрывалось через заданное количество времени
            timer.Interval = this.DecayInterval;
            timer.Tick += timer_tick;
            timer.Enabled = true;
            this.Opacity = double.Parse(ConfigurationSettings.AppSettings.GetValues("Opacity").First().ToString())/100;
            label1.Text = ConfigurationSettings.AppSettings.GetValues("textHeder").First().ToString();
            label2.Text = ConfigurationSettings.AppSettings.GetValues("textBodyFirs").First().ToString();
            label3.Text = ConfigurationSettings.AppSettings.GetValues("textBodySec").First().ToString();
            label4.Text = ConfigurationSettings.AppSettings.GetValues("textFooter").First().ToString();

        }
        private void timer_tick(object obj, EventArgs e)
        {
            this.Close();
        }

        private void MessageWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
