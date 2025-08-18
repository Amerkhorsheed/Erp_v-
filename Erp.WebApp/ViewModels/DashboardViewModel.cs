using System.Collections.Generic;

namespace Erp.WebApp.ViewModels
{
    public class DashboardViewModel
    {
        public List<KpiViewModel> KPIs { get; set; }
        public string CurrencySymbol { get; set; }
    }

    public class KpiViewModel
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public string Format { get; set; } // "currency" or "int"
        public string TrendText { get; set; }
        public string IconClass { get; set; }
        public string TrendIcon { get; set; }
        public string GradientClass { get; set; }
    }
}