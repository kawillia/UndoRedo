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
        [TestMethod]
        public void ExecuteChangesTheWidthOfTheShape()
        {
            var shape = new Shape();
            shape.Width = 10;

            var command = new ChangeWidthCommand(shape, 50);
            command.Execute();

            Assert.AreEqual(50, shape.Width);
        }

        [TestMethod]
        public void UnexecuteSetsTheWidthBackToItsOriginalWidth()
        {
            var shape = new Shape();
            shape.Width = 10;

            var command = new ChangeWidthCommand(shape, 50);
            command.Execute();
            command.Unexecute();

            Assert.AreEqual(10, shape.Width);
        }

        [TestMethod]
        public void ConstructorDoesNotChangeValue()
        {
            var shape = new Shape();
            shape.Width = 10;

            var command = new ChangeWidthCommand(shape, 50);

            Assert.AreEqual(10, shape.Width);
        }
    }
}
