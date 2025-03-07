using System.Globalization;
using System.Net.WebSockets;
using System.Numerics;

public class main
{
    public static void Main(string[] args)
    {
    }
}

public class vector
{
    private const int max = 10000;
    private int length;
    private int[] elements;

    public vector()
    {
        length = 0;
        elements = new int[max];
    }

    public int getLength()
    {
        return length;
    }
    public void setLength(int length)
    {
        this.length = length;
    }
    public void getElement(int index)
    {
        if (index >= 0 && index < getLength())
        {
            Console.WriteLine(elements[index]);
        }
        else
        {
            Console.WriteLine("ERROR: Invalid Index");
        }
    }
    public void add(int element)
    {
        if (length <= max)
        {
            setLength(getLength() + 1);
            elements[getLength()-1] = element;
        }
        else
        {
            Console.WriteLine("ERROR: VectorOverflow");
        }
    }
    public void swap(int index_1, int index_2)
    {
        int temp = elements[index_1];
        elements[index_1] = elements[index_2];
        elements[index_2] = temp;
    }
    public void insert(int index, int element)
    {
        if (index >= 0 && index<=getLength())
        {
            if (index == getLength())
            {
                add(element);
            }
            else
            {
                insertElement(index, element, -1);
            }
        }
        else
        {
            Console.WriteLine("ERROR: Invalid Index");
        }

    }
    public void insertElement(int index, int element, int point)
    {
        if (point == -1)
        {
            add(element);
            insertElement(index, element, getLength() - 1);
        }
        else
        {
            if (point != index)
            {
                swap(point, point - 1);
                insertElement(index, element, point - 1);
            }
        }
    }
    public void remove(int index)
    {
        if (index >=0 && index < getLength())
        {
            removeElement(index);
        }
        else
        {
            Console.WriteLine("ERROR: Invalid Index");
        }
    }
    public void removeElement(int index)
    {
        if (index == getLength()-1)
        {
            setLength(getLength() - 1);
        }
        else
        {
            swap(index, index + 1);
            removeElement(index + 1);
        }
    }
    public bool compare(vector v2)
    {
        if (getLength() != v2.getLength())
        {
            return false;
        }
        else
        {
            for (int start = 0, end = getLength()-1;  start < end; ++start, --end)
            {
                if (elements[start] != v2.elements[start] || elements[end] != v2.elements[end])
                {
                    return false;
                }
            }
            return true;
        }
    }
    public void unique(int index)
    {
        for (int point = 0; point < getLength(); ++point)
        {
            if (elements[point] == elements[index] && point != index)
            {
                remove (point);
            }
        }
    }
    public void removeDuplicates()
    {
        for (int point = 0; point < getLength(); ++point)
        {
            unique(point);
        }
        
    }
    public void fullJoin(vector v2, int index=0)
    {
        for (int point = index; point < v2.getLength(); ++point)
        {
            add(v2.elements[point]);
        }
    }
    public void join(vector v2)
    {
        fullJoin(v2);
        removeDuplicates();
    }
    public vector createSubVector (int start, int end)
    {
        vector subvector = new vector();
        for (int point = start; point <= end; ++point)
        {
            subvector.add(elements[point]);
        }
        return subvector;
    }
    public bool subVector(vector v2)
    {
        if (getLength() > v2.getLength())
        {
            return false;
        }
        else
        {
            for (int point = 0; point < v2.getLength(); ++point)
            {
                if (v2.elements[point] == elements[0] && point+getLength()-1 < v2.getLength())
                {
                    if (compare(v2.createSubVector(point, point + getLength() - 1)) == true){
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public void print()
    {
        for (int point = 0; point < getLength(); ++point)
        {
            Console.Write(elements[point] + " ");
        }
    }
    public int minElement(int index = 0)
    {
        int min = index;
        for (int point = index+1; point < getLength(); ++point)
        {
            if (elements[point] < elements[min])
            {
                min = point;
            }
        }
        return min;
    }
    public void selectionSort()
    {
        for (int point = 0; point < getLength()-1; ++point)
        {
            swap(point, minElement(point)); 
        }
    }
    public void insertionSort()
    {
        for (int point = 1; point < getLength(); ++point)
        {
            int tempPoint = point;
            while (tempPoint > 0 && elements[tempPoint-1] > elements[tempPoint])
            {
                swap(tempPoint, tempPoint-1);
                --tempPoint;
            }
        }
    }
    public (vector left, vector right) split()
    {
        int half = (getLength()-1)/2;
        vector left = createSubVector(0, half);
        vector right = createSubVector(half+1, getLength()-1);
        return (left, right);
    }
    public vector joinSort(vector v2)
    {
        vector sort = new vector();
        int point_1 = 0, point_2 = 0;
        while (point_1 < getLength() && point_2 < v2.getLength())
        {
            if (elements[point_1] <= v2.elements[point_2])
            {
                sort.add(elements[point_1]);
                ++point_1;
            }
            else
            {
                sort.add(v2.elements[point_2]);
                ++point_2;
            }
        }
        sort.fullJoin(this, point_1);
        sort.fullJoin(v2, point_2);
        return sort;
    }
    public vector merge()
    {
        if (getLength() <= 1)
        {
            return this;
        }
        else
        { 
            (vector left, vector right) = split();
            left = left.merge();
            right = right.merge();
            return left.joinSort(right);
        }

    }
    public void mergeSort()
    {
        vector sort = merge();
        for (int point = 0; point<getLength(); ++point)
        {
            this.elements[point] = sort.elements[point];
        }
    }
}