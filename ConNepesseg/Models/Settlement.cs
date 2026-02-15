using System.Security.Cryptography.X509Certificates;

namespace ConNepesseg.Models
{
    public class Settlement
    {
        // id;varmegye;telepules;tipus;ferfi_2024;no_2024;ferfi_2025;no_2025

        // Itt dolgozhat!

        public int Id { get; }
        public string Varmegye { get; } = string.Empty;
        public string Telepules { get; } = string.Empty;
        public string Tipus { get; } = string.Empty;
        public int Ferfi2024 { get; }
        public int No2024 { get; }
        public int Ferfi2025 { get; }
        public int No2025 { get; }
        public int Lakossag2024 { get => TotalPopulation(2024); }
        public int Lakossag2025 { get => TotalPopulation(2025); }
        public int Novekedes { get => Lakossag2025 - Lakossag2024; }
        public float NovekedesSzazalek { get => (float)Novekedes / Lakossag2024 * 100; }

        // ■ ■ ■ ->
        /// <summary>
        /// A konstruktor egy település adatait tölti be egy CSV sorból.
        /// </summary>
        /// <param name="sourceLine">Egy CSV szerkezetű sor</param>
        public Settlement(String sourceLine) // <- ■ ■ ■
        {
            // Itt implementálja a konstruktort!
            // A CSV sor feldolgozása és a mezők értékének beállítása

            var split = sourceLine.Split(';');
            Id = int.Parse(split[0]);
            Varmegye = split[1];
            Telepules = split[2];
            Tipus = split[3];
            Ferfi2024 = int.Parse(split[4]);
            No2024 = int.Parse(split[5]);
            Ferfi2025 = int.Parse(split[6]);
            No2025 = int.Parse(split[7]);
        }


        // ■ ■ ■ ->
        /// <summary>
        /// A település teljes lakosságát adja vissza a megadott évben.
        /// <param name="year">Az év, amelyre vonaktozóan a lakosságot lekérjük.</param>
        /// <returns>A település teljes lakossága a megadott évben. Nem létező év esetén -1-et ad vissza.</returns>
        /// </summary>
        public int TotalPopulation(int year)// <- ■ ■ ■
        {
            // Itt implementálja a metódust!
            // Ha a megadott év nem 2024 vagy 2025, akkor -1-et adjon vissza
            if (year == 2024)
            {
                return Ferfi2024 + No2024;
            }

            if (year == 2025)
            {
                return Ferfi2025 + No2025;
            }

            return -1;
        }

        //ToString() felülírása
        public override string ToString()
        {
            return $"{Telepules} város lakossága 2024-ben {Lakossag2024} fő, 2025-ben {Lakossag2025} fő";
        }
    }
}
