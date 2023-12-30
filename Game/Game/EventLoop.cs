namespace Game;

public class EventLoop
{
    public event EventHandler<EventArgs> LeftHandler = (sender, args) => { };
    public event EventHandler<EventArgs> RightHandler = (sender, args) => { };
    public event EventHandler<EventArgs> TopHandler = (sender, args) => { };
    public event EventHandler<EventArgs> BottomHandler = (sender, args) => { };

    public void Run()
    {
        var shouldContinue = true;
        while (shouldContinue)
        {
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    shouldContinue = false;
                    break;
                case ConsoleKey.LeftArrow:
                    LeftHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.RightArrow:
                    RightHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.UpArrow:
                    TopHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.DownArrow:
                    BottomHandler(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }
    }
}
