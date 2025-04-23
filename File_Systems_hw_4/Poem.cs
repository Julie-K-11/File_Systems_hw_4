using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Systems_hw_4
{
    public class Poem
    {
        public string Title;
        public string Author;
        public int Year;
        public string Text;
        public string Theme;

        public override string ToString()
        {
            return $"Назва: {Title}\nАвтор: {Author}\nРік: {Year}\nТема: {Theme}\nТекст: {Text}\n";
        }

        public string ToFileFormat()
        {
            return $"{Title}|{Author}|{Year}|{Theme}|{Text.Replace("\n", "\\n")}";
        }

        public static Poem FromFileFormat(string line)
        {
            var parts = line.Split('|');
            if (parts.Length != 5) return null;

            if (!int.TryParse(parts[2], out int year)) return null;

            return new Poem
            {
                Title = parts[0],
                Author = parts[1],
                Year = year,
                Theme = parts[3],
                Text = parts[4].Replace("\\n", "\n")
            };
        }
    }
}
