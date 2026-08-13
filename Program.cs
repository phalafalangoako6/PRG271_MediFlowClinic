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
            PatientManager patientManager = new PatientManager();
            AppointmentManager appointmentManager = new AppointmentManager();

            bool running = true;

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
                    case "1":
                    {
                        Console.WriteLine("Enter patient ID: ");
                        int patientID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter First Name: ");
                        string firstname = Console.ReadLine();

                        Console.Write("Enter Last Name: ");
                        string lastname = Console.ReadLine();

                        Console.Write("Enter Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Priority: (Emergency/Urgent/Routine): ");
                        string input = Console.ReadLine();

                        PriorityLevel priorityValue;
                        if (!Enum.TryParse<PriorityLevel>(input, true, out priorityValue))
                        {
                            Console.WriteLine("Invalid priority entered. Defaulting to Routine");
                            priorityValue = PriorityLevel.Routine;
                        }

                        Console.Write("Enter Status: ");
                        string status = Console.ReadLine();

                        Patient patient = new Patient();

                        patient.PatientID = patientID;
                        patient.Firstname = firstname;
                        patient.Lastname = lastname;
                        patient.Age = age;
                        patient.Priority = priorityValue;
                        patient.Status = status;

                        patientManager.AddPatient(patient);

                        Console.WriteLine("Patient added successfully.");
                        break;
                    }

                    case "2":
                    {
                        try
                        {
                            Console.Write("Enter Patient ID: ");
                            int patientIDBooking = int.Parse(Console.ReadLine());
                            Patient bookingPatient = patientManager.SearchPatient(patientIDBooking);

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

                            Appointment appt = appointmentManager.AddAppointment(bookingPatient, someStaffMember, scheduledTime, priority);
                            Console.WriteLine($"Appointment {appt.AppointmentId} booked successfully.");
                        }
                        catch (AppointmentConflictException ex)
                        {
                            Console.WriteLine($"Booking failed: {ex.Message}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format — please check date/ID entries.");
                        }
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


                    case "3":
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

                        patientManager.UpdatePatient(updateID, newPriority, newStatus);
                        break;

                    case "6":
                        Console.WriteLine("Enter Patient's ID to remove: ");
                        int removeID = Convert.ToInt32(Console.ReadLine());

                        patientManager.RemovePatient(removeID);
                        break;

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
    }
}
