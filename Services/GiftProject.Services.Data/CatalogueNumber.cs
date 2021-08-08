namespace GiftProject.Services.Data
{
    using System;

    public class CatalogueNumber : ICatalogueNumber
    {
        public string CreateCatalogueNumber(string date)
        {
            var parseDate = DateTime.Parse(date);
            var ticks = parseDate.Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");

            return uniqueId;
        }
    }
}
