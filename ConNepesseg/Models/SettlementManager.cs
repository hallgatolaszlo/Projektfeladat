using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConNepesseg.Models;

public class SettlementManager : AbstractSettlementManager
{
    public override void SaveToCSV(string path, List<Settlement> settlementsList)
    {
        using (StreamWriter sw = new(path))
        {
            sw.WriteLine("id;Vármegye;Település;Típus;Férfi_2024;Nő_2024;Férfi_2025;Nő_2025");
            foreach (Settlement s in settlementsList)
            {
                var values = s.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.GetValue(s)).ToList();
                sw.WriteLine(string.Join(';', values));
            }
        }
    }
    public override List<string> RegionalStatistics(string varmegye)
    {
        List<Settlement> settlements = ListSettlements.Where(s => s.Varmegye == varmegye).ToList();
        int lakossag2024 = settlements.Sum(s => s.Lakossag2024);
        int lakossag2025 = settlements.Sum(s => s.Lakossag2025);
        int novekedes = lakossag2025 - lakossag2024;
        List<String> rows = new();
        rows.Add($"\tVármegye: {varmegye}");
        rows.Add($"\tTelepülések száma: {settlements.Count()}");
        rows.Add($"\tÖsszes lakos 2024-ben: {lakossag2024} fő");
        rows.Add($"\tÖsszes lakos 2025-ben: {lakossag2025} fő");
        rows.Add($"\tNövekedés: {novekedes} fő");
        rows.Add($"\tNövekedés %-ban: {((float)novekedes / lakossag2024 * 100).ToString("F2")}%");
        return rows;
    }
}
