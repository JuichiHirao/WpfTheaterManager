using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheaterManager.data
{
    public class Akb48Data
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Member { get; set; }

        public DateTime TheDate { get; set; }

        public string Memo { get; set; }

        public string Memo2 { get; set; }

        public string Remark { get; set; }

        public string Filename { get; set; }

        public int Rating1 { get; set; }

        public int Rating2 { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
