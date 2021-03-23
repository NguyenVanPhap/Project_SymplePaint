using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
namespace SymplePaint
{
    
    class clsPolygon : clsDrawObject
    {
        public List<PointF> PolygonPoints { get; set; } = new List<PointF>();

        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                if (PolygonPoints.Count < 3)
                {
                    path.AddLine(PolygonPoints[0], PolygonPoints[1]);
                }
                else
                {
                    path.AddPolygon(PolygonPoints.ToArray());
                }
                
                return path;
            }
        }

       
        public override void Draw(Graphics myGp)
        {
            if (PolygonPoints.Count != 0)
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
                    myGp.DrawPolygon(myPen, PolygonPoints.ToArray());
                else
                {
                    myGp.FillPolygon(myBrush, PolygonPoints.ToArray());
                    myGp.DrawPolygon(myPen, PolygonPoints.ToArray());
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
           
            for (int i = 0; i < PolygonPoints.Count; i++)
            {
                PolygonPoints[i] = new PointF(PolygonPoints[i].X + distance.X, PolygonPoints[i].Y + distance.Y);
            }
        }
        public override void Resize(PointF distance)
        {
           
        }
        public  void Resize(PointF distance,PointF begin,PointF end)
        {
            //end : điểm góc dưới phải của khung
            //begin : điểm góc trên trái của khung
            
            for (int i = 0; i < PolygonPoints.Count; i++)
            {
                float rX = 0;
                float rY = 0;
                float X1 = distance.X;
                float Y1 = distance.Y;
                float KCX = Math.Abs(PolygonPoints[i].X - begin.X);
                float KCY = Math.Abs(PolygonPoints[i].Y - begin.Y);

                float maxX = Math.Abs(end.X - begin.X);
                float maxY = Math.Abs(end.Y - begin.Y);
                if (maxX > 15 && maxY > 15)
                {
                    rX = (float)distance.X / maxX;
                    rY = (float)distance.Y / maxY;
                }
                else
                if(distance.X>0&&distance.Y>0)
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

                if (PolygonPoints[i] != end)
                    PolygonPoints[i] = new PointF(PolygonPoints[i].X + X1, PolygonPoints[i].Y + Y1);
            }      
        }

    }
}
