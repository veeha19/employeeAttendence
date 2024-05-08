using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DatabaseEntities
{
    public class Employee
    {
        
        public Guid EMPId {  get; set; } 

        public required string FristName {  get; set; }
        public required string LastName { get; set; }

        public string? MobileNumber { get; set; }
        public DateTime UserCreateTime { get; set; }  
        public DateTime UpdateValueTime {  get; set; } 

    }
}
