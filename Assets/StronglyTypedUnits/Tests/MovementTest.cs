using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Distance
{
	public class MovementTest : MonoBehaviour
	{
		void Start()
		{
			//We present some base cases (and then cross check the AllUnits output with Google)
			FeetPerSecond running = new FeetPerSecond(new Foot(2.5f), new Second(1));
			MetersPerSecond baseMpS = new MetersPerSecond(new Meter(1), new Second(1));
			MilesPerHour ResidentialDrive = new MilesPerHour(new Mile(30), new Hour(1));

			//This one is a doozy. We use strongly typed variables made up of the basic unit and a power representation
			MetersPerSecondSqrd Gravity = new MetersPerSecondSqrd(new Meter(9.8f), Pow<Second, Squared>.New(new Second(1)));

			Debug.Log("Gravity : " + Gravity.AllUnits() + "\n");
			Debug.Log("Running: " + running.AllUnits() + "\n");
			Debug.Log("Base Meter per second: " + baseMpS.AllUnits() + "\n");
			Debug.Log("A Residential Drive: " + ResidentialDrive.AllUnits() + "\n");
		}
	}
}