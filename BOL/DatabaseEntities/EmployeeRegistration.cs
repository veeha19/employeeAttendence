using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DatabaseEntities
{
    public class EmployeeRegistration
    {
        
        public Guid EMPId {  get; set; }
        public DateTime EntryDateTime { get; set; } = DateTime.Now;
        public DateTime ExitDateTime { get; set; } = DateTime.Now;

        public int MonthlyHours { get; set; }   
        //public bool Status { get; set; }

    }
}
