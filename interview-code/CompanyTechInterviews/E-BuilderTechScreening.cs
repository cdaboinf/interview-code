using System;

namespace interview_code.CompanyTechInterviews
{
    public class EBuilderScreening
    {
        public bool Search(int target, TreeNode root)
        {
            if (target < 0 && target > 5 * Math.Pow(10, 5))
            {
                return false;
            }
            
            var current = root;
            while (current != null)
            {
                if (current.val < target)
                {
                    current = current.right;
                }
                else if (current.val > target)
                {
                    current = current.left;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        
        /*
         * Extension Methods in c#
         *
         * Extension methods enable you to "add" methods to existing types without creating a new derived type, recompiling,
         * or otherwise modifying the original type. Extension methods are static methods, but they're called as
         * if they were instance methods on the extended type. For client code written in C#, F# and Visual Basic,
         * there's no apparent difference between calling an extension method and the methods defined in a type.
         *
         * Design Patters in c#: https://refactoring.guru/design-patterns/csharp
         *
         * Creational Patterns ** :
         *
         * Abstract Factory: Lets you produce families of related objects without specifying their concrete classes.
         * Builder: Lets you construct complex objects step by step. The pattern allows you to produce different types
         *          and representations of an object using the same construction code.
         * Factory Method: Provides an interface for creating objects in a superclass, but allows subclasses to alter
         *                 the type of objects that will be created.
         * Singleton: Lets you ensure that a class has only one instance, while providing a global access point to this instance.
         *
         * Structural Patterns ** :
         *
         * Adapter: Allows objects with incompatible interfaces to collaborate.
         * Decorator: Lets you attach new behaviors to objects by placing these objects inside special wrapper objects that contain the behaviors.
         *
         * Behavioral Patterns ** :
         *
         * Chain of Responsibility: Lets you pass requests along a chain of handlers. Upon receiving a request,
         *                          each handler decides either to process the request or to pass it to the next handler in the chain.
         * Observer: Lets you define a subscription mechanism to notify multiple objects about any events that happen to the object they're observing.
         *
         * Lock, Monitor, Mutex, Semaphore: https://abhijit-k-adhikari.me/2012/04/17/lock-monitor-mutex-semaphore/
         *
         * Locking:
         * Exclusive locking is used to ensure that only one thread can enter particular sections of code at a time.
         * The two main exclusive locking constructs are lock and Mutex. 
         * Of the two, the lock construct is faster and more convenient.
         * Mutex, though, has a niche in that its lock can span applications in different processes on the computer.
         *
         * Monitor.Enter and Monitor.Exit:
         * C#’s lock statement is in fact a syntactic shortcut for a call to the methods Monitor.Enter and Monitor.
         * Exit, with atry/finally block.
         *
         * Mutex:
         * A Mutex is like a C# lock, but it can work across multiple processes. In other words, Mutex can be computer-wideas well as application-wide.
         * Acquiring and releasing an uncontended Mutex takes a few microseconds — about 50 times slower than a lock.
         * With a Mutex class, you call the WaitOne method to lock and ReleaseMutex to unlock.
         * Closing or disposing aMutex automatically releases it. Just as with the lock statement, a Mutex can be released only from the same thread that obtained it.
         *
         * Semaphore:
         * A semaphore is like a nightclub: it has a certain capacity, enforced by a bouncer.
         * Once it’s full, no more people can enter, and a queue builds up outside.
         * Then, for each person that leaves, one person enters from the head of the queue.
         * The constructor requires a minimum of two arguments: the number of places currently available in the nightclub and the club’s total capacity.
         *
         * Abstract Method
         * Abstract Method resides in abstract class and it has no body.
         * Abstract Method must be overridden in non-abstract child class.
         *
         * Virtual Method
         * Virtual Method can reside in abstract and non-abstract class.
         * It is not necessary to override virtual method in derived but it can be.
         * Virtual method must have body ....can be overridden by "override keyword".....
         */
    }
}