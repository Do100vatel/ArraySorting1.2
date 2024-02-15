
public class SortCounter
{
    public int Iterations { get; set; }
    public int Stages { get; set; } // Добавлено свойство Stages
}

public static class Sorting
{
    public static int SortingForm(int[] Array, Func<int[], SortCounter, int[]> sortingMethod)
    {
        SortCounter counter = new SortCounter();
        int[] sortedArray = sortingMethod(Array, counter);

        Console.WriteLine($"Iterations: {counter.Iterations}, Stages: {counter.Stages}");

        // Возвращаем 0 для примера. Можете вернуть что-то еще в зависимости от ваших требований.
        return 0;
    }
    public static int FindMaxValue(int[] array)
    {
        int max = array[0];
        foreach (int num in array)
        {
            if (num > max)
            {
                max = num;
            }
        }
        return max;
    }
}

public static class Boubble
{
    public static int[] BoubbleSorting(int[] array, SortCounter counter)
    {
        counter.Iterations = 0;
        counter.Stages = 0;

        int length = array.Length;
        bool swapped;
        do
        {
            swapped = false;
            for (int i = 1; i < length; i++)
            {
                if (array[i - 1] >= array[i])
                {
                    int temp = array[i - 1];
                    array[i - 1] = array[i];
                    array[i] = temp;

                    swapped = true;
                }
                counter.Iterations++;
            }
            length--;
            counter.Stages++;
        } while (swapped);
        int[] sortedArray = (int[])array.Clone();
        return sortedArray;
    }
}

public static class Insertion
{
    public static int []InsertionSorting(int[] Array, SortCounter counter)
    {
        counter.Iterations = 0;
        counter.Stages = 0;

        int n = Array.Length;

        for (int i = 1; i < n; i++)
        {
            int key = Array[i];
            int j = i - 1;

            while (j >= 0 && Array[j] > key)
            {
                Array[j + 1] = Array[j];
                j = j - 1;
                counter.Iterations++;
            }
            Array[j + 1] = key;
            counter.Iterations++;
        }
        counter.Stages++;
        int[] sortedArray = (int[])Array.Clone();
        return sortedArray;
    }
}

public static class Selection
{
    public static int []SelectionSorting(int[] Array, SortCounter counter)
    {
        counter.Iterations = 0;
        counter.Stages = 0;

        int n = Array.Length;

        for (int i = 0; i < n; i++)
        {
            int MinIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                if (Array[j] < Array[MinIndex])
                {
                    MinIndex = j;
                    counter.Iterations++;
                }
                counter.Iterations++;
            }

            int temp = Array[MinIndex];
            Array[MinIndex] = Array[i];
            Array[i] = temp;
            counter.Stages++;
        }
        int[] sortedArray = (int[])Array.Clone();
        return sortedArray;
    }

}

public static class Merge
{
    public static int[] MergeSorting(int[] array, SortCounter counter)
    {
        counter.Iterations = 0;
        counter.Stages = 0;

        if (array.Length <= 1)
        {
            return array;
        }

        int mid = array.Length / 2;
        int[] leftArray = new int[mid];
        int[] rightArray = new int[array.Length - mid];

        Array.Copy(array, 0, leftArray, 0, mid);
        Array.Copy(array, mid, rightArray, 0, array.Length - mid);

        leftArray = MergeSorting(leftArray, counter);
        rightArray = MergeSorting(rightArray, counter);

        MergePlus(array, leftArray, rightArray, counter);

        return array;
    }

    static void MergePlus(int[] array, int[] leftArray, int[] rightArray, SortCounter counter)
    {
        int i = 0, j = 0, k = 0;

        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k] = leftArray[i];
                i++;
                counter.Iterations++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
                counter.Iterations++;
            }
            k++;
        }

        while (i < leftArray.Length)
        {
            array[k] = leftArray[i];
            i++;
            k++;
            counter.Iterations++;
        }

        while (j < rightArray.Length)
        {
            array[k] = rightArray[j];
            j++;
            k++;
            counter.Iterations++;
        }
        counter.Iterations++;
    }
}

