using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.CommonEntities
{
    public class EmployeeStatus
    {
       
        public Guid EMPId { get; set; }
        public string EMPName { get; set; }

        public DateTime? EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public bool Status { get; set; }

        public DateTime CustomTime {  get; set; }
  
    }
}
