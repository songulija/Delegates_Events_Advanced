using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DelegatesAndEvents
{
    internal class Program
    {
        //delegate is definition of method. it holds references to methods.
        //it has to have same structure as method that you assign to it
        //Delegate defines the signature(return type and parameters) 
        public delegate int Manipulate(int a, int b);

        //Action is just a delegate that returns nothing and has no input
        public delegate void MyAction();

        //Func is just delegate with return type. can also pass varuables if you like as in Action
        public delegate int MyFunc();

        static void Main(string[] args)
        {
            //Invoking a normal method
            var normalMethodRes = Multiplication(2, 3);
            //Create instance of delegate
            var normalMethodDelegate = new Manipulate(Multiplication);
            //It'll invoke all methods it has references to(NormalMethod)
            var normalResults = normalMethodDelegate(2, 3);
            //Pass delegate as varuable
            var result = RunAnotherMethod(normalMethodDelegate, 4, 3);
            Console.WriteLine(result);

            //anonymous method is a = delegate() {} and it returns a delegate
            //if you just want to make simple action and not define seperate methods for it
            //I want it to be Manipulate signature. so that anonymous method delegate has to have same return type and params as Manipulate
            Manipulate anonymousMethodDelegate = delegate (int a, int b)
            {
                return a * b;
            };
            var anonymousResult = anonymousMethodDelegate(8, 8);
            Console.WriteLine(anonymousResult);




            //Lmbda expression is anything with => and a left/right value
            //They can return a delegate so method can be invoked
            // first is input(on right side) then after lambda(=>) is return.
            //and this lambda delegate will be of type "Manipulate". it expects int value, and returns int value
            //beoucse it looks at Manipulate delegate input and return types
            Manipulate lambdaDelegate = (a, b) => a * b;
            int lambdaResult = lambdaDelegate(10, 12);

            Manipulate lambdaDelegate2 = lambdaDelegate = (a, b) => a + b;
            int lambdaResult2 = lambdaDelegate(100, 12);

            //nicer way to write delegate. you can write whole function inside
            Manipulate nicerLambdaDelegate = (a, b) =>
            {
                var multiplication = a * a;
                var sum = b + b;
                return multiplication + sum;
            };
            int nicerLambdaResult = nicerLambdaDelegate(4, 5);

            //Lambda can return expression. Just have to tell to expect Expression of "Manipulate" type
            //when having expression you can manipulate its body, parameters.Then Compile it to delegate and invoke it
            Expression<Manipulate> expression = (a, b) => a * b;
            expression.Compile().Invoke(1,2);



            //MyAction delegate setting to lambda delegate. it returns nothing
            //it can take parameters if they are specified but if it takes no parameters it can be called just Action
            MyAction myAction = () => { 
                var newVal = 2;
                Console.WriteLine(newVal);
            };
            Action myAction2 = () => {
                var newVal = 2;
                Console.WriteLine(newVal);
            };
            //Action that expects input, all parameters in <> are specified expected inputs
            Action<int, int> myAction3 = (a, b) =>
            {
                var result = a * b;
                Console.WriteLine(result);
            };
            myAction3.Invoke(55, 2);



            //Func just delegate that has return type. Assigning it to lambda that returns delegate
            MyFunc myFunc = () => 2;
            var funcDelegateResult = myFunc();
            
            //Or can use keyword "Func" and specify return type. last parameter is Output type(Output can only be one)
            //and before output are inputs of Func delegate
            Func<int, int, int> myFunc2 = (a, b) => a * b;
            var funcDelegateWithParamsResult = myFunc2(5,6);
            Console.WriteLine(funcDelegateWithParamsResult);


            //passing items list, and lambda expression delegate. which expects item(String) checks if its equal to "a"
            //and returns bool if its matches
            var items = new List<string>(new[] { "a", "b", "c", "d", "e", "f", "g" });
            var foundItem1 = Helpers.GetFirstOrDefault(items, (item) => item == "a");

            //Doing same just defined Func Delegate before, that expects string and returns boolean.
            //it just checks if passed "item" equal to "c". Then passing list and delegate to Mimiced FirstOrDefault method
            Func<string, bool> fDelegate1 = (string item) => item == "c";
            var foundItem2 = Helpers.GetFirstOrDefault(items, fDelegate1);
            Console.WriteLine(foundItem2);

            //Calling Built in Linq "FirstOrDefault". passing Func delegate which expects item(sting) and returns
            //bool based on if its equal to "b" or not. firstorDefault based on that returns item
            Func<string, bool> fDelegate = (string item) => item == "b";
            var foundItem3 = items.FirstOrDefault(fDelegate);
            
        }
        /// <summary>
        /// Normal looking method
        /// </summary>
        /// <param name="a">Input value</param>
        /// <returns>Returns input value square</returns>
        public static int Multiplication(int a, int b)
        {
            return a * b;
        }

        public static int Sum(int a, int b)
        {
            return a + b;
        }

        //Delegates are basically to be able to pass in Method as varuable to another method
        //becouse you cant pass method to another method. So here we expect to pass ManipulateDelegate
        public static int RunAnotherMethod(Manipulate manipulate, int a, int b)
        {
            //invoking delegate that is passed. so it will call all methods of delegate that it has references to
            var result = manipulate(a,b);
            return result;
        }
    }

    /// <summary>
    /// Mimic Linq expression we use a lot. FirstOrDefault, Where in LinqEpxression we use Lambda epxression (which return delegate)
    /// </summary>
    public static class Helpers
    {
        //pass in List of strings, and Func delegate which expects
        //input of String and returns bool(whether its matched or not)
        //this makes possible that any varuable of List<String> type can call this method like listOfStrings.GetFirstOrDefault
        public static string GetFirstOrDefault(this List<string> items, Func<string, bool> findMatch)
        {
            foreach (var item in items)
            {
                //passing string as input to Func delegate, it returns bool. if that method that is passed to this method
                //finds Match it will return true. then i return item becouse it matched
                if (findMatch(item))
                    return item;
            }
            //if nothing found return null
            return null;
        }
    }

    public static class GenericHelpers
    {
        public static TResult Firstrr<TResult>(this List<TResult> items, Func<TResult, bool> findMatch)
        {
            foreach (var item in items)
            {
                //passing string as input to Func delegate, it returns bool. if that method that is passed to this method
                //finds Match it will return true. then i return item becouse it matched
                if (findMatch(item))
                    return item;
            }
            //if nothing found return default of that passed type, if string default is null, if int its 0
            return default(TResult);
        }
    }
}
