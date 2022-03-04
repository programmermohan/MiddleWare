using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Route("GetEmployees")]
        public IActionResult GetEmployees()
        {
            return Ok(new List<string>()
            {
                "Employee01", "Employee02", "Employee03", "Employee04", "Employee05"
            });
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------//

        [HttpGet]
        [Route("GetEmployeeById")]
        public IActionResult GetEmployeeById(long EmpId)
        {
            var employee = "Employee01";
            return Ok(employee);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
