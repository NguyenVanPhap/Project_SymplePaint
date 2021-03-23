using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplePaint
{
    class DoubleBufferPanel:System.Windows.Forms.Panel
    {
        public DoubleBufferPanel()
        {
            this.DoubleBuffered = true;
        }
    }
}
