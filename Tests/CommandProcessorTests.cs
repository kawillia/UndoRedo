using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UndoRedo.Shapes;
using UndoRedo.Shapes.Commands;
using UndoRedo.Shapes.Exceptions;

namespace UndoRedo.Tests
{
    [TestClass]
    public class CommandProcessorTests
    {
        private CommandProcessor shapeEditor;
        private Shape shape;

        public CommandProcessorTests()
        {
            shapeEditor = new CommandProcessor();
            shape = new Shape();
        }

        [TestMethod, ExpectedException(typeof(NoHistoryToUndoException))]
        public void ExceptionIsThrownIfUndoIsCalledBeforeDo()
        {
            shapeEditor.Undo();
        }

        [TestMethod, ExpectedException(typeof(NoHistoryToRedoException))]
        public void ExceptionIsThrownIfRedoIsCalledBeforeUndo()
        {
            shapeEditor.Redo();
        }

        [TestMethod]
        public void EditExecutesCommand()
        {
            var command = new ChangeHeightCommand(shape, 10);
            shapeEditor.Do(command);

            Assert.AreEqual(10, shape.Height);
        }

        [TestMethod]
        public void EditEnablesUndo()
        {
            var command = new ChangeHeightCommand(shape, 10);
            shapeEditor.Do(command);

            Assert.IsTrue(shapeEditor.CanUndo());
        }

        [TestMethod]
        public void UndoWithOneCommandDisablesUndo()
        {
            var command = new ChangeHeightCommand(shape, 10);
            shapeEditor.Do(command);
            shapeEditor.Undo();

            Assert.IsFalse(shapeEditor.CanUndo());
        }

        [TestMethod]
        public void UndoWithOneCommandEnablesRedo()
        {
            var command = new ChangeHeightCommand(shape, 10);
            shapeEditor.Do(command);
            shapeEditor.Undo();

            Assert.IsTrue(shapeEditor.CanRedo());
        }

        [TestMethod]
        public void UndoRedoIsDisabledBeforeEdit()
        {
            var command = new ChangeHeightCommand(shape, 10);

            Assert.IsFalse(shapeEditor.CanRedo());
            Assert.IsFalse(shapeEditor.CanUndo());
        }

        [TestMethod]
        public void RedoWithOneCommandEnablesUndo()
        {
            var command = new ChangeHeightCommand(shape, 10);
            shapeEditor.Do(command);
            shapeEditor.Undo();
            shapeEditor.Redo();

            Assert.IsTrue(shapeEditor.CanUndo());
        }

        [TestMethod]
        public void RedoCallsUnexecute()
        {
            var command = new ChangeHeightCommand(shape, 10);
            shapeEditor.Do(command);
            shapeEditor.Undo();
            shapeEditor.Redo();

            Assert.AreEqual(10, shape.Height);
        }

        [TestMethod]
        public void MultipleUndos()
        {
            var command = new ChangeHeightCommand(shape, 10);

            ChangeHeightManyTimes(shapeEditor, shape, 10);
            UndoMany(shapeEditor, 3);

            Assert.AreEqual(60, shape.Height);
        }

        [TestMethod]
        public void MultipleRedos()
        {
            var command = new ChangeHeightCommand(shape, 10);

            ChangeHeightManyTimes(shapeEditor, shape, 10);
            UndoMany(shapeEditor, 5);
            RedoMany(shapeEditor, 3);

            Assert.AreEqual(70, shape.Height);
        }

        [TestMethod]
        public void AfterNewEditRedoIsDisabled()
        {
            var command = new ChangeHeightCommand(shape, 10);

            ChangeHeightManyTimes(shapeEditor, shape, 10);
            UndoMany(shapeEditor, 3);
            shapeEditor.Do(new ChangeWidthCommand(shape, 100));

            Assert.IsFalse(shapeEditor.CanRedo());
        }
        
        private void ChangeHeightManyTimes(CommandProcessor editor, Shape shape, Int32 numberOfTimes)
        {
            for (var i = 0; i < numberOfTimes; i++)
            {
                var command = new ChangeHeightCommand(shape, i * 10);
                editor.Do(command);
            }
        }

        private void UndoMany(CommandProcessor shapeEditor, Int32 numberOfTimes)
        {
            for (var i = 0; i < numberOfTimes; i++)
                shapeEditor.Undo();
        }

        private void RedoMany(CommandProcessor shapeEditor, Int32 numberOfTimes)
        {
            for (var i = 0; i < numberOfTimes; i++)
                shapeEditor.Redo();
        }
    }
}