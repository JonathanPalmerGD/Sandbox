using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
	public interface IPow<T, U>
	{

	}

	public class Pow<T, U> : AbstractUnit, IPow<T, U> where T : AbstractUnit where U : RaiseToPower
	{
		public T GetBaseValue()
		{
			return null;
		}

		public float ResolvedUnit()
		{
			Debug.LogError("Unfinished\n");
			return 0;
		}
	}

	public abstract class RaiseToPower
	{
		public float Resolve(float Unit)
		{
			return Mathf.Pow(Unit, Power);
		}
		public abstract float Power { get; }
	}
	public class Squared : RaiseToPower
	{
		public override float Power
		{ get { return 2; } }
	}
	public class Cubed : RaiseToPower
	{
		public override float Power
		{ get { return 2; } }
	}
	public class Inverse : RaiseToPower
	{
		public override float Power
		{ get { return -1; } }
	}
}