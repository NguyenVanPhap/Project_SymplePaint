using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SymplePaint
{
    public class clsCircle : clsEllipse
    {
        public int Diameter { get; set; }
        public clsCircle()
        {
            GraphicsPath gp =GraphicsPath;
        }
        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();

                float length;
                PointF Tam = new Point();
                Tam = p1;
                float a = p2.X - p1.X;
                float b = p2.Y - p1.Y;
                
                length = (float)(Math.Abs(b) + Math.Abs(a)) / 2;

                if (a < 0)
                    Tam.X = p1.X - length;

                if (b < 0)
                    Tam.Y = p1.Y - length;

                path.AddEllipse(Tam.X, Tam.Y, length,length);
                if (a > 0)
                    this.p2.X = this.p1.X + length;
                else
                    this.p2.X = this.p1.X - length;

                if (b > 0)
                    this.p2.Y = this.p1.Y + length;
                else
                    this.p2.Y = this.p1.Y - length;


                p3.X = p1.X;
                p3.Y = p2.Y;
                p4.X = p2.X;
                p4.Y = p1.Y;
                return path;
            }
        }

      

        public override void Draw(Graphics myGp)
        {

            using(GraphicsPath path=GraphicsPath)
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


                if (this.Filled == false)
                    myGp.DrawPath(myPen, path);
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
            p2 = new PointF(p2.X + distance.X, p2.Y + distance.Y);
        }

        public override void Resize(PointF newP)
        {
            p2 = new PointF(newP.X, newP.Y);
            p3 = new PointF(p1.X, p2.Y);
            p4 = new PointF(p2.X, p1.Y);
        }
    }
}
