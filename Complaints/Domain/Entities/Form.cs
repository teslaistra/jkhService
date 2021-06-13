using Forms.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forms.Domain.Entities
{
    public class Form
    {
        public int UID { get; set; }
        public string adress { get; set; }
        public DateTime date { set; get; }
        public int mundepUID { get; set; }
        public int userUID { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
