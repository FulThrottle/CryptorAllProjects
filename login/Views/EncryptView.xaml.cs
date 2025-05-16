using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CryptorLogin.Models;

namespace CryptorLogin.Views
{
    public partial class EncryptView : UserControl
    {
        private string selectedFilePath;

        public EncryptView()
        {
            InitializeComponent();
        }

        // Вибір файлу через OpenFileDialog
        private void PickFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Всі файли (*.*)|*.*",
                Title = "Виберіть файл для шифрування"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                FilePathTextBlock.Text = selectedFilePath;
            }
        }

        // Обробник кнопки шифрування
        private void EncryptFile_Click(object sender, RoutedEventArgs e)
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
                string outputFile = Path.Combine(Path.GetDirectoryName(selectedFilePath), "En_" + Path.GetFileName(selectedFilePath));
                FileEncryptor.EncryptFile(selectedFilePath, outputFile, password);
                StatusTextBlock.Text = $"✅ Файл зашифровано: {outputFile}";
                PasswordBox.Clear();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"❌ Помилка: {ex.Message}";
            }
        }
    }
}
