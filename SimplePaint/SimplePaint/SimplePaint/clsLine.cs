using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplePaint
{
    public class clsLine : clsDrawObject
    {
        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddLine(p1, p2);
                return path;
            }
        }


        public override void Draw(Graphics myGp)
        {

            myPen = new Pen(myOutLineColor, width);
            myPen.DashStyle = DStyle;
            myGp.DrawLine(myPen, this.p1, this.p2);
            
        }

        public override bool IsHit(PointF point)
        {
            bool res = false;
            using (GraphicsPath path = GraphicsPath)
            {
                using (Pen pen = new Pen(Color.BlueViolet))
                {
                    res = path.IsOutlineVisible(point, pen);
                }
            }
            return res;
        }

        public override void Move(PointF distance)
        {
            p1 = new PointF(p1.X + distance.X, p1.Y + distance.Y);
            p2 = new PointF(p2.X + distance.X, p2.Y + distance.Y);
        }

        public override void Resize(PointF newP)
        {
            p2 = newP;
        }

       
    }
}
