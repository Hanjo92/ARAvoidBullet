using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Almond;

namespace ARAvoid
{
	public class PlayerController : MonoBehaviour
	{
		private Joystick mJoystick;


		public void Setup(Joystick joystick)
		{
			mJoystick = joystick;
		}

		public void PlayerUpdate(float deltaTime)
		{
			
		}
	}
}