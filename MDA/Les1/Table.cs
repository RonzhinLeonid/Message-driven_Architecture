namespace Les1
{
    public class Table
    {
        public StateTable State { get; private set; }
        public int SeatsCount { get; }
        public int Id { get; }

        public Table(int id)
        {
            Id = id;
            State = StateTable.Free;
            SeatsCount = Random.Shared.Next(2, 5);
        }

        public bool SetState(StateTable state)
        {
            if (state == State) return false;

            State = state;
            return true;
        }
    }
}
