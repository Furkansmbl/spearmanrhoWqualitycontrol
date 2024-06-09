using System;
using System.Collections.Generic;
using System.Linq;

public class AltListe
{
    public double Deger1 { get; set; }
    public int Deger2 { get; set; }
    public double Deger3 { get; set; }
    public double Deger4 { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Verilen değerler
        double[] verilenDegerler = { 0.25, 0.2, 0.27, 0.34, 0.14, 0.27, 0.25, 0.25, 0.25, 0.27, 0.22, 0.25, 0.25, 0.25 };

        List<AltListe> anaListe = new List<AltListe>();

        // Verilen değerleri ve sıralı değerleri listeye ekliyoruz
        for (int i = 0; i < verilenDegerler.Length; i++)
        {
            AltListe altListe = new AltListe
            {
                Deger1 = verilenDegerler[i],
                Deger2 = i + 1 // 1'den 14'e kadar sırayla
            };
            anaListe.Add(altListe);
        }

        // Şimdi ana listeyi, 1. değere göre sıralayalım
        var siraliListe = anaListe.OrderBy(l => l.Deger1).ThenBy(l => anaListe.IndexOf(l)).ToList();

        // Sıralanan listedeki Deger2 değerlerine göre Deger3 değerini atayalım
        for (int i = 0; i < anaListe.Count; i++)
        {
            anaListe[i].Deger3 = siraliListe[i].Deger2;
        }

        // Deger4'ü hesaplayalım (Deger3 - Deger2)^2
        foreach (var altListe in anaListe)
        {
            altListe.Deger4 = Math.Pow(altListe.Deger3 - altListe.Deger2, 2);
        }

        // Spearman Rho'yu hesaplayalım
        double toplamDeger4 = anaListe.Sum(l => l.Deger4);
        double n = anaListe.Count;
        double spearmanRho = 1 - (6 * toplamDeger4 / (n * (Math.Pow(n, 2) - 1)));

        // zDegeri hesaplayalım
        double zDegeri = spearmanRho * Math.Sqrt(n - 1);

        // Trend testi
        string Karar, Sonuc, Sonuc2;


        if (Math.Abs(zDegeri) > 2.326347874)
            Karar = "Ret";
        else
            Karar = "Kabul";

        if (Math.Abs(zDegeri) <= 2.326347874)
            Sonuc = "Trend Yok";

        else if (Math.Abs(zDegeri) >= 0 && (zDegeri) >= -2.326347874)
            Sonuc = "Negatif Trend var";
        else
            Sonuc = "Pozitif Trend var";

        // Sonuçları yazdır
        foreach (var altListe in anaListe)
        {
            Console.WriteLine($"Deger1: {altListe.Deger1}, Deger2: {altListe.Deger2}, Deger3: {altListe.Deger3}, Deger4: {altListe.Deger4}");
        }

        Console.WriteLine($"SpearmanRho: {spearmanRho}");
        Console.WriteLine($"zDegeri: {zDegeri}");
        Console.WriteLine($"Karar: {Karar}");
        Console.WriteLine($"Sonuc: {Sonuc} ");
    }
}