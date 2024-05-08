using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DatabaseEntities
{
    public class EMPProfileInformation
    {

        public  Guid EMPId {  get; set; }
        public string EMPName { get; set; }
        public DateTime Date {  get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public int PresentDays { get; set; } 
        public int AbsentDays { get; set; }
        public int Hours { get; set; } 
        public int MonthlyHours { get; set; }
    }
}
