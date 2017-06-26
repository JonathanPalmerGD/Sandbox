using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Distance
{
	public class MovementTest : MonoBehaviour
	{
		void Start()
		{
			FeetPerSecond running = new FeetPerSecond(new Foot(2.5f), new Second(1));
			MetersPerSecond baseMpS = new MetersPerSecond(new Meter(1), new Second(1));
			MilesPerHour ResidentialDrive = new MilesPerHour(new Mile(30), new Hour(1));

			Debug.Log("running: " + running.AllUnits() + "\n");
			Debug.Log("Base Meter per second: " + baseMpS.AllUnits() + "\n");
			Debug.Log("A Residential Drive: " + ResidentialDrive.AllUnits() + "\n");
		}

	}
}