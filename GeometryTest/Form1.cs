using System;
using System.Drawing;
using System.Windows.Forms;

namespace GeometryTest
{
	public partial class Form1 : Form
	{
		const int CellSize = 50;
		Map map;
		Object player;
		bool up, down, right, left;

		public Form1()
		{
			InitializeComponent();

			map = new Map(20, 12);
			map.Blocks[5, 5] = true;

			player = new Object(map, 10, 10);
			map.AddObject(player);
			map.AddObject(new Object(map, 5, 10));
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			player.MoveDirectionX = right ? 1 : (left ? -1 : 0);
			player.MoveDirectionY = up ? -1 : (down ? 1 : 0);

			map.Update((float)timer1.Interval / 1000);

			var g = pictureBox1.CreateGraphics();
			var b = new SolidBrush(Color.Red);
			var p = new Pen(Color.Green);
			for (int j = 0; j < map.SizeY; j++)
			{
				for (int i = 0; i < map.SizeX; i++)
				{
					if (map.Blocks[i, j])
						g.FillRectangle(b, i * CellSize, j * CellSize, CellSize, CellSize);
				}
			}

			foreach (var obj in map.ObjectList)
			{
				g.DrawEllipse(p, (obj.X - obj.R) * CellSize, (obj.Y - obj.R) * CellSize, obj.R * 2 * CellSize, obj.R * 2 * CellSize);
			}
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			//this.Text = $"{Math.Floor((double)e.X / 50)}, {Math.Floor((double)e.Y / 50)}";
		}

		private void PressKey(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					up = true;
					break;
				case Keys.Down:
					down = true;
					break;
				case Keys.Right:
					right = true;
					break;
				case Keys.Left:
					left = true;
					break;
			}

			this.Text = $"{up}{down}{right}{left}";
		}

		private void ReleaseKey(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					up = false;
					break;
				case Keys.Down:
					down = false;
					break;
				case Keys.Right:
					right = false;
					break;
				case Keys.Left:
					left = false;
					break;
			}
			this.Text = $"{up}{down}{right}{left}";
		}
	}
}
