using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace MediFlowClinic
{
    public class AppointmentManager
    {
        // kept private — Encapsulation
        private List<Appointment> appointments = new List<Appointment>();
        private int nextAppointmentID = 1;      //tracks the nec=xt appointmentID

        //EVENTS 
        public event EventHandler<AppointmentEventArgs> AppointmentScheduled;
        public event EventHandler<AppointmentEventArgs> AppointmentConflict;


        //Appointment booking create and store new appointments
        public Appointment AddAppointment(Patient patient, MedicalStaff staff, DateTime scheduledTime, PriorityLevel priority)
        {
           
            //LINQ 
            bool conflict = appointments.Any(a =>
                 a.Staff.FirstName == staff.FirstName &&
                 a.Staff.LastName == staff.LastName &&
                 a.Status == "Scheduled" &&
                 Math.Abs((a.ScheduledTime - scheduledTime).TotalMinutes) < 30);

            if (conflict)
            {
                Appointment conflictAppointment = new Appointment(0, patient, staff, scheduledTime, priority);
                 
                AppointmentConflict?.Invoke(this, new AppointmentEventArgs(conflictAppointment));

                throw new AppointmentConflictException(
                    $"{staff.FirstName} {staff.LastName} already has an appointment within 30 minutes.");


            }

            Appointment appointment = new Appointment(nextAppointmentID++, patient, staff, scheduledTime, priority);
            appointments.Add(appointment);
            
            //Trigger Event
            AppointmentScheduled?.Invoke(this, new AppointmentEventArgs(appointment));

            return appointment;
        }

        //Search for appointments using its ID
        public Appointment FindAppointment(int appointmentID)
        {
            Appointment appointment = appointments.FirstOrDefault(a => a.AppointmentId == appointmentID);

            if (appointment == null)
            {
                throw new AppointmentNotFoundException($"No appointment found with ID {appointmentID}.");
            }

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

        //FILE I/O   

        public void SaveAppointments(string filePath)
        {
            var appointmentData = appointments.Select(a => new
            {
                AppointmentId = a.AppointmentId,
                PatientName = a.Patient.Firstname + " " + a.Patient.Lastname,
                StaffName = a.Staff.FirstName + " " + a.Staff.LastName,
                ScheduledTime = a.ScheduledTime,
                Priority = a.Priority,
                Status = a.Status,


            }).ToList();

            string json = JsonConvert.SerializeObject(
               appointmentData, Formatting.Indented );

            File.WriteAllText(filePath, json);

            Console.WriteLine("Appointments saved to file.");
        }
        public void LoadAppointments(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No saved appointments files found");
                return;
            }
            string json = File.ReadAllText(filePath);

            Console.WriteLine("\nSaved appointments loaded from file:");
            Console.WriteLine(json);

        }
        
        //MULTITHREADING
        public void StartAppointmentMonitoring()
        {
            Task.Run(() =>
            {
                Console.WriteLine("\nAppointment monitoring started...");

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(
                        $"Monitoring appointments ... Check {i + 1}");

                    Thread.Sleep(2000);
                }
                Console.WriteLine("Appointment monitoring finished.");

            });

        }






    }
}