using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;

namespace Main_Controler

{
    //-------------------------------
    //
    //-------------------------------
    class Program
    {

        //-------------------------------
        // call the main controller
        //-------------------------------
        static void Main(string[] args)
        {
            try
            {
                Main_Controler _Main = new Main_Controler();
                _Main.Do_Work();
            }
            catch
            {
                // do nothing
            }
        }
    }
}
