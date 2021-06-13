using Forms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Domain.Entities;

namespace Forms.Presentation.Models
{
    public class FormModel
    {
        public int UID { get; set; }
        public string adress { get; set; }
        public DateTime date { set; get; }
        public int mundepUID { get; set; }
        public FormModel()
        {

        }

        public FormModel(Form form)
        {
            adress = form.adress;
            date = form.date;
            mundepUID = (int)form.mundepUID;
            UID = (int)form.UID;
        }

        public Form ToEntity()
        {
            return new Form()
            {
                adress = this.adress,
                date = this.date,
                mundepUID = (int)this.mundepUID,
                UID = (int)this.UID
        };
        }

    }
}