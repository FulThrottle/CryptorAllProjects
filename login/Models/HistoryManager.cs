using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CryptorLogin.Models
{
    public static class HistoryManager
    {
        private static readonly string HistoryFilePath = "viewHistory.json";

        public static List<ViewedFile> LoadHistory()
        {
            if (!File.Exists(HistoryFilePath))
                return new List<ViewedFile>();

            var json = File.ReadAllText(HistoryFilePath);
            return JsonSerializer.Deserialize<List<ViewedFile>>(json) ?? new List<ViewedFile>();
        }

        public static void AddToHistory(ViewedFile file)
        {
            var history = LoadHistory();
            history.Insert(0, file); // додаємо в початок для зручності

            var json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(HistoryFilePath, json);
        }
        public static void RegisterFileView(string filePath)
        {
            var viewedFile = new ViewedFile
            {
                FilePath = filePath,
                FileName = Path.GetFileName(filePath),
                ViewedDate = DateTime.Now
            };

            AddToHistory(viewedFile);
        }
        public static void ClearHistory()
        {
            if (File.Exists(HistoryFilePath))
                File.Delete(HistoryFilePath);
        }

    }
}
