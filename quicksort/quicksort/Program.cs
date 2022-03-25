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

Console.WriteLine("\nData Quicksorted: ");

//var quicksorted = new List<int>();//[list.Length];
//quicksorted = QuicksortTest(list);
//Console.WriteLine("\nData Quicksorted: ");
//for (int i = 0; i < quicksorted.Count; i++)
//{
//    if (i == 0)
//    {
//        Console.Write(quicksorted[i]);
//        continue;
//    }

//    Console.Write(", " + quicksorted[i]);
//}

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

List<int> QuicksortTest(List<int> list)
{
    //base case
    //perché un array con 0 o 1 elemento è già di per sè ordinata
    if (list.Count < 2)
    {
        return list;
    }

    //prendo l'elemento centrale come pivot, per cadere nel caso medio di O(n*log(n))
    //NB: int arrotonda per difetto
    var pivot = list[list.Count / 2];

    //costruisco due sub-array che contengano rispettivamente i valori più bassi e più alti del pivot
    //la cui lunghezza concide con quella dell'array originaria, perché gli elementi potrebbero essere tutti minori o maggiori
    //a seconda del pivot selezionato

    //se minore
    var subLesser = new List<int>();//int[arr.Length];
    //var count = 0;
    for (int i = 0; i < list.Count; i++)
    {
        if (list[i] < pivot)
        {
            subLesser.Add(list[i]);
            //count++;
        }
    }

    //se maggiore
    var subGreater = new List<int>();//[list.Length];
    //count = 0;
    for (int i = 0; i < list.Count; i++)
    {
        if (list[i] > pivot)
        {
            subGreater.Add(list[i]);// [count] = list[i];
            //count++;
        }
    }

    var lesser = QuicksortTest(subLesser);

    var greater = QuicksortTest(subGreater);

    //compongo il risultato
    var allData = lesser;
    allData.Add(pivot);
    allData.Concat(greater);

    return allData;
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
