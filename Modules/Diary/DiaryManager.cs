using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DNDHelper.Modules.Diary
{
    internal class DiaryManager
    {
        public static List<Note> Notes = new();
        Main main = Main.Instance;
        int SelectedIndex = -1;


        public DiaryManager()
        {
            main.ListBoxNotes.SelectionChanged += ListBoxNotes_SelectionChanged;
            main.diaryTB.TextChanged += DiaryTB_TextChanged;
            main.CreateNote.Click += CreateNote_Click;
            main.DeleteMenuNote.Click += DeleteMenuNote_Click;
        }

        private void DeleteMenuNote_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                string path = Notes[SelectedIndex].Path;
                var messageBox = MessageBox.Show($"Вы хотите удалить: {Notes[SelectedIndex].Name}", "Удаление windows", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBox == MessageBoxResult.Yes)
                {
                    File.Delete(path);
                    LoadNotes();
                }
            }
        }

        private void CreateNote_Click(object sender, RoutedEventArgs e)
        {
            var name = main.NameNote.Text;
            if (name.Length > 0)
            {
                string pathNote = $"{DataManager.SelectedSave}/Notes/{name}.rtf";
                if (File.Exists(pathNote))
                {
                    MessageBox.Show("Название занято", "Эбл", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                RichTextBox rtb = new RichTextBox();
                TextRange range = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd);
                using (FileStream fs = new FileStream(pathNote, FileMode.Create))
                {
                    range.Save(fs, DataFormats.Rtf);
                }
                LoadNotes();
            }
        }

        public static void LoadNotes()
        {
            Main.Instance.ListBoxNotes.Items.Clear();
            Notes.Clear();

            string pathNotes = $"{DataManager.SelectedSave}/Notes";

            if(!Directory.Exists(pathNotes)) 
                Directory.CreateDirectory(pathNotes);

            var notes = Directory.GetFiles(pathNotes);
            foreach (var notePath in notes)
            {
                var noteName = Path.GetFileName(notePath).Replace(".rtf", "");
                int index = Main.Instance.ListBoxNotes.Items.Add(noteName);
                Note note = new Note { Name = noteName, Path = notePath };

                Notes.Add(note);
            }
        }

        public static void SaveFlowDocumentToRtf(FlowDocument flowDoc, string savePath)
        {
            TextRange textRange = new TextRange(
                flowDoc.ContentStart,
                flowDoc.ContentEnd
            );

            using (FileStream fs = new FileStream(savePath, FileMode.Create))
            {
                textRange.Save(fs, DataFormats.Rtf);
            }
        }

        private void LoadRtfFile(string filePath)
        {
            try
            {
                TextRange range = new TextRange(
                     main.diaryTB.Document.ContentStart,
                     main.diaryTB.Document.ContentEnd);

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    range.Load(fs, DataFormats.Rtf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveRtfFile(string filePath)
        {
            try
            {
                TextRange range = new TextRange(
                    main.diaryTB.Document.ContentStart,
                    main.diaryTB.Document.ContentEnd);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    range.Save(fs, DataFormats.Rtf);
                }
            }
            catch
            {

            }
        }

        private void ListBoxNotes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedIndex = Main.Instance.ListBoxNotes.SelectedIndex;
            if (SelectedIndex != -1)
                LoadRtfFile(Notes[SelectedIndex].Path);
        }

        private void DiaryTB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (SelectedIndex != -1)
                SaveRtfFile(Notes[SelectedIndex].Path);
        }
    }

    public class Note
    {
        public string Name { get; set; }

        public FlowDocument Description { get; set; }

        public string Path { get; set; }
    }
}
