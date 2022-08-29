/****************************************************************
-Creator: Erika Gonzalez 
-Creation date: 2022-08-29
-Project: EERP Project name
-Epic: EP003 
-UH: UH002, UH003
***************************************************************/
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

//Get for consult all Surveys
    [HttpGet("ConsultAllSurveys")]
    public IActionResult ConsultAllSurveys()
    {
        var consultSurvey = this._DBContext.Surveys.ToList();
        return Ok(consultSurvey);
    }

//Post for create new Survey
    [HttpPost("CreateNewSurvey")]
    public IActionResult CreateNewSurvey([FromBody] Survey _survey)
    {
        var newsurvey = this._DBContext.Surveys.FirstOrDefault(o => o.IdSurvey == _survey.IdSurvey);
        if (newsurvey != null)
        {
            
        }
        else
        {
            this._DBContext.Surveys.Add(_survey);
            this._DBContext.SaveChanges();
        }

        return Ok(true);
    }
//Post for edit Survey

    [HttpPost("EditSurvey")]
    public IActionResult EditSurvey([FromBody] Survey _survey)
    {
        var newsurvey = this._DBContext.Surveys.FirstOrDefault(o => o.IdSurvey == _survey.IdSurvey);
        if (newsurvey != null)
        {
            
        }
        else
        {
            this._DBContext.Surveys.Add(_survey);
            this._DBContext.SaveChanges();
        }

        return Ok(true);
    }


}
