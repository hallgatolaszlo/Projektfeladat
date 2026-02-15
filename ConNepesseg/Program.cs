//using ConNepesseg.Models;
using ConNepesseg.Models;
using System;

namespace ConNepesseg
{
    // Jelmagyarázat:
    // ■ ■ ■ -> Innnen kezdve nem módosítható a kód
    // <- ■ ■ ■ Ettől kezdve módosítható!
    public class Program
    {
        // ■ ■ ■ ->
        const string CSV_Source = "Source\\lakossag_2024_2025.csv";

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("B) Asztali- és webes szoftverfejlesztés, adatbázis-kezelés vizsgarész\n\tkonzolos asztali alkalmazásfejlesztés C# nyelven (15 pont)\n\n");
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine(" 1. feladat\n\tSettlement osztály implementálása\n");
            Console.WriteLine(" 2. feladat\n\tSettlementManager osztály implementálása\n");// <- ■ ■ ■

            // Az osztályok elkészítése után a következő kódrészletet ki kell venni a megjegyzésből!
            // Helyezze el a (using ConNepesseg.Models;) sort is!
            // Amennyiben az osztályok implementálásával nehézsége akadt, akkor azoktól függetlenül is megoldhatja a következő feladatokat!
            // <- ■ ■ ■

            SettlementManager manager = new SettlementManager();
            manager.LoadFromCSV(CSV_Source);

            // Itt ellenőrízheti, hogy sikeres volt-e a beolvasás, ha kiveszi megjegyzésből.
            // Console.WriteLine(manager.ListSettlements.Last().Telepules + " " + manager.ListSettlements.Last().Tipus);

            // Itt ellenőrízheti a RegionalStatistics metódus működését, ha kiveszi megjegyzésből.
            // manager.RegionalStatistics("HAJ").ForEach(t => Console.WriteLine(t));

            // ■ ■ ■ ->
            Console.WriteLine(" 3. feladat");
            Console.Write("\tHajdú-Bihar vármegyében lévő nagyközségek listája:\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // <- ■ ■ ■

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<Settlement> hajduBihar = manager.ListSettlements.Where(s => s.Varmegye == "HAJ").ToList();
            foreach (Settlement s in hajduBihar)
            {
                Console.WriteLine("\t\t" + s.ToString());
            }

            // ■ ■ ■ ->
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n 4. feladat");
            Console.WriteLine("\tA 10 legnagyobb növekedési mértékkel rendelkező település:");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // <- ■ ■ ■

            var top10 = manager.GetTop10SettlementsByHighestGrowthRate();
            foreach (Settlement s in top10)
            {
                Console.WriteLine($"\t\t{s.Telepules} : {s.NovekedesSzazalek.ToString("F1")}%");
            }

            // ■ ■ ■ ->
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n 5. feladat");
            Console.WriteLine("\tA 2025-ben 10 000 fő feletti lakosságszámú települések száma vármegyék szerint rendezve:");
            Console.ForegroundColor = ConsoleColor.DarkRed;

            // <- ■ ■ ■

            var dict = new SortedDictionary<string, int>();
            foreach (Settlement s in manager.ListSettlements)
            {
                if (dict.ContainsKey(s.Varmegye))
                {
                    dict[s.Varmegye] += 1;
                }
                else
                {
                    dict[s.Varmegye] = 1;
                }
            }

            foreach (var entry in dict)
            {
                Console.WriteLine($"\t{entry.Key} : {entry.Value} db");
            }

            // ■ ■ ■ ->
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n 6. feladat\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // <- ■ ■ ■

            Console.Write("Kérem a vármegye kódját! : ");
            string? input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Kérem a vármegye kódját! : ");
                input = Console.ReadLine();
            }

            foreach (string row in manager.RegionalStatistics(input))
            {
                Console.WriteLine(row);
            }

            var varosok = manager.ListSettlements.Where(s => s.Varmegye == input).ToList();
            manager.SaveToCSV($"{input}.CSV", varosok);

            // ■ ■ ■ ->
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("VÉGE!\n");
        }
    }
}
