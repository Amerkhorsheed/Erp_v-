﻿//using System.Collections.Generic;

//namespace Erp_V1.BLL
//{
//    public class RolePermissionBLL
//    {
//        private readonly DAL.DAO.RolePermissionDAO _dao = new DAL.DAO.RolePermissionDAO();

//        /// <summary>
//        /// Gets a list of permissions for a given role ID.
//        /// </summary>
//        public List<string> GetPermissionsByRole(int roleId)
//        {
//            return _dao.GetPermissionsByRole(roleId);
//        }

//        /// <summary>
//        /// Updates the permissions for a given role.
//        /// </summary>
//        public bool UpdatePermissionsForRole(int roleId, List<string> newPermissions)
//        {
//            return _dao.UpdatePermissionsForRole(roleId, newPermissions ?? new List<string>());
//        }

//        /// <summary>
//        /// Provides a master list of all available permissions in the system.
//        /// This list MUST be derived from the 'Name' property of BarButtonItems in Form1.
//        /// </summary>
//        /// <returns>A list of strings representing each permission.</returns>
//        public List<string> GetAllPossiblePermissions()
//        {
//            return new List<string>
//            {
//                // Category Management
//                "barButtonItem1",   // FrmCategory
//                "barButtonItem2",   // FrmCategoryList

//                // Stock Management
//                "barButtonItem3",   // FrmAddStock
//                "barButtonItem4",   // FrmStockAlert
//                "barButtonItem14",  // FrmStockCal
//                "barButtonItem12",  // frmRs (Report)
//                "barButtonItem26",  // FrmStockFilter

//                // Customer Management
//                "barButtonItem5",   // FrmCustomer
//                "barButtonItem6",   // FrmCustomerList
//                "barButtonItem50",  // FrmCustomerSearchExport

//                // Product Management
//                "barButtonItem7",   // FrmProduct
//                "barButtonItem8",   // FrmProductList
//                "barButtonItem15",  // frmSearchProduct
//                "barButtonItem55",  // FrmProductPredictorModern

//                // Sales Management
//                "barButtonItem9",   // FrmSales
//                "barButtonItem10",  // FrmSalesList
//                "barButtonItem11",  // FrmDeleted
//                "barButtonItem16",  // frmMultiSales (Button 1)
//                "barButtonItem17",  // frmMultiSales (Button 2)
//                "barButtonItem18",  // frmSalesChart (Button 1)
//                "barButtonItem19",  // frmSalesChart (Button 2)
//                "barButtonItem20",  // frmProfitReport
//                "barButtonItem51",  // FrmSalesExport
//                "barButtonItem53",  // FrmScanSales

//                // Recommendations & Analytics
//                "barButtonItem21",  // FrmProductRecommendations
//                "barButtonItem22",  // frmRecommendation
//                "barButtonItem23",  // frmProductShortagePredictor
//                "barButtonItem24",  // RecommendationForm

//                // Supplier & Purchase
//                "barButtonItem34",  // FrmSupplier
//                "barButtonItem35",  // frmSupplierList
//                "barButtonItem27",  // frmPurchase
//                "barButtonItem28",  // FrmPurchaseList
//                "barButtonItem29",  // FrmPurchaseChart
//                "barButtonItem30",  // FrmSupplierPurchaseSummary
//                "barButtonItem31",  // frmSupplierPurchaseReport

//                // Returns & Reporting
//                "barButtonItem32",  // frmReturn
//                "barButtonItem33",  // frmReturnAnalysis
//                "barButtonItem52",  // frmReturnSearch

//                // Expenses & Finance
//                "barButtonItem36",  // FrmExpenses
//                "barButtonItem38",  // FrmExpensesList
//                "barButtonItem37",  // frmOverallProfitReport
//                "barButtonItem39",  // ProfitDashboard
//                "barButtonItem40",  // ProfitForm

//                // HR & User Management
//                "barButtonItem41",  // FrmMagic
//                "barButtonItem42",  // CrudChatbotForm
//                "barButtonItem43",  // cvvrank
//                "barButtonItem44",  // frmchatbot1
//                "barButtonItem45",  // FrmAi
//                "barButtonItem56",  // DepartmentForm
//                "barButtonItem57",  // frmDepartmentSearch
//                "barButtonItem58",  // FrmPosition
//                "barButtonItem59",  // FrmPositionList
//                "barButtonItem60",  // FrmEmployee
//                "barButtonItem61",  // FrmEmployeeList
//                "barButtonItem62",  // FrmRole
//                "barButtonItem63",  // FrmRolesList (was commented out)

//                // Session & System
//                "barButtonItem64",  // Logout
//                "barButtonItem67",  // FrmLoginHistory
//                "barButtonItem66",  // Exit Application
//            };
//        }
//    }
//}

using System.Collections.Generic;

namespace Erp_V1.BLL
{
    public class RolePermissionBLL
    {
        private readonly DAL.DAO.RolePermissionDAO _dao = new DAL.DAO.RolePermissionDAO();

        /// <summary>
        /// Gets a list of permissions for a given role ID.
        /// </summary>
        public List<string> GetPermissionsByRole(int roleId)
        {
            return _dao.GetPermissionsByRole(roleId);
        }

        /// <summary>
        /// Updates the permissions for a given role.
        /// </summary>
        public bool UpdatePermissionsForRole(int roleId, List<string> newPermissions)
        {
            return _dao.UpdatePermissionsForRole(roleId, newPermissions ?? new List<string>());
        }

