using FullStack.Data;
using FullStack.Models;
using FullStack.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.Controllers
{
    //Routing
    [Route("api/[controller]")]
    //This attribute tells .NET framework this is an API controller
    [ApiController]

    public class EmployeeController : Controller
    {
        private readonly FullStackDbContext fullStackDbContext;
        private readonly IEmp employee;
        private readonly IReview addReviews;
        private readonly object empID;

        public EmployeeController(FullStackDbContext fullStackDbContext, IEmp employee, IReview addReviews)
        {
            this.fullStackDbContext = fullStackDbContext;
            this.employee = employee;
            this.addReviews = addReviews;
        }

        [HttpGet("GetEmployeebyID/{empID}")]
        public IActionResult GetDetailsbyID(int empID)
        {
            var emp = employee.GetEmployeebyID(empID);
            if(emp != null)
            { return Ok(emp); } 
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetDetails()
        {
            var emp = employee.GetEmployees();
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult AddReviews(Models.Review reviewAdd)
        {
            addReviews.AddReviews(reviewAdd);
            return Ok(reviewAdd);
        }
        [HttpGet("GetReviewbyID/{empID}")]
        public IActionResult GetReviewsbyID(int empID)
        {
            var empReview = addReviews.GetReviewbyID(empID);
            if(empReview != null)
            { return Ok(empReview); }
            return NotFound("Employee does not exist");
        }
        [HttpDelete("{empID}")]
        public IActionResult DeleteEmployee(int empID)
        {
            var emp = employee.DeleteEmployee(empID);
            if(emp!= null)
            { return Ok(emp); }
            return NotFound("Employee does not exist");
        }
        [HttpDelete("DeleteReview/{reviewID}")]
        public IActionResult DeleteReview(int reviewID)
        {
            var review = addReviews.DeleteReview(reviewID);
            if (review != null)
            { return Ok(review); }
            return NotFound("Employee does not exist");
        }

    }
}
