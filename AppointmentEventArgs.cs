using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    //EVENTS 
    public class AppointmentEventArgs: EventArgs 
    {
        public Appointment Appointment { get; set; }

        public AppointmentEventArgs(Appointment appointment)
        {
            Appointment = appointment;
        }

    }
}
