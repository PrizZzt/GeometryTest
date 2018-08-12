using System;

namespace GeometryTest
{
	public class Object
	{
		public Map Map;

		public float X { get; set; }
		public float Y { get; set; }
		public float R { get; set; }

		public float Speed;
		public float MoveDirectionX;
		public float MoveDirectionY;

		public Object(Map map, float x, float y)
		{
			Map = map;

			X = x;
			Y = y;
			R = 0.5f;

			Speed = 4;
			MoveDirectionX = 0;
			MoveDirectionY = 0;
		}

		public void Update(float deltaTime)
		{
			if (MoveDirectionX != 0 || MoveDirectionY != 0)
			{
				if (Move(deltaTime) == false)
				{
					if (MoveDirectionX != 0 && MoveDirectionY != 0)
					{
						if (MoveDirectionX != 0)
						{
							float MoveDirectionXSave = MoveDirectionX;
							MoveDirectionX = 0;
							Move(deltaTime);
							MoveDirectionX = MoveDirectionXSave;
						}
						if (MoveDirectionY != 0)
						{
							float MoveDirectionYSave = MoveDirectionY;
							MoveDirectionY = 0;
							Move(deltaTime);
							MoveDirectionY = MoveDirectionYSave;
						}
					}
				}
			}
		}

		public bool Move(float deltaTime)
		{
			int XIndex = (int)Math.Floor(X);
			int YIndex = (int)Math.Floor(Y);
			float NewX = X + MoveDirectionX * Speed * deltaTime;
			float NewY = Y + MoveDirectionY * Speed * deltaTime;
			int NewXIndex = (int)Math.Floor(NewX);
			int NewYIndex = (int)Math.Floor(NewY);

			if (NewXIndex < 0 || NewXIndex >= Map.SizeX || NewYIndex < 0 || NewYIndex >= Map.SizeY)
				return false;

			for (int j = -1; j < 2; j++)
			{
				for (int i = -1; i < 2; i++)
				{
					if (NewXIndex + i < 0 || NewXIndex + i >= Map.SizeX || NewYIndex + j < 0 || NewYIndex + j >= Map.SizeY)
						continue;

					if (Map.Blocks[NewXIndex + i, NewYIndex + j])
					{
						float blockX = NewXIndex + i + 0.5f;
						float blockY = NewYIndex + j + 0.5f;

						if (
							(NewX - R < blockX + 0.5) &&
							(NewX + R > blockX - 0.5) &&
							(NewY - R < blockY + 0.5) &&
							(NewY + R > blockY - 0.5)
							)
							return false;
					}

					for (int k = 0; k < 4; k++)
					{
						Object target = Map.Objects[NewXIndex + i, NewYIndex + j, k];
						if (target != null && target != this)
						{
							if (((target.X - NewX) * (target.X - NewX) + (target.Y - NewY) * (target.Y - NewY)) < (target.R + R))
								return false;
						}
					}
				}
			}

			if (NewXIndex != XIndex || NewYIndex != YIndex)
			{
				for (int i = 0; i < 4; i++)
				{
					if (Map.Objects[XIndex, YIndex, i] == this)
					{
						Map.Objects[XIndex, YIndex, i] = null;
						break;
					}
				}
				for (int i = 0; i < 4; i++)
				{
					if (Map.Objects[NewXIndex, NewYIndex, i] == null)
					{
						Map.Objects[NewXIndex, NewYIndex, i] = this;
						break;
					}
				}
			}

			X = NewX;
			Y = NewY;
			return true;
		}
	}
}
