using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sorting_Visualizer
{
    public partial class Form1 : Form
    {
        private int[] array;
        private ISortingVisualizer sorting;
        private Graphics graphics;
        private Random random;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            random = new Random();
            graphics = panel1.CreateGraphics();
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            int capacity = panel1.Width;
            int maxHeight = panel1.Height;
            array = new int[capacity];

            for (int i = 0; i < capacity; ++i)
            {
                array[i] = random.Next(maxHeight);
                graphics.FillRectangle(Brushes.White, i, maxHeight - array[i], 1, array[i]);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            UpdateState(ref sorting, (SortingType)comboBox1.SelectedIndex);
            sorting.ExecuteSort(array, graphics, panel1.Height);

            if (Utility.IsSorted(array))
                label1.Text = "yes";
            else
                label1.Text = "no";

        }

        private void UpdateState(ref ISortingVisualizer obj, SortingType state)
        {
            switch (state)
            {
                case SortingType.Bubble:
                    obj = new BubbleSort();
                    break;
                case SortingType.Selection:
                    obj = new SelectionSort();
                    break;
                case SortingType.Quick:
                    obj = new QuickSort();
                    break;
                default:
                    throw new ArgumentNullException("Combobox can't be null.");
            }
        }
    }
}
