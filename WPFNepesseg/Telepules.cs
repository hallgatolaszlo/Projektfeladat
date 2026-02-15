namespace WPFNepesseg
{
    // ■ ■ ■ ->
    public class Telepules
    {
        private string id;
        private string varmegye;
        private string telepulesNev;
        private string tipus;
        private int ferfi2024;
        private int no2024;
        private int ferfi2025;
        private int no2025;

        //ID;vármegye;település;típus;férfi_2024;nő_2024;férfi_2025;nő_2025
        public Telepules(string line)
        {
            var fields = line.Split(';');
            id = fields[0];
            varmegye = fields[1];
            telepulesNev = fields[2];
            tipus = fields[3];
            ferfi2024 = int.Parse(fields[4]);
            no2024 = int.Parse(fields[5]);
            ferfi2025 = int.Parse(fields[6]);
            no2025 = int.Parse(fields[7]);
        }

        public string ID { get => id; }
        public string Varmegye { get => varmegye; }
        public string TelepulesNev { get => telepulesNev; }
        public string Tipus { get => tipus; }
        public int Ferfi2024 { get => ferfi2024; set => ferfi2024 = value; }
        public int No2024 { get => no2024; set => no2024 = value; }
        public int Ferfi2025 { get => ferfi2025; set => ferfi2025 = value; }
        public int No2025 { get => no2025; set => no2025 = value; }

        public int Lakosok2024 { get => ferfi2024 + no2024; }
        public int Lakosok2025 { get => ferfi2025 + no2025; }
        public int Valtozas { get => Lakosok2025 - Lakosok2024; }
        public double ValtozasSzazalekban { get => (double)Lakosok2025 / Lakosok2024 * 100 - 100; }
        //<- ■ ■ ■
        // TODO: VarosTipusuTelepules tulajdonság létrehozása
        // Itt Dolgozzon!
        public bool VarosTipusuTelepules { get => Tipus == "város" || Tipus == "vármegyei jogú város" || Tipus == "vármegye székhely"; }
        // ■ ■ ■ ->
    }
}