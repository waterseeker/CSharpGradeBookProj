using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBook : GradeTracker //GradeBook inherits from GradeTracker
    //public class GradeBook : object //technically, the gradebook object inherits from the system.object object. this is true of pretty much anything you don't devine a derived class on. that's why
        //intellisense on the gradebook will show you methods that you haven't defined. it'll include methods that are on the stystem.object
    {
        public GradeBook() //this is the constructor for the GradeBook
        {
            _name = "Empty"; //sets initial value of the _name property of the GradeBook
            grades = new List<float>();
        }

        public override GradeStatistics ComputeStatistics()
        //public virtual GradeStatistics ComputeStatistics() //we got an error on this line when we changed over to the abstract class because we can't use the virtual keyword with the abstract class
        //we have to use the override keyword instead. 
        {
            Console.WriteLine("GradeBook::ComputeStatistics");//console logging to tell us we get into the compute statistics method of the gradebook

            GradeStatistics stats = new GradeStatistics();

            float sum = 0;
            foreach (float grade in grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade);
                sum += grade;
            }
            stats.AverageGrade = sum / grades.Count;
            return stats;
        }
        public override void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public override IEnumerator GetEnumerator()
        {
            return grades.GetEnumerator();
        }

        protected List<float> grades; //protected is between private and public access. It can be accessed by code in it's own class or in a derived class. 
        //since throwawaygradebook is a derived class of gradebook, throwawyagradebook will be able to access the list of grades. If it was set to private, 
        //the derived class would not be able to access it. 

        public override void WriteGrades(TextWriter destination)
        {
            for (int i = 0; i < grades.Count; i++)
            {
                destination.WriteLine(grades[i]);
            }
        }
        //this loops through the grades array and prints each line to the console. 

        //if we wanted to print the grades out in reverse order we could start at grades.Count instead of 0 for the initial value of i and decrement i for each loop.
        public void WriteGradesInReverseOrder(TextWriter destination)
        {
            for (int i = grades.Count; i > 0; i--)
            {
                destination.WriteLine("reverse order " + grades[i - 1]);
            }
        }
        //this loops through the grades array and prints each line to the console. 
    }
}
