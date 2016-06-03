using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

/*

Creator: Blue Eyes (HF)
http://hackforums.net/member.php?action=profile&uid=1560348
Version: 1.0
For any bugs / errors, PM me.

*/
    public enum MouseState : byte
    {
        None = 0,
        Over = 1,
        Down = 2,
        Block = 3
    }
    public enum ButtonType
 {
     Green,
     Red,
     Grey,
     YellowLight,
     BlueLight,
     Yellow
 }
    public enum AlerteColor
 {
     Green,
     Red,
     Grey,
     YellowLight,
     BlueLight,
     Yellow
 }
    public class SatoshiMinesForm : ContainerControl
    {
        private int W;
        private int H;
        private bool Cap = false;
        private Point MousePoint = new Point(0, 0);
        private int MoveHeight = 50;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left & new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
            {
                Cap = true;
                MousePoint = e.Location;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Cap)
            {
                Parent.Location = new Point(
                    MousePosition.X - MousePoint.X,
                    MousePosition.Y - MousePoint.Y
                );
            }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.AllowTransparency = false;
            ParentForm.TransparencyKey = Color.Fuchsia;
            ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
            Dock = DockStyle.Fill;
            Invalidate();
        }
        public SatoshiMinesForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.Fuchsia;
            Font = new Font("sans-serif", 15);
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width;
            H = Height;
            G.FillRectangle(new SolidBrush(Color.FromArgb(42, 42, 42)), new Rectangle(0, 0, W, H));
            G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 8, W, H), new StringFormat { Alignment = StringAlignment.Center });
            G.FillRectangle(new SolidBrush(Color.FromArgb(17,17,17)), new Rectangle(1, 41, W -2, H-42));
            G.DrawLine(new Pen(Color.FromArgb(51,51,51), 2), 0, 40, W, 40);
            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
        
    }
    public class SatoshiMinesButton : Control
    {
       
        [Category("Options")]
        public ButtonType Type
        {
            get { return BT; }
            set { BT = value; }
        }
        private ButtonType BT;
        private int W = 0;
        private int H = 0;

        private MouseState State = MouseState.None;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }
        public SatoshiMinesButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new Size(106, 32);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            Font = new Font("sans-serif", 12);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
         
            GraphicsPath GP = new GraphicsPath();


            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.Clear(BackColor);
            Color textcolor = Color.White;
            if (Enabled == true)
            {
                switch (BT)
                {
                    case ButtonType.Green:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(144, 199, 57)), new Rectangle(0, 0, Width, Height));
                        G.DrawLine(new Pen(Color.FromArgb(82, 176, 0), 6), 0, Height, Width, Height);
                        G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        textcolor = Color.White;
                        break;
                    case ButtonType.BlueLight:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(153, 204, 255)), new Rectangle(0, 0, Width, Height));
                        G.DrawLine(new Pen(Color.FromArgb(0, 17, 136), 6), 0, Height, Width, Height);
                        G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(0, 17, 136)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        textcolor = Color.FromArgb(0, 17, 136);
                        break;
                    case ButtonType.YellowLight:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(255, 230, 153)), new Rectangle(0, 0, Width, Height));
                        G.DrawLine(new Pen(Color.FromArgb(173, 95, 27), 6), 0, Height, Width, Height);
                        G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(173, 95, 27)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        textcolor = Color.FromArgb(173, 95, 27);
                        break;
                    case ButtonType.Grey:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(221, 221, 221)), new Rectangle(0, 0, Width, Height));
                        G.DrawLine(new Pen(Color.FromArgb(68, 68, 68), 6), 0, Height, Width, Height);
                        G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(68, 68, 68)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        textcolor = Color.FromArgb(68, 68, 68);
                        break;
                    case ButtonType.Red:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(238, 17, 17)), new Rectangle(0, 0, Width, Height));
                        G.DrawLine(new Pen(Color.FromArgb(119, 0, 0), 6), 0, Height, Width, Height);
                        G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        textcolor = Color.White;
                        break;
                    case ButtonType.Yellow:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(229, 172, 0)), new Rectangle(0, 0, Width, Height));
                        G.DrawLine(new Pen(Color.FromArgb(170, 102, 0), 6), 0, Height, Width, Height);
                        G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        textcolor = Color.White;
                        break;
                }


                switch (State)
                {
                    case MouseState.None:

                        break;
                    case MouseState.Over:
                        G.FillRectangle(new SolidBrush(Color.FromArgb(80, Color.FromArgb(71, 139, 161))), new Rectangle(0, 2, W, H));
                        G.DrawString(Text, Font, new SolidBrush(textcolor), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        break;
                    case MouseState.Down:
                        switch (BT)
                        {
                            case ButtonType.Green:
                                G.FillRectangle(new SolidBrush(Color.FromArgb(144, 199, 57)), new Rectangle(0, 0, Width, Height));
                                G.DrawLine(new Pen(Color.FromArgb(82, 176, 0), 4), 0, 0, Width, 0);
                                G.DrawLine(new Pen(Color.FromArgb(82, 176, 0), 4), 0, 0, 0, Height);
                                G.DrawLine(new Pen(Color.FromArgb(82, 176, 0), 4), Width, 0, Width, Height);
                                G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(82, 176, 0)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                                break;
                            case ButtonType.BlueLight:
                                G.FillRectangle(new SolidBrush(Color.FromArgb(153, 204, 255)), new Rectangle(0, 0, Width, Height));
                                G.DrawLine(new Pen(Color.FromArgb(0, 17, 136), 4), 0, 0, Width, 0);
                                 G.DrawLine(new Pen(Color.FromArgb(0, 17, 136), 4), 0, 0, 0, Height);
                                G.DrawLine(new Pen(Color.FromArgb(0, 17, 136), 4), Width, 0, Width, Height);
                                G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(0, 17, 136)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                                break;
                            case ButtonType.YellowLight:
                                G.FillRectangle(new SolidBrush(Color.FromArgb(255, 230, 153)), new Rectangle(0, 0, Width, Height));
                                G.DrawLine(new Pen(Color.FromArgb(173, 95, 27), 4), 0, 0, Width, 0);
                                G.DrawLine(new Pen(Color.FromArgb(173, 95, 27), 4), 0, 0, 0, Height);
                                G.DrawLine(new Pen(Color.FromArgb(173, 95, 27), 4), Width, 0, Width, Height);
                                G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(173, 95, 27)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                                break;
                            case ButtonType.Grey:
                                G.FillRectangle(new SolidBrush(Color.FromArgb(221, 221, 221)), new Rectangle(0, 0, Width, Height));
                                G.DrawLine(new Pen(Color.FromArgb(68, 68, 68), 4), 0, 0, Width, 0);
                                G.DrawLine(new Pen(Color.FromArgb(68, 68, 68), 4), 0, 0, 0, Height);
                                G.DrawLine(new Pen(Color.FromArgb(68, 68, 68), 4), Width, 0, Width, Height);
                                G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(68, 68, 68)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                                break;
                            case ButtonType.Red:
                                G.FillRectangle(new SolidBrush(Color.FromArgb(238, 17, 17)), new Rectangle(0, 0, Width, Height));
                                G.DrawLine(new Pen(Color.FromArgb(119, 0, 0), 4), 0, 0, Width, 0);
                                G.DrawLine(new Pen(Color.FromArgb(119, 0, 0), 4), 0, 0, 0, Height);
                                G.DrawLine(new Pen(Color.FromArgb(119, 0, 0), 4), Width, 0, Width, Height);
                                G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(119, 0, 0)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                                break;
                            case ButtonType.Yellow:
                                G.FillRectangle(new SolidBrush(Color.FromArgb(229, 172, 0)), new Rectangle(0, 0, Width, Height));
                                G.DrawLine(new Pen(Color.FromArgb(170, 102, 0), 4), 0, 0, Width, 0);
                                G.DrawLine(new Pen(Color.FromArgb(170, 102, 0), 4), 0, 0, 0, Height);
                                G.DrawLine(new Pen(Color.FromArgb(170, 102, 0), 4), Width, 0, Width, Height);
                                G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(170, 102, 0)), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                                break;
                        }
                          break;
                      
                }
            }
            else 
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(17,17,17)), new Rectangle(0, 0, Width, Height));
                G.DrawRectangle(new Pen(Color.FromArgb(51,51,51), 1), new Rectangle(0, 0, Width, Height));
                G.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 3), 0, 0, Width, 0);
                G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            
            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
        

    }
    public class SatoshiMinesAlerte : Control
    {
       [Category("Options")]
        public AlerteColor Colors
        {
            get { return AC; }
            set { AC = value; }
        }
        [Category("Apparence")]
       [DisplayName("Texte center")]
       public bool textcenter
       {
           get { return tc; }
           set { tc = value; }
       }
        private bool tc;
       private AlerteColor AC;
        private int W;
        private int H;
       public SatoshiMinesAlerte()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new Size(180, 37);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            Font = new Font("sans-serif", 12);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width;
            H = Height;
            GraphicsPath GP = new GraphicsPath();
            Color SetColor = Color.FromArgb(34, 136, 0);
            switch (Colors)
            {
                case AlerteColor.Green:
                    SetColor = Color.FromArgb(34, 136, 0);
                break;
                case AlerteColor.Grey:
                SetColor = Color.FromArgb(68,68,68);
                break;
                case AlerteColor.Red:
                SetColor = Color.FromArgb(119,17,17);
                break;
            }
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.Clear(BackColor);
            G.FillRectangle(new SolidBrush(SetColor), new Rectangle(0, 0, W, H));
            G.FillRectangle(new SolidBrush(Color.FromArgb(17, 17, 17)), new Rectangle(2, 2, W - 4, H - 4));
            if (textcenter)
            {
             
                    G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 8, W, H), new StringFormat { Alignment = StringAlignment.Center });
            }
            else
            {
               G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(15, 8, W, H));
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
        

    }
    public class  SatoshiMinesGroupBox : ContainerControl
    {
        private int W;
        private int H;


        public SatoshiMinesGroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Size = new Size(240, 180);
            Font = new Font("Segoe ui", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
             Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width;
            H = Height;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.Clear(BackColor);

            G.FillRectangle(new SolidBrush(Color.FromArgb(51,51,51)), Rectangle.Round(new RectangleF(1, 15, W -2, H - 15)));
            G.FillRectangle(new SolidBrush(Color.FromArgb(0, 45, 68)), Rectangle.Truncate(new RectangleF(((W / 2) - (Text.Length * 3)) - 20, 0, (Text.Length * 7) + 20, 30)));
            
            G.DrawRectangle(new Pen(Color.FromArgb(0, 45, 68), 2), Rectangle.Round(new RectangleF(1, 15, W - 2, H - 16)));
            G.DrawString(Text, Font, new SolidBrush(Color.White), new Rectangle(0, 4, W, H), new StringFormat { Alignment = StringAlignment.Center });
            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

    }
    public class SatoshiMinesLabel : Label
    {
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public SatoshiMinesLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("sans-serif", 10);
            ForeColor = Color.White;
            BackColor = Color.Transparent;
            Text = Text;
        }
    }
    public class SatoshiMinesLabelBackColor : Control
    {
        [Category("Apparence")]
        [DisplayName("Color BackGround")]
        public Color colorbackground
        {
            get { return CB; }
            set { CB = value; }
        }
        private Color CB;
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public SatoshiMinesLabelBackColor()
        {
            colorbackground = Color.FromArgb(51, 51, 51);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("sans-serif", 11);
            ForeColor = Color.White;
            BackColor = Color.Transparent;
            Text = Text;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            GraphicsPath GP = new GraphicsPath();


            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.Clear(BackColor);
            G.FillRectangle(new SolidBrush(colorbackground), new Rectangle(0, 0, Width, Height));
            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

    }