using System;
using System.Diagnostics;

static class Optional
{

    public static int[] smallArray = Generate(100);
    public static int[] mediumArray = Generate(1000);
    public static int[] largeArray = Generate(10000);

    private static int[] Generate(int sizeArray)
    {
        Random random = new Random();
        int[] array = new int[sizeArray];
        for (int i = 0; i < sizeArray; i++)
        {
            array[i] = random.Next(1, 1000);
        }
        return array;
    }

}