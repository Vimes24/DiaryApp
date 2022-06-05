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
using System.Windows.Shapes;

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
    }
}
