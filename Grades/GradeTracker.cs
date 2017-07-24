using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public abstract class GradeTracker : IGradeTracker //we want to be able to use this abstract class so many types of gradebooks can inherit its functionality
    {
        public abstract void AddGrade(float grade); //we set this to abstract and left out any implementation details because those are going to differ from one type to another
        public abstract GradeStatistics ComputeStatistics();
        public abstract void WriteGrades(TextWriter destination);
        public abstract IEnumerator GetEnumerator();
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null of empty");
                }

                if (_name != value && NameChanged != null) //if _name is not equal to the incoming vale AND is not null
                {
                    NameChangedEventArgs args = new NameChangedEventArgs();
                    //creates an instance of the NameChangedEventArgs
                    args.ExistingName = _name;
                    args.NewName = value;
                    NameChanged(this, args);
                    //in C# this will reference the object that you are inside of. so "this" here will reference the GradeBook object. 
                }

                _name = value;

            }
        }

        //public NameChangedDelegate NameChanged; //declares a public field of type NameChangedDelegate that is called NameChanged

        public event NameChangedDelegate NameChanged;

        protected string _name;
    }
}
