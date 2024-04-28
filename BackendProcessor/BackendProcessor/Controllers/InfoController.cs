using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class InfoController : ControllerBase
    {
        private readonly IInfoRepository _infoRepository;

        public InfoController(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        [HttpGet("specializations/{Id}")]
        public async Task<IActionResult> GetSpecializationAsync(int Id)
        {
            Specialization specialization = await _infoRepository.GetSpecializationAsync(Id);

            if (specialization == null)
            {
                return NoContent();
            }

            return Ok(specialization);
        }

        [HttpGet("regions/{Id}")]
        public async Task<IActionResult> GetRegionAsync(int Id)
        {
            Region region = await _infoRepository.GetRegionAsync(Id);

            if (region == null)
            {
                return NoContent();
            }

            return Ok(region);
        }

        [HttpGet("insurances/{Id}")]
        public async Task<IActionResult> GetInsuranceAsync(int Id)
        {
            Insurance insurance = await _infoRepository.GetInsuranceAsync(Id);

            if (insurance == null)
            {
                return NoContent();
            }

            return Ok(insurance);
        }
    }
}
