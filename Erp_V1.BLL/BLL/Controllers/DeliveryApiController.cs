//// The necessary using directives
//using Erp_V1.ApiDTO;
//using Erp_V1.ApiDTO.Erp_V1.ApiDTO;
//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Linq;
//using System.Web.Http;
//using System.Web.Http.Cors; // <-- THIS LINE WILL NOW WORK CORRECTLY

//namespace Erp_V1.Controllers
//{
//    // The [EnableCors] attribute is now recognized
//    [EnableCors(origins: "*", headers: "*", methods: "*")]
//    [RoutePrefix("api/delivery")]
//    public class DeliveryApiController : ApiController
//    {
//        private readonly EmployeeBLL _employeeBll = new EmployeeBLL();
//        private readonly DeliveryBLL _deliveryBll = new DeliveryBLL();

//        [HttpPost]
//        [Route("login")]
//        public IHttpActionResult Login(LoginRequestDTO loginRequest)
//        {
//            if (loginRequest == null)
//                return BadRequest("Invalid login request.");

//            var employee = _employeeBll.GetByUserNo(loginRequest.UserNo);
//            if (employee == null)
//            {
//                return Unauthorized();
//            }

//            if (!_employeeBll.Authenticate(employee.EmployeeID, loginRequest.Password))
//            {
//                return Unauthorized();
//            }

//            return Ok(new { employee.EmployeeID, FullName = employee.FullName, employee.PositionName });
//        }

//        [HttpGet]
//        [Route("mydeliveries/{deliveryPersonId:int}")]
//        public IHttpActionResult GetMyDeliveries(int deliveryPersonId)
//        {
//            try
//            {
//                var allDeliveries = _deliveryBll.Select().Deliveries;
//                var myDeliveries = allDeliveries
//                    .Where(d => d.DeliveryPersonID == deliveryPersonId && d.Status != "Delivered")
//                    .OrderBy(d => d.AssignedDate)
//                    .ToList();
//                return Ok(myDeliveries);
//            }
//            catch (Exception ex)
//            {
//                return InternalServerError(ex);
//            }
//        }

//        [HttpPost]
//        [Route("updatestatus")]
//        public IHttpActionResult UpdateDeliveryStatus(UpdateStatusRequestDTO request)
//        {
//            if (request == null || request.DeliveryID <= 0 || string.IsNullOrWhiteSpace(request.Status))
//                return BadRequest("Invalid request data.");

//            try
//            {
//                var delivery = _deliveryBll.Select().Deliveries.FirstOrDefault(d => d.DeliveryID == request.DeliveryID);

//                if (delivery == null)
//                    return NotFound();

//                if (delivery.DeliveryPersonID != request.DeliveryPersonID)
//                    return Unauthorized();

//                delivery.Status = request.Status;
//                if (request.Status == "Delivered")
//                {
//                    delivery.DeliveredDate = DateTime.Now;
//                }

//                bool success = _deliveryBll.Update(delivery);

//                if (success)
//                    return Ok(new { message = "Status updated successfully." });
//                else
//                    return InternalServerError(new Exception("Database update failed."));
//            }
//            catch (Exception ex)
//            {
//                return InternalServerError(ex);
//            }
//        }
//    }
//}