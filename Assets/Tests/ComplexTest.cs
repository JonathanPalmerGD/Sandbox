using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sandbox.Distance;
using Sandbox.Things;

namespace Sandbox
{
	public class ComplexTest : MonoBehaviour
	{

		void Start()
		{
			//We have a frog. He weighs 22.7 grams
			Gram frogWeight = new Gram(22.7f);

			Debug.Log("I have a frog. He weighs " + frogWeight.Display + "\n");

			//We have a fly. It weighs 
			Milogram flyWeight = new Milogram(12f);

			Debug.Log("I have a housefly. It weighs " + flyWeight.Display + "\n");

			//Operator overloading means we don't need to manually convert these units.
			frogWeight = (frogWeight + flyWeight);

			Debug.Log("If our frog eats our fly. It will weigh " + frogWeight + "\n");

			float numberOfFlies = frogWeight.ToMilogram().Value / flyWeight.Value;
			Debug.Log("If flies could merge biomass, how many would it take to weigh as much as a our new frog? " + numberOfFlies + "\n\tWe'll go ahead and round up - " + Mathf.CeilToInt(numberOfFlies) + " flies have merged into one megafly.");

			//Our new fly (weighing X) is pretty big.
			//Housefly wings beat 265 Hz - source: https://www.nature.com/articles/srep25706

			//Lets say it's wings stayed the same.

			//How fast is the wing moving? How much force are we generating?

			//Lets say it's wings scaled in square of the size they once were. Much like bodymass

			//How fast is the wing moving? How much force are we generating?

			
		}
	}
}