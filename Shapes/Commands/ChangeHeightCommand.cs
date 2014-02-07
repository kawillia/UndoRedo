using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndoRedo.Shapes.Commands
{
    public class ChangeHeightCommand : ICommand
    {
        private Int32 newHeight;
        private Int32 oldHeight;
        private Shape shape;

        public ChangeHeightCommand(Shape shape, Int32 newHeight)
        {
            this.shape = shape;
            this.newHeight = newHeight;
            this.oldHeight = shape.Height;
        }

        public void Execute()
        {
            shape.Height = newHeight;   
        }

        public void Unexecute()
        {
            shape.Height = oldHeight;
        }
    }
}
