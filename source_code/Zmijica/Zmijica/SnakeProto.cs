using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Zmijica
{
    internal class SnakeProto : IDrawable
    {
        (float, float) IDrawable.Position
        {
            get => (0, 0);
        }
    }
}
