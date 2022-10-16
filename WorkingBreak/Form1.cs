using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
            try
            {
                // заполняем данными из конфигурации
                this.RequiredWorkingTime = int.Parse(ConfigurationSettings.AppSettings.GetValues("TimeWork").First()); /// Значение задержки берем из файла конфигурации
                this.TimeWithoutWork = int.Parse(ConfigurationSettings.AppSettings.GetValues("TimeWithoutWork").First()); ///Время без работы 
                this.DecayInterval = int.Parse(ConfigurationSettings.AppSettings.GetValues("DecayInterval").First()); /// Задержка окна оповещения
                this.widtWindowDeflection = int.Parse(ConfigurationSettings.AppSettings.GetValues("widtWindowDeflection").First()); /// Нормали от нижнего правого края по горизонтали
                this.heightWindowDeflection = int.Parse(ConfigurationSettings.AppSettings.GetValues("heightWindowDeflection").First()); /// Нормали от нижнего правого края по вертикали 
                Settings.Opacity = double.Parse(ConfigurationSettings.AppSettings.GetValues("Opacity").First().ToString()) / 100;
                Settings.textHeader = ConfigurationSettings.AppSettings.GetValues("textHeder").First().ToString();
                Settings.textBodyFirs = ConfigurationSettings.AppSettings.GetValues("textBodyFirs").First().ToString();
                Settings.textBodySec = ConfigurationSettings.AppSettings.GetValues("textBodySec").First().ToString();
                Settings.textFooter = ConfigurationSettings.AppSettings.GetValues("textFooter").First().ToString();
            }
            catch (ConfigurationException ex)
            {
                Logger.WriteLine("Ошибка чтения конфигурации (" + ex.Message + ")");
                pastSettings();
            }
            catch (Exception ex) 
            {
                Logger.WriteLine("Ошибка чтения конфигурации (" + ex.Message + ")");
                pastSettings();
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
          
            //выход в любом случаее 
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
        /// <summary>/// Таймер.
        /// </summary>
        private void getTimer()
        {
            try
            {
                timer.Interval = 5000;
                timer.Tick += new System.EventHandler(TimerTick);
                timer.Start();
                while (true)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch
            {
                Logger.WriteLine("Ошибка запуска конфигурации");
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
                    try
                    {
                        MessageWindow mesageWindow = new MessageWindow(DecayInterval, widtWindowDeflection, heightWindowDeflection);
                        mesageWindow.Show();
                        timeWork = 0;
                    }
                    catch
                    {
                        Logger.WriteLine("Ошибка формирования окна.");
                    }
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

        #region вставить настройки
        private void pastSettings() 
        {
            this.RequiredWorkingTime = 7200000;
            this.TimeWithoutWork = 180000;
            this.DecayInterval = 10000;
            this.widtWindowDeflection = 5;
            this.heightWindowDeflection = 50;
            Settings.Opacity = 60;
            Settings.textHeader = "Отдохни, если устал.";
            Settings.textBodyFirs = "Сделай зарядку.";
            Settings.textBodySec = "Подумай о море.";
            Settings.textFooter = "Выпей кофе";
        }
        #endregion

    }


    #region Просто пишем логи в папку Temp. C:\Users\Rinat\AppData\Local\Temp

    public static class Logger    {

        /*Логи в строчку*/
        public static void Write(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(Path.GetTempPath()) + @"\WorkingBreakloglog.txt", true))
            {
                sw.Write(DateTime.Now.ToString() + " " + text);
            }
        }

        /*Логи с возвратом коретки*/
        public static void WriteLine(string text)
        {          
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(Path.GetTempPath()) + @"\WorkingBreaklog.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ": " + text);
            }
        }
    }
    #endregion
}
