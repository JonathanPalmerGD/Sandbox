using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Things;
using Sandbox.Distance;

namespace Sandbox
{
	public class AbstractForce : AbstractUnit
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
	}
}