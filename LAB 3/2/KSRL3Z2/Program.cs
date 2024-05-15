using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KSRL3Z2
{
    class Program
    {
        static void Main(string[] args)
        {
            string progID = "KSR20.COM3Klasa.1";

            try
            {
                Type typeOfCOM = Type.GetTypeFromProgID(progID, true);

                object comObject = Activator.CreateInstance(typeOfCOM);

                object[] mArgs = { "My super arg" };

                typeOfCOM.InvokeMember("Test", BindingFlags.InvokeMethod, null, comObject, mArgs);

                Console.WriteLine("Exercise 2 Success");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

    }
}
