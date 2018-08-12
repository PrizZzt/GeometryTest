using System;
using System.Collections.Generic;

namespace GeometryTest
{
	public class Map
	{
		public int SizeX { get; }
		public int SizeY { get; }

		public List<Object> ObjectList { get; set; }
		public Object[,,] Objects { get; set; }
		public bool[,] Blocks { get; set; }

		public Map(int x, int y)
		{
			SizeX = x;
			SizeY = y;

			ObjectList = new List<Object>();
			Objects = new Object[SizeX, SizeY, 4];
			Blocks = new bool[SizeX, SizeY];
			for (int j = 0; j < SizeY; j++)
			{
				for (int i = 0; i < SizeX; i++)
				{
					Blocks[i, j] = i == 0 || j == 0 || i == SizeX - 1 || j == SizeY - 1;
				}
			}
		}

		public void Update(float deltaTime)
		{
			foreach (var obj in ObjectList)
			{
				obj.Update(deltaTime);
			}
		}

		public void AddObject(Object obj)
		{
			for (int i = 0; i < 4; i++)
			{
				if (Objects[(int)Math.Round(obj.X), (int)Math.Round(obj.Y), i] == null)
				{
					Objects[(int)Math.Round(obj.X), (int)Math.Round(obj.Y), i] = obj;
					ObjectList.Add(obj);
					return;
				}
			}
		}
	}
}
