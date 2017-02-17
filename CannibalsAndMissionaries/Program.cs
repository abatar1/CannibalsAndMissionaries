using System;
using System.Linq;

namespace CannibalsAndMissionaries
{
    internal static class Program
    {
        private static void Main(string[] args)
        {                  
            var node = new Node();
            var solution = node.FindSolution();

            var nodeThree = solution.PathToRoot.Reverse().ToArray();
            var statements = solution.Statements.Reverse().ToArray();
            var boat = statements.Zip(statements.Skip(1), (a, b) => (b - a).Abs()).ToArray();
            
            for (var i = 0; i < statements.Length; i++)
            {
                Console.WriteLine(nodeThree[i].Formatted(i == nodeThree.Length - 1 ? null : boat[i]));
            }
        }
    }
}
