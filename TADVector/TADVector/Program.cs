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
    public void join(vector v2)
    {
        for (int point = 0; point < v2.getLength(); ++point)
        {
            add(v2.elements[point]);
        }
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
}