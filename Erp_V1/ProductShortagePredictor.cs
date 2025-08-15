using System;
using System.Collections.Generic;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public class ProductShortagePredictor
    {
        private LinearRegressionModel _linearRegressionModel;
        private ProductBLL _productBLL;

        public ProductShortagePredictor(ProductBLL productBLL)
        {
            _productBLL = productBLL;
            _linearRegressionModel = new LinearRegressionModel();
        }

        public List<ProductDetailDTO> PredictStockAmounts()
        {
            List<ProductDetailDTO> products = _productBLL.Select().Products;

            // Train the linear regression model
            _linearRegressionModel.Train(products);

            // Predict stock amounts and update the product list
            foreach (ProductDetailDTO product in products)
            {
                int predictedStockAmount = _linearRegressionModel.Predict(product);
                product.stockAmount = predictedStockAmount;
            }

            return products;
        }
    }
}