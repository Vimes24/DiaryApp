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

namespace DiaryApp.Memo
{
    /// <summary>
    /// Logika interakcji dla klasy AddMemoWindow.xaml
    /// </summary>
    public partial class AddMemoWindow : Window
    {
        public AddMemoWindow()
        {
            InitializeComponent();
        }

        
        // Logika interakcji przycisków dla tekstu pisanego w RichTextBox
        private void allignLeftButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignLeft.Execute(null, this.rtc);
        }

        private void allignCenterButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignCenter.Execute(null, this.rtc);
        }

        private void allignRightButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignRight.Execute(null, this.rtc);
        }

        private void incIndentButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.IncreaseIndentation.Execute(null, this.rtc);
        }

        private void decIndentButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.DecreaseIndentation.Execute(null, this.rtc);
        }

        private void boldButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBold.Execute(null, this.rtc);
        }

        private void italicButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleItalic.Execute(null, this.rtc);
        }

        private void underlineButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleUnderline.Execute(null, this.rtc);
        }

        private void incSizeButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.IncreaseFontSize.Execute(null, this.rtc);
        }

        private void decSizeButt_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.DecreaseFontSize.Execute(null, this.rtc);
        }

        private void cutButt_Click(object sender, RoutedEventArgs e)
        {
            this.rtc.Cut();
        }

        private void copyButt_Click(object sender, RoutedEventArgs e)
        {
            this.rtc.Copy();
        }

        private void pasteButt_Click(object sender, RoutedEventArgs e)
        {
            this.rtc.Paste();
        }

        private void undoButt_Click(object sender, RoutedEventArgs e)
        {
            this.rtc.Undo();
        }

        private void redoButt_Click(object sender, RoutedEventArgs e)
        {
            this.rtc.Redo();
        }

        // ustawia przyciski jako nieaktywne do czasu wyboru nowej notatki
        private void AddMemoWind_Loaded(object sender, RoutedEventArgs e)
        {
            this.detailsGroupBox.IsEnabled = false;
        }

        // ustawia przyciski jako aktywne lub nieaktywne, w zależności od wybranego przycisku
        private void newButt_Click(object sender, RoutedEventArgs e)
        {
            this.detailsGroupBox.IsEnabled = true;
            this.newButt.IsEnabled = false;
            this.saveButt.IsEnabled = true;
            // czyszczenie tekstu w polach tekstowych i wyborze daty
            this.memoTitleTbx.Text = "";
            this.datePicker.SelectedDate = System.DateTime.Now;
            this.rtc.Document.Blocks.Clear();
            // Dodawanie kolejnego numeru notatki
            long lastID = DiaryApp.Properties.Settings.Default.LastID + 1;
            this.memoIDTbx.Text = lastID.ToString();

            this.memoTitleTbx.Focus();
        }

        private void saveButt_Click(object sender, RoutedEventArgs e)
        {
            this.detailsGroupBox.IsEnabled = false;
            this.newButt.IsEnabled = true;
            this.saveButt.IsEnabled = false;
            // zachowanie ostatniego numeru ID w ustawieniach
            long lastID = DiaryApp.Properties.Settings.Default.LastID + 1;
            DiaryApp.Properties.Settings.Default.LastID = lastID;
            DiaryApp.Properties.Settings.Default.Save();

            // zapisywanie notatki w pliku tekstowym (utworzenie trzech oddzielnych plików dla nazwy, daty i treści)
            string titleFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_title_" + lastID.ToString() + ".txt";
            string dateFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_date_" + lastID.ToString() + ".txt";
            string rtfFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_text_" + lastID.ToString() + ".rtf";

            // zapisywanie notatki w pliku tekstowym (tytuł i data)
            System.IO.File.WriteAllText(titleFileName, this.memoTitleTbx.Text, Encoding.UTF8);
            System.IO.File.WriteAllText(dateFileName, this.datePicker.Text, Encoding.UTF8);

            // zapisywanie notatki w pliku tekstowym (plik rtf)
            TextRange tr = new TextRange(this.rtc.Document.ContentStart, this.rtc.Document.ContentEnd);
            System.IO.FileStream fileStream = new System.IO.FileStream(rtfFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            tr.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
            MessageBox.Show("Saved!");
        }
    }
}
