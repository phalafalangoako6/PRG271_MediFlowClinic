using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{ 
    //INTERFACE connects to patientmanager
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        void ViewPatients();
        Patient SearchPatient(int patientID);
        void UpdatePatient(int patientID, string priority, string status);
        void RemovePatient(int patientID);
    }
}
