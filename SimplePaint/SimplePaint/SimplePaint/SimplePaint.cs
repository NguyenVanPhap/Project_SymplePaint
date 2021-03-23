using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics.Eventing.Reader;

namespace SymplePaint
{
    public partial class SimplePaint : Form
    {
        #region Property
        private Brush brush = new SolidBrush(Color.Blue);
        private int Getbrush;
        private Color myColor;
        private Color myFillColor;
        private int thick;
        private DashStyle Dstyle;
        private Point previousPoint;
        private System.Drawing.Rectangle selectedRegion;
        private bool isControlKeyPress=false;
        private bool isResizingShape = false;
        private bool isMovingShape = false;
        private bool IsDrawNothing = false;
        private bool bLine = false;
        private bool bRect = false;
        private bool bEcllipse = false;
        private bool bPolygon = false;
        private bool bCircle = false;
        private bool bSquare = false;
        private bool bCurve = false;
        private bool isMouseSelect;
        private bool isPress = false;
        private int isStartCurve = 0;
        private int isStartpoly = 0;
        private bool Fill;
        private int position = 2;
       
        private Pen framePen = new Pen(Color.Black, 1)
        {
            DashPattern = new float[] { 3, 3, 3, 3 },
            DashStyle = DashStyle.Custom
        };
        Image IMG = Image.FromFile(@"image\image.jpg");
        PointF Polybegin = new PointF();
        PointF PolyEnd = new PointF();
        clsDrawObject selectedShape;
        clsDrawObject ReSizeShape;
        List<clsDrawObject> lstObject = new List<clsDrawObject>();
        List<clsPolygon> Polygons = new List<clsPolygon>();
        List<clsCurve> LstCurve = new List<clsCurve>();
        List<Group> lstGroup=new List<Group>();
        #endregion
        public SimplePaint()
        {
            InitializeComponent();
            

            KeyPreview = true;
            myColor = Color.Black;
            myFillColor = Color.Black;
            thick = 1;
            Dstyle = DashStyle.Solid;
            cmbShapeMode.SelectedIndex = 0;
            cmbbrushStyle.SelectedIndex = 0;
            cmbDashMode.SelectedIndex = 0;
            cmbShapeMode.SelectedIndex = 0;
            Fill = false;  
            isMouseSelect = false;
        }

