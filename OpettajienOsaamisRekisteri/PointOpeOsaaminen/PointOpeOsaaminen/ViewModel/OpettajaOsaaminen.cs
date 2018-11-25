using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOpeOsaaminen.ViewModel
{
    public class OpettajaOsaaminen
    {
        //public OpettajaOsaaminen()
        //{
        //    this.Osaamiset = new HashSet<Osaamiset>();
        //}

        public int? OpettajaID { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string Sähköposti { get; set; }
        public string Henkilönumero { get; set; }
        public string Yksikkö { get; set; }
        public string Toimenkuva { get; set; }

        public string OpeNimi
        {
            get
            {
                return Etunimi + " " + Sukunimi;
            }
        }
        public string Nimi { get; set; }
        public int OsaamisID { get; set; }
        public string Osaaminen { get; set; }
        public bool OpettamisenHalukkuus { get; set; }
        public string Kuvaus { get; set; }
        

        //public virtual ICollection<Opettaja> Opettaja { get; set; }

        
        public virtual ICollection<OpettajaOsaaminen> Osaamiset { get; set; }
    }
}