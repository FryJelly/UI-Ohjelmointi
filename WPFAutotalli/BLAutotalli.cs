using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // sisältää ADOn 

namespace JAMK.IT
{
    public static class Autotalli
    {
        public static List<Auto> HaeAutoDB()
        {
            try
            {
                // haetaan tietokannasta ja muutetaan olioiksi
                List<Auto> autos = new List<Auto>();
                DataTable dt = JAMK.IT.DBAutotalli.GetAllAutosFromMysqlDt();
                // mapataan Datatablen rivit olioitten ominaisuuksiksi
                foreach (DataRow dr in dt.Rows)
                {
                    Auto auto = new Auto();
                    auto.Merkki = dr[0].ToString();
                    auto.Malli = dr[1].ToString();
                    auto.VM = int.Parse(dr[2].ToString());
                    auto.Hinta = float.Parse(dr[3].ToString());
                    auto.URL = dr[4].ToString();
                    autos.Add(auto);
                }
                return autos;
            }
            catch
            {
                throw;
            }
        }
        public static List<Auto> Haeautot()
        {
            // GUIn testaamista varten dummy-dataa 
            List<Auto> autot = new List<Auto>();
            Auto a = new Auto();
            a.Merkki = "Volvo";
            a.Malli = "V70";
            a.VM = 2007;
            a.KM = 300000;
            a.Hinta = 9000F;
            a.URL = "VolvoV70.png";
            autot.Add(a);
            //toinen auto
            Auto b = new Auto() { Merkki = "Audi", Malli = "A4", VM = 2010, KM = 100000, Hinta = 20000, URL = "AudiA4.png" };
            autot.Add(b);
            // kolmas auto
            autot.Add(new Auto() { Merkki = "Saab", Malli = "99", VM = 2000, KM = 500000, Hinta = 2000, URL = "Saab99.png" });

            autot.Add(new Auto() { Merkki = "Cadillac", Malli = "DeVille", VM = 1969, KM = 75000, Hinta = 7000, URL = "Cadillac69.png" });


            //palautus
            return autot;
        }
    }
    public class Auto
    {
        public string Merkki { get; set; }
        public string Malli { get; set; }
        public int VM { get; set; }
        public int KM { get; set; }
        public float Hinta { get; set; }
        public string URL { get; set; }

    }
}
