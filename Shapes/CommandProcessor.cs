using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndoRedo.Shapes.Commands;
using UndoRedo.Shapes.Exceptions;

namespace UndoRedo.Shapes
{
    public class CommandProcessor
    {
        private Stack<ICommand> undo = new Stack<ICommand>();
        private Stack<ICommand> redo = new Stack<ICommand>();

        public void Do(ICommand command)
        {
            command.Execute();
            undo.Push(command);
            redo.Clear();
        }

        public void Undo()
        {
            if (!CanUndo())
                throw new NoHistoryToUndoException();

            var command = undo.Pop();
            command.Unexecute();
            redo.Push(command);
        }

        public void Redo()
        {
            if (!CanRedo())
                throw new NoHistoryToRedoException();

            var command = redo.Pop();
            command.Execute();
            undo.Push(command);
        }

        public Boolean CanRedo()
        {
            return redo.Any();
        }

        public Boolean CanUndo()
        {
            return undo.Any();
        }
    }
}
