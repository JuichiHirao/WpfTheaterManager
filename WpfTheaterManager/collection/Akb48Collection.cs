using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfTheaterManager.common;
using WpfTheaterManager.data;
using WpfTheaterManager.service;

namespace WpfTheaterManager.collection
{
    class Akb48Collection
    {
        Regex RegexDate = new Regex("(20[0-9][0-9][01][0-9]|20[0-9][0-9])");

        public List<Akb48Data> listData;
        public ICollectionView ColViewListData;

        Akb48Service service;

        public string FilterStatus = "";
        public string FilterWord = "";

        public Akb48Collection()
        {
            listData = new List<Akb48Data>();
            service = new Akb48Service();

            listData = service.GetList(new MySqlDbConnection());
            ColViewListData = CollectionViewSource.GetDefaultView(listData);
        }

        public void Update(Akb48Data myData)
        {
            service.Update(myData);
        }

        public void Execute()
        {
            string targetWord = FilterWord;
            int year = 0;
            int month = 0;
            if (FilterWord.Length > 0)
            {
                if (RegexDate.IsMatch(targetWord))
                {
                    string strDate = RegexDate.Match(targetWord).Value;
                    targetWord = targetWord.Replace(strDate, "").Trim();

                    if (strDate.Length == 4)
                    {
                        year = Convert.ToInt32(strDate);
                    }
                    else if (strDate.Length == 6)
                    {
                        year = Convert.ToInt32(strDate.Substring(0,4));
                        month = Convert.ToInt32(strDate.Substring(4));
                    }
                }
            }
            ColViewListData.Filter = delegate (object o)
            {
                Akb48Data data = o as Akb48Data;

                if (FilterStatus.Length > 0)
                {
                    if (data.Status == FilterStatus)
                        return true;

                    return false;
                }

                if (FilterWord.Length > 0)
                {
                    if (year > 0)
                    {
                        if (year != data.TheDate.Year)
                            return false;
                        if (month > 0 && month != data.TheDate.Month)
                            return false;
                    }
                    if (data.Member.IndexOf(targetWord) >= 0)
                        return true;
                    else
                        return false;
                }

                return true;
            };

            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("TheDate", ListSortDirection.Ascending));
        }

    }
}
