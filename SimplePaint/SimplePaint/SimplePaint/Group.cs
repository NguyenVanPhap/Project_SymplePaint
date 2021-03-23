using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplePaint
{
    public class Group : clsDrawObject
    {
        private List<clsDrawObject> Shapes = new List<clsDrawObject>();
        public List<clsDrawObject>  SHAPES 
        {
            get {
                return Shapes; }
        }
        public clsDrawObject this[int index]
        {
            get => Shapes[index];
            set => Shapes[index] = value;
        }

        public void Add(clsDrawObject shape)
        {
            Shapes.Add(shape);
        }
        private GraphicsPath[] GraphicsPaths
        {
            get
            {
                GraphicsPath[] paths = new GraphicsPath[Shapes.Count];

                for (int i = 0; i < Shapes.Count; i++)
                {
                    GraphicsPath path = new GraphicsPath();
                    if (Shapes[i] is clsLine line)
                    {
                        path.AddLine(line.p1, line.p2);
                    }
                    else if (Shapes[i] is clsRectangle rect)
                    {
                        if (rect is clsSquare square)
                        {      
                            PointF Tam = new PointF();
                            Tam =square.p1; float a = square.p2.X - square.p1.X; float b = square.p2.Y - square.p1.Y;                         
                            float length = Math.Abs((square.p2.X - square.p1.X));
                            if (a < 0)
                                Tam.X = square.p1.X - length;
                            if (b < 0)
                                Tam.Y = square.p1.Y - length;
                            path.AddRectangle(new System.Drawing.RectangleF(Tam.X, Tam.Y, length, length));
                        }
                        else
                        {
                            PointF Tam = new PointF();
                            Tam =rect.p1;   float a = Math.Abs(rect.p2.X - rect.p1.X);    float b = Math.Abs(rect.p2.Y - rect.p1.Y);
                            if (rect.p1.X > rect.p2.X)
                                Tam.X = rect.p2.X;
                            if (rect.p1.Y > rect.p2.Y)
                                Tam.Y = rect.p2.Y;
                            path.AddRectangle(new System.Drawing.RectangleF(Tam.X, Tam.Y, a, b));
                        }
                    }
                    else if (Shapes[i] is clsEllipse ellipse)
                    {
                        if (ellipse is clsCircle circle)
                        {
                            float length;
                            PointF Tam = new Point();
                            Tam = circle.p1;
                            float a = circle.p2.X -circle.p1.X; float b = circle.p2.Y - circle.p1.Y;  
                            if (Math.Abs(a) <= Math.Abs(b))
                                length = Math.Abs(a);
                            else
                                length = Math.Abs(b);
                            if (a < 0)
                                Tam.X = circle.p1.X - length;
                            if (b < 0)
                                Tam.Y = circle.p1.Y - length;
                            path.AddEllipse(new System.Drawing.RectangleF(Tam.X, Tam.Y, length, length));         
                        }
                        else
                        {
                            path.AddEllipse(new System.Drawing.RectangleF(ellipse.p1.X, ellipse.p1.Y, ellipse.p2.X - ellipse.p1.X, ellipse.p2.Y - ellipse.p1.Y));
                        }
                    }
                    else if (Shapes[i] is clsCurve curve)
                    {
                        path.AddCurve(curve.Points.ToArray());
                    }
                    else if (Shapes[i] is clsPolygon polygon)
                    {
                        path.AddPolygon(polygon.PolygonPoints.ToArray());
                    }
                    else if (Shapes[i] is Group group)
                    {
                        for (int j = 0; j < group.GraphicsPaths.Length; j++)
                        {
                            path.AddPath(group.GraphicsPaths[j], false);
                        }
                    }
                    paths[i] = path;
                }

                return paths;
            }
        }

        public override void Draw(Graphics myGp)
        {
            GraphicsPath[] paths = GraphicsPaths;
            for (int i = 0; i < paths.Length; i++)
            {
                Shapes[i].Draw(myGp);
            }
        }

        public override bool IsHit(PointF point)
        {
            GraphicsPath[] paths = GraphicsPaths;
            for (int i = 0; i < paths.Length; i++)
            {
                using (GraphicsPath path = paths[i])
                {
                    if (Shapes[i] is clsEllipse|| Shapes[i] is clsSquare|| Shapes[i] is clsRectangle|| Shapes[i] is clsCircle||Shapes[i] is clsPolygon||Shapes[i] is clsCurve)
                    {
                        if (Shapes[i].Filled==true)
                        {
                            if (path.IsVisible(point))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (path.IsOutlineVisible(point, Shapes[i].myPen))
                            {
                                return true;
                            }
                        }
                    }
                    else if (!(Shapes[i] is Group))
                    {
                        if (path.IsOutlineVisible(point, Shapes[i].myPen))
                        {
                            return true;
                        }
                    }
                    else if (Shapes[i] is Group group)
                    {
                        return group.IsHit(point);
                    }
                }
            }

            return false;
        }

        public override void Move(PointF distance)
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                if (Shapes[i] is clsCurve curve)
                {
                    curve.Move(distance);
                }
                else 
                if (Shapes[i] is clsPolygon polygon)
                {
                    polygon.Move(distance);
                }
                else 
                if (Shapes[i] is Group group)
                {
                    group.Move(distance);
                }
                else
                {
                    Shapes[i].p1 = new PointF(Shapes[i].p1.X + distance.X, Shapes[i].p1.Y + distance.Y);
                    Shapes[i].p2 = new PointF(Shapes[i].p2.X + distance.X, Shapes[i].p2.Y + distance.Y);
                }
            }
            p1 = new PointF(p1.X + distance.X, p1.Y + distance.Y);
            p2 = new PointF(p2.X + distance.X, p2.Y + distance.Y);
            p3 = new PointF(p1.X, p2.Y);
            p4 = new PointF(p2.X, p1.Y);
        }

        

        private void Calculate(PointF distance, PointF begin, PointF end, PointF P,ref float X1,ref float Y1)
        {
            float rX = 0;
            float rY = 0;
            X1 = distance.X;
            Y1 = distance.Y;
            float KCX = Math.Abs(P.X - begin.X);
            float KCY = Math.Abs(P.Y - begin.Y);

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

        }
        public void Resize(PointF distance, PointF begin, PointF end)
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                float X1 = distance.X;
                float Y1 = distance.Y;
                if (Shapes[i] is clsCurve curve)
                {

                    for (int j = 0; j < curve.Points.Count; j++)
                    {
                        Calculate(distance, begin, end, curve.Points[j], ref X1, ref Y1);
                        if (curve.Points[j] != end)
                            curve.Points[j] = new PointF(curve.Points[j].X + X1, curve.Points[j].Y + Y1);
                    }
                }
                else if (Shapes[i] is clsPolygon polygon)
                {

                    for (int j = 0; j < polygon.PolygonPoints.Count; j++)
                    {

                        Calculate(distance, begin, end, polygon.PolygonPoints[j], ref X1, ref Y1);
                        if (polygon.PolygonPoints[j] != end)
                            polygon.PolygonPoints[j] = new PointF(polygon.PolygonPoints[j].X + X1,polygon.PolygonPoints[j].Y + Y1);
                    }
                }
                else if (Shapes[i] is Group group)
                {
                    group.Resize(distance,begin,end);
                }
                else
                {
                    Calculate(distance, begin, end, Shapes[i].p1, ref X1, ref Y1);
                    if (Shapes[i].p1 != end)
                        Shapes[i].p1 = new PointF(Shapes[i].p1.X + X1, Shapes[i].p1.Y + Y1);
                      

                    Calculate(distance, begin, end, Shapes[i].p2, ref X1, ref Y1);
                    
                    if (Shapes[i].p2 != end)
                        Shapes[i].p2 = new PointF(Shapes[i].p2.X + X1, Shapes[i].p2.Y +Y1);
                    
                }
            }
        }

        public override void Resize(PointF distance)
        {

        }
        public int Count => Shapes.Count;

        protected override GraphicsPath GraphicsPath => null;
    }
}
