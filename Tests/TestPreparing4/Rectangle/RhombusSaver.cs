using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhombus
{
    public static class RhombusSaver
    {
        private const string fileSavePath = "../../../../";
        private static int fileInd = 1;

        private static void SaveArray(List<string> lines)
        {
            if (lines == null || lines.Count == 0) return;
            string fullpath = fileSavePath + $"output-{fileInd}.txt";
            if (File.Exists(fullpath))
            {
                File.AppendAllLines(fullpath, lines);
            }
            else
            {
                File.WriteAllLines(fullpath, lines);
            }
            fileInd++;
        }

        public static void SaveOG(RhombusList? rhombs)
        { 
            if (rhombs == null) throw new ArgumentNullException();
            List<string> rombLines = new List<string>();
            foreach (Rhombus romb in rhombs.OGRhombuses)
            {
                rombLines.Add(romb.ToString());
            }
            SaveArray(rombLines);
        }

        public static void SaveSorted(RhombusList? rombs)
        {
            if (rombs == null) throw new ArgumentNullException();
            List<string> rombLines = new List<string>();
            foreach (Rhombus romb in rombs)
            {
                rombLines.Add(romb.ToString());
            }
            SaveArray(rombLines);
        }

        public static void SaveSortedReverse(RhombusList? rombs)
        {
            if (rombs == null) throw new ArgumentNullException();
            List<string> rombLines = new List<string>();
            var enumer = rombs.GetBackEnumerator();
            while (enumer.MoveNext())
            {
                rombLines.Add(enumer.Current.ToString());
            }
            SaveArray(rombLines);
        }
    }
}
