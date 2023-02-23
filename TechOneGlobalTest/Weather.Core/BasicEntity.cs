using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Core
{
    public class BasicEntity
    {
        public bool Deleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }  = new DateTime(1991,1,1);
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = new DateTime(1991, 1, 1);
        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; } = new DateTime(1991, 1, 1);
    }
}
