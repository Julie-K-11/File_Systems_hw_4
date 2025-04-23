namespace File_Systems_hw_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PoemManager manager = new PoemManager();
            string filePath = "poems.txt";
            manager.LoadFromFile(filePath);

            while (true)
            {
                Console.WriteLine("\n1. Додати вірш");
                Console.WriteLine("2. Видалити вірш");
                Console.WriteLine("3. Змінити вірш");
                Console.WriteLine("4. Пошук вірша");
                Console.WriteLine("5. Зберегти у файл");
                Console.WriteLine("6. Завантажити з файлу");
                Console.WriteLine("7. Звіт за назвою");
                Console.WriteLine("8. Звіт за автором");
                Console.WriteLine("9. Звіт за темою");
                Console.WriteLine("10. Звіт за словом у тексті");
                Console.WriteLine("11. Звіт за роком написання");
                Console.WriteLine("12. Звіт за довжиною вірша");
                Console.WriteLine("13. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": 
                        manager.AddPoem(); break;
                    case "2": 
                        manager.DeletePoem(); break;
                    case "3": 
                        manager.EditPoem(); break;
                    case "4": 
                        manager.SearchPoem(); break;
                    case "5": 
                        manager.SaveToFile(filePath); break;
                    case "6": 
                        manager.LoadFromFile(filePath); break;
                    case "7": 
                        manager.GenerateReport(p => p.Title, "Введфть назву: "); break;
                    case "8": 
                        manager.GenerateReport(p => p.Author, "Введіть автора: "); break;
                    case "9": 
                        manager.GenerateReport(p => p.Theme, "Введіть тему: "); break;
                    case "10":
                        manager.GenerateReport(p => p.Text, "Введіть слово у тексті: "); break;
                    case "11":
                        manager.GenerateNumberReport(p => p.Year, "Введіть рік: "); break;
                    case "12":
                        manager.GenerateNumberReport(p => p.Text.Length, "Введіть довжину символів: "); break;
                    case "13": 
                        return;
                    default: 
                        Console.WriteLine("Такої опції не існує"); break;
                }
            }
        }
    }
}
