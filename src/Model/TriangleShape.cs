using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    [Serializable]
    internal class TriangleShape : Shape
    {
        public TriangleShape(Rectangle tri) : base(tri)
        {

        }
        public TriangleShape(TriangleShape triangle) : base()
        {

        }

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
                // Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
                // В случая на правоъгълник - директно връщаме true
                return true;
            else
                // Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
                return false;
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            Point[] points = {
           new Point((int)Rectangle.X + ((int)Rectangle.Width / 2),
                (int)Rectangle.Y), new Point((int)Rectangle.X,
                (int)(Rectangle.Y + Rectangle.Height)), new Point((int)(Rectangle.X + Rectangle.Width),
                (int)(Rectangle.Y + Rectangle.Height))
        };
            grfx.FillPolygon(new SolidBrush(FillColor), points);
            grfx.DrawPolygon(new Pen(BorderColor, BorderWidth), points);
        }
    }
}
