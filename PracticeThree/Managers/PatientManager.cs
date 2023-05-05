using PracticeThree.Models;

namespace PracticeThree.Managers;

public class PatientManager
{
    private List<Patient> _patients;
    public PatientManager(){
        _patients = new List<Patient>();
    } 
    
    public List<Patient> GetAll(){
        return _patients;   
    }

    public Patient GetbyCI(long ci){
        return _patients.Find(patient => patient.CI == ci);
    }

    public Patient Update(long ci, string name, string lastname){
        Patient? patientFound = _patients.Find(patient => patient.CI == ci);
        
        if(patientFound == null){
            //patient not found
        }else{
            patientFound.Name = name;
            patientFound.LastName = lastname;
        }

        return patientFound;
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
        return createdPatient;
    }

    public Patient Delete(long ci){
        int patientToDeleteIndex = _patients.FindIndex(patient => patient.CI == ci);
        Patient patientToDelete = _patients[patientToDeleteIndex];
        _patients.RemoveAt(patientToDeleteIndex);
        return patientToDelete;
    }
}