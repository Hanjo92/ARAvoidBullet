using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARAvoid
{
	public static class Extensions
	{
		public static Direction Opposite(this Direction direction) =>
		direction switch
		{
			Direction.Forward => Direction.Back,
			Direction.Right => Direction.Left,
			Direction.Back => Direction.Forward,
			Direction.Left => Direction.Right,
			_ => throw new System.NotImplementedException(),
		};
	}
}