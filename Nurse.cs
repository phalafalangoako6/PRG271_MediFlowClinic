using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public class Nurse: MedicalStaff 
    {
        public override void PerformDuty()
        {
            Console.WriteLine($"{FirstName} is caring for patients");
        }
    }
}
