// See https://aka.ms/new-console-template for more information

//using ConsoleApp;
//using System.Security.Cryptography;

#region Singleton
using ConsoleApp.src;

Singleton s1 = Singleton.Instance;
s1.DoSomething();

Singleton s2 = Singleton.Instance;
s2.DoSomething();

Singleton2 s3 = Singleton2.Instance;
s3.DoSomething();
s3.MyMessage = "this is S3! Notice how the Message did not change";

Singleton2 s4 = Singleton2.Instance;
s4.DoSomething();
Console.WriteLine("S4 message=" + s4.MyMessage);
#endregion

#region ProducerConsumerQueue
// Create the shared queue
ProducerConsumerQueue<string> queue = new ProducerConsumerQueue<string>();

// Producer thread: Adds items
Thread producer1 = new Thread(() =>
{
    for (int i = 1; i <= 3; i++)
    {
        queue.Produce($"Item {i} from Producer 1");
        Thread.Sleep(500); // Simulate work (0.5 seconds)
    }
});

Thread producer2 = new Thread(() =>
{
    for (int i = 1; i <= 3; i++)
    {
        queue.Produce($"Item {i} from Producer 2");
        Thread.Sleep(700); // Slightly different pace
    }
});

// Consumer thread: Takes items
Thread consumer1 = new Thread(() =>
{
    for (int i = 0; i < 4; i++)
    {
        queue.Consume();
        Thread.Sleep(1000); // Simulate processing (1 second)
    }
});

Thread consumer2 = new Thread(() =>
{
    for (int i = 0; i < 3; i++)
    {
        queue.Consume();
        Thread.Sleep(1200); // Slightly different pace
    }
});

// Start all threads
producer1.Start();
producer2.Start();
consumer1.Start();
consumer2.Start();

// Wait for all threads to finish
producer1.Join();
producer2.Join();
consumer1.Join();
consumer2.Join();

// Show final queue state
Console.WriteLine($"Queue items remaining: {queue.Count}");

// Pause to see output
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();


#endregion


Console.WriteLine("Press ENTER to exit...");
Console.ReadLine(); // wait for user to press enter
