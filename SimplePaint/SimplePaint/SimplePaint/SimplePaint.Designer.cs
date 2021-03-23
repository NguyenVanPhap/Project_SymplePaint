namespace SymplePaint
{
    partial class SimplePaint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimplePaint));
            this.plControl = new System.Windows.Forms.Panel();
            this.lblWidth = new System.Windows.Forms.Label();
            this.trbLineWidth = new System.Windows.Forms.TrackBar();
            this.lblFillColor = new System.Windows.Forms.Label();
            this.btnOutLineColor = new System.Windows.Forms.Button();
            this.lblOutLine = new System.Windows.Forms.Label();
            this.btnUnGroup = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.cmbbrushStyle = new System.Windows.Forms.ComboBox();
            this.cmbShapeMode = new System.Windows.Forms.ComboBox();
            this.cmbDashMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnfillColor = new System.Windows.Forms.Button();
            this.plShape = new System.Windows.Forms.Panel();
            this.btnCurve = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnPolygons = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnSquare = new System.Windows.Forms.Button();
            this.plMain = new SymplePaint.DoubleBufferPanel();
            this.plControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLineWidth)).BeginInit();
            this.plShape.SuspendLayout();
            this.SuspendLayout();
            // 
            // plControl
            // 
            this.plControl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.plControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.plControl.Controls.Add(this.lblWidth);
            this.plControl.Controls.Add(this.trbLineWidth);
            this.plControl.Controls.Add(this.lblFillColor);
            this.plControl.Controls.Add(this.btnOutLineColor);
            this.plControl.Controls.Add(this.lblOutLine);
            this.plControl.Controls.Add(this.btnUnGroup);
            this.plControl.Controls.Add(this.btnDelete);
            this.plControl.Controls.Add(this.btnGroup);
            this.plControl.Controls.Add(this.cmbbrushStyle);
            this.plControl.Controls.Add(this.cmbShapeMode);
            this.plControl.Controls.Add(this.cmbDashMode);
            this.plControl.Controls.Add(this.label1);
            this.plControl.Controls.Add(this.btnfillColor);
            this.plControl.Controls.Add(this.plShape);
            this.plControl.Location = new System.Drawing.Point(1, 4);
            this.plControl.Name = "plControl";
            this.plControl.Size = new System.Drawing.Size(248, 737);
            this.plControl.TabIndex = 1;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblWidth.Location = new System.Drawing.Point(13, 247);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(52, 20);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "Width";
            // 
            // trbLineWidth
            // 
            this.trbLineWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.trbLineWidth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trbLineWidth.LargeChange = 1;
            this.trbLineWidth.Location = new System.Drawing.Point(92, 235);
            this.trbLineWidth.Minimum = 1;
            this.trbLineWidth.Name = "trbLineWidth";
            this.trbLineWidth.Size = new System.Drawing.Size(138, 56);
            this.trbLineWidth.TabIndex = 0;
            this.trbLineWidth.Value = 1;
            this.trbLineWidth.Scroll += new System.EventHandler(this.trbLineWild_Scroll);
            // 
            // lblFillColor
            // 
            this.lblFillColor.AutoSize = true;
            this.lblFillColor.Location = new System.Drawing.Point(14, 142);
            this.lblFillColor.Name = "lblFillColor";
            this.lblFillColor.Size = new System.Drawing.Size(88, 17);
            this.lblFillColor.TabIndex = 25;
            this.lblFillColor.Text = "FILL COLOR";
            // 
            // btnOutLineColor
            // 
            this.btnOutLineColor.BackColor = System.Drawing.Color.Black;
            this.btnOutLineColor.Location = new System.Drawing.Point(190, 176);
            this.btnOutLineColor.Name = "btnOutLineColor";
            this.btnOutLineColor.Size = new System.Drawing.Size(40, 40);
            this.btnOutLineColor.TabIndex = 0;
            this.btnOutLineColor.UseVisualStyleBackColor = false;
            this.btnOutLineColor.Click += new System.EventHandler(this.btnOutLineColor_Click);
            // 
            // lblOutLine
            // 
            this.lblOutLine.AutoSize = true;
            this.lblOutLine.Location = new System.Drawing.Point(14, 188);
            this.lblOutLine.Name = "lblOutLine";
            this.lblOutLine.Size = new System.Drawing.Size(125, 17);
            this.lblOutLine.TabIndex = 0;
            this.lblOutLine.Text = "OUT LINE COLOR";
            // 
            // btnUnGroup
            // 
            this.btnUnGroup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUnGroup.Location = new System.Drawing.Point(13, 478);
            this.btnUnGroup.Name = "btnUnGroup";
            this.btnUnGroup.Size = new System.Drawing.Size(94, 35);
            this.btnUnGroup.TabIndex = 24;
            this.btnUnGroup.TabStop = false;
            this.btnUnGroup.Text = "UnGroup";
            this.btnUnGroup.UseVisualStyleBackColor = false;
            this.btnUnGroup.Click += new System.EventHandler(this.btnUnGroup_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDelete.Location = new System.Drawing.Point(126, 437);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 35);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGroup.Location = new System.Drawing.Point(13, 437);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(95, 35);
            this.btnGroup.TabIndex = 0;
            this.btnGroup.TabStop = false;
            this.btnGroup.Text = "Group";
            this.btnGroup.UseVisualStyleBackColor = false;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // cmbbrushStyle
            // 
            this.cmbbrushStyle.FormattingEnabled = true;
            this.cmbbrushStyle.Items.AddRange(new object[] {
            "SolidBrush",
            "LargeCheckerBoard",
            "LargeConfetti",
            "LightUpwardDiagonal"});
            this.cmbbrushStyle.Location = new System.Drawing.Point(13, 398);
            this.cmbbrushStyle.Name = "cmbbrushStyle";
            this.cmbbrushStyle.Size = new System.Drawing.Size(217, 24);
            this.cmbbrushStyle.TabIndex = 0;
            this.cmbbrushStyle.TabStop = false;
            this.cmbbrushStyle.SelectedIndexChanged += new System.EventHandler(this.cmbbrushStyle_SelectedIndexChanged);
            // 
            // cmbShapeMode
            // 
            this.cmbShapeMode.FormattingEnabled = true;
            this.cmbShapeMode.Items.AddRange(new object[] {
            "No fill shape",
            "Fill shape"});
            this.cmbShapeMode.Location = new System.Drawing.Point(12, 355);
            this.cmbShapeMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbShapeMode.Name = "cmbShapeMode";
            this.cmbShapeMode.Size = new System.Drawing.Size(218, 24);
            this.cmbShapeMode.TabIndex = 22;
            this.cmbShapeMode.SelectedIndexChanged += new System.EventHandler(this.cmbShapeMode_SelectedIndexChanged);
            // 
            // cmbDashMode
            // 
            this.cmbDashMode.FormattingEnabled = true;
            this.cmbDashMode.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "Dot",
            "Dash Dot",
            "Dash Dot Dot"});
            this.cmbDashMode.Location = new System.Drawing.Point(92, 314);
            this.cmbDashMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDashMode.Name = "cmbDashMode";
            this.cmbDashMode.Size = new System.Drawing.Size(138, 24);
            this.cmbDashMode.TabIndex = 21;
            this.cmbDashMode.SelectedIndexChanged += new System.EventHandler(this.cmbDashMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "DashStyle";
            // 
            // btnfillColor
            // 
            this.btnfillColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnfillColor.Location = new System.Drawing.Point(190, 130);
            this.btnfillColor.Name = "btnfillColor";
            this.btnfillColor.Size = new System.Drawing.Size(40, 40);
            this.btnfillColor.TabIndex = 1;
            this.btnfillColor.UseVisualStyleBackColor = false;
            this.btnfillColor.Click += new System.EventHandler(this.btnfillColor_Click);
            // 
            // plShape
            // 
            this.plShape.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.plShape.Controls.Add(this.btnCurve);
            this.plShape.Controls.Add(this.btnCircle);
            this.plShape.Controls.Add(this.btnRectangle);
            this.plShape.Controls.Add(this.btnPolygons);
            this.plShape.Controls.Add(this.btnEllipse);
            this.plShape.Controls.Add(this.btnLine);
            this.plShape.Controls.Add(this.btnSquare);
            this.plShape.Location = new System.Drawing.Point(13, 13);
            this.plShape.Name = "plShape";
            this.plShape.Size = new System.Drawing.Size(217, 100);
            this.plShape.TabIndex = 10;
            // 
            // btnCurve
            // 
            this.btnCurve.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCurve.BackgroundImage = global::SymplePaint.Properties.Resources.LVktLcI;
            this.btnCurve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCurve.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnCurve.Location = new System.Drawing.Point(113, 56);
            this.btnCurve.Name = "btnCurve";
            this.btnCurve.Size = new System.Drawing.Size(40, 40);
            this.btnCurve.TabIndex = 12;
            this.btnCurve.TabStop = false;
            this.btnCurve.UseVisualStyleBackColor = false;
            this.btnCurve.Click += new System.EventHandler(this.btnCurve_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCircle.BackgroundImage = global::SymplePaint.Properties.Resources.pngtree_blue_hand_drawn_cartoon_round_background_png_image_3675739;
            this.btnCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCircle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnCircle.Location = new System.Drawing.Point(67, 57);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(40, 40);
            this.btnCircle.TabIndex = 11;
            this.btnCircle.TabStop = false;
            this.btnCircle.UseVisualStyleBackColor = false;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRectangle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRectangle.BackgroundImage")));
            this.btnRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRectangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.btnRectangle.Location = new System.Drawing.Point(159, 10);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(40, 40);
            this.btnRectangle.TabIndex = 2;
            this.btnRectangle.TabStop = false;
            this.btnRectangle.UseVisualStyleBackColor = false;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnPolygons
            // 
            this.btnPolygons.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPolygons.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPolygons.BackgroundImage")));
            this.btnPolygons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPolygons.Location = new System.Drawing.Point(67, 9);
            this.btnPolygons.Name = "btnPolygons";
            this.btnPolygons.Size = new System.Drawing.Size(40, 40);
            this.btnPolygons.TabIndex = 5;
            this.btnPolygons.TabStop = false;
            this.btnPolygons.UseVisualStyleBackColor = false;
            this.btnPolygons.Click += new System.EventHandler(this.btnPolygons_Click);
            // 
            // btnEllipse
            // 
            this.btnEllipse.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEllipse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEllipse.BackgroundImage")));
            this.btnEllipse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEllipse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnEllipse.Location = new System.Drawing.Point(21, 9);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(40, 40);
            this.btnEllipse.TabIndex = 9;
            this.btnEllipse.TabStop = false;
            this.btnEllipse.UseVisualStyleBackColor = false;
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // btnLine
            // 
            this.btnLine.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLine.BackgroundImage")));
            this.btnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnLine.Location = new System.Drawing.Point(113, 9);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(40, 40);
            this.btnLine.TabIndex = 4;
            this.btnLine.TabStop = false;
            this.btnLine.UseVisualStyleBackColor = false;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnSquare
            // 
            this.btnSquare.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSquare.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSquare.BackgroundImage")));
            this.btnSquare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSquare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnSquare.Location = new System.Drawing.Point(21, 56);
            this.btnSquare.Name = "btnSquare";
            this.btnSquare.Size = new System.Drawing.Size(40, 40);
            this.btnSquare.TabIndex = 1;
            this.btnSquare.TabStop = false;
            this.btnSquare.UseVisualStyleBackColor = false;
            this.btnSquare.Click += new System.EventHandler(this.btnSquare_Click);
            // 
            // plMain
            // 
            this.plMain.BackColor = System.Drawing.SystemColors.Window;
            this.plMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plMain.Location = new System.Drawing.Point(255, 4);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(934, 737);
            this.plMain.TabIndex = 1;
            this.plMain.Paint += new System.Windows.Forms.PaintEventHandler(this.plMain_Paint);
            this.plMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseDoubleClick);
            this.plMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseDown);
            this.plMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseMove);
            this.plMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plMain_MouseUp);
            // 
            // SimplePaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1201, 751);
            this.Controls.Add(this.plMain);
            this.Controls.Add(this.plControl);
            this.DoubleBuffered = true;
            this.Name = "SimplePaint";
            this.Text = "SimplePaint";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.plControl.ResumeLayout(false);
            this.plControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLineWidth)).EndInit();
            this.plShape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plControl;
        private System.Windows.Forms.Button btnPolygons;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnSquare;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Panel plShape;
        private System.Windows.Forms.Button btnfillColor;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDashMode;
        private System.Windows.Forms.ComboBox cmbShapeMode;
        private System.Windows.Forms.ComboBox cmbbrushStyle;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnGroup;
        private DoubleBufferPanel plMain;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnCurve;
        private System.Windows.Forms.Button btnUnGroup;
        private System.Windows.Forms.Label lblOutLine;
        private System.Windows.Forms.Button btnOutLineColor;
        private System.Windows.Forms.Label lblFillColor;
        private System.Windows.Forms.TrackBar trbLineWidth;
        private System.Windows.Forms.Label lblWidth;
    }
}

