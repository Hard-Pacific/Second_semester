namespace StackCalculator;

using System;

/// <summary>
/// Struct stack based on array.
/// </summary>
public class StackOnArray : IStack
{
    private float[] arrayWithElements;
    private int indexOfArray;

    /// <summary>
    /// Initializes a new instance of the <see cref="StackOnArray"/> class.
    /// </summary>
    public StackOnArray()
    {
        this.arrayWithElements = new float[10];
    }

    /// <summary>
    /// Gets the size of stack.
    /// </summary>
    public int Size
        => this.indexOfArray;

    /// <summary>
    /// Method adds the element in stack.
    /// </summary>
    /// <param name="value">Element that you want to push.</param>
    public void Push(float value)
    {
        if (this.indexOfArray == this.arrayWithElements.Length)
        {
            this.arrayWithElements = this.Resize(this.arrayWithElements.Length * 2);
        }

        this.arrayWithElements[this.indexOfArray] = value;
        ++this.indexOfArray;
    }

    /// <summary>
    /// Method gets the element from stack.
    /// </summary>
    /// <returns>value from stack. Also true if stack is not empty and pop was correct. Else false.</returns>
    public (float ReturningValue, bool IsStackNotEmpty) Pop()
    {
        if (this.indexOfArray == 0)
        {
            return (0, false);
        }

        float value = this.arrayWithElements[this.indexOfArray - 1];
        --this.indexOfArray;
        return (value, true);
    }

    /// <summary>
    /// Method checks if stack is empty.
    /// </summary>
    /// <returns>true if stack is empty. Else false.</returns>
    public bool IsEmpty()
        => this.indexOfArray == 0;

    /// <summary>
    /// Method view the last stack element, but don't pop it.
    /// </summary>
    /// <returns>value from stack. Also true if stack is not empty. Else false.</returns>
    public (float ReturningValue, bool IsStackNotEmpty) Top()
    {
        if (this.indexOfArray == 0)
        {
            return (0, false);
        }

        return (this.arrayWithElements[this.indexOfArray - 1], true);
    }

    private float[] Resize(int newLength)
    {
        var newArray = new float[newLength];
        for (int i = 0; i < this.arrayWithElements.Length; ++i)
        {
            newArray[i] = this.arrayWithElements[i];
        }

        return newArray;
    }
}
