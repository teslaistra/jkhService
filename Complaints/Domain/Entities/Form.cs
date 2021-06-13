using Forms.Infrastructure.DTO;
using System;

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

        public Form(FormDTO form)
        {
            UID = form.UID;
            Adress = form.Adress;
            Date = form.Date;
            MundepUID = form.MundepUID;
            UserUID = form.UserUID;
            Lat = form.Lat;
            Lon = form.Lon;
        }

        public Form()
        {
        }
    }
}
