namespace BibliotekaKlas.Bagaze
{
    // Dziedziczenie: Plecak dziedziczy po Bagaz
    public class Plecak : Bagaz
    {
        public bool MaLaptopa { get; private set; }

        public Plecak(string opis, double waga, bool maLaptopa) : base(opis, waga)
        {
            MaLaptopa = maLaptopa;
        }

        public override string Typ => "Plecak";

        public override string Szczegoly()
        {
            Console.WriteLine("To jest plecak");
            return $"[{Id}] \"{Opis}\" - {Typ}, {Waga} kg, Laptop: {(MaLaptopa ? "tak" : "nie")}";
        }
    }
}
