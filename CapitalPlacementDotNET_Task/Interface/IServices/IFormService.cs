using CapitalPlacementDotNET_Task.Model;

namespace CapitalPlacementDotNET_Task.Interface.IServices
{
    public interface IFormService
    {
        Task<BaseResponseModel<string>> CreateApplicationForm(ApplicationFormResquestModel model);
        Task<BaseResponseModel<IEnumerable<AllFormViewModel>>> AllForms();
        Task<BaseResponseModel<FormViewModel>> GetForm(Guid formId);
        Task<BaseResponse> UpDateForm(UpdateApplicationFormModel model);
        Task<BaseResponseModel<ApplicationResponseModel>> GetApplicationResponse(string submisionId);
        Task<BaseResponseModel<string>> SubmitApplication(SunmitApplicationFromModel model);
    }
}
