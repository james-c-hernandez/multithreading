using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.src
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ProducerConsumerQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>(); // The shared queue
        private readonly object lockObject = new object(); // Lock for thread safety

        // Add an item to the queue (Producer)
        public void Produce(T item)
        {
            lock (lockObject) // Lock to prevent multiple threads from messing up the queue
            {
                queue.Enqueue(item); // Add item to the queue
                Console.WriteLine($"Produced: {item}");
            }
        }

        // Take an item from the queue (Consumer)
        public T Consume()
        {
            lock (lockObject) // Lock to ensure safe removal
            {
                if (queue.Count > 0) // Check if there's something to take
                {
                    T item = queue.Dequeue(); // Remove and return the first item
                    Console.WriteLine($"Consumed: {item}");
                    return item;
                }
                else
                {
                    Console.WriteLine("Queue empty, nothing to consume");
                    return default(T); // Return default if empty (e.g., null for strings)
                }
            }
        }

        // Check how many items are in the queue
        public int Count
        {
            get
            {
                lock (lockObject) // Lock for safe access
                {
                    return queue.Count;
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
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
                for (int i = 0; i < 3; i++)
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
        }
    }
}
