using Forms.Domain.Entities;
using System;

namespace Forms.Presentation.Models
{
    public class FormModel
    {
        public int UID { get; set; }
        public string Adress { get; set; }
        public DateTime Date { set; get; }
        public int MundepUID { get; set; }
        public int UserUID { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public FormModel()
        {

        }

        public FormModel(Form form)
        {
            Adress = form.Adress;
            Date = form.Date;
            MundepUID = (int)form.MundepUID;
            UID = (int)form.UID;
            UserUID = (int)form.UserUID;
            Lat = form.Lat;
            Lon = form.Lon;
        }

        public Form ToEntity()
        {
            return new Form()
            {
                Adress = this.Adress,
                Date = this.Date,
                MundepUID = (int)this.MundepUID,
                UID = (int)this.UID,
                UserUID = (int)this.UserUID,
                Lat = this.Lat,
                Lon = this.Lon
            };
        }
    }
}