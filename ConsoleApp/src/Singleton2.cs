sealed class Singleton2
{
    private static Singleton2 _instance = null;
    private static object _mylock = new object();
    public string MyMessage { get; set; }
    private Singleton2()
    {
    }

    public static Singleton2 Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_mylock)
                {
                    _instance = new Singleton2();
                }
            }
            return _instance;
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("This is singleton2");
    }

}