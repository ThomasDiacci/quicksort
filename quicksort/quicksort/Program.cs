using System.Diagnostics;
using System.Linq;

var data = new int[] {2, 5, 9, 12, 3, 0, 18, 1, -1};

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

Stopwatch stopwatch = new Stopwatch();

#region v1

stopwatch.Start();

Console.WriteLine($"\nData {nameof(QuickSort1)}: ");
var quicksorted = new int[data.Length];
quicksorted = QuickSort1(data);
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

#region v2

stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine($"\nData {nameof(QuickSort2)}: ");
QuickSort2(data, 0, data.Length - 1);
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

int[] QuickSort1(int[] data)
{
    //base case
    //perché un array con 0 o 1 elemento è già di per sè ordinata
    if (data.Length < 2)
    {
        return data;
    }

    //prendo l'elemento centrale come pivot, per cadere nel caso medio di O(n*log(n))
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

    return QuickSort1(subLesser.ToArray()).Concat(new int[] { pivot }).Concat(QuickSort1(subGreater.ToArray())).ToArray();
}

static void QuickSort2(int[] arr, int left, int right)
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
            QuickSort2(arr, left, pivot - 1);
        }
        if (pivot + 1 < right)
        {
            QuickSort2(arr, pivot + 1, right);
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
