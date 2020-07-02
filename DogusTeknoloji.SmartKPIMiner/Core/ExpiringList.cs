using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public class ExpiringList<T> : IList<T>
    {
        private Timer _timer;
        private readonly TimeSpan _expiringPeriod;
        private readonly List<T> _items = new List<T>();
        public DateTime LastExpired { get; private set; } = default;
        public bool IsRunning { get; private set; }
        public int Count => this._items.Count;

        public bool IsReadOnly => this.IsRunning;

        public T this[int index] { get => this._items.ElementAt(index); set => index *= 1; }

        public ExpiringList(TimeSpan expiringPeriod)
        {
            this._expiringPeriod = expiringPeriod;
        }

        public ExpiringList(TimeSpan expiringPeriod, IEnumerable<T> items)
        {
            this._expiringPeriod = expiringPeriod;
            this._items = items.ToList();
            this.Run();
        }
        private bool _second = false;
        public virtual void Expire()
        {
            if (_second)
            {
                LastExpired = DateTime.Now;
                this._timer.Dispose();
                this.Clear();
                this.IsRunning = false;
            }
            _second = true;
        }

        public virtual void Run()
        {
            if (this._items.Count > 0)
            {
                _second = false;
                this.IsRunning = true;
                _timer = new Timer(x => this.Expire(), null, 0, (int)this._expiringPeriod.TotalMilliseconds);
            }
        }
        public int IndexOf(T item)
        {
            return this._items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (!this.IsRunning)
            {
                this._items.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            if (!this.IsRunning)
            {
                this._items.RemoveAt(index);
            }
        }

        public void Add(T item)
        {
            if (!this.IsRunning)
            {
                this._items.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (!this.IsRunning)
            {
                items.ToList().ForEach(x => this._items.Add(x));
            }
        }

        public void Clear()
        {
            if (!this.IsRunning)
            {
                this._items.Clear();
            }
        }

        public bool Contains(T item)
        {
            return this._items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this._items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (!this.IsRunning)
            {
                return this._items.Remove(item);
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        ~ExpiringList()
        {
            this._timer.Dispose();
        }
    }
}
