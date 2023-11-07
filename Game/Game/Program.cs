using Game;

var eventLoop = new EventLoop();
var game = new Game.Game();

var pathToFile = "../../../Map.txt";

try
{
    game.CreateMatrixFromFileMap(pathToFile);
    game.PrintMapMatrix();
}

catch (InvalidDataException)
{
    Console.WriteLine("Incorrect map.");
}

eventLoop.LeftHandler += game.OnLeft;
eventLoop.LeftHandler += game.UpdateCursorPosition;

eventLoop.RightHandler += game.OnRight;
eventLoop.RightHandler += game.UpdateCursorPosition;

eventLoop.TopHandler += game.Up;
eventLoop.TopHandler += game.UpdateCursorPosition;

eventLoop.BottomHandler += game.Down;
eventLoop.BottomHandler += game.UpdateCursorPosition;

eventLoop.Run();