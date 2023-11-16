namespace MapFilterFold;

/// <summary>
/// A static class that provides Map, Filter, and Fold functions for lists.
/// </summary>
public static class MapFilterFold
{
    /// <summary>
    /// Apply the function to each element of this list.
    /// </summary>
    /// <param name="list"> List of elements. </param>
    /// <param name="function"> Function to apply. </param>
    /// <typeparam name="TInp"> The type of elements in the list. </typeparam>
    /// <typeparam name="TOut"> The type of elements in the resulting list. </typeparam>
    public static List<TOut> Map<TInp, TOut>(List<TInp> list, Func<TInp, TOut> function)
    {
        var result = new List<TOut>();
        foreach (var i in list)
        {
            result.Add(function(i));
        }

        return result;
    }

    /// <summary>
    /// Getting elements from the list that satisfy the condition.
    /// </summary>
    /// <param name="list"> List of elements. </param>
    /// <param name="predicate"> Predicate for checking items from this list. </param>
    /// <typeparam name="T"> The type of elements in the list. </typeparam>
    public static List<T> Filter<T>(List<T> list, Func<T, bool> predicate)
    {
        var result = new List<T>();
        foreach (var i in list)
        {
            if (predicate(i))
            {
                result.Add(i);
            }
        }

        return result;
    }

    /// <summary>
    /// Accumulation of the original value using the accumulation function and the list that we are going through.
    /// </summary>
    /// <param name="list"> List of elements. </param>
    /// <param name="accumulator"> Initial value of accumulator. </param>
    /// <param name="function"> A function to apply to the accumulator and the next element from the list. </param>
    /// <typeparam name="TInp"> The type of elements in the list. </typeparam>
    /// <typeparam name="TOut"> Type of output element. </typeparam>
    public static TOut Fold<TInp, TOut>(List<TInp> list, TOut accumulator, Func<TOut, TInp, TOut> function)
    {
        foreach (var i in list)
        {
            accumulator = function(accumulator, i);
        }

        return accumulator;
    }
}