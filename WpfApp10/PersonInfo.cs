using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp10.Observable;

namespace WpfApp10
{
    public class PersonInfo : ObservableObject
    {
        public Person Person { get; set; }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; RaisePropertyChanged(); }
        }
    }
}
