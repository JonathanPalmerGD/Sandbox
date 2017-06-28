using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Distance;

namespace Sandbox
{
	public class Constants : MonoBehaviour
	{
		// Speed of Light was really easy to create. Ideally we want to pull the constant value out further from hard coding.
		public class SpeedOfLight
		{
			public static MetersPerSecond Unit
			{
				get { return new MetersPerSecond(new Meter(299792548), new Second(1)); }
			}
		}

		//Note to self, add Reynolds Number (fluid simulation) into here.
		//https://en.wikipedia.org/wiki/Reynolds_number

		//Not to self, put gravity into this class

		//For the PV=nRT. This is the R value.
		public class GasConstant
		{

		}
	}
}