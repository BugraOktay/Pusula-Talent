using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class MaxIncreasingSubArraySolution
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }

        List<int> maxSubarray = new List<int>();
        List<int> currentSubarray = new List<int> { numbers[0] };
        int maxSum = numbers[0];
        int currentSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            // Eğer current element öncekinden büyükse, mevcut alt diziye ekle
            if (numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                // Yeni alt dizi başla
                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }

            // Eğer mevcut alt dizinin toplamı max toplamdan büyükse güncelle
            if (currentSum > maxSum)
            {
                maxSum = currentSum;
                maxSubarray = new List<int>(currentSubarray);
            }
        }

        return JsonSerializer.Serialize(maxSubarray);
    }
}