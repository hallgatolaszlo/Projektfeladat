using Microsoft.Win32;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WPFNepesseg;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 

//A kódban alapvetően bárhol dolgozhat, de a kapott kódokat lehetőleg tartsa meg!

public partial class MainWindow : Window
{
    // A települések listája, amit a CSV fájlból töltünk be.
    List<Telepules> telepulesek = new();

    // A DataGrid-ben megjelenítendő települések listája, ami a ComboBox szűrésének eredménye.
    ObservableCollection<Telepules> szurt_telepulesek = new();

    public MainWindow()
    {
        InitializeComponent();
        // A települések betöltése a CSV fájlból.
        BetoltesCSVbol("Source\\Lakossag_2024_2025.csv");
        // A ComboBox és a DataGrid vezérlők előkészítése.
        VezerlokElokeszitese();
    }


    private void BetoltesCSVbol(String CSVfajl)
    {
        File.ReadAllLines(CSVfajl, Encoding.UTF8).Skip(1).ToList().ForEach(sor => telepulesek.Add(new Telepules(sor)));
    }

    /// <summary>
    /// A ComboBox és a DataGrid vezérlőket a települések adatai alapján tölti fel.
    /// </summary>
    private void VezerlokElokeszitese()
    {
        // TODO : Itt töltse fel a ComboBox-ot a települések adatai alapján!
        SortedSet<string> set = [];
        telepulesek.ToList().ForEach(x => set.Add(x.Varmegye));
        List<string> items = set.ToList();
        items.Insert(0, "Összes megye");

        cbTelepulesek.ItemsSource = items;

        szurt_telepulesek.Clear();

        if (cbTelepulesek.SelectedIndex == 0)
        {
            telepulesek.ToList().ForEach(telepules => szurt_telepulesek.Add(telepules));
        }
        else
        {
            telepulesek.ToList().ForEach(telepules =>
            {
                if ((string)cbTelepulesek.SelectedValue == telepules.Varmegye)
                    szurt_telepulesek.Add(telepules);
            });
        }

        dgTelepulesek.ItemsSource = szurt_telepulesek;
        SzurtAdatokSzerintFrissites();
    }

    private void dgTelepulesek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Ezt alakítsa át!
        Telepules valasztott = (Telepules)dgTelepulesek.SelectedItem;

        if (valasztott != null)
        {
            tbValasztottTelepules.Text = valasztott.TelepulesNev;
            tbValasztottTelepulesTipus.Text = valasztott.Tipus;
            tbValasztottTelepulesNok2024.Text = $"Nők: {valasztott.No2024} fő";
            tbValasztottTelepulesFerfiak2024.Text = $"Férfiak: {valasztott.Ferfi2024} fő";
            tbValasztottTelepulesNok2025.Text = $"Nők: {valasztott.No2025} fő";
            tbValasztottTelepulesFerfiak2025.Text = $"Férfiak: {valasztott.Ferfi2025} fő";
        }
    }

    private void SzurtAdatokSzerintFrissites()
    {
        // A szűrt települések számának és lakosságának frissítése.
        tbSzuresiTelepulesekSzama.Text = $"{szurt_telepulesek.Count} db";

        // A szűrt települések összes lakosságának számítása.
        // A ■ helyett a lakosság számát írja ki!
        tbSzuresiLakossagSzama2024.Text = $"{szurt_telepulesek.Sum(t => t.Lakosok2024)} fő";
        tbSzuresiLakossagSzama2025.Text = $"{szurt_telepulesek.Sum(t => t.Lakosok2025)} fő";
    }

    private void btnCSVmentes_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        saveFileDialog.Title = "CSV fájl mentése";
        saveFileDialog.FileName = saveFileDialog.FileName.Replace(".csv", "");
        saveFileDialog.FileName += ".csv";

        if (saveFileDialog.ShowDialog() == false)
        {
            return;
        }

        string CSVfejlec = "ID;Vármegye;Település;Típus;Férfi_2024;Nő_2024;Férfi_2025;Nő_2025";
        List<string> sorok = new List<string>();
        sorok.Add(CSVfejlec);
        szurt_telepulesek.ToList().ForEach(x => sorok.Add($"{x.ID};{x.Varmegye};{x.TelepulesNev};{x.Tipus};{x.Ferfi2024};{x.No2024};{x.Ferfi2025};{x.No2025}"));
        File.WriteAllLines(saveFileDialog.FileName, sorok, Encoding.UTF8);
    }

    private void btnCSVtoltes_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        openFileDialog.Title = "CSV fájl kiválasztása";
        if (openFileDialog.ShowDialog() == true)
        {
            telepulesek.Clear();
            BetoltesCSVbol(openFileDialog.FileName);
            VezerlokElokeszitese();
        }
    }

    // A szűrő alaphelyzetbe állítása
    private void btnSzuroAlaphelyzetbe_Click(object sender, RoutedEventArgs e)
    {
        // TODO : Itt állítsa vissza a ComboBox-ot az alaphelyzetbe!
        cbTelepulesek.SelectedIndex = 0;
    }

    private void cbTelepulesek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        VezerlokElokeszitese();
    }

    private void btnTorles_Click(object sender, RoutedEventArgs e)
    {
        if (dgTelepulesek.SelectedItem == null)
        {
            MessageBox.Show("Kérlek válassz ki egy települést a törléshez", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (MessageBox.Show($"Biztosan törölni szeretnéd a(z) {((Telepules)dgTelepulesek.SelectedItem).TelepulesNev} települést?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
        {
            return;
        }

        telepulesek.Remove((Telepules)dgTelepulesek.SelectedItem);
        szurt_telepulesek.Remove((Telepules)dgTelepulesek.SelectedItem);

        SzurtAdatokSzerintFrissites();
    }
}