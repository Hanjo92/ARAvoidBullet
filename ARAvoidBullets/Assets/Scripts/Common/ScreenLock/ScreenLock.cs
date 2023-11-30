using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ScreenLock
{
	private static EventSystem current;
	public static void Lock()
	{
		if(current != null && current.enabled == false)
		{
			return;
		}
		current = EventSystem.current;
		current.enabled = false;
	}
	public static void Unlock()
	{
		if(current == null)
		{
			Debug.LogWarning("No screen lock");
			return;
		}
		current.enabled = true;
	}
}
