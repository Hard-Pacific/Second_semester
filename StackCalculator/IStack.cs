namespace StackCalculator;

/// <summary>
/// Interface to work with stack.
/// </summary>
public interface IStack
{
    /// <summary>
    /// Gets the size of stack.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Method adds the element in stack.
    /// </summary>
    /// <param name="value">Element that you want to push.</param>
    public void Push(float value);

    /// <summary>
    /// Method gets the element from stack.
    /// </summary>
    /// <returns>value from stack. Also true if stack is not empty and pop was correct. Else false.</returns>
    public (float ReturningValue, bool IsStackNotEmpty) Pop();

    /// <summary>
    /// Method view the last stack element, but don't pop it.
    /// </summary>
    /// <returns>value from stack. Also true if stack is not empty. Else false.</returns>
    public (float ReturningValue, bool IsStackNotEmpty) Top();

    /// <summary>
    /// Method checks if stack is empty.
    /// </summary>
    /// <returns>true if stack is empty. Else false.</returns>
    public bool IsEmpty();
}
