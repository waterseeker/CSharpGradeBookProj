using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class ThrowAwayGradeBook : GradeBook //ThrowAwayGradeBook inherits from GradeBook which inherits from GradeTracker
    {
        public override GradeStatistics ComputeStatistics() //the override means that we have the ability to change the behavior of this method. in our example, we are changing the behavior
            //on the throwawaygradebook 
        {
            Console.WriteLine("ThrowAwayGradeBook::ComputeStatistics");//console logging to tell us we get into the compute statistics method of the throwawaygradebook

            float lowest = float.MaxValue;
            foreach (float grade in grades) //loops through each item in grades
            {
                lowest = Math.Min(grade, lowest); //finds the lowes grade in grades and sets it to a var
            }
            grades.Remove(lowest); //invokes the .Remove method and passes it the lowest var
            return base.ComputeStatistics(); //then runs the .ComputeStatistics method that is defined in the gradebook. 
        }
    }
}
