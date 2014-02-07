using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UndoRedo.Shapes;
using UndoRedo.Shapes.Commands;

namespace UndoRedo.Tests.Commands
{
    [TestClass]
    public class ChangeWidthCommandTests
    {
        private Shape shape;
        private ChangeWidthCommand changeWidthCommand;

        public ChangeWidthCommandTests()
        {
            shape = new Shape();
            shape.Width = 10;
            changeWidthCommand = new ChangeWidthCommand(shape, 50);
        }

        [TestMethod]
        public void ExecuteChangesTheWidthOfTheShape()
        {
            changeWidthCommand.Execute();

            Assert.AreEqual(50, shape.Width);
        }

        [TestMethod]
        public void UnexecuteSetsTheWidthBackToItsOriginalWidth()
        {
            changeWidthCommand.Execute();
            changeWidthCommand.Unexecute();

            Assert.AreEqual(10, shape.Width);
        }

        [TestMethod]
        public void ConstructorDoesNotChangeValue()
        {
            Assert.AreEqual(10, shape.Width);
        }
    }
}
