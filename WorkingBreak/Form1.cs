using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace WorkingBreak
{
    public partial class Form1 : Form
    {  // инициализируем начальную точку активности курсора.
        Point point = new Point(MousePosition.X,MousePosition.Y);

        public Form1()
        {
            InitializeComponent();
            // Убираем кнопки свернуть, развернуть, закрыть.
            this.ControlBox = false;
            // Убираем заголовок.
            this.Text = "";
            //скрыть с панели задач:
            this.ShowInTaskbar = false;
            //делаем окно прозрачным
            this.Opacity = 0;
            getTimer();
            Application.Exit();
        }

        #region переменные для рабты с таймером.
        Timer timer = new Timer();
        int timeWork = 0;
        int timeDonWork = 0;
        int X = 0;
        int Y = 0;
        #endregion

        #region Таймер.
        /// <summary>
        /// Таймер.
        /// </summary>
        private void getTimer()
        {
            timer.Interval = 5000;
            timer.Tick += new System.EventHandler(TimerTick);
            timer.Start();
            while (true)
            {
                Application.DoEvents();
            }
        }
        #endregion

        #region Событие таймера.
        /// <summary>
        /// Событие таймера.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void TimerTick(Object obj, EventArgs e)
        {
            timer.Stop();
            point = new Point(MousePosition.X, MousePosition.Y);
            if (X!=point.X && Y!=point.Y)
            {
                timeWork += timer.Interval;
                timeDonWork = 0;
                if (timeWork > 60000)
                {
                    if (MessageBox.Show("Мы заботимся о вашем здоровье и предлогаем отдохнуть", "Внимание",  MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
                    {
                        // Молодец ты заслужил отдых
                    }
                }
                X = point.X; Y = point.Y;
                timer.Enabled = true;               
            }
            else
            {
                timeDonWork += timer.Interval;
                if (timeDonWork > 180000)///если не длвигал мышкой больше 3 минут то время до следующего отдых сбрасывается.
                { timeWork = 0; } //если долго отдыхать то можно и потерять рабочее время.
                timer.Enabled = true;
            }            
        }
        #endregion
    }
}
