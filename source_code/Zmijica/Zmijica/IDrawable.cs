using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zmijica
{
    internal interface IDrawable
    {
        (float, float) Position
        {
            get;
        }
    }
}
