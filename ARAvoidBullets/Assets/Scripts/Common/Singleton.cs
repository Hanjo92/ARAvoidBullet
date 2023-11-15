using UnityEngine;

namespace Almond
{
	public class Singleton<T> where T : Singleton<T>, new()
	{
		static T _inst;
		public static T Inst
		{
			get
			{
				if(null == _inst)
					_inst = new T();

				return _inst;
			}
		}
		public override string ToString()
		{
			return typeof(T).ToString() + "_Singleton";
		}
	}

	public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
	{
		private static MonoSingleton<T> _inst;
		private static MonoSingleton<T> Inst => _inst;
		public static T Instance => Inst as T;

		protected virtual void Awake()
		{
			if(Inst == null)
				_inst = this;
			else
				Destroy(this);
		}

		public override string ToString()
		{
			return typeof(T).ToString() + "_Singleton";
		}
	}
}