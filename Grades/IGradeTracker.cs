using System.Collections;
using System.IO;

namespace Grades
{
    internal interface IGradeTracker : IEnumerable //made it IEnumerable so we can use a foreach on the book. otherwise book has no property on it that'll allow it to be enumerated through. 
    { //everything that uses the interface has to match the "cookie cutter" below. in this instance, it has to have AddGrade, ComputeStatistics, and WriteGrade methods and a Name property that is
        //a string with a getter and a setter (that is read/writable).
        void AddGrade(float grade); //we removed and access modifiers because anything using the interface has to have access to them, also all of them are virtual methods so we can't use abstract
        GradeStatistics ComputeStatistics();
        void WriteGrades(TextWriter destination);
        string Name { get; set; }

    }
}