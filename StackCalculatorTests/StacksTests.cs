using StackCalculator;

namespace StackCalculatorTests;

using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class StacksTests
{
    private static IEnumerable<TestCaseData> Stacks
        => new TestCaseData[]
    {
        new TestCaseData(new StackCalculator.StackOnArray()),
        new TestCaseData(new StackCalculator.StackOnList()),
    };

    [TestCaseSource(nameof(Stacks))]
    public void StackSupportPushAndPopMethods(IStack stack)
    {
        stack.Push(88);
        stack.Push(245);
        stack.Push(100);
        bool isStackNotEmpty = true;
        var stackValues = new float[3];
        for (int i = 0; i < 3; ++i)
        {
            (float stackValue, isStackNotEmpty) = stack.Pop();
            stackValues[i] = stackValue;
        }
        Assert.AreEqual(100, stackValues[0]);
        Assert.AreEqual(245, stackValues[1]);
        Assert.AreEqual(88, stackValues[2]);
        Assert.IsTrue(isStackNotEmpty);
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldNotEmptyAfterPush(IStack stack)
    {
        stack.Push(1);
        Assert.IsFalse(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void EmptyStackAfterPop(IStack stack)
    {
        (float returningValue, bool isStackNotEmpty) = stack.Pop();
        Assert.IsFalse(isStackNotEmpty);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TwoPushAndFourPop(IStack stack)
    {
        stack.Push(557);
        stack.Push(18.787F);
        var isStackNotEmpty = true;
        for (int i = 0; i < 4; ++i)
        {
            (float stackValue, isStackNotEmpty) = stack.Pop();
        }

        Assert.IsFalse(isStackNotEmpty);
    }

    [TestCaseSource(nameof(Stacks))]
    public void PushAndPopTwoEquivalentElements(IStack stack)
    {
        stack.Push(0);
        stack.Push(0);
        (var isStackNotEmpty, float returningValue) = (true, 1);
        (returningValue, isStackNotEmpty) = stack.Pop();
        var isTestCorrect = true;
        if (returningValue != 0)
        {
            isTestCorrect = false;
        }

        (returningValue, isStackNotEmpty) = stack.Pop();
        if (returningValue != 0 || !isStackNotEmpty)
        {
            isTestCorrect = false;
        }

        Assert.IsTrue(isTestCorrect);
    }
}