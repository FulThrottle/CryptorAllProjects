using CryptorLogin.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CryptorLogin.Views
{
    public partial class DecryptView : UserControl
    {
        private string selectedFilePath;

        public DecryptView()
        {
            InitializeComponent();
        }

        // Вибір файлу через OpenFileDialog
        private void PickFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Всі файли (*.*)|*.*",// поправити на всі файли
                Title = "Виберіть зашифрований файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                FilePathTextBlock.Text = selectedFilePath;
            }
        }

        // Обробник кнопки розшифрування
        private void DecryptFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                StatusTextBlock.Text = "❌ Будь ласка, виберіть файл!";
                return;
            }

            string password = PasswordBox.Password;
            if (string.IsNullOrEmpty(password))
            {
                StatusTextBlock.Text = "❌ Введіть пароль!";
                return;
            }
            

            try
            {
                //string outputFile = Path.Combine(Path.GetDirectoryName(selectedFilePath), "De_" + Path.GetFileNameWithoutExtension(selectedFilePath) + ".txt");
                string outputFile = Path.Combine(Path.GetDirectoryName(selectedFilePath), "De_" + 
                    Path.GetFileNameWithoutExtension(selectedFilePath) + Path.GetExtension(selectedFilePath));

                FileEncryptor.DecryptFile(selectedFilePath, outputFile, password);
                StatusTextBlock.Text = $"✅ Файл розшифровано: {outputFile}";
                PasswordBox.Clear();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"❌ Помилка: {ex.Message}";
            }
        }
    }
}
