namespace Game;

public class Game
{
    private char[,]? mapMatrix;
    private (int, int) CurrentCursorPosition;
    public (int, int) CurrentMatrixPosition; //Для тестов

    public void OnLeft(object? sendler, EventArgs args)
    {
        Move(this.CurrentMatrixPosition.Item1, this.CurrentMatrixPosition.Item2 - 1);
    }

    public void OnRight(object? sendler, EventArgs args)
    {
        Move(this.CurrentMatrixPosition.Item1, this.CurrentMatrixPosition.Item2 + 1);
    }

    public void Up(object? sendler, EventArgs args)
    {
        Move(this.CurrentMatrixPosition.Item1 - 1, this.CurrentMatrixPosition.Item2);
    }

    public void Down(object? sendler, EventArgs args)
    {
        Move(this.CurrentMatrixPosition.Item1 + 1, this.CurrentMatrixPosition.Item2);
    }

    public void Move(int newItem1CurrentMatrixPosition, int newItem2CurrentMatrixPosition)
    {
        if (newItem1CurrentMatrixPosition < mapMatrix!.GetLength(0) && newItem1CurrentMatrixPosition >= 0
            && newItem2CurrentMatrixPosition < mapMatrix!.GetLength(1) && newItem2CurrentMatrixPosition >= 0
            && mapMatrix![newItem1CurrentMatrixPosition, newItem2CurrentMatrixPosition] == ' ')
        {
            this.CurrentMatrixPosition = (newItem1CurrentMatrixPosition, newItem2CurrentMatrixPosition);
        }
    }

    public void CreateMatrixFromFileMap(string pathToFile)
    {
        using (StreamReader streamReader = new(pathToFile))
        {
            var line = streamReader.ReadLine();
            if (line == null)
            {
                throw new InvalidDataException();
            }

            var countOfColumns = line.Length;
            var countOfLines = 1;
            while (streamReader.ReadLine() != null)
            {
                ++countOfLines;
            }

            streamReader.Close();
            var newStreamReader = new StreamReader(pathToFile);
            this.CurrentCursorPosition = (0, 0);
            var resultMatrix = new char[countOfLines, countOfColumns];
            for (var i = 0; i < countOfLines; ++i)
            {
                line = newStreamReader.ReadLine();
                if (line == null)
                {
                    throw new InvalidDataException();
                }

                for (var j = 0; j < countOfColumns; ++j)
                {
                    resultMatrix[i, j] = line[j];
                    if (Equals(this.CurrentCursorPosition, (0, 0)) && resultMatrix[i, j] == ' ')
                    {
                        this.CurrentCursorPosition = (j, i);
                        this.CurrentMatrixPosition = (i, j);
                    }
                }
            }

            if (Equals(this.CurrentCursorPosition, (0, 0)))
            {
                throw new InvalidDataException();
            }

            mapMatrix = resultMatrix;
        }
    }

    public void PrintMapMatrix()
    {
        if (mapMatrix == null)
        {
            throw new InvalidDataException();
        }

        for (var i = 0; i < mapMatrix.GetLength(0); ++i)
        {
            for (var j = 0; j < mapMatrix.GetLength(1); ++j)
            {
                Console.Write(mapMatrix[i, j]);
            }
            Console.WriteLine();
        }

        SetCursor(this.CurrentMatrixPosition);
    }

    public void UpdateCursorPosition(object? sendler, EventArgs args)
    {
        Console.Write(" ");
        SetCursor(this.CurrentMatrixPosition);
    }

    private void SetCursor((int, int) newPosition)
    {
        Console.SetCursorPosition(newPosition.Item2, newPosition.Item1);
        Console.Write("@");
        Console.SetCursorPosition(newPosition.Item2, newPosition.Item1);
        this.CurrentCursorPosition = newPosition;
    }
}