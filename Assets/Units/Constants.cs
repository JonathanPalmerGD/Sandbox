using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Distance;

namespace Sandbox
{
	public class Constants : MonoBehaviour
	{
		public class SpeedOfLight
		{
			public static MetersPerSecond Unit
			{
				get { return new MetersPerSecond(new Meter(299792548), new Second(1)); }
			}
		}
		public class GasConstant
		{

		}
	}
}