        #region Gọi Hàm
        /// <summary>
        /// Vẽ Các điểm nút
        /// </summary>
        private void drawObjectPoint(PointF p, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(brush, new System.Drawing.RectangleF(p.X - 4,p.Y - 4, 8, 8));
        }
        /// <summary>
        /// Tìm cái khung chứa đường cong này
        /// </summary>
        private void FindCurveRegion(clsCurve curve)
        {
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            curve.Points.ForEach(p =>
            {
                if (minX > p.X)
                {
                    minX = p.X;
                }
                if (minY > p.Y)
                {
                    minY = p.Y;
                }
                if (maxX < p.X)
                {
                    maxX = p.X;
                }
                if (maxY < p.Y)
                {
                    maxY = p.Y;
                }
            });
            curve.p1 = new PointF(minX-10, minY-10);
            curve.p2 = new PointF(maxX+10, maxY+10);
        }
        /// <summary>
        /// Tìm cái khung chứa đa giác này
        /// </summary>
        private void FindPolygonRegion( clsPolygon polygon)
        {
            double minX = double.MaxValue, minY = double.MaxValue, maxX = double.MinValue, maxY = double.MinValue;
            polygon.PolygonPoints.ForEach(p =>
            {
                if (minX > p.X)
                {
                    minX = p.X;
                }
                if (minY > p.Y)
                {
                    minY = p.Y;
                }
                if (maxX < p.X)
                {
                    maxX = p.X;
                }
                if (maxY < p.Y)
                {
                    maxY = p.Y;
                }
            });
            polygon.p1 = new Point((int)minX, (int)minY);
            polygon.p2 = new Point((int)maxX, (int)maxY);
        }
        /// <summary>
        /// Tìm cái khung chứa group này
        /// </summary>
        private void FindGroupRegion(Group group)
        {
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;

            for (int i = 0; i < group.Count; i++)
            {
                clsDrawObject shape = group[i];

                if (shape is clsCurve curve)
                {
                    FindCurveRegion(curve);
                }
                else if (shape is clsPolygon polygon)
                {
                    FindPolygonRegion(polygon);
                }
                else if(shape is Group group1)
                {
                    FindGroupRegion(group1);
                }    

                if (shape.p1.X < minX)
                {
                    minX = shape.p1.X;
                }
                if (shape.p2.X < minX)
                {
                    minX = shape.p2.X;
                }

                if (shape.p1.Y < minY)
                {
                    minY = shape.p1.Y;
                }
                if (shape.p2.Y < minY)
                {
                    minY = shape.p2.Y;
                }

                if (shape.p1.X > maxX)
                {
                    maxX = shape.p1.X;
                }
                if (shape.p2.X > maxX)
                {
                    maxX = shape.p2.X;
                }

                if (shape.p1.Y > maxY)
                {
                    maxY = shape.p1.Y;
                }
                if (shape.p2.Y > maxY)
                {
                    maxY = shape.p2.Y;
                }
            }
            group.p1 = new PointF(minX-5, minY-5);
            group.p2 = new PointF(maxX+5, maxY+5);
            group.p3 = new PointF(group.p1.X, group.p2.Y);
            group.p4 = new PointF(group.p2.X, group.p1.Y);
        }
        /// <summary>
        /// Thêm tượng vào danh sách
        /// </summary>
        void AddObject(ref Group group)
        {
            for (int i = 0; i < lstObject.Count; i++)
            {
                if (lstObject[i].IsSelected)
                {
                    group.Add(lstObject[i]);
                    lstObject.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < LstCurve.Count; i++)
            {
                if (LstCurve[i].IsSelected)
                {
                    group.Add(LstCurve[i]);
                    LstCurve.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < Polygons.Count; i++)
            {
                if (Polygons[i].IsSelected)
                {
                    group.Add(Polygons[i]);
                    Polygons.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < lstGroup.Count; i++)
            {
                if (lstGroup[i].IsSelected)
                {
                    group.Add(lstGroup[i]);
                    lstGroup.RemoveAt(i);
                    i--;
                }
            }

        }
        /// <summary>
        /// Kiểm tra 2 điểm có gần như khớp nhau không 
        /// </summary>
        bool isHitPoint(Point p1, PointF p2)
        {
            bool X = false;
            if (p1.X >= p2.X - 8 && p1.X <= p2.X + 8 && p1.Y >= p2.Y - 8 && p1.Y <= p2.Y + 8)
                X = true;
            return X;
        }
        /// <summary>
        /// Reset các khởi tạo
        /// </summary>
        private void unCheckAll()
        {
            IsDrawNothing = false;
            bLine = false;
            bRect = false;
            bEcllipse = false;
            bCircle = false;
            bSquare = false;

            bPolygon = false;
            bCurve = false;
            isPress = false;

            isStartpoly = 0;
            isStartCurve = 0;

        }
        #endregion

        #region Mouse_Event
        private void plMain_MouseDown(object sender, MouseEventArgs e)
        {
            this.isPress = true;
            this.isStartpoly += 1;
            this.isStartCurve += 1;
            #region Vẽ Hình   
            
            if (this.bLine == true)
            {
                clsDrawObject myObj;
                myObj = new clsLine();
                myObj.myPen = new Pen(myColor, thick);
                myObj.myPen.DashStyle = Dstyle;
                myObj.Filled = Fill;
                myObj.p1 = e.Location;
                myObj.myOutLineColor = myColor;
                myObj.width= thick;
                myObj.DStyle = Dstyle;
                this.lstObject.Add(myObj);
            }
            
            if (this.bCircle == true)
            {
                clsDrawObject myObj;
                myObj = new clsCircle();
                myObj.Filled = Fill;
                myObj.myOutLineColor = myColor;
                myObj.width = thick;
                myObj.myFillColor = myFillColor;
                myObj.DStyle = Dstyle;
                myObj.BrushTypes = Getbrush;
                myObj.p1 = e.Location;
                
                this.lstObject.Add(myObj);
            }
            if (this.bEcllipse == true)
            {
                clsDrawObject myObj;
                myObj = new clsEllipse();
                myObj.DStyle = Dstyle;
                myObj.Filled = Fill;
                myObj.myOutLineColor = myColor;
                myObj.width = thick;
                myObj.myFillColor = myFillColor;
                myObj.BrushTypes = Getbrush;
                myObj.p1 = e.Location;
               
                this.lstObject.Add(myObj);
            }
            if (this.bSquare == true)
            {
                clsDrawObject myObj;
                myObj = new clsSquare();
                
                myObj.myOutLineColor = myColor;
                myObj.width = thick;
                myObj.myFillColor = myFillColor;
                myObj.DStyle = Dstyle;
                myObj.BrushTypes = Getbrush;
                myObj.Filled = Fill;
                myObj.p1 = e.Location;
                this.lstObject.Add(myObj);
            }
            if (this.bRect == true)
            {
                clsDrawObject myObj;
                myObj = new clsRectangle();
                
                myObj.myOutLineColor = myColor;
                myObj.width = thick;
                myObj.myFillColor = myFillColor;
                myObj.DStyle = Dstyle;
                myObj.BrushTypes = Getbrush;
                myObj.Filled = Fill;
                myObj.p1 = e.Location;
                
                this.lstObject.Add(myObj);
            }
            if (this.bPolygon == true)
            {
                if (isStartpoly == 1)
                {
                    clsPolygon mypoly;
                    mypoly = new clsPolygon();
                    
                    mypoly.Filled = Fill;
                    mypoly.myOutLineColor = myColor;
                    mypoly.width = thick;
                    mypoly.myFillColor = myFillColor;
                    mypoly.DStyle = Dstyle;
                    mypoly.BrushTypes = Getbrush;
                    mypoly.PolygonPoints.Add(e.Location);
                   
                    this.Polygons.Add(mypoly);
                }
                this.Polygons[this.Polygons.Count - 1].PolygonPoints.Add(e.Location);

            }
            if (this.bCurve == true)
            {
                if (isStartCurve == 1)
                {
                    clsCurve myCurve;
                    myCurve = new clsCurve();
                   
                    myCurve.Filled = Fill;
                    myCurve.myOutLineColor = myColor;
                    myCurve.width = thick;
                    myCurve.myFillColor = myFillColor;
                    myCurve.DStyle = Dstyle;
                    myCurve.BrushTypes = Getbrush;
                   
                    myCurve.Points.Add(e.Location);
                    this.LstCurve.Add(myCurve);
                }
                this.LstCurve[this.LstCurve.Count - 1].Points.Add(e.Location);

            }
            #endregion;

            #region Không vẽ hình
            if (IsDrawNothing==true)
            {
                if (isControlKeyPress==true)
                {
                   
                    for (int i = 0; i < lstObject.Count; i++)
                    {
                        if (lstObject[i].IsHit(e.Location))
                        {
                            lstObject[i].IsSelected = true;      
                            plMain.Refresh();
                            
                        }
                    }
                    for (int i = 0; i < Polygons.Count; i++)
                    {
                        if (Polygons[i].IsHit(e.Location)&&bPolygon!=true)
                        {
                            Polygons[i].IsSelected = true;
                            plMain.Refresh();
                            
                        }
                    }
                    for (int i = 0; i < LstCurve.Count; i++)
                    {
                        if (LstCurve[i].IsHit(e.Location) && bCurve != true)
                        {
                            LstCurve[i].IsSelected = true;
                            plMain.Refresh();

                        }
                    }
                    for (int i = 0; i < lstGroup.Count; i++)
                    {
                        if (lstGroup[i].IsHit(e.Location))
                        {
                            lstGroup[i].IsSelected = true;
                            plMain.Refresh();

                        }
                    }
                }
                else
                {
                    LstCurve.ForEach(shape => shape.IsSelected = false);
                    lstObject.ForEach(shape => shape.IsSelected = false);
                    Polygons.ForEach(shape => shape.IsSelected = false);
                    lstGroup.ForEach(shape => shape.IsSelected = false);
                    plMain.Invalidate();
                    plMain.Refresh();


                    for (int i = 0; i < lstObject.Count; i++)
                    {
                        
                        if(isHitPoint(e.Location ,lstObject[i].p2)|| isHitPoint(e.Location, lstObject[i].p1)
                            || isHitPoint(e.Location, lstObject[i].p3) || isHitPoint(e.Location, lstObject[i].p4))
                        {
                            
                            ReSizeShape = lstObject[i];
                            lstObject[i].IsSelected = true;
                            lstObject[i].IsReSized = true;
                            plMain.Refresh();
                            break;
                            
                        }
                        if (lstObject[i].IsHit(e.Location))
                        {
                            selectedShape = lstObject[i];
                            lstObject[i].IsSelected = true;
                            plMain.Refresh();
                            break;
                        }
                       
                    }
                    for (int i = 0; i < Polygons.Count; i++)
                    {
                        FindPolygonRegion(Polygons[i]);
                        if (isHitPoint(e.Location,Polygons[i].p2)|| isHitPoint(e.Location, Polygons[i].p1))
                        {
                            if (isHitPoint(e.Location, Polygons[i].p1))
                                position = 1;
                            if (isHitPoint(e.Location, Polygons[i].p2))
                                position = 2;
                            ReSizeShape = Polygons[i];
                            Polygons[i].IsSelected = true;
                            Polygons[i].IsReSized = true;
                            plMain.Refresh();
                            break;

                        }
                        if (Polygons[i].IsHit(e.Location))
                        {
      
                            selectedShape = Polygons[i];
                            Polygons[i].IsSelected = true;
                            plMain.Refresh();
                            break;
                        }
                    }
                    for (int i = 0; i <LstCurve.Count; i++)
                    {
                        FindCurveRegion(LstCurve[i]);
                        if (isHitPoint(e.Location, LstCurve[i].p2)|| isHitPoint(e.Location, LstCurve[i].p1))
                        {
                            if (isHitPoint(e.Location, LstCurve[i].p1))
                                position = 1;
                            if (isHitPoint(e.Location, LstCurve[i].p2))
                                position = 2;
                            ReSizeShape = LstCurve[i];
                            LstCurve[i].IsSelected = true;
                            LstCurve[i].IsReSized = true;
                            plMain.Refresh();
                            break;

                        }
                        if (LstCurve[i].IsHit(e.Location))
                        {
                            selectedShape = LstCurve[i];
                            LstCurve[i].IsSelected = true;
                            plMain.Refresh();
                            break;
                        }
                       
                    }
                    for (int i = 0; i < lstGroup.Count; i++)
                    {
                        FindGroupRegion(lstGroup[i]);
                        if (isHitPoint(e.Location, lstGroup[i].p2))
                        {

                            ReSizeShape = lstGroup[i];
                            lstGroup[i].IsSelected = true;
                            lstGroup[i].IsReSized = true;
                            plMain.Refresh();
                            break;

                        }
                        if (lstGroup[i].IsHit(e.Location))
                        {
                            selectedShape = lstGroup[i];
                            lstGroup[i].IsSelected = true;
                            plMain.Refresh();
                            break;
                        }
                    }
                    if (selectedShape != null)
                    {
                        
                        isMovingShape = true;
                        
                        previousPoint = e.Location;
                    }
                    else
                    if(ReSizeShape!=null)
                    {
                        
                        isResizingShape = true;
                        previousPoint = e.Location;
                        if (position == 2)
                        {
                            Polybegin = ReSizeShape.p1;
                            PolyEnd = ReSizeShape.p2;
                        }
                        if(position==1)
                        {
                            Polybegin = ReSizeShape.p2;
                            PolyEnd = ReSizeShape.p1;
                        }    
                        
                    }
                    else
                    {
                        isMouseSelect = true;
                        selectedRegion = new System.Drawing.Rectangle(e.Location, new Size(0, 0));
                    }

                }
            }
            #endregion
        }
        private void plMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMovingShape == true)
            {
                Point d = new Point(e.X - previousPoint.X, e.Y - previousPoint.Y);
                selectedShape.Move(d);
                previousPoint = e.Location;
                plMain.Invalidate();
            }
            if(isResizingShape==true)
            {
                if (ReSizeShape is clsPolygon polygon)
                {
                    if(position==1)
                    {
                        PointF P = ReSizeShape.p1;
                        ReSizeShape.p1 = ReSizeShape.p2;
                        ReSizeShape.p2 = P;
                    }
                    PointF d = new PointF(e.X - previousPoint.X, e.Y - previousPoint.Y);
                    polygon.Resize(d, Polybegin, PolyEnd);
                    //FindPolygonRegion(polygon);
                    PolyEnd = polygon.p2;
                    previousPoint = e.Location;
                    plMain.Invalidate();
                }
                else if(ReSizeShape is Group group)
                {

                   
                    PointF d = new PointF(e.X - previousPoint.X, e.Y - previousPoint.Y);
                    group.Resize(d, Polybegin, PolyEnd);
                    FindGroupRegion(group);
                    PolyEnd = group.p2;
                    previousPoint = e.Location;
                    plMain.Invalidate();
                }
                else if(ReSizeShape is clsCurve curve)
                {
                    if (position == 1)
                    {
                        PointF P = ReSizeShape.p1;
                        ReSizeShape.p1 = ReSizeShape.p2;
                        ReSizeShape.p2 = P;
                    }
                    PointF d = new PointF(e.X - previousPoint.X, e.Y - previousPoint.Y);
                    curve.Resize(d, Polybegin, PolyEnd);
                   // FindCurveRegion(curve);
                    PolyEnd = curve.p2;
                    previousPoint = e.Location;
                    plMain.Invalidate();
                }    
                else
                {
                    if (isHitPoint(e.Location, ReSizeShape.p1))
                    {
                        ReSizeShape.p1 = ReSizeShape.p2;
                    }
                    if (isHitPoint(e.Location, ReSizeShape.p3))
                    {
                        ReSizeShape.p1 = ReSizeShape.p4;

                    }
                    if (isHitPoint(e.Location, ReSizeShape.p4))
                    {
                        ReSizeShape.p1 = ReSizeShape.p3;
                    }

                    ReSizeShape.Resize(e.Location);
                }
                
                plMain.Refresh();
            }
            if (IsDrawNothing == true)
            {
                if (isMouseSelect)
                {
                    selectedRegion.Width = e.Location.X - selectedRegion.X;
                    selectedRegion.Height = e.Location.Y - selectedRegion.Y;

                    plMain.Refresh();
                }
                else
                {
                    plMain.Cursor = Cursors.Default;
                    foreach (clsDrawObject shape in lstObject)
                    {
                        
                        if (isHitPoint(e.Location, shape.p2)|| isHitPoint(e.Location, shape.p1)
                            ||isHitPoint(e.Location, shape.p3) || isHitPoint(e.Location, shape.p4))
                        {
                            plMain.Cursor = Cursors.Hand;
                        }
                        if (shape.IsHit(e.Location))
                            plMain.Cursor = Cursors.SizeAll;
                    }
                    foreach (clsPolygon shape in Polygons)
                    {
                        FindPolygonRegion(shape);
                        if (isHitPoint(e.Location, shape.p2)|| isHitPoint(e.Location, shape.p1))
                        {
                            plMain.Cursor = Cursors.Hand;
                        }
                        if (shape.IsHit(e.Location))
                            plMain.Cursor = Cursors.SizeAll;
                    }
                    foreach (Group shape in lstGroup)
                    {
                        FindGroupRegion(shape);
                        if (isHitPoint(e.Location, shape.p2) )
                        {
                            plMain.Cursor = Cursors.Hand;
                        }
                        if (shape.IsHit(e.Location))
                            plMain.Cursor = Cursors.SizeAll;
                    }
                    foreach (clsCurve shape in LstCurve)
                    {
                        FindCurveRegion(shape);
                        if (isHitPoint(e.Location, shape.p2) || isHitPoint(e.Location, shape.p1))
                        {
                            plMain.Cursor = Cursors.Hand;
                        }
                        if (shape.IsHit(e.Location))
                            plMain.Cursor = Cursors.SizeAll;
                    }
                    if (Polygons.Exists(shape => shape.IsHit(e.Location)) || LstCurve.Exists(shape => shape.IsHit(e.Location)) || lstGroup.Exists(shape => shape.IsHit(e.Location)))                    
                        plMain.Cursor = Cursors.SizeAll;
                }
            }
            if (this.isPress == true)
            {
                if (bLine == true || bEcllipse == true || bRect == true || bSquare == true)
                {

                    this.lstObject[this.lstObject.Count - 1].p2 = e.Location;
                    
                    this.plMain.Refresh();
                }
                if (bCircle == true)
                {

                    if (this.lstObject[this.lstObject.Count - 1] is clsCircle)
                    {

                        this.lstObject[this.lstObject.Count - 1].p2 = e.Location;
                        clsDrawObject X = this.lstObject[this.lstObject.Count - 1];

                        plMain.Refresh();
                    }
                }
                if (isStartpoly > 1)
                    if (bPolygon == true)
                    {
                        this.Polygons[this.Polygons.Count - 1].PolygonPoints[this.Polygons[this.Polygons.Count - 1].PolygonPoints.Count - 1] = e.Location;
                        this.plMain.Refresh();
                    }
                if (isStartCurve > 1)
                    if (bCurve == true)
                    {
                        this.LstCurve[this.LstCurve.Count - 1].Points[this.LstCurve[this.LstCurve.Count - 1].Points.Count - 1] = e.Location;
                        this.plMain.Refresh();
                    }
            }
        }
        private void plMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMovingShape)
            {
                isMovingShape = false;
                selectedShape = null;
            }
            if(isResizingShape)
            {
                isResizingShape = false;
                ReSizeShape = null;
                selectedShape = null;
            }
            if (isMouseSelect)
            {
                isMouseSelect = false;
                for (int i = 0; i < lstObject.Count; i++)
                { 
                    lstObject[i].IsSelected = false;
                    if (lstObject[i].p1.X >= selectedRegion.X && lstObject[i].p2.X <= selectedRegion.X + selectedRegion.Width && lstObject[i].p1.Y >= selectedRegion.Y && lstObject[i].p2.Y <= selectedRegion.Y + selectedRegion.Height) 
                        lstObject[i].IsSelected = true;
                }
                for (int i = 0; i <LstCurve.Count; i++)
                {
                    LstCurve[i].IsSelected = false;
                    if (LstCurve[i].p1.X >= selectedRegion.X && LstCurve[i].p2.X <= selectedRegion.X + selectedRegion.Width && LstCurve[i].p1.Y >= selectedRegion.Y && LstCurve[i].p2.Y <= selectedRegion.Y + selectedRegion.Height)
                        LstCurve[i].IsSelected = true;
                }
                for (int i = 0; i < Polygons.Count; i++)
                {
                    Polygons[i].IsSelected = false;
                    if (Polygons[i].p1.X >= selectedRegion.X && Polygons[i].p2.X <= selectedRegion.X + selectedRegion.Width && Polygons[i].p1.Y >= selectedRegion.Y && Polygons[i].p2.Y <= selectedRegion.Y + selectedRegion.Height) 
                        Polygons[i].IsSelected = true;        
                }
                for (int i = 0; i < lstGroup.Count; i++)
                {
                    lstGroup[i].IsSelected = false;
                    if (lstGroup[i].p1.X >= selectedRegion.X && lstGroup[i].p2.X <= selectedRegion.X + selectedRegion.Width && lstGroup[i].p1.Y >= selectedRegion.Y && lstGroup[i].p2.Y <= selectedRegion.Y + selectedRegion.Height)
                        lstGroup[i].IsSelected = true;
                }

                plMain.Refresh();
            }
            if (bLine == true||bEcllipse==true||bRect==true||bCircle==true||bSquare==true)
            {

                this.lstObject[this.lstObject.Count - 1].p2 = e.Location;
                selectedShape = null;
                this.plMain.Refresh();
                isStartpoly = 0;
            }
            
