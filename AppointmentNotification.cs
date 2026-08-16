using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{

    public class Appointment
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime Time { get; set; }
    }

    public class AppointmentEventArgs : EventArgs 
    {
        public Appointment Appointment { get; }
        public AppointmentEventArgs(Appointment appt) 
        {
        Appointment = appt;
        }
    }

    public class AppointmentManager
    {
        public event EventHandler<AppointmentEventArgs> AppointmentScheduled;
        public event EventHandler<AppointmentEventArgs> AppointmentConflict;

        private List<Appointment> appointments = new List<Appointment>();

        public void ScheduleAppointment(Appointment appt) 
        {
        bool conflict = appointments.Any(a => a.Time == appt.Time && a.DoctorName == appt.DoctorName);

        if (conflict) 
        {
            AppointmentConflict?.Invoke(this, new AppointmentEventArgs(appt));
            Console.WriteLine("Conflict detected!");
        } else 
        {
            appointments.Add(appt);
            AppointmentScheduled?.Invoke(this, new AppointmentEventArgs(appt));
            Console.WriteLine("Appointment scheduled successfully.");
        }
        }
    }

}