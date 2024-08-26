using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
		private readonly IVacancyService vacancyService;

		public VacancyController(IVacancyService vacancyService)
        {
			this.vacancyService = vacancyService;
		}

        [HttpGet]
        public async Task<IActionResult> GetAllVacancy()
        {
            var result = await vacancyService.GetAllVacancy();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacancy(CreateVacancyDTO createVacancy)
        {
            var result = await vacancyService.CreateVacancy(createVacancy);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVacancy(UpdateVacancyDTO updateVacancy)
        {
            var result = await vacancyService.UpdateVacancy(updateVacancy);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var result = await vacancyService.DeleteVacancy(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
