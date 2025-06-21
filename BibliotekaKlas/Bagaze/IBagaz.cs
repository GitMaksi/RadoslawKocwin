namespace BibliotekaKlas.Bagaze
{
    // Abstrakcja: interfejs mówi, co obiekt bagażu musi umieć
    public interface IBagaz
    {
        Guid Id { get; }
        string Opis { get; }
        double Waga { get; }
        string Typ { get; }
        string Szczegoly();
    }
}
