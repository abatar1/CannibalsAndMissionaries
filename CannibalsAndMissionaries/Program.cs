using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannibalsAndMissionaries
{
    internal static class Program
    {
        private static void Main(string[] args)
        {           
            var node = new Node();
            foreach (var state in node.FindGoal().Solution.Reverse())
            {
                Console.WriteLine(state.ToString());
            }
        }
    }
}
