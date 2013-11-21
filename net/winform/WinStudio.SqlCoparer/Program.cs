using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Forms;

namespace WinStudio.SqlCoparer
{
    static class Program
    {
        //private static CompositionContainer _container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();

            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            //catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\SomeUser\\Documents\\Visual Studio 2010\\Projects\\SimpleCalculator3\\SimpleCalculator3\\Extensions"));


            //Create the CompositionContainer with the parts in the catalog
            //_container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                new CompositionContainer(catalog).ComposeParts(form);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }

            Application.Run(form);
        }
    }
}
