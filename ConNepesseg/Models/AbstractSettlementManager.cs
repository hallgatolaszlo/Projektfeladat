using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConNepesseg.Models
{
    // ■ ■ ■ ->
    public abstract class AbstractSettlementManager
    {

        private List<Settlement> settlements = new List<Settlement>();

        public List<Settlement> ListSettlements { get => settlements; }

        /// <summary>
        /// Visszaadja a 10 települést, amelyek a legnagyobb mértékű népességnövekedést mutatták.
        /// </summary>
        /// <returns>A legnagyobb relatív növekedést elérő települések listája.</returns>
        public List<Settlement> GetTop10SettlementsByHighestGrowthRate()
        {
            return
                ListSettlements
                .Select(s => new
                {
                    Settlement = s,
                    GrowthRate = (s.TotalPopulation(2025) - s.TotalPopulation(2024)) / (double)s.TotalPopulation(2024)
                })
                .OrderByDescending(x => x.GrowthRate)
                .Take(10)
                .Select(x => x.Settlement)
                .ToList();
        }

        /// <summary>
        /// A megadott CSV fájból betölti a települések adatait.
        /// </summary>
        /// <param name="path">A CSV fájl elérési útvonala és neve</param>
        /// <exception cref="FileNotFoundException">A megadott fájl nem található.</exception>"
        public void LoadFromCSV(string path)
        {
            // <- ■ ■ ■
            // TODO: Csak ezt a kódrészletet módosíthatja!
            try
            {
                File.ReadAllLines(path).Skip(1).ToList()
                    .ForEach(line => ListSettlements.Add(new Settlement(line)));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // ■ ■ ■ ->
        }

        /// <summary>
        /// A megadott települések adatait elmenti egy CSV fájlba.
        /// A CSV szerkezete megegyezik a betöltött fájl szerkezetével.
        /// </summary>
        /// <param name="path">A CSV fájl elérési útvonala és neve</param>
        /// <param name="settlementsList">A fájlba mentendő települések listája</param>
        public abstract void SaveToCSV(string path, List<Settlement> settlementsList);

        /// <summary>
        /// Visszaadja a megadott vármegye településeinek statisztikáit.
        /// </summary>
        /// <param name="varmegye">A vármegye kódja</param>
        /// <returns>Több sorból álló szöveg</returns>
        public abstract List<String> RegionalStatistics(string varmegye);

        //Ezt használja fel az implementálásnál, hogy időt nyerjen a feladatok megoldásához!

        //const string IDE_IRJON_KODOT = "■";
        //List<String> rows = new();
        //rows.Add($"Vármegye: {varmegye}");
        //rows.Add($"Települések száma: {IDE_IRJON_KODOT}");
        //rows.Add($"Összes lakos 2024-ben: {IDE_IRJON_KODOT} fő");
        //rows.Add($"Összes lakos 2025-ben: {IDE_IRJON_KODOT} fő");
        //rows.Add($"Növekedés: {IDE_IRJON_KODOT} fő");
        //rows.Add($"Növekedés %-ban: {IDE_IRJON_KODOT}%");
        //return rows;
    }
}
