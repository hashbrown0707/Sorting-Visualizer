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
        Selection,
        Insertion,
        Shell,
        Quick,
        Merge
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

    public class SelectionSort : ISortingVisualizer
    {
        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            if (arr.IsSorted())
                return;

            int size = arr.Length;

            for (int i = 0; i < size; ++i)
                for (int j = i + 1; j < size; ++j)
                    if (arr[i] > arr[j])
                    {
                        arr.Swap(i, j);
                        graphics.DrawBar(i, arr[i], maxHeight);
                        graphics.DrawBar(j, arr[j], maxHeight);
                    }
        }
    }

    public class InsertionSort : ISortingVisualizer
    {
        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            if (arr.IsSorted())
                return;

            for (int i = 1; i < arr.Length; ++i)
            {
                for (int j = i; j > 0; --j)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        arr.Swap(j, j - 1);
                        graphics.DrawBar(j, arr[j], maxHeight);
                        graphics.DrawBar(j - 1, arr[j - 1], maxHeight);
                    }
                    else
                        break;
                }
            }
        }
    }

    public class ShellSort : ISortingVisualizer
    {
        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            if (arr.IsSorted())
                return;

            for (int gap = arr.Length / 2; gap > 0; gap /= 2)    //算gap
            {
                for (int i = 0; i < gap; ++i)                                       //for每個gap組成的集合的開頭
                {
                    for (int j = i; j < arr.Length; j += gap)               //抓出那個gap集合的所有數字
                    {
                        for (int k = j - gap; k >= i; k -= gap)             //進行InsertionSort
                        {
                            if (arr[k] > arr[k + gap])
                            {
                                arr.Swap(k, k + gap);
                                graphics.DrawBar(k, arr[k], maxHeight);
                                graphics.DrawBar(k + gap, arr[k + gap], maxHeight);
                            }
                            else
                                break;
                        }
                    }
                }
            }
        }
    }

    public class QuickSort : ISortingVisualizer
    {
        private Graphics g;
        private int maxHeight;

        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            if (arr.IsSorted())
                return;

            g = graphics;
            this.maxHeight = maxHeight;

            quicksort(arr, 0, arr.Length - 1);
        }

        private void quicksort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int pivot = Patition(arr, start, end);
                quicksort(arr, start, pivot -  1);
                quicksort(arr, pivot + 1, end);
            }
        }

        private int Patition(int[] arr, int start, int end)
        {
            int pivot = end;
            int index = start - 1, current = start;

            for (; current < end; ++current)
            {
                if(arr[current] < arr[pivot])
                {
                    ++index;
                    arr.Swap(index, current);
                    g.DrawBar(current, arr[current], maxHeight);
                    g.DrawBar(index, arr[index], maxHeight);
                }
            }

            ++index;
            arr.Swap(index, pivot);
            g.DrawBar(pivot, arr[pivot], maxHeight);
            g.DrawBar(index, arr[index], maxHeight);
            return index;
        }
    }

    public class MergeSort : ISortingVisualizer
    {
        public void ExecuteSort(int[] arr, Graphics graphics, int maxHeight)
        {
            if (arr.IsSorted())
                return;

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
