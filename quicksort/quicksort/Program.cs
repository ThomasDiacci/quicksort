var arr = new int[] {2, 5, 9, 12, 3, 0, 18, 1, -1};

Console.WriteLine("Array not sorted: ");
for (int i = 0; i < arr.Length; i++)
{
    if (i == 0)
    {
        Console.Write(arr[i]);
        continue;
    }

    Console.Write(", " + arr[i]);
}

Console.WriteLine("\nArray Quicksorted: ");

var arrayQuicksorted = new int[arr.Length];

QuickSort(arr, 0, arr.Length -1);

for (int i = 0; i < arr.Length; i++)
{
    if (i == 0)
    {
        Console.Write(arr[i]);
        continue;
    }

    Console.Write(", " + arr[i]);
}

void QuickSort(int[] arr, int start, int end)
{
    int i;
    if (start < end)
    {
        i = Partition(arr, start, end);

        QuickSort(arr, start, i - 1);
        QuickSort(arr, i + 1, end);
    }
}

int Partition(int[] arr, int start, int end)
{
    int temp;
    int p = arr[end];
    int i = start - 1;

    for (int j = start; j <= end - 1; j++)
    {
        if (arr[j] <= p)
        {
            i++;
            temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

    temp = arr[i + 1];
    arr[i + 1] = arr[end];
    arr[end] = temp;
    return i + 1;
}