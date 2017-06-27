using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Things
{
	public class MassTest : MonoBehaviour
	{
		void Start()
		{
			//We present some base cases (and then cross check the AllUnits output with Google)
			Gram PaperbackBookWeight = new Gram(75);
			Kilogram RocketLauncherWeight = new Kilogram(4.5f);
			Pound MyWeight = new Pound(175);

			Debug.Log("Paperback Book Weight: " + PaperbackBookWeight.AllUnits() + "\n");
			Debug.Log("Rocket Launcher Weight: " + RocketLauncherWeight.AllUnits() + "\n");
			Debug.Log("MyWeight: " + MyWeight.AllUnits() + "\n");
		}

		void Update()
		{

		}
	}
}