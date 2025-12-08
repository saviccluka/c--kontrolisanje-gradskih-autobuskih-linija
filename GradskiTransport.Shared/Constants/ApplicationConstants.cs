namespace GradskiTransport.Shared.Constants
{
    public static class ApplicationConstants
    {
        // Cene karata
        public const decimal CENA_JEDNOKRATNA = 90m;
        public const decimal CENA_DNEVNA = 200m;
        public const decimal CENA_NEDELJNA = 800m;
        public const decimal CENA_MESECNA = 3000m;
        public const decimal CENA_GODISNJA = 30000m;

        // Tipovi vozila
        public const string TIP_AUTOBUS = "Autobus";
        public const string TIP_TRAMVAJ = "Tramvaj";
        public const string TIP_TROLEJBUS = "Trolejbus";

        // Statusi karata
        public const string STATUS_AKTIVNA = "Aktivna";
        public const string STATUS_ISKORISCENA = "Iskorišćena";
        public const string STATUS_ISTEKLA = "Istekla";
        public const string STATUS_OTKAZANA = "Otkazana";

        // Database
        public const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=GradskiTransport;Integrated Security=true;TrustServerCertificate=true;Connection Timeout=10;Command Timeout=30;";

        // Validacija
        public const int MIN_PUTNIK_ID = 1;
        public const int MIN_STANICA_ID = 1;
        public const int MIN_LINIJA_ID = 1;
        public const int MIN_KARTA_ID = 1;

        // UI
        public const string APP_TITLE = "Gradski Transport";
        public const string APP_VERSION = "1.0.0";
    }
}
