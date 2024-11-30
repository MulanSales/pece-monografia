string CombineEvenNumbers(int[] numbers)
{
    string result = "";
    foreach (var num in numbers)
    {
        if (num % 2 == 0)
            result += num + ", ";
    }
    return result;
}