            this.bLine = false;
            this.bEcllipse = false;
            this.bRect = false;
            this.bCircle = false;
            this.bSquare = false;
            this.isPress = false;
            IsDrawNothing = true;
           
            if (bPolygon==true)
            {
                this.Polygons[this.Polygons.Count - 1].PolygonPoints[this.Polygons[this.Polygons.Count - 1].PolygonPoints.Count-1]=e.Location;
                this.plMain.Refresh();
                IsDrawNothing = false;
            }
            if (bCurve== true)
            {
                this.LstCurve[this.LstCurve.Count - 1].Points[this.LstCurve[this.LstCurve.Count - 1].Points.Count - 1] = e.Location;
                this.plMain.Refresh();
                IsDrawNothing = false;
            }

        }
        #endregion

        #region Main Panel
        private void plMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            lstObject.ForEach(lstObject =>
            {
                lstObject.Draw(e.Graphics);
                if (lstObject.IsSelected==true)
                {
                    
                    if (lstObject is clsCircle)
                    {
                        float length;
                        PointF Tam = new PointF();
                        Tam = lstObject.p1;
                        float a = lstObject.p2.X - lstObject.p1.X;
                        float b =lstObject.p2.Y -lstObject.p1.Y;
                        
                        length = (float)(Math.Abs(b) + Math.Abs(a)) / 2;

                        if (a < 0)
                            Tam.X = lstObject.p1.X - length;

                        if (b < 0)
                            Tam.Y =lstObject.p1.Y - length;
                        
                       
                        e.Graphics.DrawRectangle(framePen, Tam.X, Tam.Y, length, length);
                        drawObjectPoint(lstObject.p1, e); drawObjectPoint(lstObject.p3, e);
                        drawObjectPoint(lstObject.p2, e); drawObjectPoint(lstObject.p4, e);
                    }
                    else
                    if (lstObject is clsEllipse)
                    {
                        PointF Tam = new PointF();
                        Tam = lstObject.p1;
                        float a = Math.Abs(lstObject.p2.X -lstObject.p1.X);
                        float b = Math.Abs(lstObject.p2.Y -lstObject.p1.Y);
                        if (lstObject.p1.X >lstObject.p2.X)
                            Tam.X =lstObject.p2.X;
                        if (lstObject.p1.Y >lstObject.p2.Y)
                            Tam.Y =lstObject.p2.Y;
                        e.Graphics.DrawRectangle(framePen, Tam.X, Tam.Y, a,b);
                        drawObjectPoint(lstObject.p1, e); drawObjectPoint(lstObject.p3, e);
                        drawObjectPoint(lstObject.p2, e); drawObjectPoint(lstObject.p4, e);
                    }
                    if (lstObject is clsLine)
                    {
                        drawObjectPoint(lstObject.p1, e); drawObjectPoint(lstObject.p2, e);
                    }
                    if ( lstObject is clsRectangle)
                    {
                        drawObjectPoint(lstObject.p1, e); drawObjectPoint(lstObject.p3, e);
                        drawObjectPoint(lstObject.p2, e); drawObjectPoint(lstObject.p4, e);
                    }

                }
            });
         
