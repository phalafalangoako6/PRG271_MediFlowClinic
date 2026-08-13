using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public class AppointmentManager
    {
        // kept private — same encapsulation pattern as PatientManager
        private List<Appointment> appointments = new List<Appointment>();
        private int nextAppointmentID = 1;

        public Appointment AddAppointment(Patient patient, MedicalStaff staff, DateTime scheduledTime, PriorityLevel priority)
        {
            // Domain rule: a staff member cannot be double-booked within a 30-minute window
            bool conflict = appointments.Any(a =>
                a.Staff == staff &&
                a.Status == "Scheduled" &&
                Math.Abs((a.ScheduledTime - scheduledTime).TotalMinutes) < 30);

            if (conflict)
            {
                throw new AppointmentConflictException(
                    $"{staff.FirstName} {staff.LastName} already has an appointment within 30 minutes of {scheduledTime:t}.");
            }

            Appointment appointment = new Appointment(nextAppointmentID++, patient, staff, scheduledTime, priority);
            appointments.Add(appointment);
            return appointment;
        }

        public Appointment FindAppointment(int appointmentID)
        {
            Appointment appointment = appointments.FirstOrDefault(a => a.AppointmentId == appointmentID);

            if (appointment == null)
                throw new AppointmentNotFound($"No appointment found with ID {appointmentID}.");

            return appointment;
        }

        public void CompleteAppointment(int appointmentID)
        {
            Appointment appointment = FindAppointment(appointmentID);
            appointment.Complete();
        }

        public void CancelAppointment(int appointmentID)
        {
            Appointment appointment = FindAppointment(appointmentID);
            appointment.Cancel();
        }

        // Domain rule: Emergency-priority patients are always shown/served before Urgent or Routine,
        // regardless of when they were booked
        public void ViewAppointments()
        {
            var ordered = appointments.OrderBy(a => a.Priority).ThenBy(a => a.ScheduledTime);

            foreach (Appointment a in ordered)
            {
                Console.WriteLine($"Appointment ID: {a.AppointmentId}");
                Console.WriteLine($"Patient: {a.Patient.Firstname} {a.Patient.Lastname}");
                Console.WriteLine($"Staff: {a.Staff.FirstName} {a.Staff.LastName}");
                Console.WriteLine($"Time: {a.ScheduledTime}");
                Console.WriteLine($"Priority: {a.Priority}");
                Console.WriteLine($"Status: {a.Status}");
                Console.WriteLine();
            }
        }
    }
}