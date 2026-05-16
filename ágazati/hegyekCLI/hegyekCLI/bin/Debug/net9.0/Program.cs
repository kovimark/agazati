using System.Net.Security;

namespace hegyekCLI
{


    public class Hegy
    {
        public string hegyCsucsNeve { get; private set; }
        public string hegyseg { get; private set; }
        public int magassag { get; private set; }

        public Hegy(string sor)
        {
            string[] adatok = sor.Split(';');

            this.hegyCsucsNeve = adatok[0];
            this.hegyseg = adatok[1];
            this.magassag = int.Parse(adatok[2]);
        }

        public static List<Hegy> hegyCsucsok = new List<Hegy>();

        public static bool tartalmaz (string beirtSzo, string hegyCsucs, string hegysegNeve)
        {
            if (hegyCsucs.Contains(beirtSzo) || hegysegNeve.Contains(beirtSzo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            
        public class Program
        {
            static void Main(string[] args)
            {
                var Adatok = File.ReadAllLines("hegyek.csv").Skip(1);
                foreach(string sor in Adatok)
                {
                    hegyCsucsok.Add(new Hegy(sor));
                }

                //8.Feladat

                Console.WriteLine("8.Feladat");
                var nagyobbmint950 = hegyCsucsok.Where(x => x.magassag > 950);

                foreach(var item in nagyobbmint950)
                {
                    Console.WriteLine($"{item.hegyCsucsNeve}, {item.hegyseg}, {item.magassag}");
                }

                //11 A program tartalmazzon egy függvényt amely megkap egy keresett szót, majd a hegycsúcs és a hegység nevét.A függvény igaz vagy hamis logikai értékkel térjen vissza. (1pont) 0.A függvény döntse el, hogy a keresett szó megtalálható - e a hegycsúcs, vagy ahegység nevében! A függvény igaz logikai értékkel tér vissza, amennyiben szerepel az adott szó bármelyik névben! Hamissal, ha nem!(1pont)
                Console.WriteLine("Kérem a keresett szót: ");
                string beirtSzo = Console.ReadLine();
                foreach (var item in hegyCsucsok)
                {
                    if (tartalmaz(beirtSzo, item.hegyCsucsNeve, item.hegyseg))
                    {
                        Console.WriteLine(item.hegyCsucsNeve);
                    }
                }

            }
        }
    }
}