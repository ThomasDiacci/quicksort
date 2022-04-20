using System.Diagnostics;
using System.Linq;

var data = new int[] {2, 5, 9, 12, 3, 0, 18, 1, -1, -11, 4, 7, 88, -15, 60, 100, 45, 23, -23, 17, 11, 13, -13, 25, -25, 77, -77, 36, -36, 43, -43};
//{2, 5, ... 45, ... 100, 60}
//RandomDataGenerator(){

//}

Console.WriteLine("Data not sorted: ");
for (int i = 0; i < data.Length; i++)
{
    if (i == 0)
    {
        Console.Write(data[i]);
        continue;
    }

    Console.Write(", " + data[i]);
}

Console.WriteLine();

//pausa per avere una performance temporale più accurata
Thread.Sleep(5000);

Stopwatch stopwatch = new Stopwatch();

#region sorting

stopwatch.Start();

Console.WriteLine($"\nData {nameof(SelectionSort)}: ");
var sorted = SelectionSort(data);
for (int i = 0; i < sorted.Length; i++)
{
    if (i == 0)
    {
        Console.Write(sorted[i]);
        continue;
    }

    Console.Write(", " + sorted[i]);
}

stopwatch.Stop();
Console.WriteLine("\nElapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

#endregion

#region quicksorting v1

stopwatch.Start();

Console.WriteLine($"\nData {nameof(QuickSort1)}: ");
var quicksorted = QuickSort1(data);
for (int i = 0; i < quicksorted.Length; i++)
{
    if (i == 0)
    {
        Console.Write(quicksorted[i]);
        continue;
    }

    Console.Write(", " + quicksorted[i]);
}

stopwatch.Stop();
Console.WriteLine("\nElapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

#endregion

#region orderby v2

stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine($"\nData Orderby Linq: ");
var linqsorted = data.ToList().OrderBy(x => x).ToArray();
for (int i = 0; i < linqsorted.Length; i++)
{
    if (i == 0)
    {
        Console.Write(linqsorted[i]);
        continue;
    }

    Console.Write(", " + linqsorted[i]);
}

stopwatch.Stop();
Console.WriteLine("\nElapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

#endregion

//NB: arriva già sorted dal metodo precedente

#region quicksorting v3

stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine($"\nData {nameof(QuickSort3)}: ");
QuickSort3(data, 0, data.Length - 1);
for (int i = 0; i < data.Length; i++)
{
    if (i == 0)
    {
        Console.Write(data[i]);
        continue;
    }

    Console.Write(", " + data[i]);
}

stopwatch.Stop();
Console.WriteLine("\nElapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

#endregion

#region methods

int[] SelectionSort(int[] arr)
{
    int[] newArr = new int[arr.Length];
    int cycles = arr.Length;

    for (int i = 0; i < cycles; i++)
    {
        int smallestIndex = FindSmallest(arr);
        newArr[i] = arr[smallestIndex];
        arr = arr.Except(new int[] { arr[smallestIndex] }).ToArray();
    }

    return newArr;
}

int FindSmallest(int[] arr)
{
    //ipotesi
    int smallestIndex = 0;

    for (int i = 1; i < arr.Length; i++)
    {
        if (arr[i] < arr[smallestIndex])
        {
            smallestIndex = i;
        }
    }

    return smallestIndex;
}

int[] QuickSort1(int[] data)
{
    //base case
    //perché un array con 0 o 1 elemento è già di per sè ordinata
    //{} {1}
    if (data.Length < 2)
    {
        return data;
    }

    //prendo l'elemento centrale come pivot, per cadere nel caso medio di O(n*log(n)) = O(n) * O(log(n))
    //NB: int arrotonda per difetto
    var pivot = data[data.Length / 2];

    //costruisco due sub-array che contengano rispettivamente i valori più bassi e più alti del pivot
    //la cui lunghezza concide con quella dell'array originaria, perché gli elementi potrebbero essere tutti minori o maggiori
    //a seconda del pivot selezionato

    //se minore
    var subLesser = new List<int>();//int[arr.Length];
    //var count = 0;
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i] < pivot)
        {
            subLesser.Add(data[i]);
            //count++;
        }
    }

    //se maggiore
    var subGreater = new List<int>();//[list.Length];
    //count = 0;
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i] > pivot)
        {
            subGreater.Add(data[i]);// [count] = list[i];
            //count++;
        }
    }

    //var lesser = QuicksortTest(subLesser);

    //var greater = QuicksortTest(subGreater);

    ////compongo il risultato
    //var allData = lesser;
    //allData.Add(pivot);
    //allData.Concat(greater);

    //return allData;
    //return QuickSort1(subLesser) + [pivot] + QuickSort1(subGreater)
    return QuickSort1(subLesser.ToArray()).Concat(new int[] { pivot }).Concat(QuickSort1(subGreater.ToArray())).ToArray();
}

static void QuickSort3(int[] arr, int left, int right)
{
    int pivot;
    //base case
    //in cui se "data" ha 0 o un solo elemento il metodo non elabora i valori
    //in quanto già ordinati
    if (left < right)
    {
        //recursion
        pivot = Partition(arr, left, right);
        //primo giro:
        //pivot = 2
        if (pivot > 1)
        {
            QuickSort3(arr, left, pivot - 1);
        }
        if (pivot + 1 < right)
        {
            QuickSort3(arr, pivot + 1, right);
        }
    }
}

static int Partition(int[] arr, int left, int right)
{
    int pivot;
    //prendo il primo elemento come "pivot"
    pivot = arr[left];
    while (true)
    {
        //primo giro:
        //2 < 2
        //esce subito
        while (arr[left] < pivot)
        {
            left++;
        }
        //primo giro:
        //-1 > 2
        while (arr[right] > pivot)
        {
            right--;
        }
        //primo giro:
        //0 < (9-1) = 8
        if (left < right)
        {
            //primo giro:
            //temp = -1
            int temp = arr[right];
            //primo giro:
            //2
            arr[right] = arr[left];
            //primo giro:
            //-1
            arr[left] = temp;
        }
        else
        {
            return right;
        }
    }
}

#endregion
