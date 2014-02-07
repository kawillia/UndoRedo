using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UndoRedo.Shapes;
using UndoRedo.Shapes.Commands;

namespace UndoRedo.Tests.Commands
{
    [TestClass]
    public class ChangeHeightCommandTests
    {
        private Shape shape;
        private ChangeHeightCommand changeHeightCommand;

        public ChangeHeightCommandTests()
        {
            shape = new Shape();
            shape.Height = 10;
            changeHeightCommand = new ChangeHeightCommand(shape, 50);
        }

        [TestMethod]
        public void ExecuteChangesTheHeightOfTheShape()
        {
            changeHeightCommand.Execute();

            Assert.AreEqual(50, shape.Height);
        }

        [TestMethod]
        public void UnexecuteSetsTheHeightBackToItsOriginalHeight()
        {
            changeHeightCommand.Execute();
            changeHeightCommand.Unexecute();

            Assert.AreEqual(10, shape.Height);
        }

        [TestMethod]
        public void ConstructorDoesNotChangeValue()
        {
            Assert.AreEqual(10, shape.Height);
        }
    }
}
