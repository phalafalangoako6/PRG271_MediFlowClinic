using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediFlowClinic
{
    //CUSTOM EXCEPTION

    //When a n appointment status is changed in a way that makes no sense e.g Marking a cancelled appointment as completed
    public class IllegalStatusTransitionException : Exception
    {
        
        public IllegalStatusTransitionException (string message) : base (message) { }
    }
}
