using System.Security.Cryptography;

namespace GLS_CLI1
{
    public class Program
    {
        public static double NapiFogyasztas(int km, int fogyasztas)
        {
            if (km<=0 || fogyasztas<=0)
            {
                return 0;
            }


            return fogyasztas / (km / 100.0);
        }


        static void Main(string[] args)
        {
            string[] adatok = File.ReadAllLines("GLS.txt");
            List<AutoAdatok> autoLista = new List<AutoAdatok>();
            foreach (var item in adatok)
            {
                autoLista.Add(new AutoAdatok(item));
            }

            Console.WriteLine($"Az autó használatban töltött napjainak száma: {autoLista.Count()}");

            HashSet<string> soforok = new HashSet<string>();
            foreach (var item in autoLista)
            {
                soforok.Add(item.sofNev);
            }
            Console.WriteLine($"Különböző sofőrok száma: {soforok.Count()}");

            int km = autoLista.Sum(x => x.napiKilometer);
            Console.WriteLine($"Az összes megtett kilométer: {km} km");
            int fogy = autoLista.Sum(x => x.napiFogyasztasLiter);
            Console.WriteLine($"Átlagos fogyasztás: {NapiFogyasztas(km,fogy)} liter/100 km");

            var sofor = autoLista.GroupBy(s => s.sofNev)
                .Select(x => new { SoforNev = x.Key, VezetettNap = x.Count() })
                .OrderByDescending(y => y.VezetettNap)
                .First();

            Console.WriteLine($"7. feladat \n\tLegtöbbet vezető sofőr: {sofor.SoforNev}, napok száma: {sofor.VezetettNap}");

        }
    }
}
