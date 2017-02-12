using System;

namespace CannibalsAndMissionaries
{
    public class State : IEquatable<State>
    {
        public int Missioners { get; }
        public int Cannibals { get; }
        public int Boat { get; }

        public State(int missioners, int cannibals, int boat)
        {
            Missioners = missioners;
            Cannibals = cannibals;
            Boat = boat;
        }

        public bool IsValid
        {
            get
            {
                var correctManipulation = Missioners >= 0 && Cannibals >= 0 && 3 - Missioners >= 0 && 3 - Cannibals >= 0;
                var wrongSide = Missioners == 0 || Missioners >= Cannibals;
                var rightSide = 3 - Missioners == 0 || 3 - Missioners >= 3 - Cannibals;

                return correctManipulation && wrongSide && rightSide;
            }
        }

        public bool IsGoal => Missioners == 0 && Cannibals == 0 && Boat == 0;

        public static State operator -(State s1, State s2)
        {
            return new State(s1.Missioners - s2.Missioners, s1.Cannibals - s2.Cannibals, s1.Boat - s2.Boat);
        }

        public static State operator +(State s1, State s2)
        {
            return new State(s1.Missioners + s2.Missioners, s1.Cannibals + s2.Cannibals, s1.Boat + s2.Boat);
        }

        public override string ToString()
        {
            return "(M" + Missioners + " C" + Cannibals + ")->(" + "M" + (3 - Missioners) + " C" + (3 - Cannibals) + ")";
        }

        public bool Equals(State other)
        {
            return other != null && Missioners == other.Missioners && Cannibals == other.Cannibals && Boat == other.Boat;
        }
    }
}
