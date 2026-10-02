using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tm.Mobile.Core
{
    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        public RangeObservableCollection()
        {
        }

        public RangeObservableCollection(IEnumerable<T> items)
            : base()
        {
            AddRange(items);
        }

        protected override void InsertItem(int index, T item)
        {
            InsertRange(index, new T[] { item });
        }

        public virtual void InsertRange(int index, IEnumerable<T> items)
        {
            if (items == null) return;
            int position = index;
            foreach (T item in items)
                Items.Insert(position++, item);

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            // Cannot use NotifyCollectionChangedAction.Add, because Constructor supports only the 'Reset' action.
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        public void AddRange(IEnumerable<T> items)
        {
            InsertRange(Count, items);
        }

        protected override void RemoveItem(int index)
        {
            RemoveRange(new T[] { this[index] });
        }

        public virtual void RemoveRange(IEnumerable<T> items)
        {
            bool removed = false;
            T[] removeItems = items.ToArray();
            foreach (T item in removeItems)
                if (Items.Remove(item)) removed = true;

            if (removed)
            {
                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                // Cannot use NotifyCollectionChangedAction.Add, because Constructor supports only the 'Reset' action.
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        protected override void ClearItems()
        {
            FastClear();
        }

        public virtual void FastClear()
        {
            Items.Clear();
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            // Cannot use NotifyCollectionChangedAction.Add, because Constructor supports only the 'Reset' action.
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public class ReplaceData
        {
            public T OldItem { get; set; }
            public T NewItem { get; set; }

        }//internal class

        public void Replace(ReplaceData[] replaceData)
        {
            foreach (ReplaceData data in replaceData)
            {
                if (data.OldItem == null)
                {
                    if (data.NewItem != null) Items.Add(data.NewItem);
                    continue;
                }

                int index = Items.IndexOf(data.OldItem);
                Items.RemoveAt(index);
                if (data.NewItem != null) Items.Insert(index, data.NewItem);
            }
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