public static class Quick
{
    public static int [] QuickSorting(int[] array,SortCounter counter)
    {
        quickSortingAgorithm(array, 0, array.Length - 1,counter);
        return array;
    }
    static void quickSortingAgorithm(int[] array, int low, int high,SortCounter counter)
    {
        if(low < high)
        {
            int partitionIndex = Partition(array, low, high, counter);

            quickSortingAgorithm(array, low, partitionIndex -1, counter);
            quickSortingAgorithm(array, partitionIndex + 1, high, counter);
            counter.Iterations++;
        }
        counter.Iterations++;
    }
    static int Partition(int[] array, int low, int high, SortCounter counter)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                Swap(array, i, j, counter);
            }
            counter.Iterations++;
        }

        Swap(array, i + 1, high, counter);
        return i + 1;
    }
    static void Swap(int[] array, int i, int j, SortCounter counter)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
        counter.Iterations++;
    }
}

public static class Heap
{
    public static int [] HeapSorting(int[] array, SortCounter counter)
    {
        heapSortingAlgorith(array, counter);
        return array;
    }

    static void heapSortingAlgorith(int[] array, SortCounter counter)
    {
        int n = array.Length;

        // Построение начальной кучи (отсортированной по возрастанию)
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(array,n ,i, counter);
            counter.Iterations++;
        }
        counter.Stages++;
        // Постепенно извлекаем элементы из кучи
        for(int i = n - 1; i >= 0; i--)
        { 
            // Перемещаем текущий корень в конец массива
            int temp = array[0];
            array[0] = array[i];
            array[i] = temp;

            counter.Iterations++;
            // Вызываем Heapify на уменьшенной куче
            Heapify(array,i ,0, counter);
        }
        counter.Stages++;
    }
    static void Heapify(int[] array, int n, int rootIndex, SortCounter counter)
    {
        int largest = rootIndex;
        int leftChild = 2 * rootIndex + 1;
        int rightChild = 2 * rootIndex + 2;

        // Если левый потомок больше корня
        if(leftChild < n && array[leftChild] > array[largest])
        {
            largest = leftChild;
            counter.Iterations++;
        }
        
        // Если правый потомок больше, чем самый большой элемент на данный момент
        if(rightChild < n && array[rightChild] > array[largest])
        {
            largest = rightChild;
            counter.Iterations++;
        }

        // Если самый большой элемент не корень
        if(largest != rootIndex)
        {
            int swap = array[rootIndex];
            array[rootIndex] = array[largest];
            array[largest] = swap;

            counter.Iterations++;
            // Рекурсивно вызываем Heapify для поддерева
            Heapify(array, n, largest,counter);
        }
        counter.Stages++;
    }
}

public static class Counting
{
    public static int[] CoutingSorting(int[] array, SortCounter counter)
    {
        int max = Sorting.FindMaxValue(array);

        // step 1: Подсчёт вхождений 
        int[] countArray = new int[max + 1];
        foreach(int num in array)
        {
            countArray[num]++;
            counter.Iterations++;
        }
        counter.Stages++;

        // step 2: Обновление массива посчета
        for(int i = 1; i < countArray.Length; i++)
        {
            countArray[i] += countArray[i - 1];
            counter.Iterations++;
        }
        counter.Stages++;

        // step 3: Создание отсортированного массива 
        int[] sortedArray = new int[array.Length];
        for(int i = array.Length - 1; i >= 0; i--)
        {
            int num = array[i];
            int index = countArray[num] - 1;
            sortedArray[index] = num;
            countArray[num]--;
            counter.Iterations++;
        }
        counter.Stages++;
        return sortedArray;
    }
}

public static class Radix
{
    public static int [] RadixSorting(int[] array, SortCounter counter)
    {
        int max = Sorting.FindMaxValue(array);
        
        // Pass thriugn all digits
        for(int exp = 1; max / exp > 0; exp *= 10)
        {
            counter.Iterations++;
            CountingSortByDigit(array, exp,counter);
        }
        counter.Stages++;
        return array;
    }
    private static void CountingSortByDigit(int[] array, int exp, SortCounter counter)
    {
        
        int n = array.Length;
        int[] output = new int[n];
        int[] count = new int[10];

        // Step 1: Counting Occurrences
        for (int i = 0; i < n; i++)
        {
            count[(array[i] / exp) % 10]++;
            counter.Iterations++;
        }
        counter.Stages++;

        // Step 2: Update the count array
        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
            counter.Iterations++;
        }
        counter.Stages++;

