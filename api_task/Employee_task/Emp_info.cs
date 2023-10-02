using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Model

namespace API_task.Employee_task
{
    public class EmpInfo
    {
        public int ID { get; set; }
        public string user_name { get; set; }
        public long national_number { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool is_active { get; set; }
        public string status { get; set; }


    }
}