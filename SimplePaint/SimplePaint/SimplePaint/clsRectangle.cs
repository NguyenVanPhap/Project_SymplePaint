using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplePaint
{
    class clsRectangle : clsDrawObject
    {
        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                PointF Tam = new Point();
                Tam = p1;
                float a = Math.Abs(p2.X - p1.X);
                float b = Math.Abs(p2.Y - p1.Y);
                if (p1.X > p2.X)
                    Tam.X = p2.X;
                if (p1.Y > p2.Y)
                    Tam.Y = p2.Y;
                path.AddRectangle(new System.Drawing.RectangleF(Tam.X, Tam.Y, a, b));
                p3.X = p1.X;
                p3.Y = p2.Y;
                p4.X = p2.X;
                p4.Y = p1.Y;
                return path;
            }
        }
        public override void Draw(Graphics myGp)
        {
            using (GraphicsPath path = GraphicsPath)
            {
                myPen = new Pen(myOutLineColor, width);
                myPen.DashStyle = DStyle;
                myBrush = new SolidBrush(myFillColor);
                if (BrushTypes == 0)
                    myBrush = new SolidBrush(myFillColor);
                if (BrushTypes == 1)
                    myBrush = new HatchBrush(HatchStyle.LargeCheckerBoard, myFillColor);
                if (BrushTypes == 2)
                    myBrush = new HatchBrush(HatchStyle.LargeConfetti, myFillColor);
                if (BrushTypes == 3)
                    myBrush = new HatchBrush(HatchStyle.LightUpwardDiagonal, myFillColor);


                if (Filled == false)
                {
                    myGp.DrawPath(myPen, path);
                }
                else
                {
                    myGp.FillPath(myBrush, path);
                    myGp.DrawPath(myPen, path);
                }
            }
            
        }

        public override bool IsHit(PointF point)
        {
            bool res = false;
            using (GraphicsPath path = GraphicsPath)
            {
                if (!Filled)
                {
                    using (Pen pen = new Pen(Color.BlueViolet))
                    {
                        res = path.IsOutlineVisible(point, pen);
                    }
                }
                else
                {
                    res = path.IsVisible(point);
                }
            }
            return res;

        }
        
        public override void Move(PointF distance)
        {
            p1 = new PointF(p1.X + distance.X, p1.Y + distance.Y);
            p2 = new PointF(p2.X+ distance.X, p2.Y + distance.Y);
        }

        public override void Resize(PointF newP)
        {
            p2 = new PointF(newP.X, newP.Y);
            p3 = new PointF(p1.X, p2.Y);
            p4 = new PointF(p2.X, p1.Y);
        }

        
    }
}
