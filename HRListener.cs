using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB1
{

    public class HRListener
    {
        public void OnItemAdded(string message)
        {
            Console.WriteLine($"[Событие] {message}");
        }

        public void OnItemRemoved(string message)
        {
            Console.WriteLine($"[Событие] {message}");
        }
    }

}
