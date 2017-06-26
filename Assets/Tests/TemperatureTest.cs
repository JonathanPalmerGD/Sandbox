using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Thermal
{
	public class TemperatureTest : MonoBehaviour
	{
		void Start()
		{
			Kelvin AbsoluteZero = new Kelvin(0);
			Celsius waterBoilingSTP = new Celsius(100);
			Fahrenheit ShortsWeather = new Fahrenheit(75);
			Kelvin SurfaceOfTheSun = new Kelvin(5800);

			Debug.Log("Absolute Zero: " + AbsoluteZero.AllUnits() + "\n");
			Debug.Log("Water Boiling at STP: " + waterBoilingSTP.AllUnits() + "\n");
			Debug.Log("Shorts Weather: " + ShortsWeather.AllUnits() + "\n");
			Debug.Log("Surface of the Sun: " + SurfaceOfTheSun.AllUnits() + "\n");
		}

		void Update()
		{

		}
	}
}