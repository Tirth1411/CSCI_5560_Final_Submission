using Microsoft.AspNetCore.Mvc;
using ROS.Implement.Repository;
using ROS.Model;

namespace ROS.UI.Controllers
{
    public class HW6Controller : Controller
    {
        HttpClient client = new HttpClient();
        private readonly IHW6Repository _IHW6Repository;
        public HW6Controller(IHW6Repository IHW6Repository)
        {
            this._IHW6Repository = IHW6Repository;
        }


        public async Task<IActionResult> Index()
        {
            var sd = await _IHW6Repository.getShipment();
            return View();
        }






        public async Task<IActionResult> InsertShipment55(Shipment paramData)
        {
            try
            {
                var message = await _IHW6Repository.InsertShipment(paramData);

                if (message == "Insertion successful.")
                {
                    return Ok(new { Result = "OK", Message = message });
                }
                else if (message.Contains("Duplicate entry"))
                {
                    return BadRequest(new { Result = "ERROR", Message = message });
                }
                else
                {
                    return StatusCode(500, new { Result = "ERROR", Message = message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Result = "ERROR", Message = "An error occurred: " + ex.Message });
            }
        }


        #region Testing
        [HttpPost]
        public async Task<IActionResult> InsertShipment(Shipment shipment)
        {
            try
            {
                var message = await _IHW6Repository.InsertShipment(shipment);

                if (message.StartsWith("Success"))
                    return Json(new { Result = "Success", Message = message });
                else if (message.StartsWith("Duplicate"))
                    return Json(new { Result = "Warning", Message = message });

                return Json(new { Result = "Warning", Message = message });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = $"An unexpected error occurred: {ex.Message}" });
            }
        }
        #endregion

        public async Task<IActionResult> IncreaseSupplierStatus()
        {
            try
            {
                var message = await _IHW6Repository.IncreaseSupplierStatusBy10Percent();

                if (message.StartsWith("Status"))
                {
                    return Json(new { Result = "OK", Message = message });
                    // return Ok(new { Result = "OK", Message = message });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Somthing goes wrong" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = $"An unexpected error occurred: {ex.Message}" });
                // return StatusCode(500, new { Result = "ERROR", Message = "An error occurred: " + ex.Message });
            }
        }


        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                var suppliers = await _IHW6Repository.GetAllSuppliers();
                return Json(suppliers);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        public async Task<IActionResult> GetSuppliersByPart(string partNumber)
        {
            try
            {
                var suppliers = await _IHW6Repository.GetSuppliersByPartNumber(partNumber);
                return Json(suppliers);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = $"An unexpected error occurred: {ex.Message}" });
                // return StatusCode(500, new { Result = "ERROR", Message = "An error occurred: " + ex.Message });
            }
        }


        //public async Task<IActionResult> DisplayAllSuppliers()
        //{
        //    try
        //    {
        //        var suppliers = await _IHW6Repository.GetAllSuppliers();
        //        return View(suppliers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Result = "ERROR", Message = "An error occurred: " + ex.Message });
        //    }
        //}

    }
}

