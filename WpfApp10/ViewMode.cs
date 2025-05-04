using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp10.Commads;
using WpfApp10.Observable;
using static WpfApp10.CustomItemsControl;

namespace WpfApp10
{
    public class ViewMode : ObservableObject
    {
        public ObservableCollection<PersonInfo> ObservableCollection { get; set; }
        private Person _selectedItem;
        public Person SelectedItem 
        { 
            get { return _selectedItem; } 
            set { _selectedItem = value; RaisePropertyChanged(); }
        }
        private RelayCommand _toggle;
        public RelayCommand ToggleCommand
        {
            get { return _toggle; }
            set { _toggle = value; RaisePropertyChanged(); }
        }
    }
}
