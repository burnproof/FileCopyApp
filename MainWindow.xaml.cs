using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using System.Timers;

namespace FileCopyApp
{
    public partial class MainWindow : Window
    {
        private System.Timers.Timer fileExistenceTimer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            fileExistenceTimer = new System.Timers.Timer();
            fileExistenceTimer.Interval = 1000; // Set interval to 1 second
            fileExistenceTimer.Elapsed += OnFileExistenceCheck;
            fileExistenceTimer.Enabled = true;
        }

        private void OnFileExistenceCheck(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateFileExistenceIndicator();
            });
        }

        private void UpdateFileExistenceIndicator()
        {
            /*if (!string.IsNullOrEmpty(InputFileTextBox.Text) && !string.IsNullOrEmpty(OutputFolderTextBox.Text))
            {
                string outputFile = Path.Combine(OutputFolderTextBox.Text, Path.GetFileName(InputFileTextBox.Text));
                bool fileExistsInOutput = File.Exists(outputFile);
                FileExistsIndicator.Fill = fileExistsInOutput ? Brushes.LimeGreen : Brushes.LightGray;
                CheckButtonAvailability();
            }
            else
            {
                FileExistsIndicator.Fill = Brushes.LightGray;
                CheckButtonAvailability();
            }*/
            CheckButtonAvailability();
        }

        private void SelectInputFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                InputFileTextBox.Text = openFileDialog.FileName;
                //CheckButtonAvailability();
            }
        }

        private void SelectOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputFolderTextBox.Text = dialog.FileName;
                //CheckButtonAvailability();
            }
        }

        private void CheckButtonAvailability()
        {
            if (File.Exists(InputFileTextBox.Text) && Directory.Exists(OutputFolderTextBox.Text) && InputFileTextBox.Text.Length > 0 && OutputFolderTextBox.Text.Length > 0)
            {
                string outputFile = Path.Combine(OutputFolderTextBox.Text, Path.GetFileName(InputFileTextBox.Text));
                bool fileExistsInOutput = File.Exists(outputFile);
                FileExistsIndicator.Fill = fileExistsInOutput ? Brushes.LimeGreen : Brushes.LightGray;

                if (fileExistsInOutput)
                {
                    if(OverwriteCheckBox.IsChecked.HasValue && OverwriteCheckBox.IsChecked.Value)
                    {
                        CopyButton.IsEnabled = true;
                        CopyButton.Content = $"Copy File!\nFile Exists";
                        CopyButton.Background = Brushes.LimeGreen;
                    }
                    else
                    {
                        CopyButton.IsEnabled = false;
                        CopyButton.Content = $"File Exists";
                        CopyButton.Background = Brushes.LightGray;
                    }
                }
                else
                {
                    CopyButton.IsEnabled = true;
                    CopyButton.Content = "Copy File";
                    CopyButton.Background = Brushes.LimeGreen;
                    FileExistsIndicator.Fill = Brushes.LightGray;
                }
            }
            else
            {
                CopyButton.IsEnabled = false;
                CopyButton.Content = $"Select Input File\nand Output Folder";
                CopyButton.Background = Brushes.DarkOrange;
                //FileExistsIndicator.Fill = Brushes.LightGray; // Gray out the indicator
            }

            if (File.Exists(InputFileTextBox.Text))
            {
                InputFileTextBox.BorderBrush = Brushes.Gray;
            }
            else
            {
                InputFileTextBox.BorderBrush = Brushes.DarkOrange;
            }

            if (Directory.Exists(OutputFolderTextBox.Text))
            {
                OutputFolderTextBox.BorderBrush = Brushes.Gray;
            }
            else
            {
                OutputFolderTextBox.BorderBrush = Brushes.DarkOrange;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string outputFile = Path.Combine(OutputFolderTextBox.Text, Path.GetFileName(InputFileTextBox.Text));
                if (OverwriteCheckBox.IsChecked.HasValue && OverwriteCheckBox.IsChecked.Value)
                {
                    File.Copy(InputFileTextBox.Text, outputFile, true);
                }
                else
                {
                    if (!File.Exists(outputFile))
                    {
                        File.Copy(InputFileTextBox.Text, outputFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
