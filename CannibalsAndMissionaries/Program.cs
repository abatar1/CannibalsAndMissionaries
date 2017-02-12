using System;
using System.Linq;

namespace CannibalsAndMissionaries
{
    internal static class Program
    {
        private static void Main(string[] args)
        {           
            var node = new Node();
            var solution = node.FindGoal().Solution.Reverse().ToArray();
            var boat = solution.Zip(solution.Skip(1), (a, b) => (b - a).Abs()).ToArray();            
            for(var i = 0; i < solution.Length; i++)
            {
                if (i == solution.Length - 1) Console.WriteLine("L" + solution[i] + solution[i].Direction + "R" + solution[i].Reverse());
                else Console.WriteLine("L" + solution[i] + solution[i].Direction + "B" + boat[i] + solution[i].Direction + "R" + solution[i].Reverse());
            }
        }
    }
}
