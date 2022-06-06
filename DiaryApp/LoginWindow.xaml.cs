using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DiaryApp
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // zamyka okno logowania
        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OkButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //logika interakcji z hasłem użytkownika
                if (this.UserPassBox.Password == "")
                {
                    MessageBox.Show("Please enter the password!");
                    return;
                }

                string orgPass = "";
                if (UserCombo.SelectedIndex == 0)
                {
                    orgPass = DiaryApp.Properties.Settings.Default.User1Pass;
                }

                if (UserCombo.SelectedIndex == 1)
                {
                    orgPass = DiaryApp.Properties.Settings.Default.User2Pass;
                }

                if (UserCombo.SelectedIndex == 2)
                {
                    orgPass = DiaryApp.Properties.Settings.Default.User3Pass;
                }

                if (this.UserPassBox.Password != orgPass)
                {
                    MessageBox.Show("Wrong password!");
                    return;
                }
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoginWind_Loaded(object sender, RoutedEventArgs e)
        {
            this.UserCombo.Items.Add(DiaryApp.Properties.Settings.Default.User1Name);
            this.UserCombo.Items.Add(DiaryApp.Properties.Settings.Default.User2Name);
            this.UserCombo.Items.Add(DiaryApp.Properties.Settings.Default.User3Name);

            //ustawienie domyślnie użytkownika
            this.UserCombo.SelectedIndex = 0;
        }

        // ustawia zdjęcie użytkownika wybranego z listy rozwijanej
        private void UserCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
    }
}
