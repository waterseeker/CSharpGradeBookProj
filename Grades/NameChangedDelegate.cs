using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    //public delegate void NameChangedDelegate(string existingName, string newName); //the names of the parameters are arbitrary and don't affect the compiler
    //when this code is ran, the compiler doesn't check the parameter names to make sure they match. It's only going to check the type...
    // so it'll be looking for a method that returns void, and takes 2 strings as parameters. 
    

    public delegate void NameChangedDelegate(object sender, NameChangedEventArgs args);
    //the convention for an event delegate is for it to always have the sender as the first parameter and all of the parameters of the method as the second argument. 
}
