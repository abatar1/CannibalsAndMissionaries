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

        // Все возможные ходы
        private static readonly IReadOnlyList<State> PossibleMoves = new List<State>
        {
            new State(1, 0),
            new State(2, 0),
            new State(0, 1),
            new State(0, 2),
            new State(1, 1)
        };

        // Конструктор класса
        public Node(State state = null, Node parent = null, bool toGoal = true)
        {
            _state = state ?? new State(3, 3); // Начальное состояние.
            _parent = parent;
            _toGoal = toGoal;
        }

        // Метод движения
        private Node Transition(State move) 
        {
            return new Node(_toGoal ? _state - move : _state + move, this, !_toGoal);
        }

        // Берем возможные ходы, применяем их все к вершине посредством Transition и выбираем те, которые удовлетворяют условию isvalid
        private IEnumerable<Node> FindChildren()
        {
                return PossibleMoves
                    .Select(Transition)
                    .Where(node => node._state.IsValid);           
        }

        // Возвращаем список из вершин от итоговой до рута.
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
            var examined = new HashSet<Node>(); // пустой список
            var queue = new Queue<Node>(FindChildren()); // очередь

            while (true)
            {
                // Читаем и удаляем элемент из головы очереди.
                var node = queue.Dequeue();

                // Если вершина содержится в examined, то continue, если не содержится, то добавляем её.
                if (!examined.Add(node)) continue;

                // Если вершина удовлетворяет IsGoal, то возвращаем вершину.
                if (node._state.IsGoal) return node;

                // Иначе все child из node.FindChildren() добавляем в очередь.
                foreach (var child in node.FindChildren())
                {
                    queue.Enqueue(child);
                }
            }
        }

        public string Direction
        {
            get
            {
                return _toGoal ? "->" : "<-";
            }
        } 

        // Преобразование элементов PathToRoot из Node в State.

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

        // Метод форматированного вывода.
        public string Formatted(State boat = null)
        {
            var dirStr = boat == null ? "     ==     " : Direction + "B" + boat + Direction;
            return "L" + _state + dirStr + "R" + _state.Reverse();
        }
    }
}
