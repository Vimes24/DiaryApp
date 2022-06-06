using System;
using System.Windows;

namespace DiaryApp
{
    /// <summary>
    /// Logika interakcji dla klasy AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        // Wczytywanie danych wersji aplikacji
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.FileVersionInfo fileVersion;
                string fileName = Environment.CurrentDirectory + "\\DiaryApp.exe";
                fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(fileName);

                this.companyLbl.Content = fileVersion.CompanyName;
                this.productLbl.Content = fileVersion.ProductName;
                this.versionLbl.Content = fileVersion.FileVersion;
                this.webLbl.Content = fileVersion.Comments;
                this.trademarkLbl.Content = fileVersion.LegalTrademarks;
                this.copyrightLbl.Content = fileVersion.LegalCopyright;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
