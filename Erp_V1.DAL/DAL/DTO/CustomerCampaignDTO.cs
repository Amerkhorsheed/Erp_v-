using System;

namespace Erp_V1.DAL.DTO
{
    public class CustomerCampaignDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } 
        public string CampaignName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Impact { get; set; }
    }
}
