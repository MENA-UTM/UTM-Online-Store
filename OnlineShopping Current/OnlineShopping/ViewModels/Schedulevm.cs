using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModels
{
    public class Schedulevm
    {
        
        public int Schedule_id { get; set; }
        public int Schedule_day { get; set; }
        public int doc_fid { get; set; }

        public int no_of_slots { get; set; }
        public string start_time { get; set; }
        public Nullable<int> fee { get; set; }
    }
}