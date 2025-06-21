namespace BibliotekaKlas.Bagaze
{
    // Abstrakcja + Hermetyzacja: ukrycie implementacji + wspólna baza dla klas dziedziczących
    public abstract class Bagaz : IBagaz
    {
        public Guid Id { get; private set; }
        public string Opis { get; protected set; }
        public double Waga { get; protected set; }

        public Bagaz(string opis, double waga)
        {
            Id = Guid.NewGuid();
            Opis = opis;
            Waga = waga;
        }

        public abstract string Typ { get; }
        public abstract string Szczegoly(); // Polimorfizm

        //bazowa implementacja - którą można nadpisać
        public void Przeszukaj()
        {
            Console.WriteLine("Przeszukuje bagaz");
        }
    }
}
