# WpfApp10

这是一种应用于DataTemplate（主要是itemsControl.ItemsTemplate，即设置它的子项的展示模板）的自定义类RadioButton方案。
RadioButton有个缺点，那就是同一组RadioButton中，必须有一个是被选中的
而该方案与RadioButton一样，那就是同一时刻下，一个组中至多只能有一个被选中，但是还有一个额外的优点：被选中项可取消勾选。


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
