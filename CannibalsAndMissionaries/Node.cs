using System;
using System.Collections.Generic;
using System.Linq;

namespace CannibalsAndMissionaries
{
    public class Node : IEquatable<Node>
    {
        private readonly State _state;
        private readonly Node _parent;
        private readonly bool _toGoal;

        private readonly List<State> _possibleMoves = new List<State>
        {
            new State(1, 0),
            new State(2, 0),
            new State(0, 1),
            new State(0, 2),
            new State(1, 1)
        };

        public Node(State state = null, Node parent = null, bool toGoal = true)
        {
            _state = state ?? new State(3, 3);
            _parent = parent;
            _toGoal = toGoal;
        }

        private Node Transition(State move)
        {
            return new Node(_toGoal ? _state - move : _state + move, this, !_toGoal);
        }

        private IEnumerable<Node> Children
        {
            get
            {
                return _possibleMoves
                    .Select(Transition)
                    .Where(node => node._state.IsValid);
            }
        }
        
        public IEnumerable<Node> PathToRoot
        {
            get
            {
                var node = this;
                while (true)
                {
                    yield return node;
                    node = node._parent;
                    if (node._parent == null)
                    {
                        yield return node;
                        break;
                    }
                }
            }
        }

        public Node FindSolution()
        {
            var examined = new List<Node>();
            var queue = new Queue<Node>(Children);

            while (true)
            {
                var node = queue.Dequeue();

                if (examined.Contains(node)) continue;
                examined.Add(node);

                if (node._state.IsGoal) return node;
                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public string Direction => _toGoal ? "->" : "<-";

        public IEnumerable<State> Statements
        {
            get
            {
                return PathToRoot
                    .Select(node => node._state);
            }
        }

        public bool Equals(Node other)
        {
            return other != null && _state.Equals(other._state) && _toGoal == other._toGoal;
        }

        public string Formatted(State boat = null)
        {
            var dirStr = boat == null ? "==" : Direction + "B" + boat + Direction;
            return "L" + _state + dirStr + "R" + _state.Reverse();
        }
    }
}
