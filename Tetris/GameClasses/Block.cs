using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses
{
    public class Block
    {
        private int speed = 1;
        private int speedBuffer = 0;
        private int maxSpeedBuffer = 60;
        private int[,] blockMap = new int[3, 3];

        private int _x = 0;
        private int _y = 0;

        public Block() { }

        public Block(int x, int y)
        {
            this._x = x;
            this._y = y;
            blockMap[1, 1] = 1;
        }

        public int[,] GetBlockMap() { return blockMap; }

        public void Update(int fps)
        {
            maxSpeedBuffer = fps;
            speedBuffer += speed;
            if (speedBuffer >= maxSpeedBuffer)
            {
                this._y += 1;
            }
        }

        public bool CollideCheck(int[,] map)
        {
            List<Tuple<int, int>> bottomPixels = this.GetBottom();
            foreach (var pix in bottomPixels)
            {
                int bY = pix.Item1;
                int bX = pix.Item2;
                if (map[bY + 1, bX] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Tuple<int, int>> GetBottom()
        {
            List<Tuple<int, int>> bottomPixels = new List<Tuple<int, int>>();
            int h = blockMap.GetLength(0);
            int w = blockMap.GetLength(1);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i + 1 == h || blockMap[i + 1, j] == 0)
                    { 
                        bottomPixels.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return bottomPixels;
        }

        public int[,] Draw(int[,] map)
        {
            int[,] newMap = ArrayFunctions.Copy(map);
            for (int i = 0; i < blockMap.GetLength(0); i++)
            {
                for (int j = 0; j < blockMap.GetLength(1); j++)
                {
                    if (newMap[i + _y - 1, j + _x - 1] == 0)
                        newMap[i + _y - 1, j + _x - 1] = blockMap[i, j];
                    else
                        throw new ArgumentException();
                }
            }
            return newMap;
        }
    }
}
