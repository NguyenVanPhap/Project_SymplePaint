using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace SymplePaint
{
    public class clsEllipse : clsDrawObject
    {
        
        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                p3.X = p1.X;
                p3.Y = p2.Y;
                p4.X = p2.X;
                p4.Y = p1.Y;
                return path;
                
            }
        }

        public override void Draw(Graphics myGp)
        {
            myPen = new Pen(myOutLineColor, width);
            myPen.Alignment = PenAlignment.Inset;
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

            if (this.Filled == false)
                myGp.DrawEllipse(myPen, this.p1.X, this.p1.Y, this.p2.X - this.p1.X, this.p2.Y - this.p1.Y);
            else
            {
                myGp.FillEllipse(myBrush, this.p1.X, this.p1.Y, this.p2.X - this.p1.X, this.p2.Y - this.p1.Y);
                myGp.DrawEllipse(myPen, this.p1.X, this.p1.Y, this.p2.X - this.p1.X, this.p2.Y - this.p1.Y);
                
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
            p2 = new PointF(p2.X + distance.X, p2.Y + distance.Y);
        }

        public override void Resize(PointF newP)
        {
            p2 = new PointF( newP.X,newP.Y);
            p3 = new PointF(p1.X, p2.Y);
            p4 = new PointF(p2.X, p1.Y);
        }

       
    }
}
