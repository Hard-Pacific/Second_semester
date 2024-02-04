namespace MapFilterFoldTests;

using static MapFilterFold.MapFilterFold;

public class Tests
{
    [Test]
    public void Map_Int_Int()
    {
        var expected = new List<int>() { 2, 4, 6 };

        var actual = Map<int, int>(new List<int>() { 1, 2, 3 }, x => x * 2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Map_String_Int()
    {
        var expected = new List<int>() { 3, 5 };

        var actual = Map<string, int>(new List<string>() { "Bob", "Alice" }, elem => elem.Length);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Map_String_String()
    {
        var expected = new List<string>() { "Bob!", "Alice!" };

        var actual = Map<string, string>(new List<string>() { "Bob", "Alice" }, elem => elem + "!");

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Filter_Int_Int()
    {
        var expected = new List<int>() { 2, 4, 6 };

        var actual = Filter<int>(new List<int>() { 1, 2, 3, 4, 5, 6 }, x => x % 2 == 0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Filter_String_String()
    {
        var expected = new List<string>() { "Bob" };

        var actual = Filter<string>(new List<string>() { "Bob", "Alice" }, elem => elem.Length < 4);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Fold_IntAndInt_Int()
    {
        var expected = 6;

        var actual = Fold<int, int>(new List<int>() { 1, 2, 3 }, 1, (acc, x) => acc * x);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Fold_StringAndInt_Int()
    {
        var expected = 8;

        var actual = Fold<string, int>(new List<string>() { "Bob", "Alice" }, 0, (acc, elem) => acc + elem.Length);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Fold_EmptyList_AccumulatorHasNotChanged()
    {
        var expected = 0;

        var actual = Fold<string, int>(new List<string>() { }, 0, (acc, elem) => acc + elem.Length);

        Assert.That(actual, Is.EqualTo(expected));
    }
}