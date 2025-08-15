using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Seller.API.Controllers
{
    [Authorize(Roles = "Seller,Admin")] // Only allow users with the "Seller" or "Admin" role
    [RoutePrefix("api/sales")]
    public class SalesController : ApiController
    {
        private readonly SalesBLL _salesBll = new SalesBLL();

        // GET: api/sales
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetSales()
        {
            var salesDto = _salesBll.Select();
            if (salesDto == null || salesDto.Sales == null)
            {
                return NotFound();
            }
            return Ok(salesDto.Sales);
        }

        // POST: api/sales
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSale([FromBody] SalesDetailDTO saleDetail)
        {
            // --- SOLUTION PART 1: ADD THIS VALIDATION ---
            if (saleDetail == null)
            {
                return BadRequest("The request body is empty or contains malformed JSON.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Now it's safe to pass saleDetail to the BLL
                bool success = _salesBll.Insert(saleDetail);
                if (success)
                {
                    return Ok(new { message = "Sale created successfully." });
                }
                // If BLL returns false without an exception, it's an internal failure
                return InternalServerError(new System.Exception("The sale could not be saved."));
            }
            catch (System.Exception ex)
            {
                // Log the exception (ex) in a real application
                return InternalServerError(ex);
            }
        }

        // POST: api/sales/cart
        [HttpPost]
        [Route("cart")]
        public IHttpActionResult CreateCartSale([FromBody] List<SalesDetailDTO> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Cart is empty or contains malformed JSON.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Generate a unique transaction ID for this cart sale
                var transactionId = System.Guid.NewGuid().ToString();
                
                // Use the new cart-based transaction method
                var savedSaleIds = _salesBll.InsertTransactionWithItems(cartItems, transactionId);
                if (savedSaleIds != null && savedSaleIds.Any())
                {
                    return Ok(new { message = "Cart sale created successfully.", itemCount = cartItems.Count, saleIds = savedSaleIds, transactionId = transactionId });
                }
                return InternalServerError(new System.Exception("The cart sale could not be saved."));
            }
            catch (System.Exception ex)
            {
                // Log the exception (ex) in a real application
                return InternalServerError(ex);
            }
        }

        // PUT: api/sales/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateSale(int id, [FromBody] SalesDetailDTO saleDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure the ID from the URL matches the DTO
            saleDetail.SalesID = id;

            try
            {
                bool success = _salesBll.Update(saleDetail);
                if (success)
                {
                    return Ok(new { message = $"Sale with ID {id} updated." });
                }
                return NotFound(); // Or InternalServerError if the update failed for other reasons
            }
            catch (System.Exception ex)
            {
                // Log the exception ex
                return InternalServerError(ex);
            }
        }

        // DELETE: api/sales/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteSale(int id)
        {
            try
            {
                // The BLL expects a DTO, so we create one with the necessary ID.
                var dtoToDelete = new SalesDetailDTO { SalesID = id };
                bool success = _salesBll.Delete(dtoToDelete);

                if (success)
                {
                    return Ok(new { message = $"Sale with ID {id} deleted." });
                }
                return NotFound();
            }
            catch (System.Exception ex)
            {
                // Log the exception ex
                return InternalServerError(ex);
            }
        }
    }
}