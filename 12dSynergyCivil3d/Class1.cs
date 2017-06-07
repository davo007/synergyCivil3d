using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
using Autodesk.Civil.DatabaseServices.Styles;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;


namespace _12dSynergyCivil3d
{
    public class Class1:IExtensionApplication
    {
        public PaletteSet mypaleteset;

        //public PaletteSet mypaleteset2;

        public UserControl1 mypalette;

        public void Initialize()
        {
            //throw new NotImplementedException();
        }

        [CommandMethod("synciv")]

        public void palette()
        {
            if (mypaleteset == null)
            {
                mypaleteset = new PaletteSet("12dSynergy for Civil 3D", new Guid("801E3156-0779-4D98-A703-100521E57D1C"));

                mypalette = new UserControl1();
                //mypalette.ProgressBar1.PerformStep()
                mypaleteset.Add("Palette1", mypalette);


            }

            mypaleteset.Visible = true;
        }

        public void Terminate()
        {
            //throw new NotImplementedException();
        }
    }
}
