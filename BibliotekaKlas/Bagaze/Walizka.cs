namespace BibliotekaKlas.Bagaze
{
    // Dziedziczenie: Walizka dziedziczy po Bagaz
    public class Walizka : Bagaz
    {
        public int Kolka { get; private set; }

        public Walizka(string opis, double waga, int kolka) : base(opis, waga)
        {
            Kolka = kolka;
        }

        public override string Typ => "Walizka";

        public override string Szczegoly()
        {
            //ABSTRAKCJA NIEZALEZNA OD IMPLEMENTACJI

            //operacje pośrednie w kontekscie metody:
            //pobrać dane z bazy
            //zmodyfikować dane
            Console.WriteLine("To jest Walizka");
            return $"[{Id}] \"{Opis}\" - {Typ}, {Waga} kg, Kółka: {Kolka}";
        }
    }
}
