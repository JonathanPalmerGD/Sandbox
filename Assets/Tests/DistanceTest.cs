using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Distance
{
	public class DistanceTest : MonoBehaviour
	{
		void Start()
		{
			Inch MyHeight = new Inch(74);
			Meter ThreeMeters = new Meter(3);
			Mile DistanceToTheMoon = new Mile(238900);
			Foot ThreeFeet = new Foot(3);

			Debug.Log("MyHeight: " + MyHeight.AllUnits() + "\n");
			Debug.Log("ThreeMeters: " + ThreeMeters.AllUnits() + "\n");
			Debug.Log("DistanceToTheMoon: " + DistanceToTheMoon.AllUnits() + "\n");
			Debug.Log("ThreeFeet: " + ThreeFeet.AllUnits() + "\n");
		}

	}
}