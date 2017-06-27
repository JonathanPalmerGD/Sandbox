using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
	//This is a beautiful horror of generic programming.
	//It allows strongly typed to the Nth power variables with a few stipulations
	//		1. You need to add a parameterless constructor to each AbstractUnit you want to use Pow<T,U> for. You can't force this abstract class or with an interface. It'll compile error if you try something that doesn't have new()
	//		2. You need to reimplement each Typed RaiseToPower value. This means we can't do arbitrary or programmatic powers, but we can hit the common unit bases. I started with 1, 2, 3, -1, E and C (but C is likely not to be useful)
	public class Pow<T, U> : AbstractUnit where T : AbstractUnit, new() where U : RaiseToPower, new()
	{
		public T BaseUnit;
		public U Power;
		public static Pow<T, U> New(T TVariable)
		{
			Pow<T, U> poweredUnit = new Pow<T, U>();

			poweredUnit.BaseUnit = TVariable;

			poweredUnit.Power = new U();

			return poweredUnit;
		}

		public override string DisplayUnit
		{
			get
			{
				return BaseUnit.DisplayUnit + "^" + Power.Power;
			}
		}
		public T GetBaseValue()
		{
			return BaseUnit;
		}

		public float ResolvedUnit()
		{
			return Power.Resolve(BaseUnit.Unit);
		}
	}

	/// <summary>
	/// Represents an AbstractPower. I thought about naming it, but then we conflict when I get into Force/Work/Power.
	/// This name is a bit clearer that it has to do with exponents.
	/// </summary>
	public abstract class RaiseToPower
	{
		public float Resolve(float Unit)
		{
			return Mathf.Pow(Unit, Power);
		}
		public abstract float Power { get; }
	}
	public class FirstPower : RaiseToPower
	{
		public override float Power
		{ get { return 1; } }
	}
	public class Squared : RaiseToPower
	{
		public override float Power
		{ get { return 2; } }
	}
	public class Cubed : RaiseToPower
	{
		public override float Power
		{ get { return 3; } }
	}
	public class Inverse : RaiseToPower
	{
		public override float Power
		{ get { return -1; } }
	}
	public class EthPower : RaiseToPower
	{
		public override float Power
		{ get { return 2.71828f; } }
	}
	public class CthPower : RaiseToPower
	{
		public override float Power
		{ get { return 299792458; } }
	}
}