            Polygons.ForEach(shape =>
            {
                if (shape.IsSelected)
                {
                    shape.Draw(e.Graphics);
                    if (shape is clsPolygon polygon)
                    {
                        for (int i = 0; i < polygon.PolygonPoints.Count; i++)
                        {
                            e.Graphics.FillEllipse(brush, polygon.PolygonPoints[i].X - 4, polygon.PolygonPoints[i].Y - 4, 8, 8);
                        }
                        FindPolygonRegion(polygon);
                        drawObjectPoint(polygon.p1, e);
                        drawObjectPoint(polygon.p2, e);
                    }
                    
                    e.Graphics.DrawRectangle(framePen,shape.p1.X, shape.p1.Y, shape.p2.X - shape.p1.X, shape.p2.Y - shape.p1.Y);
                }
                shape.Draw(e.Graphics);
            });
            LstCurve.ForEach(shape =>
            {
                if (shape.IsSelected)
                {
                    shape.Draw(e.Graphics);
                    if (shape is clsCurve curve)
                    {
                        for (int i = 0; i < curve.Points.Count; i++)
                        {
                            e.Graphics.FillEllipse(brush,curve.Points[i].X - 4, curve.Points[i].Y - 4, 8, 8);
                        }
                    }
                    FindCurveRegion(shape);
                    drawObjectPoint(shape.p2, e);
                    drawObjectPoint(shape.p1, e);
                    e.Graphics.DrawRectangle(framePen, shape.p1.X, shape.p1.Y, shape.p2.X - shape.p1.X, shape.p2.Y - shape.p1.Y);
                }
                shape.Draw(e.Graphics);
            });
            lstGroup.ForEach(shape =>
            {
                if (shape.IsSelected)
                {
                    shape.Draw(e.Graphics);
                    FindGroupRegion(shape);
                    e.Graphics.DrawRectangle(framePen, shape.p1.X, shape.p1.Y, shape.p2.X-shape.p1.X, shape.p2.Y - shape.p1.Y);
                    drawObjectPoint(shape.p1, e); drawObjectPoint(shape.p2, e);
                    drawObjectPoint(shape.p3, e); drawObjectPoint(shape.p4, e);
                }
                shape.Draw(e.Graphics);
            });

