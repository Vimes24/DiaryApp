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
        }
    }
}
