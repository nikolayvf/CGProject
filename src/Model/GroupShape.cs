using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    [Serializable]
    public class GroupShape : Shape
    {
        public List<Shape> shapeSub = new List<Shape>();


        public GroupShape(RectangleF rect) : base(rect)
        {
        }
        public GroupShape(Rectangle rectangle) : base(rectangle)
        {

        }

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
            {
                foreach (Shape item in shapeSub)
                {
                    if (item.Contains(point))
                    {

                        return true;
                    }
                }
                return true;

            }
            else
                return false;

        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            foreach (Shape item in shapeSub)
            {
                item.DrawSelf(grfx);
            }
        }
        public override void Move(float dx, float dy)
        {
            base.Move(dx, dy);
            foreach (var item in shapeSub)
            {
                item.Move(dx * 2, dy * 2);
            }
        }
        public override void GroupFillColor(Color color)
        {
            base.GroupFillColor(color);
            foreach (var item in shapeSub)
            {
                item.FillColor = color;
            }
        }

        public override void GroupOpacity(int op)
        {
            base.GroupOpacity(op);
            foreach (var item in shapeSub)
            {
                item.Opacity = op;
            }
        }
    }
}
