using Microsoft.Win32;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.XlsIO;
using Syncfusion.Windows.Shared;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Spreadsheet;
using Syncfusion.Windows.Controls.RichTextBoxAdv;
using CryptorLogin.Models;

namespace CryptorLogin.Views
{
    public partial class OptionsView : UserControl
    {
        private string selectedFilePath="";

        public OptionsView()
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
                selectedFilePath = openFileDialog.FileName;
                LoadDocument(Path.GetExtension(selectedFilePath).ToLower(), File.ReadAllBytes(selectedFilePath));
                HistoryManager.RegisterFileView(selectedFilePath);
            }
        }

        private void LoadDocument(string fileExtension, byte[] data)
        {
            ViewerContainer.Children.Clear();

            if (fileExtension == ".pdf")
            {
                LoadPdf(data);
            }
            else if (fileExtension == ".txt")
            {
                LoadTxt(data);
            }
            else if (fileExtension == ".xlsx" || fileExtension == ".xls")
            {
                LoadExcel(data);
            }
            else if (fileExtension == ".docx" || fileExtension == ".doc")
            {
                LoadWord(data);
            }
            else
            {
                MessageBox.Show("Цей тип файлу не підтримується!");
            }
        }

        private void LoadPdf(byte[] pdfData)
        {
            PdfViewerControl pdfViewer = new PdfViewerControl
            {
                ShowToolbar = true // Відображаємо панель інструментів
            };

            try
            {
                using (MemoryStream ms = new MemoryStream(pdfData))
                {
                    // Створюємо новий потік, що підтримує пошук (CanSeek)
                    MemoryStream pdfStream = new MemoryStream(ms.ToArray());

                    if (pdfStream.CanSeek) // Переконуємося, що потік можна читати
                    {
                        pdfViewer.Load(pdfStream); // Завантажуємо PDF
                    }
                    else
                    {
                        MessageBox.Show("Помилка: Потік PDF не підтримує пошук!");
                        return;
                    }
                }

                pdfViewer.Loaded += (s, e) => pdfViewer.ZoomTo(50); // Встановлюємо масштаб

                ViewerContainer.Children.Clear();
                ViewerContainer.Children.Add(pdfViewer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відкриття PDF: {ex.Message}");
            }
        }

        private void LoadTxt(byte[] txtData)
        {
            string textContent = Encoding.UTF8.GetString(txtData);
            TextBox textViewer = new TextBox
            {
                Text = textContent,
                IsReadOnly = true,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                Background = System.Windows.Media.Brushes.Black,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            };
            ViewerContainer.Children.Add(textViewer);
        }
        private void LoadExcel(byte[] excelData)
        {
            SfSpreadsheet excelViewer = new SfSpreadsheet();

            using (MemoryStream ms = new MemoryStream(excelData))
            {
                try
                {
                    excelViewer.Open(ms);
                    excelViewer.HorizontalAlignment= System.Windows.HorizontalAlignment.Stretch;
                    excelViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    ViewerContainer.Children.Clear();
                    ViewerContainer.Children.Add(excelViewer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка відкриття Excel: {ex.Message}");
                }
            }
        }

        private void LoadWord(byte[] wordData)
        {
            SfRichTextBoxAdv wordViewer = new SfRichTextBoxAdv();
            using (MemoryStream ms = new MemoryStream(wordData))
            {
                wordViewer.Load(ms, Syncfusion.Windows.Controls.RichTextBoxAdv.FormatType.Docx);
                //wordViewer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                //wordViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                //wordViewer.LayoutType = Syncfusion.Windows.Controls.RichTextBoxAdv.LayoutType.Pages;
                wordViewer.ZoomFactor = 70.0f;

                ViewerContainer.Children.Add(wordViewer);
            }
        }
    }
}