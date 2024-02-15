public static class OutputArray
{
    public static int Output(int[] Array)
    {
        int n = Array.Length;
        for(int i = 0; i < n; i++)
        {
            Console.WriteLine(Array[i]);
        }
        Console.WriteLine(" /n \n");
        return 0;
    }
}