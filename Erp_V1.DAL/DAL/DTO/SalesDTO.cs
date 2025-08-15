using Erp_V1.DAL.DTO;

using System.Collections.Generic;


public class SalesDTO
{
    
    public List<SalesDetailDTO> Sales { get; set; }

  
    public List<CustomerDetailDTO> Customers { get; set; }


    public List<ProductDetailDTO> Products { get; set; }

 
    public List<CategoryDetailDTO> Categories { get; set; }
}
