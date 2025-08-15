using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class SupplierBLL : IBLL<SupplierDetailDTO, SupplierDTO>
    {
        private readonly SupplierDAO _supplierDao = new SupplierDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO();

        public SupplierDTO Select()
        {
            var suppliers = _supplierDao.Select();    // now guaranteed non‑null & ordered
            var products = _productDao.Select();
            var categories = _categoryDao.Select();

            return new SupplierDTO
            {
                Suppliers = suppliers,
                Products = products,
                Categories = categories
            };
        }

        public bool Insert(SupplierDetailDTO entity)
        {
            var supplier = new SUPPLIER
            {
                SupplierName = entity.SupplierName,
                PhoneNumber = entity.PhoneNumber
            };
            return _supplierDao.Insert(supplier);
        }

        public bool Update(SupplierDetailDTO entity)
        {
            var supplier = new SUPPLIER
            {
                ID = entity.SupplierID,
                SupplierName = entity.SupplierName,
                PhoneNumber = entity.PhoneNumber
            };
            return _supplierDao.Update(supplier);
        }

        public bool Delete(SupplierDetailDTO entity)
        {
            var supplier = new SUPPLIER { ID = entity.SupplierID };
            return _supplierDao.Delete(supplier);
        }

        public bool GetBack(SupplierDetailDTO entity)
            => _supplierDao.GetBack(entity.SupplierID);

        public bool UpdateProductStock(int productId, int newStock)
            => _productDao.UpdateStock(productId, newStock);
    }
}
