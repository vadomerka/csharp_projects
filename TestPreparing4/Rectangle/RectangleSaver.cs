using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangle
{
    public static class RectangleSaver
    {
        private const string fileSavePath = "../../../../";
        private static int fileInd = 1;
        public static void SaveOG(RectangleList? rects)
        { 
            if (rects == null) throw new ArgumentNullException();
            List<string> rectLines = new List<string>();
            foreach (Rectangle rect in rects.OGRectangles)
            {
                rectLines.Add(rect.ToString());
            }
            File.WriteAllLines(fileSavePath + $"output-{fileInd}.txt", rectLines.ToArray());
            fileInd++;
        }

        public static void SaveSorted(RectangleList? rects)
        {
            if (rects == null) throw new ArgumentNullException();
            List<string> rectLines = new List<string>();
            foreach (Rectangle rect in rects)
            {
                rectLines.Add(rect.ToString());
            }
            File.WriteAllLines(fileSavePath + $"output-{fileInd}.txt", rectLines.ToArray());
            fileInd++;
        }

        public static void SaveSortedReverse(RectangleList? rects)
        {
            if (rects == null) throw new ArgumentNullException();
            List<string> rectLines = new List<string>();
            var enumer = rects.GetBackEnumerator();
            while (enumer.MoveNext())
            {
                rectLines.Add(enumer.Current.ToString());
            }
            File.WriteAllLines(fileSavePath + $"output-{fileInd}.txt", rectLines.ToArray());
            fileInd++;
        }
    }
}
