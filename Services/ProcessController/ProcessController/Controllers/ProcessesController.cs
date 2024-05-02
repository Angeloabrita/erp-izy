using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessController.Data;
using ProcessController.Model;
using ProcessController.Services;
using ProcessController.Services.IRepository;
using ProcessController.Services.Repository;

namespace ProcessController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Process> _processRepository;

        public ProcessController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _processRepository = new ProcessRepository(_unitOfWork);
        }

        // GET: api/Process
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcessControls()
        {
            return await _processRepository.Get();
        }

        // GET: api/Process/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcessControl(int id)
        {
            var processControl = await _processRepository.GetById(id);

            if (processControl == null)
            {
                return NotFound();
            }

            return processControl;
        }

        // PUT: api/Process/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProcessControl(int id, Process processControl)
        {
            if (id != processControl.Id)
            {
                return BadRequest();
            }

            await _processRepository.Update(id, processControl);

            return NoContent();
        }

        // POST: api/Process
        [HttpPost]
        public async Task<ActionResult<Process>> CreateProcessControl(Process processControl)
        {
            await _processRepository.Create(processControl);

            return CreatedAtAction("GetProcessControl", new { id = processControl.Id }, processControl);
        }

        // DELETE: api/Process/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessControl(int id)
        {
            await _processRepository.Delete(id);

            return NoContent();
        }
    }
}
