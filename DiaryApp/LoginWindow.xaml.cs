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

        /// <summary>
        /// Zamyka okno logowania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// logika interakcji z hasłem użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

        /// <summary>
        /// ustawienie domyślnie użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginWind_Loaded(object sender, RoutedEventArgs e)
        {
            this.UserCombo.Items.Add(DiaryApp.Properties.Settings.Default.User1Name);
            this.UserCombo.Items.Add(DiaryApp.Properties.Settings.Default.User2Name);
            this.UserCombo.Items.Add(DiaryApp.Properties.Settings.Default.User3Name);

            this.UserCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// ustawia zdjęcie użytkownika wybranego z listy rozwijanej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                /// Ustawienie wybranego obrazu uzytkownika
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
