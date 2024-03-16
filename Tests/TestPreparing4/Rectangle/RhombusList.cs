using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhombus
{
    public class RhombusList : IEnumerable<Rhombus>
    {
        private Rhombus[] _rhombuses = new Rhombus[0];
        private Rhombus[] _og_rhombuses = new Rhombus[0];
        private string _fileName = "";

        public RhombusList() { }

        public RhombusList(Rhombus[] rhombuses)
        {
            _og_rhombuses = rhombuses;
            List<Rhombus> listOfBlacks = new List<Rhombus>(rhombuses);
            listOfBlacks.Sort((Rhombus x, Rhombus y) =>
            {
                if (x.Space() < y.Space()) return -1;
                if (x.Space() == y.Space()) return 0;
                return 1;
            });
            _rhombuses = listOfBlacks.ToArray();
        }

        public RhombusList(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            string[] lines = File.ReadAllLines(fileName);
            List<Rhombus> listOfBlacks = new List<Rhombus>();
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    listOfBlacks.Add(new Rhombus(lines[i]));
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
            _og_rhombuses = listOfBlacks.ToArray();
            listOfBlacks.Sort((Rhombus x, Rhombus y) =>
            {
                if (x.Space() < y.Space()) return -1;
                if (x.Space() == y.Space()) return 0;
                return 1;
            });
            _fileName = fileName;
            _rhombuses = listOfBlacks.ToArray();
        }

        public Rhombus this[int index] { get => _rhombuses[index]; }

        public Rhombus[] OGRhombuses
        {
            get => _og_rhombuses;
        }

        public string FileName
        {
            get => _fileName;
        }

        public IEnumerator<Rhombus> GetEnumerator()
        {
            for (int i = 0; i < _rhombuses.Length; i++)
            {
                yield return _rhombuses[i];
            }
        }

        public IEnumerator<Rhombus> GetBackEnumerator()
        {
            for (int i = _rhombuses.Length - 1; i > -1; i--)
            {
                yield return _rhombuses[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
