using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public abstract class MedicalStaff
    {
      public string FirstName { get; set; }
        public string LastName { get; set; }
        public abstract void PerformDuty();


    }
}
