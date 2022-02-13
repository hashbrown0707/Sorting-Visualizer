using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Visualizer
{
    public enum SortingType
    {
        Bubble,
        Insertion,
        Quick
    }

    public interface ISortingVisualizer
    {
        void ExecuteSort(int[] arr, Graphics graphics, int maxHeight);
    }

    public class BubbleSort : ISortingVisualizer
    {
        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            if (arr.IsSorted())
                return;

            for (int i = 0; i < arr.Length - 1; ++i)
                for (int j = 0; j < arr.Length - i - 1; ++j)
                    if (arr[j] > arr[j + 1])
                    {
                        arr.Swap(j, j + 1);
                        graphics.DrawBar(j, arr[j], maxHeight);
                        graphics.DrawBar(j + 1, arr[j + 1], maxHeight);
                    }


        }
    }

    public class QuickSort : ISortingVisualizer
    {

        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            for (int i = 0; i < arr.Length; ++i)
                for (int j = i + 1; j < arr.Length; ++j)
                    if (arr[i] > arr[j])
                    {
                        arr.Swap(i, j);
                        graphics.DrawBar(i, arr[i], maxHeight);
                        graphics.DrawBar(j, arr[j], maxHeight);
                    }
        }

    }
}

public static class Utility
{
    public static void Swap(this int[] arr, int index1, int index2)
    {
        int temp = arr[index1];
        arr[index1] = arr[index2];
        arr[index2] = temp;
    }

    public static bool IsSorted(this int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; ++i)
            if (arr[i] > arr[i + 1])
                return false;

        return true;
    }

    public static void DrawBar(this Graphics graphics, int position, int height, int maxHeight)
    {
        graphics.FillRectangle(Brushes.Black, position, 0, 1, maxHeight);
        graphics.FillRectangle(Brushes.White, position, maxHeight - height, 1, height);
    }
}
