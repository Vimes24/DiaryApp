using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace DiaryApp
{
    /// <summary>
    /// Logika interakcji dla klasy UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        // Wczytuje dane uzytkownikow po wyborze z listy rozwijanej + wybiea zdjecie ktore ustawil uzytkownik
        private void UserCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UserCombo.SelectedIndex == 0)
            {
                this.UserNameBox.Text = DiaryApp.Properties.Settings.Default.User1Name;
                this.UserPassBox.Password = DiaryApp.Properties.Settings.Default.User1Pass;
            }

            if (this.UserCombo.SelectedIndex == 1)
            {
                this.UserNameBox.Text = DiaryApp.Properties.Settings.Default.User2Name;
                this.UserPassBox.Password = DiaryApp.Properties.Settings.Default.User2Pass;
            }

            if (this.UserCombo.SelectedIndex == 2)
            {
                this.UserNameBox.Text = DiaryApp.Properties.Settings.Default.User3Name;
                this.UserPassBox.Password = DiaryApp.Properties.Settings.Default.User3Pass;
            }

            String userImageName = Environment.CurrentDirectory + "\\Data\\Pictures\\Users\\" + this.UserCombo.SelectedIndex.ToString() + ".jpg";

            if (File.Exists(userImageName) == false)
            {
                return;
            }
            Uri userImageUri = new Uri(userImageName);

            // Ustawienie wybranego obrazu uzytkownika
            BitmapImage bmUserImage = new BitmapImage();
            bmUserImage.BeginInit();
            bmUserImage.CacheOption = BitmapCacheOption.OnLoad;
            bmUserImage.UriSource = userImageUri;
            bmUserImage.EndInit();

            this.UserImage.Source = bmUserImage;
        }

        // Zmiana danych uzytkownikow
        private void SaveUserDataButt_Click(object sender, RoutedEventArgs e)
        {
            // Zabezpieczenie przed brakiem wyboru uzytkownika
            if (this.UserCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your user!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Sprawdzenie czy poprawnie podano haslo
            if (this.UserPassBox.Password != this.RepearUserPassBox.Password)
            {
                MessageBox.Show("Please enter the same password!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (this.UserCombo.SelectedIndex == 0)
            {
                DiaryApp.Properties.Settings.Default.User1Name = this.UserNameBox.Text;
                DiaryApp.Properties.Settings.Default.User1Pass = this.UserPassBox.Password;
            }

            if (this.UserCombo.SelectedIndex == 1)
            {
                DiaryApp.Properties.Settings.Default.User2Name = this.UserNameBox.Text;
                DiaryApp.Properties.Settings.Default.User2Pass = this.UserPassBox.Password;
            }

            if (this.UserCombo.SelectedIndex == 2)
            {
                DiaryApp.Properties.Settings.Default.User3Name = this.UserNameBox.Text;
                DiaryApp.Properties.Settings.Default.User3Pass = this.UserPassBox.Password;
            }

            DiaryApp.Properties.Settings.Default.Save();

            String userNewImage = Environment.CurrentDirectory + "\\Data\\Pictures\\Users\\" + this.UserCombo.SelectedIndex.ToString() + ".jpg";

            if (UserImage.Source == null)
            {
                MessageBox.Show("Data saved!", "Data confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            JpegBitmapEncoder jpEncoder = new JpegBitmapEncoder();
            BitmapSource bmSource = (BitmapSource)this.UserImage.Source;
            jpEncoder.Frames.Add(BitmapFrame.Create(bmSource));
            using (FileStream fs = new FileStream(userNewImage, FileMode.Create))
            {
                jpEncoder.Save(fs);
            }

            MessageBox.Show("Data saved!", "Data confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SelectImageButt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files|*.jpg|PNG files|*.png";
            openFile.Title = "Select the user image";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFile.ShowDialog();

            if (openFile.Title == "")
            {
                return;
            }

            String userImageName;
            userImageName = openFile.Title;
            Uri userImageUri = new Uri(userImageName);
            
            // Ustawienie wybranego obrazu i jego linku - podstawienie zdjecia uzytkownika
            BitmapImage bmUserImage = new BitmapImage();
            bmUserImage.BeginInit();
            bmUserImage.CacheOption = BitmapCacheOption.OnLoad;
            bmUserImage.UriSource = userImageUri;
            bmUserImage.EndInit();

            UserImage.Source = bmUserImage;
        }

        // Ustawienie domyslnego uzytkownika nr 1
        private void UsersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.UserCombo.SelectedIndex = 0;
        }
    }
}
