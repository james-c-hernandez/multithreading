// this is a template for the Singleton Pattern in C#
//
//The Singleton pattern is a creational design pattern in software engineering
//that ensures a class has only one instance and provides a global point of access
//to that instance. It’s like having a single, unique key to a special room—only
//one key exists, and everyone uses it to enter.
//

//Key Features
//Single Instance: No matter how many times you request it, you get the same object.
//Global Access: A static method (e.g., getInstance()) acts as the doorkeeper.
//Controlled Creation: The class itself manages its instantiation, typically by hiding the constructor.

//Purpose
//Used when exactly one object is needed to coordinate actions across a system (e.g., a logger, configuration manager, or database connection).
//Ensures consistency (e.g., one configuration file isn’t loaded multiple times).



using System;

public class Singleton
{
    // The single instance, initialized to null
    private static Singleton _instance = null;

    // Lock object for thread safety
    private static readonly object _lock = new object();
    public string MyMessage { get; set; }

    // Private constructor to prevent direct instantiation
    private Singleton()
    {
    }

    // Public method to get the instance
    public static Singleton Instance
    {
        get
        {
            // Double-check locking
            if (_instance == null) // First check (no lock yet) for performance before costly operation
            {
                lock (_lock) // Lock to make it thread-safe (costly operation)
                {
                    if (_instance == null) // Second check (inside lock)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }
    }

    // Example method to test it
    public void DoSomething()
    {
        Console.WriteLine("Singleton is working!");
    }
}

