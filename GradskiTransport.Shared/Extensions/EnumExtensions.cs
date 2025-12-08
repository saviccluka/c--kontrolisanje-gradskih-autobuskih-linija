namespace GradskiTransport.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue switch
            {
                Shared.Enums.TipKarte.Jednokratna => "Jednokratna",
                Shared.Enums.TipKarte.Dnevna => "Dnevna",
                Shared.Enums.TipKarte.Nedeljna => "Nedeljna",
                Shared.Enums.TipKarte.Mesecna => "Mesečna",
                Shared.Enums.TipKarte.Godisnja => "Godišnja",
                Shared.Enums.StatusKarte.Aktivna => "Aktivna",
                Shared.Enums.StatusKarte.Iskoriscena => "Iskorišćena",
                Shared.Enums.StatusKarte.Istekla => "Istekla",
                Shared.Enums.StatusKarte.Otkazana => "Otkazana",
                _ => enumValue.ToString()
            };
        }

        public static decimal GetCena(this Shared.Enums.TipKarte tipKarte)
        {
            return tipKarte switch
            {
                Shared.Enums.TipKarte.Jednokratna => Constants.ApplicationConstants.CENA_JEDNOKRATNA,
                Shared.Enums.TipKarte.Dnevna => Constants.ApplicationConstants.CENA_DNEVNA,
                Shared.Enums.TipKarte.Nedeljna => Constants.ApplicationConstants.CENA_NEDELJNA,
                Shared.Enums.TipKarte.Mesecna => Constants.ApplicationConstants.CENA_MESECNA,
                Shared.Enums.TipKarte.Godisnja => Constants.ApplicationConstants.CENA_GODISNJA,
                _ => 0m
            };
        }
    }
}
