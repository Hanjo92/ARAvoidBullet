using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Almond;

namespace ARAvoid
{
	public class PlayerController : FixedJoystick
	{
		public Vector3 GetControllerValue(float deltaTime)
		{
			var direction = Vector3.forward * Vertical + Vector3.right * Horizontal;
			return direction;
		}
	}
}