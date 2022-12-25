using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace MINESWEEPERprog2lab2
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Cell> cells;
        private Cell[,] cellsforgame;

        private int minefieldrows = 0;
        private int minefieldcols = 0;
        private bool isGameStarted = false;
        private int cellsflagged = 0;
        private GameManager gameManager;
        public AppViewModel()
        {

        }


        public ObservableCollection<Cell> Cells
        {
            get
            {
                return this.cells;
            }
        }

        public string LabelRows
        {
            get
            {
                return $"Ширина:{this.minefieldrows}";
            }
        }
        public string LabelCols
        {
            get
            {
                return $"Высота:{this.minefieldcols}";
            }
        }

        public int MineFieldRows
        {
            get
            {
                return this.minefieldrows;
            }
            set
            {
                this.minefieldrows = value;
                OnPropertyChanged("MineFieldRows");
                OnPropertyChanged("LabelRows");
            }
        }

        public int MineFieldCols
        {
            get
            {
                return this.minefieldcols;
            }
            set
            {
                this.minefieldcols = value;
                OnPropertyChanged("MineFieldCols");
                OnPropertyChanged("LabelCols");
            }
        }

        public bool IsGameStarted
        {
            get
            {
                return this.isGameStarted;
            }
            set
            {
                this.isGameStarted = value;
                if (isGameStarted)
                {
                    gameManager = new GameManager(minefieldrows - 1);
                   
                    gameManager.NotifyLose += Lose;
                    gameManager.NotifyWin += Win;
                    cellsforgame = GameManager.GetGameField(minefieldrows, minefieldcols);
                    cells = new ObservableCollection<Cell>();
                    for (int i = 0; i < minefieldrows; i++)
                    {
                        for (int j = 0; j < minefieldcols; j++)
                        {
                            cells.Add(cellsforgame[i, j]);
                        }
                    }
                }
                else
                {
                  
                    gameManager.NotifyLose -= Lose;
                    gameManager.NotifyWin -= Win;
                    gameManager = null;
                    cellsflagged = 0;
                    cells = new ObservableCollection<Cell>();
                    OnPropertyChanged("IsGameStarted");
                }
                OnPropertyChanged("CellsFlagged");
                OnPropertyChanged("Cells");
                OnPropertyChanged("StartGameButtonText");
            }
        }

        public string StartGameButtonText
        {
            get
            {
                if (isGameStarted)
                    return $"Стереть поле";
                else
                    return $"Создать поле";
            }
        }
        public string CellsFlagged
        {
            get
            {
                if (gameManager != null)
                    return $"Помеченных клеток:\n{cellsflagged}/{this.minefieldrows - 1}";
                else
                    return $"";
            }
        }

        public void Lose()
        {
            IsGameStarted = false;
            ResultMassage window = new ResultMassage("Саперы ошибаются только однажды");
            window.Show();
        }

        public void Win()
        {
            IsGameStarted = false;
            ResultMassage window = new ResultMassage("Ура, Вы победили!!!!");
            window.Show();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        //<ToggleButton Content="{Binding Content}" IsChecked="{Binding IsOpened}" IsEnabled="{Binding IsFreeToCheck}">
    }


}
/*
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MINESWEEPERprog2lab2
{
    class AppViewModel
    {
        //ээкземпляр класса 
        private ObservableCollection<Cell> cells;  //динамическая коррекция данных. Когда данные в этой коллекции
                                                   //обновляются\добавляются\удаляются будут обновляться пользовательским интерфейсом
        private Cell[,] cellsforgame;

        private int minefieldrows = 0;
        private int minefieldcols = 0;
        private bool isGameStarted = false;
        private GameManager gameManager;
        public AppViewModel()
        {

        }

        public ObservableCollection<Cell> Cells
        {
            get
            {
                return this.cells;
            }
        }

        public string LabelRows
        {
            get
            {
                return $"Ширина:{this.minefieldrows}"; //динамический вывод, генерация "на ходу"
            }
        }
        public string LabelCols
        {
            get
            {
                return $"Высота:{this.minefieldcols}";
            }
        }

        public int MineFieldRows
        {
            get
            {
                return this.minefieldrows;
            }
            set
            {
                this.minefieldrows = value;
                OnPropertyChanged("MineFieldRows");
                OnPropertyChanged("LabelRows");
            }
        }

        public int MineFieldCols
        {
            get
            {
                return this.minefieldcols;
            }
            set
            {
                this.minefieldcols = value;
                OnPropertyChanged("MineFieldCols");
                OnPropertyChanged("LabelCols");
            }
        }

        public bool IsGameStarted
        {
            get
            {
                return this.isGameStarted;
            }
            set
            {
                this.isGameStarted = value;
                if (isGameStarted)
                {
                    gameManager = new GameManager(minefieldrows - 1);
                    gameManager.NotifyLose += Lose;
                    gameManager.NotifyWin += Win;
                    cellsforgame = GameManager.GetGameField(minefieldrows, minefieldcols);
                    cells = new ObservableCollection<Cell>();
                    for (int i = 0; i < minefieldrows; i++)
                    {
                        for (int j = 0; j < minefieldcols; j++)
                        {
                            cells.Add(cellsforgame[i, j]);
                        }
                    }
                }
                else
                {
                    gameManager.NotifyLose -= Lose;
                    gameManager.NotifyWin -= Win;
                    gameManager = null;
                    cells = new ObservableCollection<Cell>();
                    OnPropertyChanged("IsGameStarted");
                }
                OnPropertyChanged("Cells");
                OnPropertyChanged("StartGameButtonText");
            }
        }

        public string StartGameButtonText
        {
            get
            {
                if (isGameStarted)
                    return $"Стереть поле";
                else
                    return $"Создать поле";
            }
        }

        public void Lose()
        {
            IsGameStarted = false;
            ResultMassage window = new ResultMassage("ВЫ ПРОИГРАЛИ");
            window.Show();
        }

        public void Win()
        {
            IsGameStarted = false;
            ResultMassage window = new ResultMassage("УРА, ПОБЕДА!");
            window.Show();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
*/
