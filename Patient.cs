using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    
    public class Patient
    {
        public int PatientID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        private int age; ///cannot be directly accessed from outside the class
        public int Age
        {
           get { return age; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Age cannot be negative.");
                }
                age = value;

            }
        }
        public string Priority { get; set; }    
        public string Status { get; set; }  

    }
}
