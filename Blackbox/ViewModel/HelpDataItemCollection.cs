using System.Collections.Generic;

namespace Blackbox.ViewModel
{
    public class HelpDataItemCollection
    {
        private List<HelpDataItem> _list;
        private int _current;

        public void Add(HelpDataItem dataItem)
        {
            _list.Add(dataItem);
        }

        public void Remove(HelpDataItem dataItem)
        {
            _list.Remove(dataItem);
        }

        public int Count()
        {
            return _list.Count;
        }

        public int Current()
        {
            return (Count() <= 0 ? -1 : _current + 1);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool MovePrevious()
        {
            bool ret = true;

            if (_current <= 0)
            {
                ret = false;
            }
            else
            {
                --_current;
            }

            return ret;
        }

        public bool MoveNext()
        {
            bool ret = true;

            if (_current >= _list.Count - 1)
            {
                ret = false;
            }
            else
            {
                ++_current;
            }

            return ret;
        }

        public HelpDataItem DataItem()
        {
            return (Count() <= 0 ? null : _list[_current]);
        }

        public HelpDataItemCollection()
        {
            _list = new List<HelpDataItem>();
            _current = 0;
        }
    }
}
