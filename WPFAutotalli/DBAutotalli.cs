using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace JAMK.IT
{
    public class DBAutotalli
    {
        public static DataTable GetAllAutosFromMysqlDt()
        {
            try
            {
                // haetaan autojen tiedot ja palautetaan ne DataTablena
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT merkki, malli, vm, hinta, url FROM autotalli";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {

                throw;
            }
        }
        public static List<Auto> GetAllAutosFromMysql()
        {
            try
            {
                // metodi palauttaa listan auto-olioita joiden tiedot haettu Mysql:stä
                List<Auto> autos = new List<Auto>();
                //luodaan yhteys tietokantaan
                string connStr = GetConnectionString();
                string sql = "SELECT merkki, malli, vm, hinta FROM autotalli";
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Auto auto = new Auto();
                            auto.Merkki = rdr.GetString(0);
                            auto.Malli = rdr.GetString(1);
                            auto.VM = rdr.GetInt16(2);
                            auto.Hinta = rdr.GetFloat(3);
                            autos.Add(auto);

                        }
                    }
                }
                //palautus
                return autos;
            }
            catch
            {

                throw;
            }
        }

        private static string GetConnectionString()
        {
            /*//huono tapa = kovakoodattu
            string palvelin = "mysql.labranet.jamk.fi";
            string tietokanta = "K9105";
            string tunnus = "K9105";
            string pw = "QyfaBqBoJjYPmAXHv4LokkIf82B0EWs7";
            return string.Format("Data source={0};Initial catalog={1};user={2};password={3}", palvelin, tietokanta, tunnus, pw);*/
            //parempi data = luetaan app.configista
            string palvelin = WPFAutotalli.Properties.Settings.Default.palvelin;
            string tietokanta = WPFAutotalli.Properties.Settings.Default.tietokanta;
            string tunnus = WPFAutotalli.Properties.Settings.Default.tunnus;
            string pw = WPFAutotalli.Properties.Settings.Default.salasana;
            return string.Format("Data source={0};Initial catalog={1};user={2};password={3}", palvelin, tietokanta, tunnus, pw);
        }
    }
}
