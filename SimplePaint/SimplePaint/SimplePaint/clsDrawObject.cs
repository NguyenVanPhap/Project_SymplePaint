using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SymplePaint
{
    public abstract class clsDrawObject
    {
        public PointF p1;
        public PointF p2;
        public PointF p3;
        public PointF p4;

        public bool Filled { get; set; }
        public Pen myPen { get; set; }
        public Brush myBrush { get; set; }

        public DashStyle DStyle { get; set; }
        public int width { get; set; }
        public Color myOutLineColor { get; set; }
        public Color myFillColor { get; set; }

        public int BrushTypes { get; set; }
       
        
        public abstract void Draw(Graphics myGp);

        public bool IsSelected { get; set; }
        public bool IsReSized { get; set; }

        protected abstract GraphicsPath GraphicsPath { get; }
        
        public abstract bool IsHit(PointF point);
        
        public abstract void Move(PointF distance);
        public abstract void Resize(PointF newP);
    }
}
