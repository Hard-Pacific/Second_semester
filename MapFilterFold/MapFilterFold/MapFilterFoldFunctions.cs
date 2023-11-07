namespace MapFilterFold;

/// <summary>
/// Class contains functions "Map", "Filter" and "Fold".
/// </summary>
/// <typeparam name="T">the type with which the functions will work.</typeparam>
public static class MapFilterFoldFunctions<T>
{
    /// <summary>
    /// Function creates a new list using list elements and a function for each list element.
    /// </summary>
    /// <param name="list">initial list for function.</param>
    /// <param name="functionForListElements">function which will be applied to list elements.</param>
    /// <returns>a new list.</returns>
    public static List<T> Map(List<T> list, Func<T, T> functionForListElements)
    {
        var resultList = new List<T>();
        foreach (var element in list)
        {
            resultList.Add(functionForListElements(element));
        }

        return resultList;
    }

    /// <summary>
    /// Function creates a new list using list elements and boolean function for each list element.
    /// </summary>
    /// <param name="list">initial list for function.</param>
    /// <param name="functionForListElements">boolean function which will be applied to list elements.</param>
    /// <returns>a new list.</returns>
    public static List<T> Filter(List<T> list, Func<T, bool> functionForListElements)
    {
        var resultList = new List<T>();
        foreach (var item in list)
        {
            if (functionForListElements(item))
            {
                resultList.Add(item);
            }
        }

        return resultList;
    }

    /// <summary>
    /// Function creates accumulated value using initialValue and list elements.
    /// </summary>
    /// <param name="list">initial list.</param>
    /// <param name="initialValue">initialValue.</param>
    /// <param name="functionForListElement">function which changes accumulated value.</param>
    /// <returns>accumulated value.</returns>
    public static T Fold(List<T> list, T initialValue, Func<T, T, T> functionForListElement)
    {
        T result = initialValue;
        foreach (var item in list)
        {
            result = functionForListElement(result, item);
        }

        return result;
    }
}
