using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Domain.Entities;

namespace Forms.Infrastructure.DTO
{
    public class FormDTO
    {
        public int UID { get; set; }
        public string adress { get; set; }
        public DateTime date { set; get; }
        public int mundepUID { get; set; }

        public double lat { get; set; }
        public double lon { get; set; }
        public Form ToEntity()
        {
            return new Form()
            {
                UID = UID,
                adress = adress,
                date = date,
                mundepUID = mundepUID
            };
        }

        public FormDTO(Form form)
        {

            UID = form.UID;
            adress = form.adress;
            date = form.date;
            mundepUID = form.mundepUID;

        }
    }
}
