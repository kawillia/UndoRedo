using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndoRedo.Shapes.Commands
{
    public class ChangeWidthCommand : ICommand
    {
        private Shape shape;
        private Int32 auxWidth;

        public ChangeWidthCommand(Shape shape, Int32 width)
        {
            this.shape = shape;
            this.auxWidth = width;
        }

        public void Execute()
        {
            var hold = shape.Width;
            shape.Width = auxWidth;
            auxWidth = hold;
        }

        public void Unexecute()
        {
            shape.Width = auxWidth;
        }
    }
}