        /// <summary>
        /// Provides a master list of all available permissions in the system.
        /// This list MUST be derived from the 'Name' property of BarButtonItems in Form1.
        /// </summary>
        /// <returns>A list of strings representing each permission.</returns>
        public List<string> GetAllPossiblePermissions()
        {
            return new List<string>
            {
                // Category Management
                "barButtonItem1",   // FrmCategory
                "barButtonItem2",   // FrmCategoryList

                // Stock Management
                "barButtonItem3",   // FrmAddStock
                "barButtonItem4",   // FrmStockAlert
                "barButtonItem14",  // FrmStockCal
                "barButtonItem12",  // FrmRecommendations (was FrmRs)
                "barButtonItem26",  // FrmStockFilter

                // Customer Management
                "barButtonItem5",   // FrmCustomer
                "barButtonItem6",   // FrmCustomerList
                "barButtonItem50",  // FrmCustomerSearchExport
                "barButtonItem68",  // CustomerCampaignForm
                "barButtonItem69",  // CustomerClassificationForm
                "barButtonItem70",  // CustomerContractForm
                "barButtonItem71",  // CustomerDocumentForm
                "barButtonItem72",  // CustomerInteractionForm
                "barButtonItem73",  // CustomerPointsForm
                "barButtonItem79",  // ChurnPredictionForm

                // Product Management
                "barButtonItem7",   // FrmProduct
                "barButtonItem8",   // FrmProductList
                "barButtonItem15",  // frmSearchProduct
                "barButtonItem55",  // FrmProductPredictorModern

                // Sales Management
                "barButtonItem9",   // FrmSales
                "barButtonItem10",  // FrmSalesList
                "barButtonItem11",  // FrmDeleted (Sales)
                "barButtonItem16",  // frmMultiSales (Button 1)
                "barButtonItem17",  // frmMultiSales (Button 2)
                "barButtonItem18",  // frmSalesChart (Button 1)
                "barButtonItem19",  // frmSalesChart (Button 2)
                "barButtonItem20",  // frmProfitReport
                "barButtonItem51",  // FrmSalesExport
                "barButtonItem53",  // FrmScanSales
                "barButtonItem54",  // ForecastForm

                // Recommendations & Analytics
                "barButtonItem21",  // FrmProductRecommendations
                "barButtonItem22",  // frmRecommendation
                "barButtonItem23",  // frmProductShortagePredictor
                "barButtonItem24",  // RecommendationForm
                
                // Supplier & Purchase
                "barButtonItem27",  // frmPurchase
                "barButtonItem28",  // FrmPurchaseList
                "barButtonItem29",  // FrmPurchaseChart
                "barButtonItem30",  // FrmSupplierPurchaseSummary
                "barButtonItem31",  // frmSupplierPurchaseReport
                "barButtonItem34",  // FrmSupplier
                "barButtonItem35",  // frmSupplierList
                "barButtonItem74",  // SupplierCommunicationForm
                "barButtonItem75",  // SupplierContractForm
                "barButtonItem76",  // SupplierPerformanceForm
                "barButtonItem77",  // SupplierQuoteForm
                "barButtonItem78",  // SupplyScheduleForm

                // Returns & Reporting
                "barButtonItem32",  // frmReturn
                "barButtonItem33",  // frmReturnAnalysis
                "barButtonItem52",  // frmReturnSearch

                // Expenses & Finance
                "barButtonItem36",  // ExpensesAddForm
                "barButtonItem37",  // frmOverallProfitReport
                "barButtonItem38",  // FrmExpensesList (Handler is empty)
                "barButtonItem39",  // ProfitDashboard
                "barButtonItem40",  // ProfitForm
                "barButtonItem46",  // FrmPayment (Button 1)
                "barButtonItem47",  // FrmPartialPayment (Button 1)
                "barButtonItem48",  // FrmPayment (Button 2)
                "barButtonItem49",  // FrmPartialPayment (Button 2)
                "barButtonItem80",  // ExpensesListForm
                "barButtonItem81",  // CategoryExpensesAddForm


                // HR, Employee & User Management
                "barButtonItem41",  // FrmMagic
                "barButtonItem42",  // CrudChatbotForm
                "barButtonItem43",  // cvvrank
                "barButtonItem44",  // frmchatbot1
                "barButtonItem45",  // FrmAi
                "barButtonItem56",  // DepartmentForm
                "barButtonItem57",  // frmDepartmentSearch
                "barButtonItem58",  // FrmPosition
                "barButtonItem59",  // FrmPositionList
                "barButtonItem60",  // FrmEmployee
                "barButtonItem61",  // FrmEmployeeList
                "barButtonItem62",  // FrmRoleAdd
                "barButtonItem63",  // FrmRolesList
                "barButtonItem82",  // TaskAddForm
                "barButtonItem83",  // TaskListForm
                "barButtonItem84",  // SalaryAddForm
                "barButtonItem85",  // SalaryListForm

                // Delivery Management
                "barButtonItem86",  // DeliveryListForm
                "barButtonItem87",  // DeliveryDetailForm (Handler is empty)


                // Session & System
                "barButtonItem64",  // Logout
                "barButtonItem67",  // FrmLoginHistory
                "barButtonItem66",  // Exit Application
            };
        }
    }
}