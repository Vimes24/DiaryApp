using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DiaryApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Ustawienie zmiany tapety w oknie glownym
        // Utworzenie String imageName jako dowiazanie do nazwy tapety (+1 bo index zaczynamy od 0)
        // Uri imageUri - obiekt odnoszacy sie do lokalizacji danej tapety
        private void ImageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String imageName = Environment.CurrentDirectory + "\\Data\\Pictures\\" + (this.ImageComboBox.SelectedIndex + 1).ToString() + ".jpg";
            Uri imageUri = new Uri(imageName);
            ImageBrush image = new ImageBrush(new BitmapImage(imageUri));
            image.Stretch = Stretch.Fill;
            this.Background = image;
            // Zapisanie zmian w ustawieniach - ostatnio wybrana tapeta pojawi sie przy kolejnym uruchomieniu aplikacji
            DiaryApp.Properties.Settings.Default.LastImage = this.ImageComboBox.SelectedIndex;
            DiaryApp.Properties.Settings.Default.Save();
        }

        // Wczytuje ostatnio zapisana tapete, uruchamia zegar i aktualizuje kalendarz
        private void StartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int fileIndex = DiaryApp.Properties.Settings.Default.LastImage;
            this.ImageComboBox.SelectedIndex = fileIndex;
            // Uruchamianie zegara w aplikacji, czas zmienia sie co 1s
            this.TimeLabel.Content = "...";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += TimeUpdate;
            timer.Start();
            // aktualizacja kalendarza
            this.YearLabel.Content = DateTime.Now.Year.ToString();
            int monthNumber = DateTime.Now.Month;
            if(monthNumber == 1)
            {
                this.MonthLabel.Content = "January";
            }
            if (monthNumber == 2)
            {
                this.MonthLabel.Content = "February";
            }
            if (monthNumber == 3)
            {
                this.MonthLabel.Content = "March";
            }
            if (monthNumber == 4)
            {
                this.MonthLabel.Content = "April";
            }
            if (monthNumber == 5)
            {
                this.MonthLabel.Content = "May";
            }
            if (monthNumber == 6)
            {
                this.MonthLabel.Content = "June";
            }
            if (monthNumber == 7)
            {
                this.MonthLabel.Content = "July";
            }
            if (monthNumber == 8)
            {
                this.MonthLabel.Content = "August";
            }
            if (monthNumber == 9)
            {
                this.MonthLabel.Content = "September";
            }
            if (monthNumber == 10)
            {
                this.MonthLabel.Content = "October";
            }
            if (monthNumber == 11)
            {
                this.MonthLabel.Content = "November";
            }
            if (monthNumber == 12)
            {
                this.MonthLabel.Content = "December";
            }

            this.DayNumberLabel.Content = DateTime.Now.Day.ToString();
            this.DayNameLabel.Content = DateTime.Now.DayOfWeek.ToString();
        }
        // zawartosc etykiety zostala przypisana do aktualnego czasu komputera
        private void TimeUpdate(object sender, EventArgs e)
        {
            this.TimeLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void MinButt_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButt_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Do you want to exit?", "Alert!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }
        
        // Ustawienie przyciskow z prawej strony wraz ze zmiana rozmiaru okna
        private void StartWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(MinButt, TopPanel.ActualWidth - 60);
            Canvas.SetLeft(CloseButt, TopPanel.ActualWidth - 30);

        }
    }
}
