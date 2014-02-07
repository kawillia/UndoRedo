using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UndoRedo.Shapes;
using UndoRedo.Shapes.Commands;

namespace UndoRedo.Tests.Commands
{
    [TestClass]
    public class ChangeHeightCommandTests
    {
        [TestMethod]
        public void ExecuteChangesTheHeightOfTheShape()
        {
            var shape = new Shape();
            shape.Height = 10;

            var command = new ChangeHeightCommand(shape, 50);
            command.Execute();

            Assert.AreEqual(50, shape.Height);
        }

        [TestMethod]
        public void UnexecuteSetsTheHeightBackToItsOriginalHeight()
        {
            var shape = new Shape();
            shape.Height = 10;

            var command = new ChangeHeightCommand(shape, 50);
            command.Execute();
            command.Unexecute();

            Assert.AreEqual(10, shape.Height);
        }

        [TestMethod]
        public void ConstructorDoesNotChangeValue()
        {
            var shape = new Shape();
            shape.Height = 10;

            var command = new ChangeHeightCommand(shape, 50);

            Assert.AreEqual(10, shape.Height);
        }
    }
}
