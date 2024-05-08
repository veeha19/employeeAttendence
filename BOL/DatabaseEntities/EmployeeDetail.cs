using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DatabaseEntities
{
    public class EmployeeDetail
    {

        public Guid EMPId { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan EntryTime { get; set; }
        public TimeSpan ExitTime { get; set; }

    }
}