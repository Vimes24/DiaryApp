using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DiaryApp.Memo
{
    /// <summary>
    /// Logika interakcji dla klasy SearchMemoWindow.xaml
    /// </summary>
    public partial class SearchMemoWindow : Window
    {
        public SearchMemoWindow()
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
        /// Czyści listę notatek po zmianach (usuwa nieaktualne)
        /// Załadowanie notatek do okna wyszukiwania
        /// Sprawdza czy plik istnieje, jeżeli tak to dodaje do okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchMemoWind_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                this.MemoListBox.Items.Clear();

                long li = DiaryApp.Properties.Settings.Default.LastID;
                string titleFileName = "";
                for (int i = 1; i <= li; i++)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Tag = i;
                    titleFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_title_" + i.ToString() + ".txt";

                    if (System.IO.File.Exists(titleFileName) == true)
                    {
                        item.Content = System.IO.File.ReadAllText(titleFileName, Encoding.UTF8);
                        this.MemoListBox.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Kod do aktualizacji danych w polach informacyjnych w oknie (zmiana każdorazowo przy wyborze kolejnej notatki)
        /// Sprawdza czy index notatki nie jest równy -1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.MemoListBox.SelectedIndex == -1)
                {
                    return;
                }

                ListBoxItem li = new ListBoxItem();
                int selInd = this.MemoListBox.SelectedIndex;
                li = (ListBoxItem)this.MemoListBox.Items[selInd];
                this.memoTitleTbx.Text = li.Content.ToString();
                this.memoIDTbx.Text = li.Tag.ToString();
                string dateFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_date_" + li.Tag.ToString() + ".txt";
                if (System.IO.File.Exists(dateFileName) == true)
                {
                    this.datePicker.Text = System.IO.File.ReadAllText(dateFileName, Encoding.UTF8);
                }
                string rtfFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_text_" + li.Tag.ToString() + ".rtf";
                TextRange tr = new TextRange(this.rtc.Document.ContentStart, this.rtc.Document.ContentEnd);
                System.IO.FileStream fileStream = new System.IO.FileStream(rtfFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                tr.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Logika wyszukiwania notatek po tytule
        /// Czyszczenie wyświetlanych danych
        /// Zmienia każdorazowo kolor przy kolejnych wyszukiwaniach
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchTitleButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.MemoListBox.SelectedIndex = -1;
                this.memoIDTbx.Text = "";
                this.memoTitleTbx.Text = "";
                this.datePicker.Text = "";
                this.rtc.Document.Blocks.Clear(); 
                ListBoxItem li = new ListBoxItem();

                for (int i = 0; i < this.MemoListBox.Items.Count; i++)
                {
                    li = (ListBoxItem)this.MemoListBox.Items[i];
                    li.Background = Brushes.White;
                    if (this.similarCheckBox.IsChecked == false)
                    {
                        if (li.Content.ToString() == this.searchTitleTbx.Text)
                        {
                            li.Background = Brushes.Yellow;
                        }
                    }

                    if (this.similarCheckBox.IsChecked == true)
                    {
                        if (li.Content.ToString().Contains(this.searchTitleTbx.Text) == true)
                        {
                            li.Background = Brushes.Yellow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Logika aktualizowania notatek wybranych przez użytkownika
        /// Sprawdzenie czy nie próbujemy aktualizować notatki bez nr ID
        /// Zapisywanie notatki w pliku tekstowym (utworzenie trzech oddzielnych plików dla nazwy, daty i treści)
        /// Zapisywanie notatki w pliku tekstowym (tytuł i data)
        /// Usuwa starą notatkę i zastępuje ją zaktualizowaną na wypadek użycia nieobsługiwanych znaków w treści
        /// Zapisywanie notatki w pliku tekstowym (plik rtf)
        /// Odświeżanie okna po aktualizacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long lastID;
                long.TryParse(this.memoIDTbx.Text, out lastID);

                if (lastID == 0 || this.MemoListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Wrong ID number!");
                    return;
                }
 
                string titleFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_title_" + lastID.ToString() + ".txt";
                string dateFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_date_" + lastID.ToString() + ".txt";
                string rtfFileName = Environment.CurrentDirectory + "\\Data\\Docs\\Memo_text_" + lastID.ToString() + ".rtf";

                
                System.IO.File.WriteAllText(titleFileName, this.memoTitleTbx.Text, Encoding.UTF8);
                System.IO.File.WriteAllText(dateFileName, this.datePicker.Text, Encoding.UTF8);

                
                if (System.IO.File.Exists(rtfFileName) == true)
                {
                    System.IO.File.Delete(rtfFileName);
                }
                
                TextRange tr = new TextRange(this.rtc.Document.ContentStart, this.rtc.Document.ContentEnd);
                System.IO.FileStream fileStream = new System.IO.FileStream(rtfFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                tr.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
                
                SearchMemoWind_Loaded(sender, e);
                MessageBox.Show("Saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
