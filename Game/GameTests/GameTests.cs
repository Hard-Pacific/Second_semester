using NUnit.Framework;
using System;
using System.IO;
using Game;

namespace GameTests;

public class GameTests
{
    private readonly EventLoop eventLoop = new();
    private readonly Game.Game game = new();

    private readonly string pathToStandartMap = "../../../StandartMap.txt";
    private readonly string pathToEmptyMap = "../../../EmptyMap.txt";
    private readonly string pathToIncorrectMap = "../../../IncorrectMap.txt";

    [Test]
    public void StartCursorTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        var currentPosition = game.CurrentMatrixPosition;
        Assert.AreEqual((0, 3), currentPosition);
    }

    [Test]
    public void LeftMovementWithEmptySpaceAndWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (1, 3);
        for (var i = 0; i < 3; ++i)
        {
            game.OnLeft(this, EventArgs.Empty);
        }

        Assert.AreEqual((1, 1), game.CurrentMatrixPosition);
    }

    [Test]
    public void LeftMovementWithConsoleWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (2, 2);
        for (var i = 0; i < 4; ++i)
        {
            game.OnLeft(this, EventArgs.Empty);
        }

        Assert.AreEqual((2, 0), game.CurrentMatrixPosition);
    }

    [Test]
    public void RightMovementWithEmptySpaceAndWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (1, 1);
        for (var i = 0; i < 4; ++i)
        {
            game.OnRight(this, EventArgs.Empty);
        }

        Assert.AreEqual((1, 3), game.CurrentMatrixPosition);
    }

    [Test]
    public void RightMovementWithConsoleWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (2, 4);
        for (var i = 0; i < 4; ++i)
        {
            game.OnRight(this, EventArgs.Empty);
        }

        Assert.AreEqual((2, 6), game.CurrentMatrixPosition);
    }

    [Test]
    public void UpMovementWithEmptySpaceAndWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (2, 2);
        for (var i = 0; i < 3; ++i)
        {
            game.Up(this, EventArgs.Empty);
        }

        Assert.AreEqual((1, 2), game.CurrentMatrixPosition);
    }

    [Test]
    public void UpMovementWithConsoleWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (0, 4);
        for (var i = 0; i < 2; ++i)
        {
            game.Up(this, EventArgs.Empty);
        }

        Assert.AreEqual((0, 4), game.CurrentMatrixPosition);
    }

    [Test]
    public void DownMovementWithEmptySpaceAndWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (0, 1);
        for (var i = 0; i < 3; ++i)
        {
            game.Down(this, EventArgs.Empty);
        }

        Assert.AreEqual((2, 1), game.CurrentMatrixPosition);
    }

    [Test]
    public void DownMovementWithConsoleWallTest()
    {
        game.CreateMatrixFromFileMap(pathToStandartMap);
        game.CurrentMatrixPosition = (2, 3);
        for (var i = 0; i < 3; ++i)
        {
            game.Down(this, EventArgs.Empty);
        }

        Assert.AreEqual((3, 3), game.CurrentMatrixPosition);
    }

    [Test]
    public void EmptyMapTest()
    {
        Assert.Throws<InvalidDataException>(()
            => game.CreateMatrixFromFileMap(pathToEmptyMap));
    }

    [Test]
    public void IncorrectMapTest()
    {
        Assert.Throws<InvalidDataException>(()
            => game.CreateMatrixFromFileMap(pathToIncorrectMap));
    }
}