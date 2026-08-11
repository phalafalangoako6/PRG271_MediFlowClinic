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

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("===============================================");
                Console.WriteLine("MEDIFLOW CLINIC SYSTEM");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. View Patient");
                Console.WriteLine("3. Search Patient");
                Console.WriteLine("4. Update Patient");
                Console.WriteLine("5. Remove Patient");
                Console.WriteLine("6. View Medical Staff");
                Console.WriteLine("7. Exit");
                Console.WriteLine("======================================================");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "1":
                        Console.WriteLine("Enter patient ID: ");
                        int patientID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter First Name: ");
                        string firstname = Console.ReadLine();

                        Console.Write("Enter Last Name: ");
                        string lastname = Console.ReadLine();

                        Console.Write("Enter Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Priority: ");
                        string priority = Console.ReadLine();

                        Console.Write("Enter Status: ");
                        string status = Console.ReadLine();

                        Patient patient = new Patient();

                        patient.PatientID = patientID;
                        patient.Firstname = firstname;
                        patient.Lastname = lastname;
                        patient.Age = age;
                        patient.Priority = priority;
                        patient.Status = status;

                        patientManager.AddPatient(patient);

                        Console.WriteLine("Patient added successfully.");
                        break;


                    case "2":
                        patientManager.ViewPatients();
                        break;


                    case "3":
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


                    case "4":
                        Console.WriteLine("Enter the patient's ID to update: ");
                        int updateID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter the new priority: ");
                        string newPriority = Console.ReadLine();

                        Console.Write("Enter the new status: ");
                        string newStatus = Console.ReadLine();

                        patientManager.UpdatePatient(updateID, newPriority, newStatus);
                        break;

                    case "5":
                        Console.WriteLine("Enter Patient's ID to remove: ");
                        int removeID = Convert.ToInt32(Console.ReadLine());

                        patientManager.RemovePatient(removeID);
                        break;

                    case "6":
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

                    case "7":
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