        // Step 3: Create sorted array
        for (int i = n - 1; i >= 0; i--)
        {
            output[count[(array[i] / exp) % 10] - 1] = array[i];
            count[(array[i] / exp) % 10]--;
            counter.Iterations++;
        }
        counter.Stages++;

        // Step 4: Copyind a sorted array back to the original one
        for (int i = 0; i < n; i++)
        {
            array[i] = output[i];
            counter.Iterations++;
        }
        counter.Stages++;
    }
}

public static class Shell
{
    public static int [] ShellSorting(int[] array, SortCounter counter)
    {
        int n = array.Length;

        // Выбираем начальный интервал (h)
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            // Применяем сортировку вставками для каждого интервала
            for (int i = gap; i < n; i++)
            {
                int temp = array[i];
                int j;

                // Сортировка вставками в подмассиве с текущим интервалом
                for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                {
                    array[j] = array[j - gap];
                    counter.Iterations++;
                }

                array[j] = temp;
                counter.Iterations++;
            }
            counter.Iterations++;
        }
        counter.Stages++;
        return array;
    }
}

public static class Shaker
{
    public static int [] ShakerSorting(int[] array, SortCounter counter )
    {
        bool swapped;
        int start = 0;
        int end = array.Length - 1;

        do
        {
            // Left to right  [1] -> [10]
            swapped = false;
            for (int i = start; i < end; i++)
            {
                if (array[i] > array[i + 1])
                {
                    // Swap element 
                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    swapped = true;
                    counter.Iterations++;
                }
                counter.Iterations++;
            }
            counter.Stages++;

            if (!swapped)
                break;

            // Decrease range
            end--;

            // Pass from right to left
            swapped = false;
            for (int i = end - 1; i >= start; i--)
            {
                if (array[i] > array[i + 1])
                {
                    // Exchange elements
                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    swapped = true;
                }
                counter.Iterations++;
            }

            // Increase range
            start++;
            counter.Stages++;
        } while (swapped);
        return array;
    }
}

public static class Tim
{
    private const int RUN = 32;

    public static int [] TimSorting(int[] array,SortCounter counter)
    {
        int n = array.Length;
        // Apply insertion sort to each RUN
        for(int i = 0; i < n; i += RUN)
        {
            insertionSort(array, i, Math.Min((i + RUN - 1), (n - 1)), counter);
            counter.Stages++;
        }

        // Apply merge sort to sorted RUNs
        for(int size = RUN; size <n; size = 2* size)
        {
            for(int left = 0; left < n;left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min((left + 2 * size - 1), (n - 1));

                Merge(array, left, mid, right, counter);
                counter.Stages++;
            }
        }
        return array;
    }

    // Helper function for merging two sorted subarrays
    private static void Merge(int[] array, int left, int mid, int right, SortCounter counter)
    {
        int len1 = mid - left + 1;
        int len2 = right - mid;

        int[] leftArray = new int[len1];
        int[] rightArray = new int[len2];

        Array.Copy(array, left, leftArray, 0, len1);
        Array.Copy(array, mid + 1, rightArray, 0, len2);

        int i = 0, j = 0, k = left;

        while (i < len1 && j < len2)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k++] = leftArray[i++];
            }
            else
            {
                array[k++] = rightArray[j++];
            }
            counter.Iterations++;
        }
        
        while (i < len1)
        {
            array[k++] = leftArray[i++];
            counter.Iterations++;
        }
        
        while (j < len2)
        {
            array[k++] = rightArray[j++];
            counter.Iterations++;
        }
    }

    // Helper function for insertion sort
    private static void insertionSort(int[] array, int left, int right, SortCounter counter)
    {
        for(int i = left + 1; i <= right; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= left && array[j] > key)
            {
                array[j +1] = array[j];
                j--;
                counter.Iterations++;
            }

            array[j + 1] = key;
            counter.Iterations++;
        }
    }
}

