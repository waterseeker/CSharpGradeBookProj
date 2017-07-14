using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {


            SpeechSynthesizer synth = new SpeechSynthesizer(); //instantiates a new object of type SpeechSynthesizer 
            //and sets that to a var so we can call it later
            synth.Speak("Hello! This is the grade book program.");
            //speaks
            DateTime now = DateTime.Now;

            //synth.Speak("Today is " + now.DayOfWeek);
            //you can also use other methods inside of the Speak

            GradeBook book = new GradeBook();
            //instantiates a new object of type GradeBook
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);
            //calls a method we defined in the GradeBook class to add some grades to the gradebook


            GradeStatistics stats = book.ComputeStatistics();
            //calls another method we defined, ComputeStatistics on the instance of GradeBook we assigned to the book 
            //var
            //Console.WriteLine("The average grade is " + stats.AverageGrade + ".");
            //Console.WriteLine("The highest grade is " + stats.HighestGrade + ".");
            //Console.WriteLine("The lowest grade is " + stats.LowestGrade + ".");


            WriteResult("average", stats.AverageGrade);

            //calls the WriteResult method we set up below and passes it some args
            WriteResult("highest grade", stats.HighestGrade);
            WriteResult("lowest grade", stats.LowestGrade);

            //synth.Speak("The average grade is " + stats.AverageGrade);
            //synth.Speak("The highest grade is " + stats.HighestGrade);
            //synth.Speak("The lowest grade is " + stats.LowestGrade + "womp womp");

        }

        static void WriteResult(string description, float result)
        {
            //    Console.WriteLine("The " + description + " is " + result + ".");

            Console.WriteLine("The {0} is {1}.", description, result);
            //this sets up a "formatting string" where you can combine the params you pass to the method with
            //whatever else you include in the quotes. 
        }

    }
}
