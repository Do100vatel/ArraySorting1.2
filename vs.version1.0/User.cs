using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class User
{
    // Dictionaries to keep track of iteration and stage counts for each sorting method
    private static readonly Dictionary<Func<int[], SortCounter, int[]>, int> iterationsCounters = new Dictionary<Func<int[], SortCounter, int[]>, int>();
    private static readonly Dictionary<Func<int[], SortCounter, int[]>, int> stagesCounters = new Dictionary<Func<int[], SortCounter, int[]>, int>();

    // Dictionary to associate a sorting method with its corresponding SortCounter
    private static readonly Dictionary<int, SortCounter> counters = new Dictionary<int, SortCounter>
    {
        { 1, new SortCounter() },
        { 2, new SortCounter() },
        { 3, new SortCounter() },
        { 4, new SortCounter() },
        { 5, new SortCounter() },
        { 6, new SortCounter() },
        { 7, new SortCounter() },
        { 8, new SortCounter() },
        { 9, new SortCounter() },
        { 10, new SortCounter() },
        { 11, new SortCounter() }
        // Initialize SortCounter instances for each sorting method
    };

    // Main method for user interaction and sorting execution
    public static void UserUsing(params Type[] arrayTypes)
    {
        foreach (var arrayType in arrayTypes)
        {
            int[] array = GenerateArray(arrayType);

            // Если массив не был сгенерирован, пропускаем его
            if(array == null)
            {
                Console.WriteLine($"Unable to generate array for type: {arrayType}");
                continue;
            }

            string arrayTypeName = GetArrayTypeName(arrayType);

            // Choose sorting methods and collect sorting results
            List<SortInfo> sortingResults = ChooseSortingMethods(array, arrayType);

            // Sort the results based on the number of iterations
            sortingResults.Sort((a, b) => a.Iterations.CompareTo(b.Iterations));

            // Prompt the user whether to output the full array
            Console.WriteLine("Do you want output the full sorted array? (yes/no)");
            string userResponse = Console.ReadLine().Trim().ToLower();
            bool outputFullArray = userResponse == "yes" || userResponse == "y";

            // Display results for each sorting method
            foreach (var result in sortingResults)
            {
                Console.WriteLine($"Result {result.SortName}: ");
                if (outputFullArray)
                    OutputArray.Output(result.SortedArray);
                Console.WriteLine($"Number of iterations: {result.Iterations}");
                Console.WriteLine($"Number of stages: {result.Stages}");
                Console.WriteLine($"Array type: {result.ArrayTypeName}");
                Console.WriteLine();
            }
        }

    }

    private static int[] GenerateArray(Type arrayType)
    {
        if (arrayType == typeof(int[]))
        {
            if (arrayType == typeof(int[]))
                return Optional.smallArray;
            else if (arrayType == typeof(int[]))
                return Optional.mediumArray;
            else if (arrayType == typeof(int[]))
                return Optional.largeArray;
        }
        return null;
    }
    private static string GetArrayTypeName(Type arrayType)
    {
        if (arrayType == typeof(int[]))
        {
            if (Optional.smallArray.Length == 100)
                return "Small";
            else if (Optional.mediumArray.Length == 1000)
                return "Medium";
            else if (Optional.largeArray.Length == 10000)
                return "Large";
        }
        return "Unknown";
    }

    // Method to prompt the user for sorting method choices
    private static List<SortInfo> ChooseSortingMethods(int[] array, Type arrayType)
    {
        Console.WriteLine("Select sorting methods (enter numbers separated by spaces):");
        Console.WriteLine("0. All methods");
        // Display available sorting methods
        Console.WriteLine("1. Bubble sort");
        Console.WriteLine("2. Insertion sort");
        Console.WriteLine("3. Selection sort");
        Console.WriteLine("4. Merge sort");
        Console.WriteLine("5. Quick sort");
        Console.WriteLine("6. Heap sort");
        Console.WriteLine("7. Counting sort");
        Console.WriteLine("8. Radix sort");
        Console.WriteLine("9. Shell sort");
        Console.WriteLine("10. Shaker sort");
        Console.WriteLine("11. Tim sort");

        var sortingResults = new List<SortInfo>();

        // Mapping of method index to corresponding sorting method
        var allSortingMethods = new Dictionary<int, Func<int[], SortCounter, int[]>>
    {
        // Initialize with sorting methods
        { 1, Boubble.BoubbleSorting    },
        { 2, Insertion.InsertionSorting },
        { 3, Selection.SelectionSorting },
        { 4, Merge.MergeSorting },
        { 5, Quick.QuickSorting },
        { 6, Heap.HeapSorting },
        { 7, Counting.CoutingSorting },
        { 8, Radix.RadixSorting },
        { 9, Shell.ShellSorting },
        { 10, Shaker.ShakerSorting },
        { 11, Tim.TimSorting }
    };

        string userInput = Console.ReadLine().Trim(); // Remove extra spaces

        if (userInput == "0")
        {
            // If the user chooses all methods, run each sorting method and collect results
            foreach (var method in allSortingMethods.Values)
            {
                sortingResults.Add(RunSorting(GetSortingMethodName(method), method, (int[])array.Clone(), arrayType));
            }
        }
        else
        {
            // If the user chooses specific methods, run each selected method and collect results
            string[] selectedMethods = userInput.Split(' ');

            foreach (string methodIndex in selectedMethods)
            {
                // Parse user input and run the corresponding sorting method
                if (int.TryParse(methodIndex, out int index) && index >= 1 && index <= allSortingMethods.Count)
                {
                    sortingResults.Add(RunSorting(GetSortingMethodName(index), allSortingMethods[index], (int[])array.Clone(), arrayType));
                }
                else
                {
                    Console.WriteLine($"Invalid sorting method index: {methodIndex}");
                }
            }
        }

        return sortingResults;
    }

    // Method to execute a sorting method and collect relevant information
    private static SortInfo RunSorting(string sortName, Func<int[], SortCounter, int[]> sortingMethod, int[] array, Type arrayType)
    {
        var counter = new SortCounter();

        // Create a stopwatch to measure elapsed time
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        int[] sortedArray = sortingMethod.Invoke(array, counter);

        // Stop the stopwatch after sorting is completed
        stopwatch.Stop();

        UpdateCounters(sortingMethod, counter);

        string arrayTypeName = arrayTypeNames.ContainsKey(array) ? arrayTypeNames[array] : "Unknown";

        return new SortInfo(sortName, sortedArray, counter.Iterations, counter.Stages, arrayType);
    }

    private static Func<int[], SortCounter, int[]> GetSortingMethod(int index)
    {
        var sortingMethods = new Dictionary<int, Func<int[], SortCounter, int[]>>
    {
        { 1, Boubble.BoubbleSorting    },
        { 2, Insertion.InsertionSorting },
        { 3, Selection.SelectionSorting },
        { 4, Merge.MergeSorting },
        { 5, Quick.QuickSorting },
        { 6, Heap.HeapSorting },
        { 7, Counting.CoutingSorting },
        { 8, Radix.RadixSorting },
        { 9, Shell.ShellSorting },
        { 10, Shaker.ShakerSorting },
        { 11, Tim.TimSorting }
         };

        return sortingMethods.TryGetValue(index, out var method) ? method : null;
    }


    private static Dictionary<Func<int[], SortCounter, int[]>, int> sortingMethodIndices = new Dictionary<Func<int[], SortCounter, int[]>, int>
{
    { Boubble.BoubbleSorting    , 1 },
    { Insertion.InsertionSorting, 2 },
    { Selection.SelectionSorting, 3 },
    { Merge.MergeSorting        , 4 },
    { Quick.QuickSorting        , 5 },
    { Heap.HeapSorting          , 6 },
    { Counting.CoutingSorting   , 7 },
    { Radix.RadixSorting        , 8 },
    { Shell.ShellSorting        , 9 },
    { Shaker.ShakerSorting      , 10 },
    { Tim.TimSorting            , 11 }
};

    // Method to get the index of a sorting method
    private static int GetSortingMethodIndex(Func<int[], SortCounter, int[]> sortingMethod)
    {
        // Return the index of the sorting method if found, otherwise return -1
        if (sortingMethodIndices.TryGetValue(sortingMethod, out int index))
        {
            return index;
        }
        return -1;
    }

    // Method to update counters with the results of a sorting method
    private static void UpdateCounters(Func<int[], SortCounter, int[]> sortingMethod, SortCounter counter)
    {
        // Update counters based on the sorting method
        int index = GetSortingMethodIndex(sortingMethod);
        if (index != -1)
        {
            counters[index] = counter;
        }
    }

    // Method to get the name of a sorting method
    private static string GetSortingMethodName(Func<int[], SortCounter, int[]> sortingMethod)
    {
        // Return the name of the sorting method based on its index
        var pair = sortingMethodIndices.FirstOrDefault(x => x.Key == sortingMethod);
        return pair.Value != 0 ? GetSortingMethodName(pair.Value) : "All methods";
    }

    // Method to get the name of a sorting method based on its index
    private static string GetSortingMethodName(int index)
    {
        // Return the name of the sorting method based on its index
        switch (index)
        {
            case 1: return "Bubble sort";
            case 2: return "Insertion sort";
            case 3: return "Selection sort";
            case 4: return "Merge sort";
            case 5: return "Quick sort";
            case 6: return "Heap sort";
            case 7: return "Counting sort";
            case 8: return "Radix sort";
            case 9: return "Shell sort";
            case 10: return "Shaker sort";
            case 11: return "Tim sort";
            default: return "Unknown method";
        }
    }

    // Method to get the number of iterations for a sorting method
    static int GetIterations(Func<int[], SortCounter, int[]> sortingMethod, int arrayLength)
    {
        // Return the total number of iterations for a sorting method
        return iterationsCounters.TryGetValue(sortingMethod, out int iterations)
            ? iterations + arrayLength - 1
            : 0;
    }

    // Class to store sorting information
    class SortInfo
    {
        public string SortName { get; }
        public int[] SortedArray { get; }
        public int Iterations { get; }
        public int Stages { get; }
        public string ArrayTypeName { get; }

        public List<int> IterationCountPerStage { get; }

        public SortInfo(string sortName, int[] sortedArray, int iterations, int stages, Type arrayType)
        {
            SortName = sortName;
            SortedArray = sortedArray;
            Iterations = iterations;
            Stages = stages;
            ArrayTypeName = GetArrayTypeName(arrayType);
            // Initialize other properties
        }
    }

    private static Dictionary<int[], string> arrayTypeNames = new Dictionary<int[], string>
    {
        { Optional.smallArray, "Small" },
        { Optional.mediumArray, "Medium" },
        { Optional.largeArray, "Large" }
    };
}