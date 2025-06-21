namespace PrzechowalniaBagazu
{
    // Abstrakcja: interfejs mówi, co obiekt baga¿u musi umieæ
    public interface IBagaz
    {
        Guid Id { get; }
        string Opis { get; }
        double Waga { get; }
        string Typ { get; }
        string Szczegoly();
    }

    // Abstrakcja + Hermetyzacja: ukrycie implementacji + wspólna baza dla klas dziedzicz¹cych
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

        //bazowa implementacja - któr¹ mo¿na nadpisaæ
        public void Przeszukaj()
        {
            Console.WriteLine("Przeszukuje bagaz");
        }
    }

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


            //pobraæ dane z bazy
            //zmodyfikowaæ dane
            Console.WriteLine("To jest Walizka");
            return $"[{Id}] \"{Opis}\" - {Typ}, {Waga} kg, Kó³ka: {Kolka}";
        }
    }

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

    public class MainForm : Form
    {
        private TextBox txtOpis;
        private NumericUpDown numWaga;
        private NumericUpDown numKolka;
        private CheckBox chkLaptop;
        private ComboBox cmbTyp;
        private Button btnDodaj, btnUsun;
        private ListBox lstBagaze;

        private List<IBagaz> bagaze = new List<IBagaz>();

        public MainForm()
        {
            Text = "Przechowalnia Baga¿u (OOP)";
            Width = 550;
            Height = 520;

            // UI controls
            Label lblOpis = new Label { Text = "Opis:", Left = 20, Top = 20 };
            txtOpis = new TextBox { Left = 120, Top = 20, Width = 350 };

            Label lblWaga = new Label { Text = "Waga (kg):", Left = 20, Top = 60 };
            numWaga = new NumericUpDown { Left = 120, Top = 60, Width = 100, DecimalPlaces = 1, Maximum = 1000 };

            Label lblTyp = new Label { Text = "Typ:", Left = 20, Top = 100 };
            cmbTyp = new ComboBox { Left = 120, Top = 100, Width = 150 };
            cmbTyp.Items.AddRange(new[] { "Walizka", "Plecak" });
            cmbTyp.SelectedIndexChanged += (s, e) => ToggleFields();

            Label lblKolka = new Label { Text = "Liczba kó³ek:", Left = 20, Top = 140 };
            numKolka = new NumericUpDown { Left = 120, Top = 140, Width = 100, Maximum = 10 };

            chkLaptop = new CheckBox { Text = "Z laptopem", Left = 120, Top = 180 };

            btnDodaj = new Button { Text = "Dodaj baga¿", Left = 120, Top = 220, Width = 150 };
            btnDodaj.Click += (s, e) => DodajBagaz();

            btnUsun = new Button { Text = "Usuñ zaznaczony", Left = 280, Top = 220, Width = 150 };
            btnUsun.Click += (s, e) => UsunWybrany();

            lstBagaze = new ListBox { Left = 20, Top = 270, Width = 500, Height = 180 };

            Controls.AddRange(new Control[]
            {
                lblOpis, txtOpis, lblWaga, numWaga, lblTyp, cmbTyp,
                lblKolka, numKolka, chkLaptop, btnDodaj, btnUsun, lstBagaze
            });
        }

        private void ToggleFields()
        {
            bool isWalizka = cmbTyp.SelectedItem?.ToString() == "Walizka";
            numKolka.Enabled = isWalizka;
            chkLaptop.Enabled = !isWalizka;
        }

        private void DodajBagaz()
        {
            if (string.IsNullOrWhiteSpace(txtOpis.Text) || cmbTyp.SelectedItem == null)
            {
                MessageBox.Show("Uzupe³nij wszystkie dane.");
                return;
            }

            IBagaz nowy;
            if (cmbTyp.SelectedItem.ToString() == "Walizka")
                nowy = new Walizka(txtOpis.Text, (double)numWaga.Value, (int)numKolka.Value);
            else
                nowy = new Plecak(txtOpis.Text, (double)numWaga.Value, chkLaptop.Checked);

            bagaze.Add(nowy);
            lstBagaze.Items.Add(nowy.Szczegoly());

            txtOpis.Clear();
            numWaga.Value = 0;
            numKolka.Value = 0;
            chkLaptop.Checked = false;
        }

        private void UsunWybrany()
        {
            if (lstBagaze.SelectedItem == null) return;

            string selected = lstBagaze.SelectedItem.ToString();
            int idStart = selected.IndexOf('[') + 1;
            int idEnd = selected.IndexOf(']');
            if (idStart >= 0 && idEnd > idStart)
            {
                string idStr = selected.Substring(idStart, idEnd - idStart);
                if (Guid.TryParse(idStr, out Guid id))
                {
                    var doUsuniecia = bagaze.FirstOrDefault(b => b.Id == id);
                    if (doUsuniecia != null)
                    {
                        bagaze.Remove(doUsuniecia);
                        lstBagaze.Items.Remove(selected);
                    }
                }
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
