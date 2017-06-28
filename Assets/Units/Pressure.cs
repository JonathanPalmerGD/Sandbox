using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Distance;
using Sandbox.Things;

namespace Sandbox
{
	//Unfinished
	public abstract class AbstractPressure : AbstractUnit
	{
		public AbstractForce Mass;
		//This limitation reappears, we must override and add the area to each child type of abstract pressure.
		//public Pow<AbstractDistance, Squared> Area;
	}

	//A specific type of pressure!
	public class Pascal : AbstractPressure
	{
		public Pow<Meter, Squared> MeterSquaredArea;
	}
}