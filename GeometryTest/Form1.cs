using System.Drawing;
using System.Windows.Forms;

namespace GeometryTest
{
	public partial class Form1 : Form
	{
		const int CellSize = 50;
		Map map;
		public Form1()
		{
			InitializeComponent();

			map = new Map(20, 12);
			map.Blocks[5, 5] = true;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			map.Update(timer1.Interval / 1000);

			var g = pictureBox1.CreateGraphics();
			var b = new SolidBrush(Color.Red);
			for (int j = 0; j < map.SizeY; j++)
			{
				for (int i = 0; i < map.SizeX; i++)
				{
					if (map.Blocks[i, j])
						g.FillRectangle(b, i * CellSize, j * CellSize, CellSize, CellSize);
				}
			}
		}
	}
}
