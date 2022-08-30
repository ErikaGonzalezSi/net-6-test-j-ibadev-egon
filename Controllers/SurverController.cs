/****************************************************************
-Creator: Erika Gonzalez 
-Creation date: 2022-08-29
-Project: EERP Project name
-Epic: EP003 
-UH: UH002, UH003
***************************************************************/
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using net_6_test_j_ibadev_egon_pr.Models;

namespace net_6_test_j_ibadev_egon_pr.Controllers;

[ApiController]
[Route("[controller]")]
public class SurverController : ControllerBase
{
    private readonly EERPContext _DBContext;

    public SurverController(EERPContext dBContext)
    {
        this._DBContext = dBContext;
    }


    //Post for create new Survey
    [HttpPost("CreateNewSurvey")]
    public IActionResult CreateNewSurvey([FromBody] Survey _survey)
    {

        // Declare error message variable
        ErrorMessage error = new ErrorMessage();

        //Validate if there is survey already created

        var newsurvey = this._DBContext.Surveys.FirstOrDefault(o => o.IdSurvey == _survey.IdSurvey);
        if (newsurvey == null)
        {
            // Validate if Title is not null
            if (_survey.Title == null || _survey.Title.Trim() == "")
            {
                error.Error = "Title";
                error.ErrorDescription = "Title is required";
                error.TechnicalError = "Title is null or empty";
                return BadRequest(error);
            }
            // Validate if Title is not duplicate
            var validetitle = this._DBContext.Surveys.FirstOrDefault(o => o.Title == _survey.Title);
            if (validetitle != null)
            {
                error.Error = "Title";
                error.ErrorDescription = "Title already exists";
                error.TechnicalError = "Cannot insert duplicate. Title is unique";
                return BadRequest(error);
            }

            // Validate if the type of Survey exists
            var valideListType = this._DBContext.SurveyTypes.FirstOrDefault(o => o.IdSurveyType == _survey.IdSurveyType);
            if (valideListType == null)
            {
                error.Error = "Survey type";
                error.ErrorDescription = "Survey type does not exist";
                error.TechnicalError = "IdSurveytype does not exist in SurveyType";
                return BadRequest(error);
            }
            // Validate if Template is Json
            try
            {
                var ValidateJson = JsonSerializer.Deserialize<Object>(_survey.Template);
            }
            catch
            {
                error.Error = "Template";
                error.ErrorDescription = "Template is not Json or is empty";
                error.TechnicalError = "Template is not Json or is null";
                return BadRequest(error);
            }
            // Disable False by default
            if (_survey.Disable == null)
            {
                _survey.Disable = false;
            }
            // Published False by default
            if (_survey.Published == null)
            {
                _survey.Published = false;
            }
           
            // Save survey data in Database
            this._DBContext.Surveys.Add(_survey);
            this._DBContext.SaveChanges();
            return Ok(true);

        }
        else //Validate survey already created
        {
            error.Error = "It already exists";
            error.ErrorDescription = "The survey is already created";
            error.TechnicalError = "The survey is already created in the database";
            return BadRequest(error);
        }

    }
    //Get for consult all Surveys
    [HttpGet("ConsultAllSurveys")]
    public IActionResult ConsultAllSurveys()
    {
        var consultSurvey = this._DBContext.Surveys.ToList();
        return Ok(consultSurvey);
    }

    //Post for edit Survey
    [HttpPost("EditSurvey")]
    public IActionResult EditSurvey([FromBody] Survey _survey)
    {
        // Declare error message variable
        ErrorMessage error = new ErrorMessage();

        // Validate if Title is not null
        if (_survey.Title == null || _survey.Title.Trim() == "")
        {
            error.Error = "Title";
            error.ErrorDescription = "Title is required";
            error.TechnicalError = "Title is null or empty";
            return BadRequest(error);
        }
        // Validate if Title is not duplicate
        var validetitle = this._DBContext.Surveys.FirstOrDefault(o => o.Title == _survey.Title && o.IdSurvey !=_survey.IdSurvey);
        if (validetitle != null)
        {
            error.Error = "Title";
            error.ErrorDescription = "Title already exists";
            error.TechnicalError = "Cannot insert duplicate. Title is unique";
            return BadRequest(error);
        }
        // Validate if Template is Json
        try
        {
            var ValidateJson = JsonSerializer.Deserialize<Object>(_survey.Template);
        }
        catch
        {
            error.Error = "Template";
            error.ErrorDescription = "Template is not Json or is empty";
            error.TechnicalError = "Template is not Json or is null";
            return BadRequest(error);
        }
        // Disable False by default
        if (_survey.Disable == null)
        {
            _survey.Disable = false;
        }
        //Validate if there is survey already created
        var editsurvey = this._DBContext.Surveys.FirstOrDefault(o => o.IdSurvey == _survey.IdSurvey);
        if (editsurvey != null)
        {
            editsurvey.Title = _survey.Title;
            editsurvey.Template = _survey.Template;
            editsurvey.Disable = _survey.Disable;
            _DBContext.Update(editsurvey);
            this._DBContext.SaveChanges();
        }
        else
        {
            error.Error = "Survey not exists";
            error.ErrorDescription = "The survey is not created";
            error.TechnicalError = "The survey is not created in the database";
            return BadRequest(error);

        }

        return Ok(true);
    }

}
