using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MINESWEEPERprog2lab2
{
    public class Cell : INotifyPropertyChanged     //модель данных
    {
        // Данные о задаче 
        private int? minescloseby;
        private bool ismine;
        private bool isopened;
        private bool ischecked;
        //конструктор(переназначили свойства)

        public Cell(int? minesclose, bool isthismine)
        {
            this.minescloseby = minesclose;
            this.ismine = isthismine;
            this.isopened = false;
            this.ischecked = false;
        }
        //getters and setters
        
        public int? MinesCloseBy
        {
            get
            {
                if (this.isopened)
                    return minescloseby;
                else
                    return null;
            }
        }

        public bool IsMine
        {
            get
            {
                return ismine;
            }
        }

        public bool IsOpened
        {
            get
            {
                return this.isopened;
            }
            set
            {
                this.isopened = value; // value-то значение, которое попытается записаться в нашу переменную
                                       //связка значения с свойствами на форме
                             
                OnPropertyChanged("IsOpened"); //если свойство меняется, вызывается метод, который уведомляет  об изменени модели 
                OnPropertyChanged("Content"); //если изменено несколько значений, можно вызвать дополнительный метод
                OnPropertyChanged("IsFreeToCheck");                
                //currentMineCount++;
                //if(minescount == currentMineCount)
                //{
                //    GameManager.singleton.Win();
                //}
                if (this.ismine)
                    GameManager.singleton.Lose();
            }
        }

        public bool IsFreeToCheck
        {
            get
            {
                return !this.isopened;
            }
        }

        public string Content
        {
            get
            {
                if (this.isopened)
                {
                    if (this.ismine)
                        return "Bomb";
                    else
                        return minescloseby.ToString();
                }
                else
                    return null;
            }
        }
        //реализация интерфейса
        public event PropertyChangedEventHandler PropertyChanged; //событие, которое будет вызвано при изменении модели 

        public void OnPropertyChanged([CallerMemberName] string prop = "") //метод, который скажет ViewModel, что нужно передать виду новые данные 
        {
            if (PropertyChanged != null) //если изменения были, то мы отправляем событие на изменения
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}//класс, в котором прописали на интерфейс изменения свойств 
