namespace StackCalculator;

using System;

/// <summary>
/// Class to make computing using stack.
/// </summary>
public class StackCalculator
{
    private IStack stack;

    /// <summary>
    /// Initializes a new instance of the <see cref="StackCalculator"/> class.
    /// </summary>
    /// <param name="stack">stack element for StackCalculator.</param>
    public StackCalculator(IStack stack)
       => this.stack = stack;

    /// <summary>
    /// Method calculate expression in postfix form and return result.
    /// </summary>
    /// <param name="commandSequence">expression in postfix form.</param>
    /// <returns>result of computing. Also return true if method completed the work correctly. Else false.</returns>
    public (float Result, bool CheckOfCorrectWork) CalculateInPostfixForm(string commandSequence)
    {
        var arrayOfNumbersAndOperators = commandSequence.Split();
        var checkOfCorrectWork = true;
        for (int i = 0; i < arrayOfNumbersAndOperators.Length; ++i)
        {
            if (arrayOfNumbersAndOperators[i] == "*" || arrayOfNumbersAndOperators[i] == "/"
                || arrayOfNumbersAndOperators[i] == "+" || arrayOfNumbersAndOperators[i] == "-")
            {
                (var firstElement, checkOfCorrectWork) = this.stack!.Pop();
                (var secondElement, checkOfCorrectWork) = this.stack.Pop();
                if (!checkOfCorrectWork)
                {
                    return (0, false);
                }

                float middleResult = 0;
                switch (arrayOfNumbersAndOperators[i])
                {
                    case "*":
                        middleResult = firstElement * secondElement;
                        break;
                    case "/":
                        if (Math.Abs(firstElement) > 0.000000001)
                        {
                            middleResult = secondElement / firstElement;
                        }
                        else
                        {
                            return (0, false);
                        }

                        break;
                    case "-":
                        middleResult = secondElement - firstElement;
                        break;
                    case "+":
                        middleResult = firstElement + secondElement;
                        break;
                    default:
                        break;
                }

                this.stack.Push(middleResult);
            }
            else
            {
                this.stack!.Push(float.Parse(arrayOfNumbersAndOperators[i]));
            }
        }

        (float finalResult, checkOfCorrectWork) = this.stack!.Pop();
        if (!this.stack.IsEmpty() || !checkOfCorrectWork)
        {
            return (0, false);
        }

        return (finalResult, true);
    }
}
