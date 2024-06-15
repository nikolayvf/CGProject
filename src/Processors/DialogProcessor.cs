using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
        #region Constructor
        public DialogProcessor(string v)
        {
        }

        public DialogProcessor()
		{
		}
		
		#endregion
		private List<Shape> shapes = new List<Shape>();

        public List<Shape> SelectionElement
        {
            get { return shapes; }
            set { shapes = value; }
        }

        #region Properties

        /// <summary>
        /// Избран елемент.
        /// </summary>
        private Shape selection;
		public Shape Selection {
			get { return selection; }
			set { selection = value; }
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLoc;
		public PointF LastLocation {
			get { return lastLoc; }
			set { lastLoc = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		
        //
        public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
            //Randomize the rectangle size
            int a = rnd.Next(10, 200);
            int b = rnd.Next(10, 200);


            RectangleShape rect = new RectangleShape(new Rectangle(x,y,a,b));
			rect.FillColor = Color.White;

			ShapeList.Add(rect);
		}
		
		public void AddRandomEllipse()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);
            //Randomize the ellipse size
            int a = rnd.Next(20, 200);
            int b = rnd.Next(20, 200);

            EllipseShape ellipseS = new EllipseShape(new Rectangle(x, y, a, b));
            ellipseS.FillColor = Color.LightGray;
            ellipseS.BorderColor = Color.HotPink;
            ellipseS.BorderColor = Color.Yellow;
            ellipseS.Opacity = 200;
            ellipseS.BorderWidth = 2;

            ShapeList.Add(ellipseS);
        }
        public void AddRandomTriangle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);
            //Randomize the triangle size
            int a = rnd.Next(20, 200);
            int b = rnd.Next(20, 200);

            TriangleShape triangleS = new TriangleShape(new Rectangle(x, y, a, b));
            triangleS.FillColor = Color.LightBlue;
            triangleS.BorderColor = Color.HotPink;
            triangleS.BorderColor = Color.Yellow;
            triangleS.Opacity = 200;
            triangleS.BorderWidth = 2;

            ShapeList.Add(triangleS);
        }
        public void AddRandomCircle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);
            //Randomize the circle size
            int a = rnd.Next(20, 200);

            CircleShape circle = new CircleShape(new Rectangle(x, y, a, a));
            circle.Opacity = 1 * 255;
            circle.FillColor = Color.Yellow;
            circle.BorderColor = Color.Black;

            circle.BorderWidth = 2;
            ShapeList.Add(circle);
        }
        public void AddRandomSquare()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 600);
            int y = rnd.Next(100, 600);
            //Randomize the square size
            int a = rnd.Next(20, 200);

            SquareShape square = new SquareShape(new Rectangle(x, y, a, a));
            square.FillColor = Color.Red;
            square.BorderColor = Color.Green;
            square.Opacity = 200;
            square.BorderWidth = 2;

            ShapeList.Add(square);
        }

        //EXAM
        public void AddRandomMysteryShape()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);
            //Randomize the mystery shape size
            int a = rnd.Next(20, 200);
           // int b = rnd.Next(20, 200);
           //a or a and b - depending on figure

            MysteryShape mysteryS = new MysteryShape(new Rectangle(x, y, a, a));
            mysteryS.FillColor = Color.LightBlue;
            mysteryS.BorderColor = Color.HotPink;
            mysteryS.BorderColor = Color.Black;
            mysteryS.Opacity = 200;
            mysteryS.BorderWidth = 2;

            ShapeList.Add(mysteryS);
        }
        //EXAM
        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					//Променя цвета на елемента
                    //ShapeList[i].FillColor = Color.Red;
						
					return ShapeList[i];
				}	
			}
			return null;
		}
        public void GroupShape()
        {
            if (SelectionElement.Count < 2)
            {
                return;
            }

            float minX = float.PositiveInfinity;
            float minY = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;
            float maxY = float.NegativeInfinity;

            foreach (Shape item in SelectionElement)
            {
                if (minX > item.Location.X)
                {

                    minX = item.Location.X;
                }
                if (minY > item.Location.Y)
                {
                    minY = item.Location.Y;
                }
                if (maxX < item.Location.X + item.Width)
                {
                    maxX = item.Location.X + item.Width;

                }
                if (maxY < item.Location.Y + item.Height)

                {
                    maxY = item.Location.Y + item.Height;
                }

                GroupShape group = new GroupShape(new RectangleF(minX, minY, maxX - minX, maxY - minY));
                group.shapeSub = SelectionElement;
                SelectionElement = new List<Shape>();
                SelectionElement.Add(group);

                foreach (Shape shape in group.shapeSub)
                {
                    ShapeList.Remove(shape);
                }
                ShapeList.Add(group);
            }
        }

        public void TranslateTo(PointF p)
        {
            if (shapes.Count > 0)
            {
                foreach (Shape item in SelectionElement)
                    item.Location = new PointF(item.Location.X + p.X - lastLoc.X,
                                                item.Location.Y + p.Y - lastLoc.Y);
                lastLoc = p;
            }
        }
        public void SetSelectedFieldColor(Color color)
        {
            foreach (Shape item in SelectionElement)
            {
                item.FillColor = color;
            }
        }

        public void SetSelectedBorderColor(Color color)
        {
            foreach (Shape item in SelectionElement)
            {

                item.BorderColor = color;
            }
        }
        public void SetOpacity(int value)
        {
            foreach (Shape item in SelectionElement)
            {
                item.Opacity = value;
            }
        }
        public void SetBorderWidth(float value)
        {
            foreach (Shape item in SelectionElement)
            {
                item.BorderWidth = value;
            }
        }

        public override void DrawShape(Graphics grfx, Shape item)
        {
            base.DrawShape(grfx, item);

            if (SelectionElement.Contains(item))
                grfx.DrawRectangle(new Pen(Color.Green),
                    item.Location.X - 4,
                    item.Location.Y - 4,
                    item.Width + 8,
                    item.Height + 8);

        }
        //Увеличаваме размера на фигурата, ако широчината и височината са по-малки или равни на 500
        public void SizeUp()
        {
            if (SelectionElement != null)
            {
                foreach (Shape item in SelectionElement)
                {
                    if (item.Height >= 500 && item.Width >= 500)
                    {
                        MessageBox.Show("Не можете да уголемявате още фигурата!", "Error");

                    }
                    else
                    {
                        ShapeList.Remove(item);
                        item.Height += 25;
                        item.Width += 25;

                        ShapeList.Add(item);
                    }
                }
            }
        }

        //Намаляваме размера на селектираната фигура, ако широчината и височината са > 100
        public void SizeDown()
        {
            foreach (Shape item in SelectionElement)
            {
                if (SelectionElement != null)
                {
                    if (item.Height > 100 && item.Width > 100)
                    {
                        ShapeList.Remove(item);

                        item.Height -= 25;
                        item.Width -= 25;

                        ShapeList.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Не можете да смалявате още фигурата!", "Error");
                    }
                }
            }
        }

      
        //Изтриваме селектираната фигура
        public void Delete()
        {
            foreach (Shape shape in SelectionElement)
            {
                ShapeList.Remove(shape);

            }
            SelectionElement.Clear();
        }

        public void WriteShapeListToFile(object obj, string path = null)
        {
            Stream stream;
            IFormatter formatter = new BinaryFormatter();
            if (path == null)
            {
                stream = new FileStream("DrawFile.asd", FileMode.Create, FileAccess.Write, FileShare.None);

            }
            else
            {
                string preparePath = path + ".asd";
                stream = new FileStream(preparePath, FileMode.Create);
            }
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public object LoadShapeListFromFile(string path = null)
        {
            object obj;
            Stream stream;
            IFormatter binaryFormatter = new BinaryFormatter();
            if (path == null)
            {
                stream = new FileStream("DrawFile.asd", FileMode.Open);
            }
            else
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            obj = binaryFormatter.Deserialize(stream);
            stream.Close();
            return obj;
        }

        //Rotate TODO



    }
}
