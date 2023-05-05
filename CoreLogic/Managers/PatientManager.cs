using CoreLogic.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
namespace PracticeThree.Managers;

public class PatientManager
{
    private List<Patient> _patients;
    private readonly string _filePath;
    public PatientManager(IConfiguration config){
        _patients = new List<Patient>();
        _filePath = config.GetValue<string>("PatientFilePath");
    } 
    
    public List<Patient> GetAll(){
        return _patients;   
    }

    public Patient GetbyCI(long ci){
        return _patients.Find(patient => patient.CI == ci);
    }

    public Patient Update(long ci, string name, string lastname){
        
        try{
            Patient? patientFound = _patients.Find(patient => patient.CI == ci);
            if(patientFound == null){
                //patient not found
                throw new ArgumentException($"Patient with CI {ci} not found");
                
            }else{
                patientFound.Name = name;
                patientFound.LastName = lastname;
            }

        return patientFound;
        }
        catch (Exception ex)
        {
            Log.Error("Patient not found");
            throw new Exception("An error occurred while updating the patient");
        }
        
        
        
    }

    public string GenerateRandomBloodType()
    {
        string[] bloodTypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
        Random rand = new Random();
        int index = rand.Next(bloodTypes.Length);
        return bloodTypes[index];
    }

    public Patient Create(string name, string lastname, long ci){
        Patient createdPatient = new Patient(){Name = name, LastName = lastname, CI=ci, bloodInformation = GenerateRandomBloodType()};
        _patients.Add(createdPatient);
        
        StreamWriter writer = new StreamWriter(_filePath);
        writer.WriteLine($"{createdPatient.Name},{createdPatient.LastName},{createdPatient.CI},{createdPatient.bloodInformation}");
        
        
        return createdPatient;
    }

    public Patient Delete(long ci){
        int patientToDeleteIndex = _patients.FindIndex(patient => patient.CI == ci);
        Patient patientToDelete = _patients[patientToDeleteIndex];
        _patients.RemoveAt(patientToDeleteIndex);
        return patientToDelete;
    }
}