            if (isMouseSelect)
            {
                e.Graphics.DrawRectangle(framePen, selectedRegion);
            }

        }
    
        private void plMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            isMouseSelect =false;
            if (bPolygon == true)
            {
                this.Polygons[this.Polygons.Count - 1].PolygonPoints[this.Polygons[this.Polygons.Count - 1].PolygonPoints.Count - 1] = e.Location;
                FindPolygonRegion(Polygons[this.Polygons.Count - 1]);
                this.plMain.Refresh();
                this.isStartpoly = 0;
                this.bPolygon = false;
                unCheckAll();
            }
            if (bCurve == true)
            {
                this.LstCurve[this.LstCurve.Count - 1].Points[this.LstCurve[this.LstCurve.Count - 1].Points.Count - 1] = e.Location;
                this.plMain.Refresh();
                this.isStartCurve = 0;
                this.bCurve = false;
                unCheckAll();
            }
            
        }
        #endregion

        #region EventCombobox
        private void cmbDashMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dstyle = (DashStyle)cmbDashMode.SelectedIndex;
            lstObject.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.DStyle = (DashStyle)cmbDashMode.SelectedIndex; });
            LstCurve.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.DStyle = (DashStyle)cmbDashMode.SelectedIndex; });
            Polygons.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.DStyle = (DashStyle)cmbDashMode.SelectedIndex; });
            lstGroup.FindAll(shape => shape.IsSelected).ForEach(shape => {
                foreach (clsDrawObject s in shape.SHAPES)
                {
                    if (s is Group s1)
                    {
                        foreach (clsDrawObject s2 in s1.SHAPES)
                        {
                            s2.DStyle = (DashStyle)cmbDashMode.SelectedIndex;
                        }
                    }
                    s.DStyle = (DashStyle)cmbDashMode.SelectedIndex;
                }
            });
            plMain.Invalidate();
        }
        private void cmbShapeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbShapeMode.SelectedIndex == 0)
            {
                Fill = false;
            }
            else if (cmbShapeMode.SelectedIndex == 1)
            {
                Fill = true;
            }
        }

        private void cmbbrushStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Getbrush = cmbbrushStyle.SelectedIndex;
            lstObject.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.BrushTypes = cmbbrushStyle.SelectedIndex; });
            LstCurve.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.BrushTypes = cmbbrushStyle.SelectedIndex; });
            Polygons.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.BrushTypes = cmbbrushStyle.SelectedIndex; });
            lstGroup.FindAll(shape => shape.IsSelected).ForEach(shape => {
                foreach (clsDrawObject s in shape.SHAPES)
                {
                    if (s is Group s1)
                    {
                        foreach (clsDrawObject s2 in s1.SHAPES)
                        {
                            s2.BrushTypes = cmbbrushStyle.SelectedIndex;
                        }
                    }
                    else
                        s.BrushTypes = cmbbrushStyle.SelectedIndex;
                }
            });
            plMain.Invalidate();

        }
        #endregion

        #region EventClick
        private void btnPolygons_Click(object sender, EventArgs e)
        {
            unCheckAll();
            bPolygon = true;
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            unCheckAll();
            this.bLine = true;
        }
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            unCheckAll();
            this.bEcllipse = true;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            unCheckAll();
            bRect = true;
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            unCheckAll();
            bSquare = true;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            unCheckAll();
            bCircle = true;
        }
        private void btnCurve_Click(object sender, EventArgs e)
        {
            unCheckAll();
            bCurve = true;
        }
        private void btnGroup_Click(object sender, EventArgs e)
        {
            int SL = lstObject.Count + LstCurve.Count + Polygons.Count + lstGroup.Count;
            if (SL > 1)
            {
                Group group = new Group();
                AddObject(ref group);
                FindGroupRegion(group);
                lstGroup.Add(group);
                group.IsSelected = true;
                plMain.Invalidate();
            }
        }
     
        private void btnDelete_Click(object sender, EventArgs e)
        {

            
            for (int i = 0; i < lstObject.Count; i++)
            {
                if (lstObject[i].IsSelected)
                {
                    lstObject.RemoveAt(i);
                    i--;
                    plMain.Refresh();

                }
            }
           
            for (int i = 0; i < Polygons.Count; i++)
            {
                if (Polygons[i].IsSelected)
                {
                    Polygons.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < LstCurve.Count; i++)
            {
                if (LstCurve[i].IsSelected)
                {
                    LstCurve.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < lstGroup.Count; i++)
            {
                if (lstGroup[i].IsSelected)
                {
                    lstGroup.RemoveAt(i);
                    i--;
                    
                }
            }
            plMain.Refresh();
            
        }
        private void btnUnGroup_Click(object sender, EventArgs e)
        {
            if (lstGroup.Find(shape => shape.IsSelected) is Group selectedGroup)
            {
                foreach (clsDrawObject shape in selectedGroup.SHAPES)
                {
                    if (shape is clsPolygon)
                    {
                        clsPolygon X = (clsPolygon)shape;

                        Polygons.Add(X);
                    }
                    else if (shape is clsCurve)
                    {
                        LstCurve.Add((clsCurve)shape);
                    }
                    else if (shape is Group)
                    {
                        lstGroup.Add((Group)shape);
                    }
                    else
                    {
                        lstObject.Add(shape);
                    }
                }
                lstGroup.Remove(selectedGroup);

            }
            plMain.Invalidate();
        }
       
        private void btnOutLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                btnOutLineColor.BackColor = color.Color;
            }
            myColor = btnOutLineColor.BackColor;
            lstObject.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.myOutLineColor = btnOutLineColor.BackColor; });
            LstCurve.FindAll(shape => shape.IsSelected).ForEach(shape =>{shape.myOutLineColor = btnOutLineColor.BackColor; });
            Polygons.FindAll(shape => shape.IsSelected).ForEach(shape =>{ shape.myOutLineColor = btnOutLineColor.BackColor; });
            lstGroup.FindAll(shape => shape.IsSelected).ForEach(shape =>
            {
                foreach (clsDrawObject s in shape.SHAPES)
                {
                    if (s is Group s1)
                    {
                        foreach (clsDrawObject s2 in s1.SHAPES)
                        {
                            s2.myOutLineColor = btnOutLineColor.BackColor;
                        }
                    }
                    else
                        s.myOutLineColor = btnOutLineColor.BackColor;
                }
            });
            plMain.Invalidate();
        }

        private void btnfillColor_Click(object sender, EventArgs e)
        {
            
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                btnfillColor.BackColor = color.Color;
            }
            myFillColor = btnfillColor.BackColor;
            if(btnfillColor.BackColor!=Color.White)
                cmbShapeMode.SelectedIndex = 1;
            else
                cmbShapeMode.SelectedIndex = 0;
            lstObject.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.Filled = true;if (btnfillColor.BackColor == Color.White) { shape.Filled = false; } shape.myFillColor = btnfillColor.BackColor;});
            LstCurve.FindAll(shape => shape.IsSelected).ForEach(shape =>  { shape.Filled = true; if (btnfillColor.BackColor == Color.White) { shape.Filled = false;  } shape.myFillColor = btnfillColor.BackColor; });
            Polygons.FindAll(shape => shape.IsSelected).ForEach(shape =>  { shape.Filled = true; if (btnfillColor.BackColor == Color.White) { shape.Filled = false; } shape.myFillColor = btnfillColor.BackColor;});
            lstGroup.FindAll(shape => shape.IsSelected).ForEach(shape =>  {
                foreach (clsDrawObject s in shape.SHAPES)
                {
                    if (s is Group s1)
                    {
                        foreach (clsDrawObject s2 in s1.SHAPES)
                        {
                            s2.Filled = true;
                            if (btnfillColor.BackColor == Color.White) s2.Filled = false;
                            s2.myFillColor = btnfillColor.BackColor;
                        }
                    }
                    else
                        s.myFillColor = btnfillColor.BackColor;
                    s.Filled = true;
                    if (btnfillColor.BackColor == Color.White) s.Filled = false;
                    
                    
                }
            });
            plMain.Invalidate();
        }

        #endregion

        #region EventKey
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            isControlKeyPress = e.Control;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            isControlKeyPress = e.Control;
        }



        #endregion

        #region Wild

        private void trbLineWild_Scroll(object sender, EventArgs e)
        {
            if (trbLineWidth.Value == 0)
            {
                lblWidth.Text = "Default";
            }
            else
            {
                lblWidth.Text = trbLineWidth.Value.ToString();
            }
            thick = trbLineWidth.Value;
            lstObject.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.width=thick; });
            LstCurve.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.width = thick; });
            Polygons.FindAll(shape => shape.IsSelected).ForEach(shape => { shape.width = thick; });
            lstGroup.FindAll(shape => shape.IsSelected).ForEach(shape =>
            {
                foreach (clsDrawObject s in shape.SHAPES)
                {
                    if (s is Group s1)
                    {
                        foreach (clsDrawObject s2 in s1.SHAPES)
                        {
                            s2.width = thick;
                        }
                    }
                    else
                        s.width = thick;
                }
            });
            plMain.Invalidate();
        }
        #endregion
    }
}
