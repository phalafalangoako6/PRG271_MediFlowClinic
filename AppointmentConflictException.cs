using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    //When a staff member is already booked for an overlapping time slot
    public class AppointmentConflictException : Exception
    {
        public AppointmentConflictException(string message) : base(message) { }
    }
}
