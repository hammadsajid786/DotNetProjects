using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Service.Entities;

using Microsoft.AspNetCore.Http;
using Common.Library.Repositories;
using Serilog;
using Sample.Service.Dtos;
using Mapster;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMongoRepository<Staff> _staffRepository;
        private readonly ILogger _logger;


        public StaffController(IMongoRepository<Staff> staffRepository,
            ILogger logger)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //private ActionResult InternalExptionError (Exception e)
        //{
        //    _logger.LogError($"{e.Message} - {e.InnerException}");
        //    return Problem(e.Message);
        //}


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Staff>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAsync()
        {

            var staff = (await _staffRepository.GetAllAsync());
            
            return Ok(staff);
    
        }


        [HttpGet("{id:length(24)}", Name = "GetByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Staff>> GetByIdAsync(string id)
        {
   
                var employee = await _staffRepository.GetAsync(id);

                if (employee == null)
                {
                    _logger.Warning($"Employee with id {id} was not found");
                    return NotFound();
                }

                return Ok(employee);
            
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult<StaffCreateModel>> PostAsync([FromBody] StaffCreateModel employee)
        {

            // var employeeNewID = employee with { Id = null };
            // Mapping By Mapster
            var newEmployee = employee.Adapt<Staff>();

            await _staffRepository.CreateAsync(newEmployee);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newEmployee.Id }, newEmployee);

        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([FromBody] Staff employee)
        {

                var existingEmployee = await _staffRepository.GetAsync(employee.Id);

                if (existingEmployee == null)
                {
                    _logger.Warning($"Employee with id {employee.Id} was not found");
                    return NotFound();
                }

                return Ok(await _staffRepository.UpdateAsync(employee));
            

        }


        [HttpDelete("{id:length(24)}", Name = "DeleteAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
                var employee = await _staffRepository.GetAsync(id);

                if (employee == null)
                {
                    _logger.Warning($"Employee with id {id} was not found");
                    return NotFound();
                }

                return Ok(await _staffRepository.RemoveAsync(employee.Id));
            
        }
    }
}
