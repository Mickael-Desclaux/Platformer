using MGA.UniFlux;

namespace MUIStore.State
{
    public record TestUIIncrementAction : ImmutableAction<TestUIState>
    {
        public override TestUIState Reduce(TestUIState state)
        {
            return new TestUIState() { Count = state.Count + 1 };
        }
    }
}