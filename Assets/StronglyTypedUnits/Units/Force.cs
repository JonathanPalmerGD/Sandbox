using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Things;
using Sandbox.Distance;

namespace Sandbox
{
	public abstract class AbstractForce : AbstractUnit
	{
		//We can use a Mass and an Acceleration to accomplish this.
		public AbstractMass Mass;
		public AbstractAcceleration Acceleration;

		//This is really cool, Area was a result of Pow<T,U> entirely.
		public AbstractPressure ApplyForceToArea(Pow<Meter, Squared> area)
		{
			return null;
			//return new AbstractPressure();
		}

		public override string Name
		{
			get { return "AbstractForce"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return Mass.DisplayUnit + "*" + Acceleration.DisplayUnit;
			}
		}
		public override string Category
		{
			get { return "Force"; }
		}
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToNewton().Display + "   ";
		}
		public abstract Newton ToNewton();
	}

	//A specific type of force!
	public class Newton : AbstractForce
	{
		public override string Name
		{
			get { return "Newton"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "N";
			}
		}

		public override Newton ToNewton()
		{
			return this;
		}

		public Newton(AbstractMass Mass, AbstractAcceleration Accel)
		{
			this.Mass = Mass;
			Acceleration = Accel;

			Value = Mass.Value * Accel.Value;
		}
	}
}