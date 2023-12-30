using System.Collections;

namespace SkipList;

public class SkipList<T> : IList<T> where T : IComparable<T>
{
    private class Node
    {
        public Node(T value, Node[] nextNodes)
        {
            Value = value;
            NextNodes = nextNodes;
        }
        
        public T Value { get; }
        public Node[] NextNodes { get; set; }
    }

    private int _currentListVersion;
    private Node _header;
    private readonly Node _tail;
    
    public SkipList()
    {
        _tail = new Node(default, Array.Empty<Node>());
        _header = new Node(default, new Node[] { _tail });
    }
    
    public int Count { get; private set; }

    private Node[] GetPreviousNodes(T value)
    {
        var currentNode = _header;
        var lastNodeOnCurrentLevel = new Node[_header.NextNodes.Length];

        for (int currentLevel = _header.NextNodes.Length - 1; currentLevel >= 0; currentLevel--)
        {
            var compareResult = value.CompareTo(currentNode.NextNodes[currentLevel].Value);
            while (compareResult == 1 && currentNode.NextNodes[currentLevel] != _tail)
            {
                currentNode = currentNode.NextNodes[currentLevel];
                compareResult = value.CompareTo(currentNode.NextNodes[currentLevel].Value);
            }

            lastNodeOnCurrentLevel[currentLevel] = currentNode;
        }

        return lastNodeOnCurrentLevel;
    }
    ///<summary>
    /// The GetRandomNodeHeight() method is used to generate a random node height.
    /// Nodes have different heights for the distribution of elements in the Skip List.
    ///</summary>
    private int GetRandomNodeHeight()
    {
        var random = new Random();
        int currentHeight = 1;
        while (currentHeight <= _header.NextNodes.Length)
        {
            if (random.Next(0, 2) == 0)
            {
                break;
            }

            currentHeight++;
        }

        return currentHeight;
    }
    
    public bool Contains(T value)
    {
        Node[] previousNodes = GetPreviousNodes(value);
        return previousNodes[0].NextNodes[0] != _tail && value.CompareTo(previousNodes[0].NextNodes[0].Value) == 0;
    }
    ///<summary>
    /// The Add() method adds a value to the Skip List.
    /// The previous nodes are obtained using the Get Previous Node s() method.
    /// A new node with the appropriate height is created and links to the following nodes are set.
    /// The node is inserted between the previous nodes and a new _header value is set if the new height exceeds the current height of _header.
    /// The number of Count elements is incremented and the _currentListVersion is updated.
    ///</summary>
    public void Add(T value)
    {
        Node[] previousNodes = GetPreviousNodes(value);
        int newNodeHeight = GetRandomNodeHeight();
        var newNode = new Node(value, new Node[newNodeHeight]);
        for (int i = 0; i < newNodeHeight && i < _header.NextNodes.Length; i++)
        {
            newNode.NextNodes[i] = previousNodes[i].NextNodes[i];
            previousNodes[i].NextNodes[i] = newNode;
        }

        if (newNodeHeight > _header.NextNodes.Length)
        {
            Node newHeader = new Node(default, new Node[newNodeHeight]);
            _header.NextNodes.CopyTo(newHeader.NextNodes, 0);
            newHeader.NextNodes[newNodeHeight - 1] = newNode;
            _header = newHeader;
            newNode.NextNodes[newNodeHeight - 1] = _tail;
        }

        Count++;
        _currentListVersion++;
    }

    public void Clear()
    {
        _header.NextNodes = new Node[1]{ _tail };
        Count = 0;
        _currentListVersion++;
    }
    ///<summary>
    /// The Remove() method removes the specified value from the Skip List.
    /// The previous nodes are obtained using the Get Previous Node s() method.
    /// A check is performed if nextNode (the next node of the first level) is not equal to _tail and the value matches the passed value.
    /// Then a new reference to the next nodes is set and the number of Count and _currentListVersion elements is updated.
    /// Returns true if deletion succeeded, otherwise false.
    ///</summary>
    public bool Remove(T value)
    {
        var previousNodes = GetPreviousNodes(value);
        var nextNode = previousNodes[0].NextNodes[0];
        
        if (nextNode != _tail && value.CompareTo(nextNode.Value) == 0)
        {
            for (int i = 0; i < nextNode.NextNodes.Length; ++i)
            {
                previousNodes[i].NextNodes[i] = nextNode.NextNodes[i];
            }

            Count--;
            _currentListVersion++;
            return true;
        }

        return false;
    }
    ///<summary>
    /// The CopyTo() method copies Skip List elements to the specified array starting from the specified index.
    /// Checks for exceptions: ArgumentNullException, ArgumentException.
    ///</summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException();
        }
        
        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("There is not enough space in the array to fit the entire list.");
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentException("Index is less than zero.");
        }

        if (arrayIndex >= array.Length)
        {
            throw new ArgumentException("Index is out of range of the size of the array.");
        }

        var currentNode = _header.NextNodes[0];
        for (var currentIndex = arrayIndex; currentNode != _tail; currentIndex++)
        {
            array[currentIndex] = currentNode.Value;
            currentNode = currentNode.NextNodes[0];
        }
    }

    public void Insert(int index, T item)
    {
        throw new NotSupportedException("You cannot insert elements by index in a sorted list.");
    }

    public bool IsReadOnly => false;

    public T this[int index]
    {
        get
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var currentNode = _header.NextNodes[0];
            for (var i = 0; i < index; i++)
            {
                currentNode = currentNode.NextNodes[0];
            }

            return currentNode.Value;
        }
        
        set => throw new NotSupportedException();
    }

    public int IndexOf(T value)
    {
        var currentNode = _header.NextNodes[0];
        
        for (int i = 0; i < Count; i++)
        {
            if (value.CompareTo(currentNode.Value) == 0)
            {
                return i;
            }

            currentNode = currentNode.NextNodes[0];
        }

        return -1;
    }
    
    public void RemoveAt(int index)
    {
        Remove(this[index]);
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return new SkipListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class SkipListEnumerator : IEnumerator<T>
    {
        private int enumeratorListVersion;
        private Node currentNode;
        private SkipList<T> skipList;

        public SkipListEnumerator(SkipList<T> skipList)
        {
            this.skipList = skipList;
            enumeratorListVersion = this.skipList._currentListVersion;
            currentNode = skipList._header;
        }
        ///<summary>
        /// A method that moves the enumerator to the next element in the data structure.
        /// The enumerator moves through the list, checks the version of the list and the current position.
        /// If the enumerator reaches the end of the list, the method returns false.
        /// Otherwise, the enumerator moves to the next node and returns true.
        ///</summary>
        public bool MoveNext()
        {
            if (enumeratorListVersion != skipList._currentListVersion)
            {
                throw new InvalidOperationException("List was changed after creation the latest enumerator.");
            }

            if (currentNode == skipList._tail || currentNode.NextNodes[0] == skipList._tail)
            {
                return false;
            }

            currentNode = currentNode.NextNodes[0];
            return true;
        }
        ///<summary>
        /// A method that resets the enumerator to its initial state.
        /// The enumerator checks the version of the list and moves the current node to the header node.
        ///</summary>
        public void Reset()
        {
            if (enumeratorListVersion != skipList._currentListVersion)
            {
                throw new InvalidOperationException("List was changed after creation the latest enumerator.");
            }

            currentNode = skipList._header;
        }

        public T Current
        {
            get
            {
                if (currentNode == skipList._header)
                {
                    throw new InvalidOperationException("Do MoveNext firstly.");
                }

                return currentNode.Value;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}
