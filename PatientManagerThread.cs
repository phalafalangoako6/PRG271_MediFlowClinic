using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public class Patient 
    {
        public string Name { get; set; }
    }

    public class PatientManager 
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient p) 
        {
            patients.Add(p);
            Console.WriteLine($"Patient {p.Name} added.");
        }
    
        public void StartMonitoring() 
        {
            Task.Run(() => 
            {
                while (true) 
                {
                    foreach (var patient in patients) 
                    {
                        Console.WriteLine($"Monitoring patient {patient.Name}...");
                    }
                    Thread.Sleep(5000);
                }
            });
        }
    }

}