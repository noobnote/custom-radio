using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfApp10
{
    /// <summary>一个弱引用对象集合的辅助类
    /// 
    /// </summary>
    public static class WeakCollectionHelper
    {
        /// <summary>往集合中添加一个对象
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceWeakList"></param>
        /// <param name="item"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddWeakReference<T>(this List<WeakReference<T>> sourceWeakList, T item) where T : class
        {
            if (sourceWeakList == null || item == null) throw new ArgumentNullException();

            sourceWeakList.Add(new WeakReference<T>(item));
        }

        /// <summary>检查集合中是否引用了同样的对象
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceWeakList"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ContainsSame<T>(this List<WeakReference<T>> sourceWeakList, T item) where T : class
        {
            if (sourceWeakList == null || item == null) throw new ArgumentNullException();

            foreach (var weakReference in sourceWeakList)
            {
                T toggle;
                if (weakReference.TryGetTarget(out toggle) && object.ReferenceEquals(toggle, item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>对队列中的所有子项进行统一操作
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceWeakList"></param>
        /// <param name="item"></param>
        /// <param name="CloseAction"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEachApply<T>(this List<WeakReference<T>> sourceWeakList, Action<T> CloseAction) where T : class
        {
            if (sourceWeakList == null || CloseAction == null) throw new ArgumentNullException();
            foreach (var weakReference in sourceWeakList)
            {
                T toggle;
                if (weakReference.TryGetTarget(out toggle))
                {
                    CloseAction(toggle);
                }
            }
        }

        /// <summary>清除所有空引用子项
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceWeakList"></param>
        public static void RemoveNullReferences<T>(this List<WeakReference<T>> sourceWeakList) where T : class
        {
            if (sourceWeakList == null) throw new ArgumentNullException();

            var releasedList = new List<WeakReference<T>>();
            foreach (var item in sourceWeakList)
            {
                if (!item.TryGetTarget(out T target))
                {
                    releasedList.Add(item);
                }
            }
            sourceWeakList.RemoveAll(cell => releasedList.Contains(cell));
        }
    }
}
