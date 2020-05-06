using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A4;

namespace A4
{
    class DemoClass : IComparable
    {
        //Variable declaration
        public string name;
        public int priority;

        //Constructor
        public DemoClass()
        {
            name = "";
            priority = 0;
        }

        public int CompareTo(Object obj)
        {   //CompareTo
            DemoClass sample = (DemoClass)obj;
            if (priority < sample.priority) return -1;
            else if (priority > sample.priority) return 1;
            else return 0; // value == sample.value
        }
        
        //Overriding virtual print method 
        public override string ToString()
        {
            return name + " " + priority + " ";
        }
    }

    class BinaryHeap<T> : IEnumerable where T : IComparable
    {
        private T[] array;
        private int count; // Count is initialised in the constructor (below) and incremented in Additem
        public BinaryHeap(int size)
        
        {
            array = new T[size];
            count = 0;
        }
        
        // Get Item should really be private but is public for demonstration  purposes
        public T GetItem(int index)
        {
            return array[index];
        }
        
        private void SetItem(int index, T value)
        {
            while (index >= array.Length) 
            Grow(array.Length * 2);
            array[index] = value;
        }
        
        private void Grow(int newsize)
        {
            Array.Resize(ref array, newsize);
        }

        // Indices of left and right children
        // "Has" methods to determine if the indices exist
        private int LeftChildIndex(int pos) { return 2 * pos + 1; }
        private int RightChildIndex(int pos) { return 2 * pos + 2; }
        private int GetParentIndex(int pos) => (pos - 1) / 2;

        private T GetRightChild(int pos) => array[RightChildIndex(pos)];
        private T GetLeftChild(int pos) => array[LeftChildIndex(pos)];
        private T GetParent(int pos) => array[GetParentIndex(pos)];
        private bool HasLeftChild(int pos)
        {
            if (LeftChildIndex(pos) < count)
                return true;
            else
                return false;
        }
        private bool HasRightChild(int pos)
        {
            if (RightChildIndex(pos) < count)
                return true;
            else
                return false;
        }
        
        private bool IsRoot(int pos) => pos == 0; // true if element is a root
        private void Swap(int position1, int position2)
        {
            T first = array[position1];
            array[position1] = array[position2];
            array[position2] = first;
        }

        // Prints all of the elements in the array in order
        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < array.Length; index++)
            {
                // Yield each element
                yield return array[index];
            }
        }
        
        public void AddItem(T value)
        {
            if (count == array.Length)
                Grow(array.Length * 2);
            array[count] = value;
            count++;
            ReCalculateUp();
        }

        //ExtractRoot (which is the same as extract min in our case)
        public T ExtractHead() // (This could also be called 'pop')
        {
            // making sure the heap isn't empty, 
            if (count <= 0) 
            {
                System.Console.WriteLine("Tried to extract from an empty heap");
                return default(T);
            }

            // this should get the head
            T head = array[0];
            array[0] = array[count - 1];
            array[count - 1] = default(T);
            count--;
            ReCalculateDown();
            return head;
        }

        private void ReCalculateDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = LeftChildIndex(index);
                if (HasRightChild(index) && (GetRightChild(index).CompareTo(GetLeftChild(index)) > 0)) 
                {
                    smallerIndex = RightChildIndex(index);
                }

                // Changed from min to max heap.
                if (array[smallerIndex].CompareTo(array[index]) < 0) //If array[smallerindex] <= array[index]

                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp()
        {
            //get the index of the last item
            int index = count - 1;

            //loop through the list, comparing the child to the parent.
            while (GetParent(index).CompareTo(array[index]) < 0) // switched this, changing from min to max heap
            {
                //if the parent is less than the child, we are swapping them 
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
                if (index == 0) { break; }
            }

        }

        public ref T peak()
        {
            return ref array[0];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Initialise a heap to some size
            int sizeOfHeap = 10;
            BinaryHeap<DemoClass> sampleHeap;
            sampleHeap = new BinaryHeap<DemoClass>(sizeOfHeap);
            Random rand = new Random();
            //Populate a heap
            for (int i = 0; i < sizeOfHeap; i++)
            {
                DemoClass A = new DemoClass();
                A.name = "Object #" + i;
                A.priority = rand.Next(0, 100);
                sampleHeap.AddItem(A);
            }

            //Printing each  element in the heap
            Console.WriteLine(sampleHeap.peak().ToString() + " <- This is the peak");
            sampleHeap.peak().priority--;
            Console.WriteLine(sampleHeap.peak().ToString() + " <- This is the peak\n\n\n");

            foreach (DemoClass i in sampleHeap)
            {
                Console.WriteLine(i.ToString() + " ");
            }
            Console.WriteLine("\n Next we pop off the top element, a couple of times to illustrate what's happening");
            Console.WriteLine(sampleHeap.ExtractHead().ToString());
            Console.WriteLine(sampleHeap.ExtractHead().ToString());

            Console.WriteLine("The remaining Heap after two elements were removed");
            foreach (DemoClass i in sampleHeap)
            {
                if (i != null)
                    Console.WriteLine(i.ToString() + " ");
            }

        }
    }
}
