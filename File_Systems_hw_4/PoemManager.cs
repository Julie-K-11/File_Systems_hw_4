using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Systems_hw_4
{
    public class PoemManager
    {
        private List<Poem> poems = new List<Poem>();

        public void AddPoem()
        {
            var poem = new Poem();

            Console.Write("Назва: ");
            poem.Title = Console.ReadLine();

            Console.Write("Автор: ");
            poem.Author = Console.ReadLine();

            poem.Year = ReadYear("Рік: ");

            Console.Write("Тема: ");
            poem.Theme = Console.ReadLine();

            Console.WriteLine("Текст:");
            poem.Text = Console.ReadLine();

            poems.Add(poem);
            Console.WriteLine("Вірш додано");
        }

        public void DeletePoem()
        {
            Console.Write("Введіть назву вірша для видалення: ");
            string title = Console.ReadLine();

            var poem = poems.FirstOrDefault(p => p.Title.ToLower() == title.ToLower());
            if (poem != null)
            {
                poems.Remove(poem);
                Console.WriteLine("Вірш видалено");
            }
            else
            {
                Console.WriteLine("Вірш не знайдено");
            }
        }

        public void EditPoem()
        {
            Console.Write("Введіть назву вірша для редагування: ");
            string title = Console.ReadLine();

            var poem = poems.FirstOrDefault(p => p.Title.ToLower() == title.ToLower());
            if (poem == null)
            {
                Console.WriteLine("Вірш не знайдено");
                return;
            }

            Console.Write("Нова назва (залиште порожнім): ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle)) poem.Title = newTitle;

            Console.Write("Новий автор (залиште порожнім): ");
            string newAuthor = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAuthor)) poem.Author = newAuthor;

            int newYear = ReadYear("Новий рік (0 - не змінювати): ");
            if (newYear != 0) poem.Year = newYear;

            Console.Write("Нова тема (залиште порожнім): ");
            string newTheme = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTheme)) poem.Theme = newTheme;

            Console.Write("Новий текст (залиште порожнім): ");
            string newText = Console.ReadLine();
            if (!string.IsNullOrEmpty(newText)) poem.Text = newText;

            Console.WriteLine("Вірш було оновлено");
        }

        public void SearchPoem()
        {
            Console.Write("Введіть пошуковий запит: ");
            string query = Console.ReadLine().ToLower();

            var found = poems.Where(p =>
                p.Title.ToLower().Contains(query) ||
                p.Author.ToLower().Contains(query) ||
                p.Theme.ToLower().Contains(query) ||
                p.Text.ToLower().Contains(query) ||
                p.Year.ToString().Contains(query)
            ).ToList();

            if (found.Count == 0)
            {
                Console.WriteLine("Вірші не знайдено");
                return;
            }

            foreach (var poem in found)
                Console.WriteLine(poem);
        }

        public void SaveToFile(string path)
        {
            var lines = poems.Select(p => p.ToFileFormat());
            File.WriteAllLines(path, lines);
            Console.WriteLine("Вірші було збережено");
        }

        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не знайдено");
                return;
            }

            var lines = File.ReadAllLines(path);
            poems = lines.Select(Poem.FromFileFormat).Where(p => p != null).ToList();
            Console.WriteLine("Вірші було завантажено");
        }

        public void GenerateReport(Func<Poem, string> selector, string prompt)
        {
            Console.Write(prompt);
            string query = Console.ReadLine().ToLower();

            OptionallySaveReport(
                poems.Where(p => selector(p).ToLower().Contains(query)).ToList()
            );
        }

        public void GenerateNumberReport(Func<Poem, int> selector, string prompt)
        {
            Console.Write(prompt);
            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.WriteLine("Некоректне число");
                return;
            }

            OptionallySaveReport(
                poems.Where(p => selector(p) <= value).ToList()
            );
        }


        private void OptionallySaveReport(List<Poem> filteredPoems)
        {
            if (filteredPoems.Count == 0)
            {
                Console.WriteLine("Вірш не знайдено");
                return;
            }

            Console.WriteLine($"\n{"Звіт"}");
            foreach (var poem in filteredPoems)
            {
                Console.WriteLine(poem);
            }

            AskToSaveReportToFile(filteredPoems);
        }

        public void AskToSaveReportToFile(List<Poem> result)
        {
            Console.Write("\nЗберегти звіт у файл? (так/ні): ");
            string saveOption = Console.ReadLine().ToLower();

            if (saveOption == "так" || saveOption == "ТАК" || saveOption == "Так")
            {
                Console.Write("Введіть назву файлу для збереження: ");
                string reportName = Console.ReadLine();
                SaveReportToFile(reportName, result);
            }
        }


        public void SaveReportToFile(string reportName, List<Poem> result)
        {
            string filePath = $"{reportName}_report.txt";
            var reportLines = result.Select(r => r.ToString());
            File.WriteAllLines(filePath, reportLines);
            Console.WriteLine($"Звіт збережено в файл: {filePath}");
        }

        private int ReadYear(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                    return value;
                Console.WriteLine("Некоректне число");
            }
        }
    }
}
