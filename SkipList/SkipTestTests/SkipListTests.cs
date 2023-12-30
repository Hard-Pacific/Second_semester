namespace SkipTestTests;
using SkipList;

public class Tests
{
    private SkipList<int> skipList;
    
    [SetUp]
    public void Setup()
    {
        skipList = new SkipList<int>();
    }

    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 7)]
    [TestCase(new [] {0, 10, 12}, 3)]
    [TestCase(new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, 12)]
    [TestCase(new int[0], 0)]
    public void CorrectWorkOfCountProperty(int[] elementsToAdd, int expectedResult)
    {
        foreach (var element in elementsToAdd)
        {
            skipList.Add(element);
        }

        Assert.That(skipList.Count, Is.EqualTo(expectedResult));
    }

    [Test]
    public void CountPropertyShouldBeZeroAfterClean()
    {
        int[] elementsToAdd = new[] { 1, 45, 32, 9, 80 };
        foreach (var element in elementsToAdd)
        {
            skipList.Add(element);
        }
        
        skipList.Clear();
        Assert.That(skipList.Count, Is.EqualTo(0));
    }

    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 44, true)]
    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 9, true)]
    [TestCase(new [] {-1, -4, 13}, -1, true)]
    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 7, false)]
    [TestCase(new int[0], 13, false)]
    [TestCase(new int[0], 0, false)]
    [TestCase(new [] {-1, -2, -3, -4, -5}, 0, false)]
    [TestCase(new [] {-1, -2, -3, -4, -5}, -10, false)]
    public void CorrectWorkOfContainsMethod(int[] elementsToAdd, int valueToCheck, bool expectedResult)
    {
        foreach (var element in elementsToAdd)
        {
            skipList.Add(element);
        }
        
        Assert.That(skipList.Contains(valueToCheck), Is.EqualTo(expectedResult));
    }

    [Test]
    public void CorrectWorkOfContainAfterAddingAndRemovingElement()
    {
        int[] elementsToAdd = { 1, 2, 3, 4, 5, 6, 7 };
        foreach (var element in elementsToAdd)
        {
            skipList.Add(element);
        }
        
        Assert.That(skipList.Contains(1), Is.EqualTo(true));
        skipList.Remove(1);
        Assert.That(skipList.Contains(1), Is.EqualTo(false));
        
        Assert.That(skipList.Contains(7), Is.EqualTo(true));
        skipList.Remove(7);
        Assert.That(skipList.Contains(7), Is.EqualTo(false));
        
        Assert.That(skipList.Contains(4), Is.EqualTo(true));
        skipList.Remove(4);
        Assert.That(skipList.Contains(4), Is.EqualTo(false));
    }

    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 44, true)]
    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 9, true)]
    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, -1, true)]
    [TestCase(new [] {-1, -4, 13, 90, 44, 1, 9}, 7, false)]
    [TestCase(new int[0], 13, false)]
    [TestCase(new int[0], 0, false)]
    [TestCase(new [] {-1, -2, -3, -4, -5}, 0, false)]
    [TestCase(new [] {-1, -2, -3, -4, -5}, -10, false)]
    public void RemoveElementElementShouldReturnExpectedResult(int[] elementsToAdd, int valueToRemove, bool expectedResult)
    {
        foreach (var element in elementsToAdd)
        {
            skipList.Add(element);
        }
        
        Assert.That(skipList.Remove(valueToRemove), Is.EqualTo(expectedResult));
    }

    [Test]
    public void CopyToWithValidArguments()
    {
        int[] expectedArray = { 15, 3, 56, 7, 98 };
        int[] actualArray = { 15, 3, 56, 0, 0 };
        int index = 3;
        skipList.Add(7);
        skipList.Add(98);
        skipList.CopyTo(actualArray, index);
        Assert.That(actualArray, Is.EqualTo(expectedArray));
    }

    [Test]
    public void CopyToWithInvalidArguments()
    {
        Assert.Throws<ArgumentException>(() => skipList.CopyTo(Array.Empty<int>(), 5));
        skipList.Add(11);
        skipList.Add(12);
        skipList.Add(13);
        Assert.Throws<ArgumentException>(() => skipList.CopyTo(new [] {1, 2, 3, 4, 5}, 3));
        Assert.Throws<ArgumentException>(() => skipList.CopyTo(new [] {1, 2, 3, 4, 5}, 10));
    }

    [TestCase(new [] { -1 }, 0, -1)]
    [TestCase(new [] { -1, -4, 13}, 0, -4)]
    [TestCase(new [] { -1, -4, 13, 90, 44, 1, 9 }, 6, 90)]
    [TestCase(new [] { -1, -4, 13, 90, 44, 1, 9 }, 3, 9)]
    public void IndexInValidRangeShouldReturnExpectedValue(int[] elementsToAdd, int index, int expectedValue)
    {
        foreach (var element in elementsToAdd)
        {
            skipList.Add(element);
        }
        
        Assert.That(skipList[index], Is.EqualTo(expectedValue));
    }

    [Test]
    public void InvalidOperationsWithIndexerShouldThrowException()
    {
        Array.ForEach(new [] {1, 2, 3, 4, 5}, skipList.Add);
        int elementByIndex = 100;
        Assert.Throws<IndexOutOfRangeException>(() => elementByIndex = skipList[10]);
        Assert.Throws<NotSupportedException>(() => skipList[2] = elementByIndex);
    }

    [Test]
    public void CorrectWorkOfEnumerator()
    {
        int[] expected = { 1, 2, 3, 4, 5 };
        Array.ForEach(expected, skipList.Add);
        var actual = new List<int>();
        foreach (var element in skipList)
        {
            actual.Add(element);
        }
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}