using System;
using System.Drawing;
using System.Windows.Forms;

namespace WorkingBreak
{
    #region Храним настройки после чтения. 
    public static class Settings
    {
        public static double Opacity     =  0;
        public static string textHeader  = "";
        public static string textBodyFirs= "";
        public static string textBodySec = "";
        public static string textFooter  = "";      
    }
    #endregion

    public partial class MessageWindow : Form
    {
        int DecayInterval = 5000;
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
            try
            {
                this.Size = new Size(186, 242);//Размер окна возможно нужно регулировать?
                Rectangle scrinSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds; //определим размер экрана основного.            
                int width = scrinSize.Size.Width;   // задать ширину
                int height = scrinSize.Size.Height; // задать длину
                int new_width = width - this.Size.Width - this.widthWindowDeflection; //Размер Width возможно нужно регулировать?
                int new_higth = height - this.Size.Height - this.heightWindowDeflection; //Размер Height возможно нужно регулировать?
                this.Location = new System.Drawing.Point(new_width, new_higth);
                Timer timer = new Timer(); //Cоздать таймер чтобы окно само закрывалось через заданное количество времени
                timer.Interval = this.DecayInterval;
                timer.Tick += timer_tick;
                timer.Enabled = true;
                this.Opacity = Settings.Opacity;
                label1.Text = Settings.textHeader;
                label2.Text = Settings.textBodyFirs;
                label3.Text = Settings.textBodySec;
                label4.Text = Settings.textFooter;
                Logger.WriteLine("Инициализация окна программы");
            }
            catch
            {

            }

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
