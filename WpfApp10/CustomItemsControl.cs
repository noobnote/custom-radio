using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfApp10.Observable;

namespace WpfApp10
{
    public class CustomItemsControl : ItemsControl
    {
        public CustomItemsControl()
        {
            this.DataContext = this.ViewMode = new ViewMode()
            {
                ToggleCommand = new Commads.RelayCommand(CanExecute, Execute),
                ObservableCollection = new ObservableCollection<PersonInfo>()
                { 
                    //测试用
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="B",Description="B"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="C",Description="C"},
                        IsEnabled=true,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="D",Description="D"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="DD",Description="DDDDDD"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },
                    new PersonInfo()
                    {
                        Person=new Person() {Name="A",Description="A"},
                        IsEnabled=false,
                    },

                },
                //SelectedItem=

            };
        }

        public ViewMode ViewMode { get; set; }




        private bool CanExecute(object parameter)
        {
            return true;
        }
        private void Execute(object parameter)
        {
            if (!(parameter != null && parameter is CommandParameter)) return;
            var param = (CommandParameter)parameter;
            if (!(param.ToggleButton != null && param.PersonInfo != null)) return;




            bool isChecked = false;
            param.ToggleButton.Dispatcher.Invoke(() =>
            {
                isChecked = param.ToggleButton.IsChecked.GetValueOrDefault();
            });

            if (isChecked && this.ViewMode.ObservableCollection.Contains(param.PersonInfo))
            {

                param.PersonInfo.IsEnabled = true;
                foreach (var cell in this.ViewMode.ObservableCollection)
                {
                    if (!object.ReferenceEquals(cell, param.PersonInfo))
                    {
                        cell.IsEnabled = false;
                    }
                }
            }
            if (!isChecked && this.ViewMode.ObservableCollection.Contains(param.PersonInfo))
            {
                param.PersonInfo.IsEnabled = false;
            }

        }

        //private void Execute(object parameter)
        //{
        //    Task.Run(() =>
        //    {

        //        if (!(parameter != null && parameter is CommandParameter)) return;
        //        var param = (CommandParameter)parameter;
        //        if (!(param.ToggleButton != null && param.PersonInfo != null)) return;




        //        bool isChecked = false;
        //        param.ToggleButton.Dispatcher.Invoke(() =>
        //        {
        //            isChecked = param.ToggleButton.IsChecked.GetValueOrDefault();
        //        });

        //        if (isChecked && this.ViewMode.ObservableCollection.Contains(param.PersonInfo))
        //        {

        //            param.PersonInfo.IsEnabled = true;
        //            foreach (var cell in this.ViewMode.ObservableCollection)
        //            {
        //                if (!object.ReferenceEquals(cell, param.PersonInfo))
        //                {
        //                    cell.IsEnabled = false;
        //                }
        //            }
        //        }
        //        if (!isChecked && this.ViewMode.ObservableCollection.Contains(param.PersonInfo))
        //        {
        //            param.PersonInfo.IsEnabled = false;
        //        }
        //    });

        //}
    }
}
