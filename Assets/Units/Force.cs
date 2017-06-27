using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Things;
using Sandbox.Distance;

namespace Sandbox
{
	public class AbstractForce : AbstractUnit
	{
		public AbstractMass Mass;
		public AbstractAcceleration Acceleration;

		public AbstractPressure ApplyForceToArea(Pow<Meter, Squared> area)
		{
			return null;
			//return new AbstractPressure();
		}
	}
}