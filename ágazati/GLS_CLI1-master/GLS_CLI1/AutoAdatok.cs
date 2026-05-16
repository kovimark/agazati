using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLS_CLI1
{
    public class AutoAdatok
    {
        public string datum { get; private set; }
        public string sofNev { get; private set; }
        public int napiKilometer { get; private set; }
        public int kezbCsomagSzam { get; private set; }
        public int napiFogyasztasLiter { get; private set; }

        public AutoAdatok(string sor)
        {
            string[] darabok=sor.Split(';');
            //2024-09-07;Kiss Gábor;126;40;13

            this.datum = darabok[0];
            this.sofNev = darabok[1];
            this.napiKilometer = int.Parse(darabok[2]);
            this.kezbCsomagSzam = int.Parse(darabok[3]);
            this.napiFogyasztasLiter = int.Parse(darabok[4]);
        }

        public AutoAdatok(string datum, string sofNev, int napiKilometer, int kezbCsomagSzam, int napiFogyasztasLiter)
        {
            this.datum = datum;
            this.sofNev = sofNev;
            this.napiKilometer = napiKilometer;
            this.kezbCsomagSzam = kezbCsomagSzam;
            this.napiFogyasztasLiter = napiFogyasztasLiter;
        }
    }
}
