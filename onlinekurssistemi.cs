

using System;


class Program
{
    // Kurs sınıfı, kursun adını, ücretini ve öğretmeninin bilgilerini içerir.
    public class Kurs
    {
        public string Ad { get; set; }
        public double Ucret { get; set; }
        public Ogretmen Ogretmen { get; set; }

        public Kurs(string ad, double ucret, Ogretmen ogretmen)
        {
            Ad = ad;
            Ucret = ucret;
            Ogretmen = ogretmen;
        }
    }

    // Öğretmen sınıfı, öğretmenin adı, soyadı ve telefon numarası bilgilerini içerir.
    public class Ogretmen
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }

        public Ogretmen(string ad, string soyad, string telefon)
        {
            Ad = ad;
            Soyad = soyad;
            Telefon = telefon;
        }

        // Öğretmenin bilgilerini yazdırır.
        public void BilgileriGoster()
        {
            Console.WriteLine($"Öğretmen: {Ad} {Soyad} | Telefon: {Telefon}");
        }
    }

    // Randevu sınıfı, randevunun öğrenci adı, kurs ve tarih bilgilerini içerir.
    public class Randevu
    {
        public string OgrenciAd { get; set; }
        public Kurs Kurs { get; set; }
        public DateTime Tarih { get; set; }

        public Randevu(string ogrenciAd, Kurs kurs, DateTime tarih)
        {
            OgrenciAd = ogrenciAd;
            Kurs = kurs;
            Tarih = tarih;
        }

        // Randevu bilgilerini yazdırır.
        public void RandevuBilgileriniGoster()
        {
            Console.WriteLine($"Öğrenci: {OgrenciAd} | Kurs: {Kurs.Ad} | Tarih: {Tarih.ToShortDateString()} | Ücret: {Kurs.Ucret} TL");
            Kurs.Ogretmen.BilgileriGoster();
        }
    }

    static void Main(string[] args)
    {
        // Öğretmen nesnelerini oluşturalım.
        Ogretmen dansOgretmeni = new Ogretmen("Selin", "Uyar", "0555-345-0847");
        Ogretmen seramikOgretmeni = new Ogretmen("Emir", "Uçar", "0555-098-4563");

        // Kurs nesnelerini oluşturalım.
        Kurs dansKursu = new Kurs("Dans", 360.0, dansOgretmeni);
        Kurs seramikKursu = new Kurs("Seramik", 220.0, seramikOgretmeni);

        List<Randevu> randevular = new List<Randevu>();


        //menü oluşturma
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Online Kurs Sistemi");
            Console.WriteLine("1. Dans Kursu için Randevu Al");
            Console.WriteLine("2. Seramik Kursu için Randevu Al");
            Console.WriteLine("3. Randevu Bilgilerini Görüntüle");
            Console.WriteLine("4. Randevu İptal Et");
            Console.WriteLine("5. Randevu Tarihi Değiştir");
            Console.WriteLine("6. Çıkış");
            Console.Write("Bir seçenek girin: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    RandevuAl(dansKursu, randevular);
                    break;
                case "2":
                    RandevuAl(seramikKursu, randevular);
                    break;
                case "3":
                    RandevulariGoruntule(randevular);
                    break;
                case "4":
                    RandevuIptalEt(randevular);
                    break;
                case "5":
                    RandevuDegistir(randevular);
                    break;
                case "6":
                    Console.WriteLine("Çıkılıyor...");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                    break;
            }
            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }

    //randevu alma işlemi
    static void RandevuAl(Kurs kurs, List<Randevu> randevular)
    {
        Console.Clear();
        Console.WriteLine($"{kurs.Ad} Kursuna Randevu Al");
        Console.WriteLine($"Kurs Ücreti: {kurs.Ucret} TL");
        Console.Write("Öğrenci Adı: ");
        string ogrenciAd = Console.ReadLine();
        Console.Write("Randevu Tarihi (yyyy-mm-dd): ");
        DateTime tarih = DateTime.Parse(Console.ReadLine());

        Randevu yeniRandevu = new Randevu(ogrenciAd, kurs, tarih);
        randevular.Add(yeniRandevu);

        Console.WriteLine("Randevunuz başarıyla alındı.");
    }

    // oluşturulmuş randevuları görüntüleme
    static void RandevulariGoruntule(List<Randevu> randevular)
    {
        Console.Clear();
        if (randevular.Count == 0)
        {
            Console.WriteLine("Henüz bir randevu alınmamış.");
        }
        else
        {
            Console.WriteLine("Alınan Randevular:");
            foreach (var randevu in randevular)
            {
                randevu.RandevuBilgileriniGoster();
                Console.WriteLine();
            }
        }
    }

    //randevu iptal etme
    static void RandevuIptalEt(List<Randevu> randevular)
    {
        Console.Clear();
        if (randevular.Count == 0)
        {
            Console.WriteLine("Henüz bir randevu alınmamış.");
            return;
        }

        Console.WriteLine("İptal Etmek İstediğiniz Randevuyu Seçin:");
        for (int i = 0; i < randevular.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {randevular[i].OgrenciAd} - {randevular[i].Kurs.Ad} - {randevular[i].Tarih.ToShortDateString()}");
        }

        Console.Write("Seçim: ");
        int secim = int.Parse(Console.ReadLine());
        if (secim > 0 && secim <= randevular.Count)
        {
            randevular.RemoveAt(secim - 1);
            Console.WriteLine("Randevu başarıyla iptal edildi.");
        }
        else
        {
            Console.WriteLine("Geçersiz seçim.");
        }
    }
    //randevuyu değiştirme
    static void RandevuDegistir(List<Randevu> randevular)
    {
        Console.Clear();
        if (randevular.Count == 0)
        {
            Console.WriteLine("Henüz bir randevu alınmamış.");
            return;
        }

        Console.WriteLine("Değiştirmek İstediğiniz Randevuyu Seçin:");
        for (int i = 0; i < randevular.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {randevular[i].OgrenciAd} - {randevular[i].Kurs.Ad} - {randevular[i].Tarih.ToShortDateString()}");
        }

        Console.Write("Seçim: ");
        int secim = int.Parse(Console.ReadLine());
        if (secim > 0 && secim <= randevular.Count)
        {
            Console.Write("Yeni Tarihi Girin (yyyy-mm-dd): ");
            DateTime yeniTarih = DateTime.Parse(Console.ReadLine());
            randevular[secim - 1].Tarih = yeniTarih;
            Console.WriteLine("Randevu tarihi başarıyla değiştirildi.");
        }
        else
        {
            Console.WriteLine("Geçersiz seçim.");
        }
    }
}
