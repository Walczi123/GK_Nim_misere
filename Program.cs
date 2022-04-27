using Nim_misere.AI;
using Nim_misere.Game;
using Nim_misere.Player;

Console.WriteLine("\n\n");
Console.WriteLine("Your game begins!\n");

var state = new State() { Stacks = new List<int> { 3, 3, 3 } };
var game = new NimMisereGame(new Man(), new MCTS(), state);
game.Start();

Console.WriteLine("Bye");
Console.ReadKey();
