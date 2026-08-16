using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    public class AppointmentManager 
    {
        private List<Appointment> appointments = new List<Appointment>();

        public void SaveAppointments(string filePath) 
        {
            string json = JsonSerializer.Serialize(appointments);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Appointments saved to file.");
        }

        public void LoadAppointments(string filePath) 
        {
            if (File.Exists(filePath)) 
            {
                string json = File.ReadAllText(filePath);
                appointments = JsonSerializer.Deserialize<List<Appointment>>(json);
                Console.WriteLine("Appointments loaded from file.");
            }   
            else 
            {
            Console.WriteLine("No saved file found.");
            }
        }
    }

}