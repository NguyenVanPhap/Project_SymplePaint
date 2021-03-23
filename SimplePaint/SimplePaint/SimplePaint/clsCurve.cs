using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SymplePaint
{
    public class clsCurve : clsDrawObject
    {
        public List<PointF> Points { get; set; } = new List<PointF>();

        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddCurve(Points.ToArray());                
                return path;
            }
        }

        public override void Draw(Graphics myGp)
        {
            myPen = new Pen(myOutLineColor, width);
            myPen.DashStyle = DStyle;
            
            using (GraphicsPath path = GraphicsPath)
            {
                myGp.DrawPath(myPen, path);
            }
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
           
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = new PointF(Points[i].X + distance.X, Points[i].Y + distance.Y);
            }
        }
        public void Resize(PointF distance, PointF begin, PointF end)
        {
            
            for (int i = 0; i < Points.Count; i++)
            {
                float rX = 0;
                float rY = 0;
                float X1 = distance.X;
                float Y1 = distance.Y;
                float KCX = Math.Abs(Points[i].X - begin.X);
                float KCY = Math.Abs(Points[i].Y - begin.Y);

                float maxX = Math.Abs(end.X - begin.X);
                float maxY = Math.Abs(end.Y - begin.Y);
                if (maxX > 20 && maxY > 20)
                {
                    rX = (float)distance.X / maxX;
                    rY = (float)distance.Y / maxY;
                }
                else
                if (distance.X > 0 && distance.Y > 0)
                {
                    rX = (float)distance.X / maxX;
                    rY = (float)distance.Y / maxY;
                }

                if (rX > 10)
                    rX = 0;
                if (rY > 10)
                    rY = 0;

                X1 = (rX * KCX);
                Y1 = (rY * KCY);

                if (Points[i] != end)
                    Points[i] = new PointF(Points[i].X + X1, Points[i].Y + Y1);
            }
        }

        public override void Resize(PointF distance)
        {
            
        }
    }
}
