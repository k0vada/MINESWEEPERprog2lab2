using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MINESWEEPERprog2lab2
{
    class AppViewModelResult : INotifyPropertyChanged
    {
        private string labeltext;
        public AppViewModelResult(string text)
        {
            labeltext = text;
        }
        public string LabelText
        {
            get
            {
                return labeltext;
            }
            set
            {
                labeltext = value;
                OnPropertyChanged("LabelText");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
