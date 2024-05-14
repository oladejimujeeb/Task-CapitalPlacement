using CapitalPlacementDotNET_Task.Interface.IServices;
using CapitalPlacementDotNET_Task.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementDotNET_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationFormController : ControllerBase
    {
        private readonly IFormService _formService;

        public ApplicationFormController(IFormService formService)
        {
            _formService = formService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel<string>), 200)]
        public async Task<IActionResult> CreateForm([FromBody]ApplicationFormResquestModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid Model");
            }
            var createForm = await _formService.CreateApplicationForm(model);
            if (!createForm.Status)
            {
                return BadRequest(createForm.Message);
            }
            return Ok(createForm);
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel<string>), 200)]
        public async Task<IActionResult> SubmitApplication([FromBody] SunmitApplicationFromModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid Model");
            }
            var createForm = await _formService.SubmitApplication(model);
            if (!createForm.Status)
            {
                return BadRequest(createForm.Message);
            }
            return Ok(createForm);
        }
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<IActionResult> UpdateForm([FromBody] UpdateApplicationFormModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid Model");
            }
            var updateForm = await _formService.UpDateForm(model);
            if (!updateForm.Status)
            {
                return BadRequest(updateForm.Message);
            }
            return Ok(updateForm);
        }
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel<IEnumerable<AllFormViewModel>>), 200)]
        public async Task<IActionResult> AllForms()
        {
            
            var forms = await _formService.AllForms();
            if (!forms.Status)
            {
                return BadRequest(forms.Message);
            }
            return Ok(forms);
        }
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel<FormViewModel>), 200)]
        public async Task<IActionResult>ApplicationForm([FromQuery][Required] Guid Id)
        {

            var form = await _formService.GetForm(Id);
            if (!form.Status)
            {
                return BadRequest(form.Message);
            }
            return Ok(form);
        }
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel<FormViewModel>), 200)]
        public async Task<IActionResult>ApplicationResponse([FromQuery][Required] string Id)
        {
            var form = await _formService.GetApplicationResponse(Id);
            if (!form.Status)
            {
                return BadRequest(form.Message);
            }
            return Ok(form);
        }
    }
}
