using System;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

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

        /// <summary>
        /// Logika interakcji przycisków dla tekstu pisanego w RichTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// ustawia przyciski jako nieaktywne do czasu wyboru nowej notatki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMemoWind_Loaded(object sender, RoutedEventArgs e)
        {
            this.detailsGroupBox.IsEnabled = false;
        }

        /// <summary>
        /// ustawia przyciski jako aktywne lub nieaktywne, w zależności od wybranego przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.detailsGroupBox.IsEnabled = true;
                this.newButt.IsEnabled = false;
                this.saveButt.IsEnabled = true;
                /// czyszczenie tekstu w polach tekstowych i wyborze daty
                this.memoTitleTbx.Text = "";
                this.datePicker.SelectedDate = System.DateTime.Now;
                this.rtc.Document.Blocks.Clear();
                /// Dodawanie kolejnego numeru notatki
                long lastID = DiaryApp.Properties.Settings.Default.LastID + 1;
                this.memoIDTbx.Text = lastID.ToString();

                this.memoTitleTbx.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// zachowanie ostatniego numeru ID w ustawieniach
        /// zapisywanie notatki w pliku tekstowym (utworzenie trzech oddzielnych plików dla nazwy, daty i treści)
        /// zapisywanie notatki w pliku tekstowym (tytuł i data)
        /// zapisywanie notatki w pliku tekstowym (plik rtf)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.detailsGroupBox.IsEnabled = false;
                this.newButt.IsEnabled = true;
                this.saveButt.IsEnabled = false;

                long lastID = DiaryApp.Properties.Settings.Default.LastID + 1;
                DiaryApp.Properties.Settings.Default.LastID = lastID;
                DiaryApp.Properties.Settings.Default.Save();

                string titleFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_title_" + lastID.ToString() + ".txt";
                string dateFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_date_" + lastID.ToString() + ".txt";
                string rtfFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_text_" + lastID.ToString() + ".rtf";

                System.IO.File.WriteAllText(titleFileName, this.memoTitleTbx.Text, Encoding.UTF8);
                System.IO.File.WriteAllText(dateFileName, this.datePicker.Text, Encoding.UTF8);

                TextRange tr = new TextRange(this.rtc.Document.ContentStart, this.rtc.Document.ContentEnd);
                System.IO.FileStream fileStream = new System.IO.FileStream(rtfFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                tr.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
                MessageBox.Show("Saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Użycie klawiszy klawiatury do wybierania opcji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMemoWind_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F2)
                {
                    newButt_Click(sender, e);
                }

                if (e.Key == Key.F5)
                {
                    saveButt_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
