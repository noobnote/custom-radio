using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfApp10
{
    public class CustomItemsControl : ItemsControl
    {
        public CustomItemsControl()
        {
            this.DataContext = this.ViewMode = new ViewMode()
            {
                ToggleCommand = new Commads.RelayCommand(CanExecute, Execute),
                ObservableCollection = new ObservableCollection<Person>()
                { 
                    //测试用
                    new Person() {Name="A",Description="A"},
                    new Person() {Name="B"},
                    new Person() {Name="C"},
                    new Person() {Name="D"},
                    new Person() {Name="E"},
                    new Person() {Name="F"},
                    new Person() {Name="G"},
                    new Person() {Name="H"},
                    new Person() {Name="I"},
                    new Person() {Name="J"},
                    new Person() {Name="K"},

                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                    new Person() {Name="B"},
                },
                //SelectedItem=

            };
        }

        public ViewMode ViewMode { get; set; }


        //itemsControl想获取所有toggleButton对象的引用，但是又怕ItemsPanel.VirtualizingStackPanel被设置为虚拟化面板（例如VirtualizingStackPanel），导致控件内存无法释放，所以只能使用WeakReference

        /// <summary>ToggleButton的弱引用列表。它专为虚拟化面板设置。
        /// 
        /// </summary>
        public List<WeakReference<ToggleButton>> WeakReferences { get; set; } = new List<WeakReference<ToggleButton>>();

        private bool CanExecute(object parameter)
        {
            return true;
        }
        private void Execute(object parameter)
        {
            if (!(parameter != null && parameter is CommandParameter)) return;
            var param = (CommandParameter)parameter;
            if (!(param.ToggleButton != null && param.Person != null)) return;


            if (!WeakReferences.ContainsSame(param.ToggleButton))
            {
                //每个执行了command的按钮都会被加入列表
                WeakReferences.AddWeakReference(param.ToggleButton);
            }
            //清理空引用
            WeakReferences.RemoveNullReferences();

            bool isChecked = false;
            param.ToggleButton.Dispatcher.Invoke(() =>
            {
                isChecked = param.ToggleButton.IsChecked.GetValueOrDefault();
            });

            if (isChecked)
            {
                this.ViewMode.SelectedItem = param.Person;

                //将除了当前toggleButton以外的所有toggleButton的IsChecked切换为false
                WeakReferences.CloseOtherObject(param.ToggleButton,
                   (toggleButton) =>
                   {
                       toggleButton?.Dispatcher.Invoke(() => { toggleButton.IsChecked = false; });
                   });
            }
            if (!isChecked && object.ReferenceEquals(param.Person, this.ViewMode.SelectedItem))
            {
                //仅当自己关闭自己时，才会把SelectedItem置为null
                this.ViewMode.SelectedItem = null;
            }

        }


        /*
         * 基本原理：
         * 1、当checked时，将“子项参数”和toggleButton自身作为参数传递给Command
         * 2、Command检查它的IsChecked：
         *      a、true，那就直接替换“当前选择项”的引用。同时还要将除了当前toggleButton以外的所有toggleButton的IsChecked切换为false
         *      b、false，（因为非用户输入导致IsChecked更改时不一定会调用command，但是为了确保万无一失），还需要再检查参数输入的子项与“当前选择项”是否为同一个，如果是，那么“当前选择项”引用null。即仅当自己关闭自己时，才会把SelectedItem置为null
         * 
         * 
         * 补充说明：
         * 1、为什么需要一个toggleButton列表？
         *      a：因为需要其它按钮调用command时进行比对，然后决定下一步行动
         * 2、为什么toggleButton列表使用WeakReference？
         *      a：因为ItemsPanel.VirtualizingStackPanel可能被设置为虚拟化面板（例如VirtualizingStackPanel），导致控件内存无法释放，所以只能使用WeakReference，这样进行提前预防
         */
    }
}
