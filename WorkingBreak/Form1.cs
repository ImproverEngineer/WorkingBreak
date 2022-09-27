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
using System.Configuration;
using System.IO;


namespace WorkingBreak
{
    public partial class Form1 : Form
    {  // инициализируем начальную точку активности курсора.
        Point point = new Point(MousePosition.X, MousePosition.Y);
        public Form1()
        {
            InitializeComponent();

            //если config директории не существует то завершаем работу
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\WorkingBreak.exe.config"))
            {
                Application.Exit();
            }
            else 
            {                
                try
                {
                    // заполняем данными из конфигурации
                    this.RequiredWorkingTime = int.Parse(ConfigurationSettings.AppSettings.GetValues("TimeWork").First()); /// Значение задержки берем из файла конфигурации
                    this.TimeWithoutWork = int.Parse(ConfigurationSettings.AppSettings.GetValues("TimeWithoutWork").First()); ///Время без работы 
                    this.DecayInterval = int.Parse(ConfigurationSettings.AppSettings.GetValues("DecayInterval").First()); /// Задержка окна оповещения
                    this.widtWindowDeflection = int.Parse(ConfigurationSettings.AppSettings.GetValues("widtWindowDeflection").First()); /// Нормали от нижнего правого края по горизонтали
                    this.heightWindowDeflection = int.Parse(ConfigurationSettings.AppSettings.GetValues("heightWindowDeflection").First()); /// Нормали от нижнего правого края по вертикали 
                }
                catch (ConfigurationException ex) 
                {
                    MessageBox.Show("Внимание в файле конфигурации присутствует ошибка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                }
            }
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
        int RequiredWorkingTime = 0; /// Значение задержки берем из файла конфигурации
        int TimeWithoutWork = 0; ///Время без работы 
        int DecayInterval = 0; /// время затухания окна
        int widtWindowDeflection = 0; /// отклонение от правого нижнего края по горизонтали  
        int heightWindowDeflection = 0; /// отклонение от правого нижнего края по вертикали 
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
            if (X != point.X && Y != point.Y)
            {
                timeWork += timer.Interval;
                timeDonWork = 0;
                if (timeWork > RequiredWorkingTime)
                {
                    MessageWindow mesageWindow = new MessageWindow(DecayInterval,widtWindowDeflection,heightWindowDeflection);
                    mesageWindow.Show();
                    timeWork = 0;
                }
                X = point.X; Y = point.Y;
                timer.Enabled = true;
               
            }
            else
            {
                timeDonWork += timer.Interval;
                if (timeDonWork > TimeWithoutWork)///если не двигал мышкой больше TimeWithoutWork милесекунд то время до следующего отдых сбрасывается.
                { timeWork = 0; } //если долго отдыхать то можно и потерять рабочее время.
                timer.Enabled = true;
            }
        }
        #endregion      
    }
}
