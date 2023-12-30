namespace StackCalculator;

using System;

/// <summary>
/// Struct stack based on list.
/// </summary>
public class StackOnList : IStack
{
    private StackElement head = new();

    /// <summary>
    /// Gets the size of stack.
    /// </summary>
    public int Size
        => this.head.Size;

    /// <summary>
    /// Method adds the element in stack.
    /// </summary>
    /// <param name="value">Element that you want to push.</param>
    public void Push(float value)
    {
        StackElement newStackElement = new(value, this.head);
        this.head = newStackElement;
        ++this.head.Size;
    }

    /// <summary>
    /// Method gets the element from stack.
    /// </summary>
    /// <returns>value from stack. Also true if stack is not empty and pop was correct. Else false.</returns>
    public (float ReturningValue, bool IsStackNotEmpty) Pop()
    {
        if (this.head.NextElement == null)
        {
            return (0, false);
        }

        float value = this.head.Value;
        this.head = this.head.NextElement;
        --this.head.Size;
        return (value, true);
    }

    /// <summary>
    /// Method checks if stack is empty.
    /// </summary>
    /// <returns>true if stack is empty. Else false.</returns>
    public bool IsEmpty()
        => this.head.NextElement == null;

    /// <summary>
    /// Method view the last stack element, but don't pop it.
    /// </summary>
    /// <returns>value from stack. Also true if stack is not empty. Else false.</returns>
    public (float ReturningValue, bool IsStackNotEmpty) Top()
        => this.head.NextElement == null ? (0, false) : (this.head.Value, true);

    private class StackElement
    {
        public StackElement()
        {
        }

        public StackElement(float value, StackElement nextElement)
        {
            this.Value = value;
            this.NextElement = nextElement;
        }

        public float Value { get; set; }

        public StackElement? NextElement { get; set; }

        public int Size { get; set; }
    }
}