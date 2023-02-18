namespace Outcompute.Toolkit.Core.CodeGenerator.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = TaskStatus.Faulted;
            var y = TaskStatus.Created.AsString();
        }
    }
}