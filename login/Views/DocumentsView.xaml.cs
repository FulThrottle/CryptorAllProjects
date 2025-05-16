using Microsoft.Win32;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Windows.Controls.RichTextBoxAdv;
using Syncfusion.UI.Xaml.Spreadsheet;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CryptorLogin.Models;

namespace CryptorLogin.Views
{
    public partial class DocumentsView : UserControl
    {
        private string selectedFilePath = "";
        private byte[] currentFileData;

        public DocumentsView()
        {
            InitializeComponent();
        }

        private void PickFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*",
                Title = "Виберіть файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                HistoryManager.RegisterFileView(selectedFilePath);
                selectedFilePath = openFileDialog.FileName;
                string ext = Path.GetExtension(selectedFilePath).ToLower();
                byte[] fileData = File.ReadAllBytes(selectedFilePath);

                if (!TryOpenDocument(ext, fileData))
                {
                    string password = PromptForPassword();

                    try
                    {
                        byte[] decryptedData = FileEncryptor.DecryptFileToMemory(selectedFilePath, password);
                        string decryptedFileExt = DetermineFileType(decryptedData);

                        if (!TryOpenDocument(decryptedFileExt, decryptedData))
                            MessageBox.Show("Невірний пароль або файл пошкоджено.");
                    }
                    catch
                    {
                        MessageBox.Show("Невірний пароль або файл пошкоджено.");
                    }
                }
            }
        }

        private bool TryOpenDocument(string ext, byte[] data)
        {
            ViewerContainer.Children.Clear();
            DecryptBtn.Visibility = Visibility.Collapsed;

            try
            {
                switch (ext)
                {
                    case ".pdf": return LoadPdf(data);
                    case ".txt": return TryOpenTxt(data);
                    case ".xls":
                    case ".xlsx": LoadExcel(data); break;
                    case ".doc":
                    case ".docx": LoadWord(data); break;
                    default: return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool LoadPdf(byte[] pdfData)
        {
            PdfViewerControl pdfViewer = new PdfViewerControl { ShowToolbar = true };

            try
            {
                MemoryStream pdfStream = new MemoryStream(pdfData);
                pdfViewer.Load(pdfStream);

                if (pdfViewer.PageCount == 0)
                    throw new Exception("PDF має 0 сторінок.");

                pdfViewer.Loaded += (s, e) => pdfViewer.ZoomTo(50);

                ViewerContainer.Children.Add(pdfViewer);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryOpenTxt(byte[] txtData)
        {
            currentFileData = txtData;
            string textContent = Encoding.UTF8.GetString(txtData);

            TextBox textViewer = new TextBox
            {
                Text = textContent,
                IsReadOnly = true,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                Background = Brushes.Black,
                Foreground = Brushes.White,
                FontSize = 14
            };

            ViewerContainer.Children.Add(textViewer);
            DecryptBtn.Visibility = Visibility.Visible;

            return true;
        }

        private void LoadExcel(byte[] excelData)
        {
            SfSpreadsheet excelViewer = new SfSpreadsheet();
            using MemoryStream ms = new MemoryStream(excelData);
            excelViewer.Open(ms);
            ViewerContainer.Children.Add(excelViewer);
        }

        private void LoadWord(byte[] wordData)
        {
            SfRichTextBoxAdv wordViewer = new SfRichTextBoxAdv();
            using MemoryStream ms = new MemoryStream(wordData);
            wordViewer.Load(ms, Syncfusion.Windows.Controls.RichTextBoxAdv.FormatType.Docx);
            wordViewer.ZoomFactor = 70.0f;
            ViewerContainer.Children.Add(wordViewer);
        }

        private string DetermineFileType(byte[] data)
        {
            if (data.Length > 4)
            {
                if (data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46)
                    return ".pdf";
                if (data[0] == 0x50 && data[1] == 0x4B)
                    return ".docx";

                string decodedText = Encoding.UTF8.GetString(data);
                if (decodedText.All(c => c >= 9 && c <= 255))
                    return ".txt";
            }

            throw new Exception("Неможливо визначити тип файлу після дешифрування.");
        }

        private void DecryptText_Click(object sender, RoutedEventArgs e)
        {
            string password = PromptForPassword();

            try
            {
                byte[] decryptedData = FileEncryptor.DecryptFileToMemory(selectedFilePath, password);
                string decryptedText = Encoding.UTF8.GetString(decryptedData);

                ViewerContainer.Children.Clear();

                TextBox textViewer = new TextBox
                {
                    Text = decryptedText,
                    IsReadOnly = true,
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    Background = Brushes.Black,
                    Foreground = Brushes.White,
                    FontSize = 14
                };

                ViewerContainer.Children.Add(textViewer);
            }
            catch
            {
                MessageBox.Show("Невірний пароль або файл пошкоджено.");
            }
        }

        private string PromptForPassword()
        {
            Window pwdWindow = new Window
            {
                Title = "Введіть пароль",
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow,
            };

            PasswordBox pwdBox = new PasswordBox { Margin = new Thickness(10) };
            Button btnOk = new Button { Content = "ОК", Width = 70, Margin = new Thickness(10), IsDefault = true };
            btnOk.Click += (s, e) => pwdWindow.DialogResult = true;

            StackPanel panel = new StackPanel { Orientation = Orientation.Vertical };
            panel.Children.Add(pwdBox);
            panel.Children.Add(btnOk);

            pwdWindow.Content = panel;

            if (pwdWindow.ShowDialog() == true)
                return pwdBox.Password;

            throw new OperationCanceledException("Введення паролю скасовано.");
        }
    }
}




//using Microsoft.Win32;
//using Syncfusion.Windows.PdfViewer;
//using Syncfusion.Windows.Controls.RichTextBoxAdv;
//using Syncfusion.UI.Xaml.Spreadsheet;
//using System;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using CryptorLogin.Models;

//namespace CryptorLogin.Views
//{
//    public partial class DocumentsView : UserControl
//    {
//        private string selectedFilePath = "";

//        public DocumentsView()
//        {
//            InitializeComponent();
//        }

//        private void PickFile_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog
//            {
//                Filter = "All Files (*.*)|*.*",
//                Title = "Виберіть файл"
//            };

//            if (openFileDialog.ShowDialog() == true)
//            {
//                selectedFilePath = openFileDialog.FileName;
//                string ext = Path.GetExtension(selectedFilePath).ToLower();
//                byte[] fileData = File.ReadAllBytes(selectedFilePath);

//                if (!TryOpenDocument(ext, fileData))
//                {
//                    string password = PromptForPassword();

//                    try
//                    {
//                        byte[] decryptedData = FileEncryptor.DecryptFileToMemory(selectedFilePath, password);
//                        string decryptedFileExt = DetermineFileType(decryptedData);

//                        if (!TryOpenDocument(decryptedFileExt, decryptedData))
//                            MessageBox.Show("Невірний пароль або файл пошкоджено.");
//                    }
//                    catch
//                    {
//                        MessageBox.Show("Невірний пароль або файл пошкоджено.");
//                    }
//                }
//            }
//        }

//        private bool TryOpenDocument(string ext, byte[] data)
//        {
//            ViewerContainer.Children.Clear();

//            try
//            {
//                switch (ext)
//                {
//                    case ".pdf": return LoadPdf(data);
//                    case ".txt": return TryOpenTxt(data);
//                    case ".xls":
//                    case ".xlsx": LoadExcel(data); break;
//                    case ".doc":
//                    case ".docx": LoadWord(data); break;
//                    default: return false;
//                }

//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        private bool LoadPdf(byte[] pdfData)
//        {
//            PdfViewerControl pdfViewer = new PdfViewerControl { ShowToolbar = true };

//            try
//            {
//                MemoryStream pdfStream = new MemoryStream(pdfData);
//                pdfViewer.Load(pdfStream);

//                if (pdfViewer.PageCount == 0)
//                    throw new Exception("PDF має 0 сторінок.");

//                pdfViewer.Loaded += (s, e) => pdfViewer.ZoomTo(50);

//                ViewerContainer.Children.Add(pdfViewer);

//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        private bool TryOpenTxt(byte[] txtData)
//        {
//            string textContent = Encoding.UTF8.GetString(txtData);
//            if (IsTextLikelyEncrypted(textContent))
//                return false;

//            TextBox textViewer = new TextBox
//            {
//                Text = textContent,
//                IsReadOnly = true,
//                TextWrapping = System.Windows.TextWrapping.Wrap,
//                Background = Brushes.Black,
//                Foreground = Brushes.White,
//                FontSize = 14
//            };

//            ViewerContainer.Children.Add(textViewer);
//            return true;
//        }

//        private void LoadExcel(byte[] excelData)
//        {
//            SfSpreadsheet excelViewer = new SfSpreadsheet();
//            using MemoryStream ms = new MemoryStream(excelData);
//            excelViewer.Open(ms);
//            ViewerContainer.Children.Add(excelViewer);
//        }

//        private void LoadWord(byte[] wordData)
//        {
//            SfRichTextBoxAdv wordViewer = new SfRichTextBoxAdv();
//            using MemoryStream ms = new MemoryStream(wordData);
//            wordViewer.Load(ms, Syncfusion.Windows.Controls.RichTextBoxAdv.FormatType.Docx);
//            ViewerContainer.Children.Add(wordViewer);
//        }

//        private bool IsTextLikelyEncrypted(string text)
//        {
//            int printableChars = text.Count(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c));
//            double nonPrintableRatio = (double)(text.Length - printableChars) / text.Length;
//            return nonPrintableRatio > 0.3;
//        }

//        private string DetermineFileType(byte[] data)
//        {
//            if (data.Length > 4)
//            {
//                if (data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46)
//                    return ".pdf";
//                if (data[0] == 0x50 && data[1] == 0x4B)
//                    return ".docx";

//                string decodedText = Encoding.UTF8.GetString(data);
//                if (decodedText.All(c => c >= 9 && c <= 255))
//                    return ".txt";
//            }

//            throw new Exception("Неможливо визначити тип файлу після дешифрування.");
//        }

//        private string PromptForPassword()
//        {
//            Window pwdWindow = new Window
//            {
//                Title = "Введіть пароль",
//                Width = 300,
//                Height = 150,
//                WindowStartupLocation = WindowStartupLocation.CenterOwner,
//                ResizeMode = ResizeMode.NoResize,
//                WindowStyle = WindowStyle.ToolWindow,
//            };

//            PasswordBox pwdBox = new PasswordBox { Margin = new Thickness(10) };
//            Button btnOk = new Button { Content = "ОК", Width = 70, Margin = new Thickness(10), IsDefault = true };
//            btnOk.Click += (s, e) => pwdWindow.DialogResult = true;

//            StackPanel panel = new StackPanel { Orientation = Orientation.Vertical };
//            panel.Children.Add(pwdBox);
//            panel.Children.Add(btnOk);

//            pwdWindow.Content = panel;

//            if (pwdWindow.ShowDialog() == true)
//                return pwdBox.Password;

//            throw new OperationCanceledException("Введення паролю скасовано.");
//        }
//    }
//}