using System;
using Forms.Domain.Entities;

namespace Forms.Infrastructure.DTO
{
    public class FormDTO
    {
        public int UID { get; set; }
        public string Adress { get; set; }
        public DateTime Date { set; get; }
        public int MundepUID { get; set; }
        public int UserUID { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public Form ToEntity()
        {
            return new Form()
            {
                UID = UID,
                Adress = Adress,
                Date = Date,
                MundepUID = MundepUID,
                Lat = Lat,
                Lon = Lon
            };
        }

        public FormDTO(Form form)
        {

            UID = form.UID;
            Adress = form.Adress;
            Date = form.Date;
            MundepUID = form.MundepUID;
            UserUID = form.UserUID;
            Lat = form.Lat;
            Lon = form.Lon;
        }

        public FormDTO()
        {
        }
    }
}
