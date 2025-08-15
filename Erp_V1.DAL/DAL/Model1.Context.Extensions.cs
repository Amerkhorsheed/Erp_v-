//------------------------------------------------------------------------------ 
// Manual extension to add missing DbSets to erp_v2Entities context 
// This file extends the auto-generated context with additional entities 
//------------------------------------------------------------------------------ 
 
namespace Erp_V1.DAL.DAL 
{ 
    using System.Data.Entity; 
 
    public partial class erp_v2Entities 
    { 
        public virtual DbSet<INVOICE_ITEM> INVOICE_ITEM { get; set; }
        public virtual DbSet<NotificationSettings> NotificationSettings { get; set; }
    } 
}
