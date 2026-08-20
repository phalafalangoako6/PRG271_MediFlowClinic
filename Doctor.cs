using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    //Inheritance 
    public class Doctor: MedicalStaff 
    {
        public override void PerformDuty()
        {
            //Doctor inherits from the MedicalStaff
            Console.WriteLine($"Dr {LastName} is diagnosing patients");
        }
    }
}
