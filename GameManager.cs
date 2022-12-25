using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINESWEEPERprog2lab2
{
    class GameManager
    {
        private int minesCount;
        public static GameManager singleton;

        public delegate void Handler();
        public event Handler? NotifyLose;
        public event Handler? NotifyWin;

        public GameManager(int minescount)
        {
            singleton = this;
            minesCount = minescount;
        }
        
        public void Win()
        {
            this.NotifyWin?.Invoke();  //принимает делегат и выполняет его в том потоке, в котором был создан элемент управления
        }
        public void Lose()
        {
            this.NotifyLose?.Invoke();  //принимает делегат и выполняет его в том потоке, в котором был создан элемент управления
        }

        

        public static Cell[,] GetGameField(int rows, int cols)
        {
            int minescount = rows - 1;
            

            Cell[,] cells = new Cell[rows, cols];
            Random rnd = new Random();
            //Расстановка мин на поле
            for (int i = 0; i < minescount; i++)
            {
                int row = rnd.Next(0, rows - 1);
                int col = rnd.Next(0, cols - 1);
                if (cells[row, col] == null)
                {
                    cells[row, col] = new Cell(null, true);
                }
                else
                {
                    row = row == rows - 1 ? row - 1 : row + 1;
                    col = col == cols - 1 ? col - 1 : col + 1;
                    cells[row, col] = new Cell(null, true);
                }
            }
            //Расстановка обычных клеток
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (cells[i, j] == null)
                    {
                        int nearbymines = 0;
                        if (i > 0 && j > 0 && cells[i - 1, j - 1] != null && cells[i - 1, j - 1].IsMine)
                            nearbymines++;
                        if (j > 0 && cells[i, j - 1] != null && cells[i, j - 1].IsMine)
                            nearbymines++;
                        if (i + 1 < rows && j > 0 && cells[i + 1, j - 1] != null && cells[i + 1, j - 1].IsMine)
                            nearbymines++;
                        if (i + 1 < rows && cells[i + 1, j] != null && cells[i + 1, j].IsMine)
                            nearbymines++;
                        if (i + 1 < rows && j + 1 < cols && cells[i + 1, j + 1] != null && cells[i + 1, j + 1].IsMine)
                            nearbymines++;
                        if (j + 1 < cols && cells[i, j + 1] != null && cells[i, j + 1].IsMine)
                            nearbymines++;
                        if (i > 0 && j + 1 < cols && cells[i - 1, j + 1] != null && cells[i - 1, j + 1].IsMine)
                            nearbymines++;
                        if (i > 0 && cells[i - 1, j] != null && cells[i - 1, j].IsMine)
                            nearbymines++;
                        cells[i, j] = new Cell(nearbymines, false);
                    }
                }
            }
            return cells;
        }
    }
}
