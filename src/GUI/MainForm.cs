using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pickUpSpeedButton.Checked)
            {
                Shape sel = dialogProcessor.ContainsPoint(e.Location);
                if (sel != null)
                {
                    if (dialogProcessor.SelectionElement.Contains(sel))
                    {
                        dialogProcessor.SelectionElement.Remove(sel);
                    }
                    else
                    {
                        dialogProcessor.SelectionElement.Add(sel);
                    }


                    statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;
                    viewPort.Invalidate();
                }
            }
        }

        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.SelectionElement != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void AddEllipseButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

            viewPort.Invalidate();
        }

        private void AddTriangleButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();
            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";
            viewPort.Invalidate();
        }

        private void AddCircleButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomCircle();
            statusBar.Items[0].Text = "Последно действие: Рисуване на кръг.";

            viewPort.Invalidate();
        }

        private void viewPort_Load(object sender, EventArgs e)
        {

        }

        private void АddSquareButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomSquare();
            statusBar.Items[0].Text = "Последно действие: Рисуване на квадрат";
            viewPort.Invalidate();
        }
        //EXAM
        private void drawMysteryShapeClick_Click(object sender, EventArgs e)
        {

            dialogProcessor.AddRandomMysteryShape();
            statusBar.Items[0].Text = "Последно действие: Рисуване на фигурата от изпита.";
            viewPort.Invalidate();
        }
        //EXAM
        private void pickUpSpeedButton_Click(object sender, EventArgs e)
        {

        }

        private void ColourFillButtonClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                dialogProcessor.SetSelectedFieldColor(colorDialog1.Color);
                viewPort.Invalidate();
            }
        }

        private void SizeUpButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.SizeUp();
            statusBar.Items[0].Text = "Последно действие: Минимизиране на примитив";

            viewPort.Invalidate();
        }

        private void SizeDownButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.SizeDown();

            statusBar.Items[0].Text = "Последно действие: Мащабиране на примитив";

            viewPort.Invalidate();
        }

        private void BorderColourButtonClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                dialogProcessor.SetSelectedBorderColor(colorDialog1.Color);
                viewPort.Invalidate();
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.Delete();
            statusBar.Items[0].Text = "Последно действие: Изтриване на фигура";
            viewPort.Invalidate();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.Delete();
            statusBar.Items[0].Text = "Последно действие: Изтриване на фигура";
            viewPort.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                {
                    dialogProcessor.WriteShapeListToFile((List<Shape>)dialogProcessor.ShapeList, saveFileDialog1.FileName);
                }
                statusBar.Items[0].Text = "Последно действие: Записване на файл.";

            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.ShapeList = (List<Shape>)dialogProcessor.LoadShapeListFromFile(openFileDialog1.FileName);
                viewPort.Invalidate();
            }
            statusBar.Items[0].Text = "Последно действие: Отваряне на файл.";

        }







        //TEST CODE TODO
        //WARNING

    }
   
}
