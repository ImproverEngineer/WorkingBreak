using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public MessageWindow()
        {
            InitializeComponent();
        }

        private void MessageWindow_Load(object sender, EventArgs e)
        {
            this.Size = new Size(160,240);//Размер окна возможно нужно регулировать?
            Rectangle scrinSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds; //определим размер экрана основного.            
            int width = scrinSize.Size.Width;   // задать ширину
            int height = scrinSize.Size.Height; // задать длину
            int new_width = width - this.Size.Width-3; //Размер Width возможно нужно регулировать?
            int new_higth = height - this.Size.Height-50 ; //Размер Height возможно нужно регулировать?
            this.Location = new System.Drawing.Point(new_width,new_higth);
            Timer timer = new Timer(); //Cоздать таймер чтобы окно само закрывалось через заданное количество времени
            timer.Interval = 3000;
            timer.Tick += timer_tick;
            timer.Enabled = true; 
            
        }
        private void timer_tick(object obj, EventArgs e)
        {
            this.Close();
        }

        private void MessageWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
