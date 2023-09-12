static class SlowPrint
{

    public static void Print(string text)
    {
        foreach(char symbol in text)
        {
            Console.Write(symbol);
            Thread.Sleep(50);
        }
    }
}
