namespace GameClasses
{
    public class Game
    {
        private bool _gameRunning = true;
        DateTime _timeCount;
        int _framesRendered = 0;
        private int _curFps = 60;
        private int _maxFps = 60;

        private int _width = 0;
        private int _height = 0;
        private int[,] _map = new int[0, 0];
        private int[,] drawnMap = new int[0, 0];
        private Block[] blocks = new Block[] { new Block(4, 0) };

        public Game() 
        {
            this._width = 10;
            this._height = 20;
            this._map = new int[_height, _width];

            this.GameMainLoop();
        }

        public void GameMainLoop()
        {
            Console.WriteLine("Press any button to start;");
            Console.ReadKey();
            while (true)
            {
                Update();
                PrintMap();
                // Console.ReadKey();
            }
        }

        public void Update()
        {
            _framesRendered++;
            if ((DateTime.Now - _timeCount).TotalSeconds >= 1)
            {
                _curFps = _framesRendered;
                _framesRendered = 0;
                _timeCount = DateTime.Now;
            }
            drawnMap = ArrayFunctions.Copy(this._map);
            foreach (var block in blocks)
            {
                block.Update(_curFps);
                drawnMap = block.Draw(this._map);
                if (block.CollideCheck(_map))
                {
                    this._map = block.Draw(this._map);
                }
            }
        }

        public void PrintMap()
        {
            Console.Clear();
            for (int i = 0; i < drawnMap.GetLength(0); i++)
            {
                for (int j = 0; j < drawnMap.GetLength(1); j++)
                {
                    Console.Write(drawnMap[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}