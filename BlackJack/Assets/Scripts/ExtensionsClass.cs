using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static class ExtensionsClass {
	private static Random rng = new Random();

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static List<CardImage> SortAscending(List<CardImage> cards) {
		CardImage[] sorted = cards.ToArray();

		//Selection Sort
		for (int i = 0; i < sorted.Length - 1; i++) {
			int index = i;
			for (int j = i + 1; j < sorted.Length; j++) {
				if (sorted[j].GetData.number < sorted[index].GetData.number) {
					index = j;
				}
			}
			if (index != i) {
				CardImage temp = sorted[i];
				sorted[i] = sorted[index];
				sorted[index] = temp;
			}
		}

		return sorted.ToList();
	}

}