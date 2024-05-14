using CapitalPlacementDotNET_Task.Entities;
using CapitalPlacementDotNET_Task.Helper;
using CapitalPlacementDotNET_Task.Interface.IRepository;
using CapitalPlacementDotNET_Task.Interface.IServices;
using CapitalPlacementDotNET_Task.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace CapitalPlacementDotNET_Task.Implementation.Services
{
    public class FormService:IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly ICustomQuestionRepository _customQuestionRepository;
        private readonly ILogger<FormService> _logger;
        private readonly IApplicationFormResponseRepository _applicationFormResponseRepository;
        public FormService(IFormRepository formRepository, ILogger<FormService> logger, ICustomQuestionRepository customQuestionRepository, 
            IApplicationFormResponseRepository applicationFormResponseRepository)
        {
            _formRepository = formRepository;
            _logger = logger;
            _customQuestionRepository = customQuestionRepository;
            _applicationFormResponseRepository = applicationFormResponseRepository;
        }
        private List<PersonalInformation> MandatoryPersonalQuestion(Guid formId)
        {
            var mandatoryField = new List<PersonalInformation>();
            foreach (string field in FormHelper.Fields)
            {
                var requiredField = new PersonalInformation()
                {
                    Field = field,
                    ApplicationFormId = formId
                };
                mandatoryField.Add(requiredField);
            }
            return mandatoryField;
        }
        public async Task<BaseResponseModel<string>>CreateApplicationForm(ApplicationFormResquestModel model)
        {
            var response = new BaseResponseModel<string>();
            if (model == null || string.IsNullOrEmpty(model.ProgramDescription) || string.IsNullOrEmpty(model.ProgramTitle)) 
            {
                response.Status = false;
                response.Message = "Invalid Model";
                response.Data = "Failed to create Application";
                return response;
            }
            var applicationForm = new ApplicationForm()
            {
                FormTitle = model.ProgramTitle,
                FormDescription = model.ProgramDescription,
            };
            var personalInformation = MandatoryPersonalQuestion(applicationForm.Id);
            applicationForm.PersonalInformation = personalInformation;

            if(!model.HideGender)
            {
                var personalInfo = new PersonalInformation
                {
                    Field = FormHelper.Gender,
                    ApplicationFormId = applicationForm.Id
                };
                applicationForm.PersonalInformation.Add(personalInfo);
            }
            if(!model.HideIDNumber) 
            {
                var personalInfo = new PersonalInformation
                {
                    Field = FormHelper.IDNumber,
                    ApplicationFormId = applicationForm.Id
                };
                applicationForm.PersonalInformation.Add(personalInfo);
            }
            if (!model.HideNationality)
            {
                var personalInfo = new PersonalInformation
                {
                    Field = FormHelper.Nationality,
                    ApplicationFormId = applicationForm.Id
                };
                applicationForm.PersonalInformation.Add(personalInfo);
            }
            if (!model.HideCurrentAddress)
            {
                var personalInfo = new PersonalInformation
                {
                    Field = FormHelper.CurrentAddress,
                    ApplicationFormId = applicationForm.Id
                };
                applicationForm.PersonalInformation.Add(personalInfo);
            }
            if (!model.HideDateOfBirth)
            {
                var personalInfo = new PersonalInformation
                {
                    Field = FormHelper.DateOfBirth,
                    ApplicationFormId = applicationForm.Id
                };
                applicationForm.PersonalInformation.Add(personalInfo);
            }
            if (!model.HidePhoneNumber)
            {
                var personalInfo = new PersonalInformation
                {
                    Field = FormHelper.PhoneNumber,
                    ApplicationFormId = applicationForm.Id
                };
                applicationForm.PersonalInformation.Add(personalInfo);
            }
            
            foreach(var question in model.CustomQuestions)
            {
                var customQuestion = new CustomQuestion 
                {
                    ApplicationFormId = applicationForm.Id,
                    Question = question.Question,
                    QuestionType = question.QuestionType,
                };
                applicationForm.CustomQuestions.Add(customQuestion);
            }

            try
            {
                var saveForm = await _formRepository.Add(applicationForm);
                response.Status = true;
                response.Message = "Form Created Successfully";
                _logger.LogInformation($"Form Created Successfully, Form Id:{saveForm.Id}");
                response.Data = $"Form Id: {saveForm.Id}";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<string> { Status = false, Message = $"Failed to save Form; Error:{ex.Message}" };
            }
          
        }

        public async Task<BaseResponseModel<IEnumerable<AllFormViewModel>>>AllForms()
        {
            var forms = await  _formRepository.Query().Select(f=> new AllFormViewModel 
            {
                FormId = f.Id,
                FormTitle = f.FormTitle,
                FormDescription = f.FormDescription,    
            }).ToListAsync();
            var response = new BaseResponseModel<IEnumerable<AllFormViewModel>>();
            if(!forms.Any())
            {
                response.Message = "No Form, create a form";
                response.Data = Enumerable.Empty<AllFormViewModel>();
                return response;
            }
            response.Status = true;
            response.Data = forms;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponseModel<FormViewModel>>GetForm(Guid formId)
        {
            var response = new BaseResponseModel<FormViewModel>();
            if(Guid.Empty == formId)
            {
                response.Message = "Id cannot be null";
                response.Status = false;
                return response;
            }
            var form = await _formRepository.Query().Where(f=> f.Id==formId).Include(f=>f.CustomQuestions)
                                                      .Include(f=>f.PersonalInformation)
                                                            .AsNoTracking().FirstOrDefaultAsync();
            if(form == null)
            {
                response.Message = "Form Not Found";
                response.Status = false;
                return response;
            }



            var customQuestions = form.CustomQuestions.Select(x => new CustomQuestionViewModel
            {
                 Question = x.Question,
                 QuestionType = x.QuestionType.ToString(),
                 QuestionId = x.Id,
            }).ToList();
            
            var personalInfo = form.PersonalInformation.Select(p=> new PersonalInformationModel
            {
                PersonalInformationId= p.Id,
                FieldName = p.Field,
                FieldType = p.FieldType,
            }).ToList();
            var applicationForm = new ApplicationFormModel
            {
                PersonalInformation = personalInfo,
                CustomQuestions = customQuestions,
            };
            var formContent = JsonConvert.SerializeObject(applicationForm);
            var formresponse = new FormViewModel
            {
                FormId = form.Id,
                FormTitle = form.FormTitle,
                FormDescription = form.FormDescription,
                FormContent = formContent
            };
            response.Message = "Success";
            response.Status = true;
            response.Data = formresponse;
            return response;
        }

        public async Task<BaseResponse>UpDateForm(UpdateApplicationFormModel model)
        {
            var response = new BaseResponse();
            if(model is null) 
            {
                response.Message = "invalid model";
                return response;
            }
            if(Guid.Empty == model.ApplicationId) 
            {
                response.Message = "Application form Id can not be null";
                return response;
            }
           
            if(!model.CustomQuestions.Any()) 
            {
                var form = await _formRepository.Get(model.ApplicationId);
                if (form is null)
                {
                    response.Message = "Application form not found";
                    return response;
                }
                form.FormDescription= string.IsNullOrEmpty(model.FormDescription)? form.FormDescription: model.FormDescription;
                form.FormTitle =string.IsNullOrEmpty( model.FormTitle)?form.FormTitle: model.FormTitle;

                try
                {
                    await _formRepository.Update(form);
                    response.Status = true;
                    response.Message = "Form Updated Successfully";
                    return response;
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = $"Failed to save order; Error:{ex.Message}" ;
                    return response;
                }
                
            }
            var appForm = await _formRepository.Get(model.ApplicationId);
            var customQuestions = await _customQuestionRepository.GetAllWhere(x => x.ApplicationFormId == model.ApplicationId);
            if (appForm is null || !customQuestions.Any())
            {
                response.Message = "Application form/ Question not found";
                return response;
            }
            appForm.FormDescription = string.IsNullOrEmpty(model.FormDescription) ? appForm.FormDescription : model.FormDescription;
            appForm.FormTitle = string.IsNullOrEmpty(model.FormTitle) ? appForm.FormTitle : model.FormTitle;
            foreach(var question in model.CustomQuestions) 
            {
                var updateQuestion = customQuestions.FirstOrDefault(x=>x.Id ==question.QuestionId);
                if (updateQuestion != null) 
                { 
                    updateQuestion.Question = string.IsNullOrEmpty(question.Question)? updateQuestion.Question: question.Question;
                    updateQuestion.QuestionType = question.QuestionType == null ? updateQuestion.QuestionType:question.QuestionType.Value;

                    try
                    {
                        await _customQuestionRepository.Update(updateQuestion);
                    }
                    catch (Exception ex)
                    {
                        response.Status = false;
                        response.Message = $"Failed to update form; Error:{ex.Message}";
                        return response;
                    }
                }
            }
          
            try
            {
                await _formRepository.Update(appForm);
                response.Status = true;
                response.Message = "Form Updated Successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Failed to update form; Error:{ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponseModel<string>>SubmitApplication(SunmitApplicationFromModel model)
        {
            var baseResponse = new BaseResponseModel<string>();
            if(Guid.Empty == model.ApplicationFormId)
            {
                baseResponse.Message = "Application FormId cannot be null";
                baseResponse.Data = "Failed to submit response";
                return baseResponse;
            }
            /*if(model.QuestionResponds.Any(r=> Guid.Empty==r.CustomQuestionId || Guid.Empty == r.PersonalInformationId))
            {
                baseResponse.Message = "Question Id cannot be null";
                baseResponse.Data = "Failed to submit response";
                return baseResponse;
            }*/

            var submitResponse = new ApplicationFormResponse();
            submitResponse.SubmissionID = $"REG/{Guid.NewGuid().ToString().Substring(0, 6)}";
            submitResponse.ApplicationFormId = model.ApplicationFormId;
            var responses = new List<Response>();
            foreach (var respon in model.QuestionResponds)
            {
                var appResponse = new Response()
                {
                    PersonalInformationId = respon.PersonalInformationId,
                    CustomQuestionId = respon.CustomQuestionId,
                    QuestionResponse = respon.QuestionResponse,
                    ApplicationFormResponseId = submitResponse.Id
                };
                responses.Add(appResponse);
            }
            submitResponse.QuestionResponse = responses;
            try
            {
                await _applicationFormResponseRepository.Add(submitResponse);
                baseResponse.Status = true;
                baseResponse.Message = "Response submitted Successfully";
                _logger.LogInformation("Response submitted Successfully");
                baseResponse.Data = $"Response submitted: SubmissionId:{submitResponse.SubmissionID}";
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<string> { Status = false, Message = $"Failed to submit Form; Error:{ex.Message}" };
            }
        }

        public async Task<BaseResponseModel<ApplicationResponseModel>> GetApplicationResponse(string submisionId)
        {
            var response = new BaseResponseModel<ApplicationResponseModel>();

            if(string.IsNullOrWhiteSpace(submisionId))
            {
                response.Message = "SumissionID cannot be null";
                return response;
            }
            var getResponse = await _applicationFormResponseRepository.Query().Include(x => x.ApplicationForm).Include(x=>x.QuestionResponse)
                                                .Include(x => x.ApplicationForm.CustomQuestions).
                                                     Include(x => x.ApplicationForm.PersonalInformation).
                                                        Where(x=>x.SubmissionID==submisionId).FirstOrDefaultAsync();
            if (getResponse is null)
            {
                response.Message = "No submission found";
                return response;
            }
            var application = getResponse.ApplicationForm;
            var customQuestions = getResponse.ApplicationForm.CustomQuestions;
            var personalnformation = getResponse.ApplicationForm.PersonalInformation;
            var responses = getResponse.QuestionResponse;

            var applicationResponse = new ApplicationResponseModel()
            {
                FormTitle = application.FormTitle,
                FormDescription = application.FormDescription,
            };

            var formContent = new Dictionary<string, string>();
            foreach(var customQuestion in customQuestions )
            {
                var questionResponse = responses.FirstOrDefault(x => x.CustomQuestionId == customQuestion.Id);
                if(questionResponse is not null) 
                {
                    formContent.Add(customQuestion.Question, questionResponse.QuestionResponse);
                }
            }
            foreach (var personalQuestion in personalnformation)
            {
                var personalQuestionResponse = responses.FirstOrDefault(x => x.PersonalInformationId == personalQuestion.Id);
                if (personalQuestionResponse is not null)
                {
                    formContent.Add(personalQuestion.Field, personalQuestionResponse.QuestionResponse);
                }
            }
            applicationResponse.FormContent = formContent;
            response.Status = true;
            response.Message = "Success";
            response.Data = applicationResponse;
            return response;

        }

    }
}
