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
        public string Adress { get; set; }
        public DateTime Date { set; get; }
        public int MundepUID { get; set; }
        public int UserUID { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
