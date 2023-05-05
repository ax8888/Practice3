using Microsoft.AspNetCore.Mvc;
using PracticeThree.Models;
using PracticeThree.Managers;
namespace PracticeThree.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    PatientManager _patientManager;
    public PatientController(){
        _patientManager = new PatientManager();
    }

    [HttpGet]
    public List<Patient> Get(){
        return _patientManager.GetAll();
    }

    [HttpGet]
    [Route("{CI}")]
    public Patient GetbyCI([FromRoute] long ci){
        return _patientManager.GetbyCI(ci);
    }

    [HttpPost]
    public Patient Post([FromBody]Patient patientToCreate){
        return _patientManager.Create(patientToCreate.Name, patientToCreate.LastName, patientToCreate.CI);
    }

    [HttpPut]
    [Route("{CI}")]
    public Patient Put([FromRoute] long ci, [FromBody]Patient PatientToUpdate){
        return _patientManager.Update(ci, PatientToUpdate.Name, PatientToUpdate.LastName);
    }


    [HttpDelete]
    public Patient Delete(long ci){
        return _patientManager.Delete(ci);
    }

    
}
