using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangle
{
    public class RectangleList : IEnumerable<Rectangle>
    {
        private Rectangle[] _rectangles = new Rectangle[0];
        private Rectangle[] _og_rectangles = new Rectangle[0];
        private string _fileName = "";

        public RectangleList() { }

        public RectangleList(Rectangle[] rectangles)
        {
            _og_rectangles = rectangles;
            List<Rectangle> listOfBlacks = new List<Rectangle>(rectangles);
            listOfBlacks.Sort((Rectangle x, Rectangle y) =>
            {
                if (x.Perimetre() < y.Perimetre()) return -1;
                if (x.Perimetre() == y.Perimetre()) return 0;
                return 1;
            });
            _rectangles = listOfBlacks.ToArray();
        }

        public RectangleList(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            string[] lines = File.ReadAllLines(fileName);
            List<Rectangle> listOfBlacks = new List<Rectangle>();
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    listOfBlacks.Add(new Rectangle(lines[i]));
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Ошибка считывания в строке {i}: {lines[i]}");
                    Console.WriteLine($"Неверный формат файла.");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"Ошибка считывания в строке {i}: {lines[i]}");
                    Console.WriteLine($"Файл содержит пустые строки.");
                }
            }
            _og_rectangles = listOfBlacks.ToArray();
            listOfBlacks.Sort((Rectangle x, Rectangle y) =>
            {
                if (x.Perimetre() < y.Perimetre()) return -1;
                if (x.Perimetre() == y.Perimetre()) return 0;
                return 1;
            });
            _fileName = fileName;
            _rectangles = listOfBlacks.ToArray();
        }

        public Rectangle this[int index] { get => _rectangles[index]; }

        public Rectangle[] OGRectangles
        {
            get => _og_rectangles;
        }

        public string FileName
        {
            get => _fileName;
        }

        public IEnumerator<Rectangle> GetEnumerator()
        {
            for (int i = 0; i < _rectangles.Length; i++)
            {
                yield return _rectangles[i];
            }
        }

        public IEnumerator<Rectangle> GetBackEnumerator()
        {
            for (int i = _rectangles.Length - 1; i > -1; i--)
            {
                yield return _rectangles[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
