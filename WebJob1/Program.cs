using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using WebJob1.Data;

namespace WebJob1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Am intrat");
            SqlClass test = new SqlClass();
           
                test.Verify();
            Console.WriteLine("Am iesit");
            //Console.ReadKey(); local
            //     var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            //   host.RunAndBlock();
        }
    }
}
