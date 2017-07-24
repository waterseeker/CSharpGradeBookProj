using System;
using System.Collections.Generic;
using System.IO;
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


            //SpeechSynthesizer synth = new SpeechSynthesizer(); //instantiates a new object of type SpeechSynthesizer 
            //and sets that to a var so we can call it later
            //synth.Speak("Hello! This is the grade book program.");
            //speaks

            DateTime now = DateTime.Now;
            Console.WriteLine("Today is " + now.DayOfWeek + ".");
            IGradeTracker book = CreateGradeBook();
            //GradeBook book = CreateGradeBook(); removed this to implement the GradeTracker abstract class
            //instantiates a new object of type GradeBook

            //book.NameChanged = new NameChangedDelegate(OnNameChanged);
            //instantiates a new NameChangedDelegate that is taking the OnNameChanged method (defined below) as a parameter. 
            //book.NameChanged = new NameChangedDelegate(OnNameChanged2);
            //if we just make a copy of of OnNameChanged, change the parameters of the method and the name of the delegate, this just overwrites the original. Now we get two lines of "***".
            //we can get this to work, though by using the += operation on the delegates instead of just assignment "="
            //there is also a -+ operation that will remove the designated reference to a function

            //book.NameChanged += new NameChangedDelegate(OnNameChanged);
            //book.NameChanged += new NameChangedDelegate(OnNameChanged2);
            //this makes both fire on name change so we'll get 2 outputs for each instance of the name changing. 
            //an issue with using delegates liks this though, is that someone can still change the values here by doing something like ...
            //book.NameChanged = null;
            //by adding the keyword "event" to the NameChangedDelegate, it makes is so that the only thing other pieces of code can do from outside the GradeBook is add or remove a subscriber to this event
            //so it's no longer possible to do book.NameChanged = null which would wipe out all of the subscriptions. 

            //we can cut that code down a little further ...
            //book.NameChanged += OnNameChanged;
            //book.NameChanged += OnNameChanged2;

            //book.Name = null; //this is to test our throw error in GradeBook.cs



            GetBookName(book);
            //catch (Exception ex) //order is imporatnt. you want to have the most specific catches at the top of the try catch block and the most general at the end. In this example, 
            //if the Exception catch was before ArgumentException/NullReferenceException then the logic wouldn't make it to the more specific conditions because both of those Exceptions are 
            //Exceptions so they're caught by the general "Exception" catch. This can be dangrous, to catch all exceptions, because you don't get any specific feedback and it might be allowing
            //the program to continue running when it would have been better off crashing. 
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //book.Name = "My Grade Book";
            //book.Name = "Changed Name.";
            AddGrades(book);

            SaveGrades(book);
            //calls a method we defined in the GradeBook class to add some grades to the gradebook
            //book.WriteGrades(Console.Out);
            //book.WriteGradesInReverseOrder(Console.Out);




            WriteResults(book);

        }

        private static IGradeTracker CreateGradeBook()
        {
            //synth.Speak("Today is " + now.DayOfWeek);
            //you can also use other methods inside of the Speak

            return new ThrowAwayGradeBook();
        }

        private static void WriteResults(IGradeTracker book)
        {
            GradeStatistics stats = book.ComputeStatistics();
            //calls another method we defined, ComputeStatistics on the instance of GradeBook we assigned to the book 
            //var
            //Console.WriteLine("The average grade is " + stats.AverageGrade + ".");
            //Console.WriteLine("The highest grade is " + stats.HighestGrade + ".");
            //Console.WriteLine("The lowest grade is " + stats.LowestGrade + ".");

            //Console.WriteLine(book.Name);

            foreach (float grade in book)
            {
                Console.WriteLine(grade);
            }

            WriteResult("average", stats.AverageGrade);

            //calls the WriteResult method we set up below and passes it some args
            WriteResult("highest grade", stats.HighestGrade);
            WriteResult("lowest grade", stats.LowestGrade);
            WriteResult("Grade", stats.LetterGrade);
            WriteResult(stats.Description, stats.LetterGrade);


            //synth.Speak("The average grade is " + stats.AverageGrade);
            //synth.Speak("The highest grade is " + stats.HighestGrade);
            //synth.Speak("The lowest grade is " + stats.LowestGrade + "womp womp");
        }

        private static void SaveGrades(IGradeTracker book)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt")) //using will make sure this resource is always closed after it is used. the code in the curly braces is attached to the
            //using statement. this pretty much sets up a try finally block to make sure any resources being used are cleaned up afterwards. 
            {
                book.WriteGrades(outputFile);
                //outputFile.Close(); since we're using a "using" statment now, there's no longer a reason for us to use an explicit .Close() because the using statment will do that for us. 
            }
        }

        private static void AddGrades(IGradeTracker book)
        {
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);
        }

        private static void GetBookName(IGradeTracker book)
        {
            try
            {
                Console.WriteLine("Please enter a name."); //prompts user for an input
                book.Name = Console.ReadLine();//sets the value of the book name to the user input
            }
            catch (ArgumentException ex) //first param is the type of error to look for, the second param is an optional one that sets a var where the system will store the exception if there is one
            {
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException ex) //we can name this variable ex as well because it's in a different scope than the var in the ArgumentException
            {
                Console.WriteLine("Something went wrong");
            }
        }

        //static void OnNameChanged(object sender, NameChangedEventArgs args)
        //{
        //    Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}");
        //}
        //static void OnNameChanged(string existingName, string newName)
        //{
        //    Console.WriteLine($"Grade book changing name from {existingName} to {newName}");
        //}

        //static void OnNameChanged2(string existingName, string newName)
        //{
        //   Console.WriteLine("***");
        //}

        static void WriteResult(string description, float result)
        {
            //    Console.WriteLine("The " + description + " is " + result + ".");

            Console.WriteLine("The {0} is {1}.", description, result);
            //this sets up a "formatting string" where you can combine the params you pass to the method with
            //whatever else you include in the quotes. 
        }

        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description}: {result}", description, result);
        }

    }
}
