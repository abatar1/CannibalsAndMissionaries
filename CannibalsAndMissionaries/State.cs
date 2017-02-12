using System;

namespace CannibalsAndMissionaries
{
    public class State : IEquatable<State>
    {
        private const int MaxPeople = 3;

        public int Missioners { get; }
        public int Cannibals { get; }      

        public State(int missioners, int cannibals)
        {
            Missioners = missioners;
            Cannibals = cannibals;
        }

        public bool IsValid
        {
            get
            {
                var correctManipulation = Missioners >= 0 && Cannibals >= 0 && MaxPeople - Missioners >= 0 && MaxPeople - Cannibals >= 0;
                var wrongSide = Missioners == 0 || Missioners >= Cannibals;
                var rightSide = MaxPeople - Missioners == 0 || MaxPeople - Missioners >= MaxPeople - Cannibals;

                return correctManipulation && wrongSide && rightSide;
            }
        }

        public bool IsGoal => Missioners == 0 && Cannibals == 0;

        public State Reverse() => new State(MaxPeople - Missioners, MaxPeople - Cannibals);

        public State Abs() => new State(Math.Abs(Missioners), Math.Abs(Cannibals));

        public static State operator -(State s1, State s2)
        {           
            return new State(s1.Missioners - s2.Missioners, s1.Cannibals - s2.Cannibals);
        }

        public static State operator +(State s1, State s2)
        {
            return new State(s1.Missioners + s2.Missioners, s1.Cannibals + s2.Cannibals);
        }

        public override string ToString()
        {
            return "(M" + Missioners + " C" + Cannibals + ")";
        }

        public bool Equals(State other)
        {
            return other != null && Missioners == other.Missioners && Cannibals == other.Cannibals;
        }
    }
}
