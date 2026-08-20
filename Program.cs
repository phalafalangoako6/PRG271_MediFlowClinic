using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create managers tha handle patient and appointments
            PatientManager patientManager = new PatientManager();
            AppointmentManager appointmentManager = new AppointmentManager();

            //Subscribe to appointments events
            appointmentManager.AppointmentScheduled += AppointmentScheduledHandler;
            appointmentManager.AppointmentConflict += AppointmentConflictHandler; 



            bool running = true;

            //Display the menu  until the user exits
            while (running)
            {
                Console.Clear();

                Console.WriteLine("===============================================");
                Console.WriteLine("MEDIFLOW CLINIC SYSTEM");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Book Appointment");
                Console.WriteLine("3. View Patient");
                Console.WriteLine("4. Search Patient");
                Console.WriteLine("5. Update Patient");
                Console.WriteLine("6. Remove Patient");
                Console.WriteLine("7. View Medical Staff");
                Console.WriteLine("8. Exit");
                Console.WriteLine("======================================================");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    //add patient
                    case "1":
                    {
                        Console.WriteLine("Enter patient ID: ");
                        int patientID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter First Name: ");
                        string firstname = Console.ReadLine();

                        Console.Write("Enter Last Name: ");
                        string lastname = Console.ReadLine();

                        
                            //ask for the patient's age which has to be > 0
                            int age; 
                            while (true)
                            {
                                 Console.Write("Enter Age: ");
                                 
                                if(int.TryParse(Console.ReadLine(), out age) && age >= 1)
                                {
                                    break;
                                }
                                Console.WriteLine("Invalid age. Please enter a valid age ");

                            }

                            
                            //ask the user to enter patient's priority
                        Console.Write("Enter Priority: (Emergency/Urgent/Routine): ");
                        string input = Console.ReadLine();

                        PriorityLevel priorityValue;
                        if (!Enum.TryParse<PriorityLevel>(input, true, out priorityValue))
                        {
                            Console.WriteLine("Invalid priority entered. Defaulting to Routine");
                            priorityValue = PriorityLevel.Routine;
                        }

                        //display available status options
                        Console.WriteLine("Select Patient Status:");
                        Console.WriteLine("1. Waiting");
                        Console.WriteLine("2. In Treatment");
                        Console.WriteLine("3. Discharged");

                        PatientStatus selectedStatus;
                            
                            while (true)
                            {

                                Console.Write("Enter option: ");

                                if(int.TryParse(Console.ReadLine(), out int statusChoice) && Enum.IsDefined(typeof(PatientStatus), statusChoice))
                                {
                                    selectedStatus = (PatientStatus)statusChoice;
                                    break;
                                }
                                Console.WriteLine("Invalid option. Please select 1,2 or 3");
                            }
                            //convert selected enum to a string
                            string status = selectedStatus.ToString();

                          
                            //create patient
                     

                        Patient patient = new Patient();

                        patient.PatientID = patientID;
                        patient.Firstname = firstname;
                        patient.Lastname = lastname;
                        patient.Age = age;
                        patient.Priority = priorityValue;
                            patient.Status = status;

                            //add new patient to the PatientManager
                        patientManager.AddPatient(patient);

                        Console.WriteLine("Patient added successfully.");
                        break;
                    }

                        //Book appointment
                    case "2":
                    {
                        try
                        {
                                //ask for patient who needs the appointment
                            Console.Write("Enter Patient ID: ");
                            int patientIDBooking = int.Parse(Console.ReadLine());

                                //search for patient by patientID 
                                Patient bookingPatient = patientManager.SearchPatient(patientIDBooking);

                               //stop if the patient does not exist in as our patient
                            if (bookingPatient == null)
                            {
                                Console.WriteLine("Patient not found.");
                                break;
                            }

                            Console.Write("Enter time (e.g. 14:30): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime scheduledTime))
                            {
                                Console.WriteLine("Invalid time format.");
                                break;
                            }

                            Console.Write("Enter priority (Emergency/Urgent/Routine): ");
                            if (!Enum.TryParse<PriorityLevel>(Console.ReadLine(), true, out PriorityLevel priority))
                            {
                                Console.WriteLine("Invalid priority; defaulting to Routine.");
                                priority = PriorityLevel.Routine;
                            }

                            // Minimal approach: use a default staff member for booking
                            MedicalStaff someStaffMember = new Doctor { FirstName = "Default", LastName = "Doctor" };


                                //Add the appointment using AppointmentManger
                            Appointment appt = appointmentManager.AddAppointment(bookingPatient, someStaffMember, scheduledTime, priority);
                            Console.WriteLine($"Appointment {appt.AppointmentId} booked successfully.");
                        }
                            //Used to handle appointments conflicts
                        catch (AppointmentConflictException ex)
                        {
                            Console.WriteLine($"Booking failed: {ex.Message}");
                        }
                            //Habdle invalid number/date input
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format — please check date/ID entries.");
                        }
                            //handle any errors
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error: {ex.Message}");
                        }
                        finally
                        {

                            Console.WriteLine("Booking attempt finished.\n");
                        }
                        break;
                    }


                        //View patients
                    case "3":

                        //display all patients in the PatientManager
                        patientManager.ViewPatients();
                        break;


                    case "4":
                        Console.Write("Enter Patient ID to search: ");
                        int searchId = Convert.ToInt32(Console.ReadLine());

                        Patient foundPatient = patientManager.SearchPatient(searchId);

                        if (foundPatient != null)
                        {
                            Console.WriteLine("Patient found!");
                            Console.WriteLine($"Patient ID: {foundPatient.PatientID}");
                            Console.WriteLine($"Name: {foundPatient.Firstname} {foundPatient.Lastname}");
                            Console.WriteLine($"Age: {foundPatient.Age}");
                            Console.WriteLine($"Priority: {foundPatient.Priority}");
                            Console.WriteLine($"Status: {foundPatient.Status}");
                        }
                        else
                        {
                            Console.WriteLine("Patient not found");
                        }
                          break;


                    case "5":
                        Console.WriteLine("Enter the patient's ID to update: ");
                        int updateID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter the new priority: ");
                        string newPriority = Console.ReadLine();

                        Console.Write("Enter the new status: ");
                        string newStatus = Console.ReadLine();


                        //Send the updated information to PatientManager
                        patientManager.UpdatePatient(updateID, newPriority, newStatus);
                        break;

                    case "6":
                        Console.WriteLine("Enter Patient's ID to remove: ");
                        int removeID = Convert.ToInt32(Console.ReadLine());

                        patientManager.RemovePatient(removeID);
                        break;


                        //View medical staff
                    case "7":
                        Doctor doctor = new Doctor();
                        doctor.FirstName = "John";
                        doctor.LastName = "Doe";

                        Nurse nurse = new Nurse();
                        nurse.FirstName = "Jane";
                        nurse.LastName = "Botha";

                        Pharmacist pharmacist = new Pharmacist();
                        pharmacist.FirstName = "Prince";
                        pharmacist.LastName = "Naidoo";

                        MedicalStaff[] staff =
                        {
                           doctor,
                           nurse,
                           pharmacist
                        };

                        Console.WriteLine("\n--- Medical Staff ---");

                        foreach (MedicalStaff member in staff)
                        {
                            Console.WriteLine($"{member.FirstName} {member.LastName}");
                            member.PerformDuty();
                            Console.WriteLine();
                        }


                        break;

                    case "8":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;


                }
                Console.WriteLine("\nPress Enter to continue");
                Console.ReadLine();

            }
            











           

        }

        //Appointment scheduled event handler 
        static void AppointmentScheduledHandler(object sender, AppointmentEventArgs e)
        {
            Console.WriteLine(
                $"EVENT: Appointment {e.Appointment.AppointmentId} was scheduled.");
        }
        static void AppointmentConflictHandler (object sender, AppointmentEventArgs e)
        {
            Console.WriteLine("EVENT: Appointment conflict detected!");
        }


    }
}
