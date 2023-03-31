using Microsoft.AspNetCore.Mvc;
using Employee_Details.Data;
using Employee_Details.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Details.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDBtype dbtype;

        public EmployeeController(EmployeeDBtype dbtype)
        {
            this.dbtype = dbtype;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegisters()
        {
            return Ok(await dbtype.Register.ToListAsync());
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegister([FromRoute] Guid id)
        {
            var tempregister = await dbtype.Register.FindAsync(id);

            if (tempregister == null)
            {
                return NotFound();
            }
            return Ok(tempregister);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewRegister(AddRegister addregister)
        {
            var tempregister = new RegisterTemplate()
            {
                Id = Guid.NewGuid(),
                Name = addregister.Name,
                Address = addregister.Address,
                Email = addregister.Email,
                Phone = addregister.Phone
            };
            await dbtype.Register.AddAsync(tempregister);
            await dbtype.SaveChangesAsync();

            return Ok("Register Added");
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateExistingRegister([FromRoute] Guid id, UpdateRegister updateregister)
        {
            var tempregister = await dbtype.Register.FindAsync(id);
            if (tempregister != null)
            {
                tempregister.Name = updateregister.Name;
                tempregister.Phone = updateregister.Phone;
                tempregister.Email = updateregister.Email;
                tempregister.Address = updateregister.Address;

                await dbtype.SaveChangesAsync();

                return Ok("Register Updated");
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegister([FromRoute] Guid id)
        {
            var tempregister = await dbtype.Register.FindAsync(id);

            if (tempregister != null)
            {
                dbtype.Remove(tempregister);
                await dbtype.SaveChangesAsync();
                return Ok("Register Deleted");
            }

            return NotFound();
        }
    }
}
