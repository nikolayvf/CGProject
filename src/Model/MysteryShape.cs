using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    [Serializable]
    internal class MysteryShape : Shape
    {
        public MysteryShape(RectangleF rect) : base(rect)
        {
        }

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
                return true;

            else
            {
                return false;
            }
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            base.DrawSelf(grfx);
            //полигон - странен триъгълник
            Point[] points = {
             new Point((int)Rectangle.X + ((int)Rectangle.Width / 2),
                 (int)(Rectangle.Y + Rectangle.Height)),
             new Point((int)Rectangle.X, (int)Rectangle.Y),
            
            
            new Point(
            ((int)Rectangle.X + ((int)Rectangle.Width / 2) + (int)Rectangle.X + (int)(Rectangle.X + Rectangle.Width)) / 3,
            ((int)(Rectangle.Y + Rectangle.Height) + (int)Rectangle.Y + (int)Rectangle.Y) / 3
        ), new Point((int)(Rectangle.X + Rectangle.Width), (int)(Rectangle.Y))

        


    };
            grfx.FillPolygon(new SolidBrush(FillColor), points);
            grfx.DrawPolygon(new Pen(BorderColor, BorderWidth), points);
            // Вертикална линия
            grfx.DrawLine(
                new Pen(BorderColor, 2),
                new Point((int)(Rectangle.X + Rectangle.Width / 2), ((int)(Rectangle.Y + Rectangle.Height) + (int)Rectangle.Y + (int)Rectangle.Y) / 3),
                new Point((int)(Rectangle.X + Rectangle.Width / 2), (int)(Rectangle.Y + Rectangle.Height))
            );
            // хоризонтална линия
            grfx.DrawLine(
              new Pen(BorderColor, 2),
                new Point((int)Rectangle.X, (int)Rectangle.Y),
             new Point((int)(Rectangle.X + Rectangle.Width), (int)Rectangle.Y)
        );
        }
    }
}
