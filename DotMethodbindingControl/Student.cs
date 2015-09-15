using System.Collections;

namespace DotMethodbindingControl
{
    public class Student
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string GetName()
        {
            return "eamon";
            //  throw new System.NotImplementedException();
        }
    }
}