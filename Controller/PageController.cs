using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_2.Controller
{
    public class PageController<T>
    {
        public int PagesCount { get; }
        public int CurrentPageNum { get; private set; }

        public int ItemsPerPage { get; set; }

        private List<T> _collection;

        public ObservableCollection<T> CurrentPage { get; private set; }

        public event Action IsLastPage;
        public event Action NotLastPage;
        public event Action IsFirstPage;
        public event Action NotFirstPage;


        public PageController(IEnumerable<T> collection, int itemsPerPage)
        {
            _collection = collection.ToList();
            ItemsPerPage = itemsPerPage;
            PagesCount = collection.Count() / ItemsPerPage + 1;
            CurrentPageNum = 0;

            
        }

        public ObservableCollection<T> MoveForward()
        {
            
            var res = new ObservableCollection<T>();

            if(CurrentPageNum < PagesCount -1)
            {
                for (int i = CurrentPageNum * ItemsPerPage; i < CurrentPageNum * ItemsPerPage + ItemsPerPage; i++)
                {
                    res.Add(_collection[i]);
                }
                CurrentPageNum++;

                if(CurrentPageNum != 1)
                {
                    NotFirstPage();
                }

                CurrentPage = res;
                return res;
            }
            else
            {
                for(int i = CurrentPageNum * ItemsPerPage; i < _collection.Count; i++)
                {
                    res.Add(_collection[i]);
                }
                CurrentPageNum = PagesCount;
                CurrentPage = res;
                IsLastPage();

                return res;
            }
        }

        public ObservableCollection<T> MoveBack()
        {
            
            if(CurrentPageNum == 1)
            {
                return CurrentPage;
            }
            else
            {
                var res = new List<T>();
                var superRes = new ObservableCollection<T>();

                for(int i = (CurrentPageNum - 1) * ItemsPerPage -1; i >= (CurrentPageNum -2) * ItemsPerPage; i--)
                {
                    res.Add(_collection[i]);
                }
                for (int i = res.Count - 1; i >= 0; i--)
                {
                    superRes.Add(res[i]);
                }


                CurrentPageNum--;

                if(CurrentPageNum == 1)
                {
                    IsFirstPage();
                }
                if(CurrentPageNum != PagesCount)
                {
                    NotLastPage();
                }


                CurrentPage = superRes;
                return superRes;
            }
        }

    }
}
