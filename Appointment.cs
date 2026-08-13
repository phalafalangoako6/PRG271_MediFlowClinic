using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public class Appointment
    {
        public int AppointmentId {  get; set; }
        public Patient Patient { get; set; }
        public MedicalStaff Staff { get; set; }
        public DateTime ScheduledTime { get; set; }
        public PriorityLevel Priority {  get; set; }
        public String Status { get; private set; }

        public Appointment(int appointmentId, Patient patient, MedicalStaff staff, DateTime scheduledTime, PriorityLevel priority)
        {
            AppointmentId = appointmentId;
            Patient = patient;
            Staff = staff;
            ScheduledTime = scheduledTime;
            Priority = priority;
            Status = "Scheduled";
        }

        public void Complete()
        {
            if (Status == "Cancelled")
                throw new IllegalStatusTransitionException($"Cannot complete appointment {AppointmentId} - it has been cancelled.");

            Status = "Completed";
        }

        public void Cancel()
        {
            if (Status == "Completed")
                throw new IllegalStatusTransitionException($"Cannot cancel appointment {AppointmentId} - it has already been completed.");

            Status = "Cancelled";
        }
    }
}
