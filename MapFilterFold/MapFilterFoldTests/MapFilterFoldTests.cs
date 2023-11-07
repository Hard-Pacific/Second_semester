using NUnit.Framework;
using System.Collections.Generic;
using MapFilterFold;

namespace MapFilterFoldTests;

public class MapFilterFoldTests
{
    private readonly List<string> stringList = new(){ "a", "b", "c", "aa", "ab" };
    private readonly List<int> intList = new(){ 1, 2, 3, 4, 5, 6 };

    [Test]
    public void StandartMapTest()
    {
        var resultIntList = MapFilterFoldFunctions<int>.Map(intList, x => x * 2);
        Assert.AreEqual(new List<int> { 2, 4, 6, 8, 10, 12 }, resultIntList);
        var resultStringList = MapFilterFoldFunctions<string>.Map(stringList, x => x += "k");
        Assert.AreEqual(new List<string> { "ak", "bk", "ck", "aak", "abk" }, resultStringList);
    }

    [Test]
    public void StandartFilterTest()
    {
        var resultIntList = MapFilterFoldFunctions<int>.Filter(intList, x => x % 2 == 0);
        Assert.AreEqual(new List<int> { 2, 4, 6 }, resultIntList);
        var resultStringList = MapFilterFoldFunctions<string>.Filter(stringList, x => string.Equals(x, "a"));
        Assert.AreEqual(new List<string> { "a" }, resultStringList);
    }

    [Test]
    public void StandartFoldTest()
    {
        var resultValue = 0;
        var resultString = string.Empty;
        resultValue = MapFilterFoldFunctions<int>.Fold(intList, 1, (acc, elem) => acc * elem);
        Assert.AreEqual(720, resultValue);
        resultString = MapFilterFoldFunctions<string>.Fold(stringList, resultString, (acc, elem) => acc += elem);
        Assert.AreEqual("abcaaab", resultString);
    }
}