using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public class PatientManager
    { 
        // the list is kept private so that the access to the patient records can be controlled through manager's methods
      private List<Patient> patients = new List<Patient>(); //creates a list that stores patients, PatientManager controls the list through methods
      
        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public void ViewPatients()
        {   //Compare the priority values of patients and swap their positions 
            var ordered = patients.OrderBy(p => p.Priority); //Orders the patients according to priority and stores them in a new list

            foreach (Patient patient in ordered)
            {
                Console.WriteLine($"Patient ID: {patient.PatientID}");
                Console.WriteLine($"Name: {patient.Firstname} {patient.Lastname}");
                Console.WriteLine($"Age: {patient.Age}");
                Console.WriteLine($"Priority: {patient.Priority}");
                Console.WriteLine($"Status: {patient.Status}");
                Console.WriteLine();
            }
        }

        public Patient SearchPatient(int patientID)
        {
            foreach (Patient patient in patients)
            {
                if (patient.PatientID == patientID) //Searches the private collection using patient's ID and return the matching patient if one exists
                {
                    return patient;
                }
            }
            return null; //When there is no patient object to return
        }
       
       
        public void UpdatePatient(int patientID, string priority, string status)
        {
            Patient patient = SearchPatient(patientID);

            if (patient != null)
            {
                // parse priority string into enum; default to Routine on failure
                if (!Enum.TryParse<PriorityLevel>(priority, true, out PriorityLevel parsedPriority))
                {
                    parsedPriority = PriorityLevel.Routine;
                }

                patient.Priority = parsedPriority;
                patient.Status = status;

                Console.WriteLine("Patient updated successfully");
            }

            else
            {
                Console.WriteLine("Patient not found");
            }

        }

        public void RemovePatient(int patientID)
        {
            Patient patient = SearchPatient(patientID);
            if (patient != null)
            {
                patients.Remove(patient);  //Removes the patient object from our private list
                Console.WriteLine("Patient removed successfully");
            }
            else
            {
                Console.WriteLine("Patient not found");
            }

        }


    }
}
