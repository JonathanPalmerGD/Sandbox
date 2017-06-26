﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Thermal
{
	//The purpose of these classes are to turn the category of math unit mistakes into compiler errors.
	//The compiler sees 'Oh, this conversion needs Kelvin, not Celsius, lets complain before that becomes a problem later'
	//It puts all unit conversion code in one place, so if a mistake is made, it can easily be changed.
	public abstract class AbstractTemperature : AbstractUnit
	{
		public string Degree { get { return "°"; } }

		public override string Name
		{
			get { return "Unknown"; }
		}
		public override string Category
		{
			get { return "Temperature"; }
		}
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToCelsius().Display + "   " + ToFahrenheit().Display + "   " + ToKelvin().Display;
		}
		public override string Display
		{
			get { return (Unit + " "+ Degree + Value); }
		}
		public abstract Celsius ToCelsius();
		public abstract Kelvin ToKelvin();
		public abstract Fahrenheit ToFahrenheit();
	}

	public class Celsius : AbstractTemperature
	{
		public Celsius(float TemperatureValue)
		{
			Unit = TemperatureValue;
		}
		public override string Name
		{
			get { return "Celsius"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "C";
			}
		}
		public override Celsius ToCelsius()
		{
			return this;
		}
		public override Kelvin ToKelvin()
		{
			return new Kelvin(Unit + 273.15f);
		}
		public override Fahrenheit ToFahrenheit()
		{
			return new Fahrenheit((Unit * 1.8f) + 32);
		}
	}

	public class Kelvin : AbstractTemperature
	{
		public override string DisplayUnit
		{
			get
			{
				return "K";
			}
		}
		public override string Name
		{
			get { return "Kelvin"; }
		}
		public Kelvin(float TemperatureValue)
		{
			Unit = TemperatureValue;
		}
		public override Celsius ToCelsius()
		{
			return new Celsius(Unit - 273.15f);
		}
		public override Kelvin ToKelvin()
		{
			return this;
		}
		public override Fahrenheit ToFahrenheit()
		{
			return new Fahrenheit((Unit * 1.8f) - 459.67f);
		}
	}


	public class Fahrenheit : AbstractTemperature
	{
		public override string DisplayUnit
		{
			get
			{
				return "F";
			}
		}
		public override string Name
		{
			get { return "Fahrenheit"; }
		}
		public Fahrenheit(float TemperatureValue)
		{
			Unit = TemperatureValue;
		}
		public override Celsius ToCelsius()
		{
			return new Celsius((Unit - 32) * 5 / 9);
		}
		public override Kelvin ToKelvin()
		{
			return new Kelvin((Unit + 459.67f) / 1.8f);
		}
		public override Fahrenheit ToFahrenheit()
		{
			return this;
		}
	}
}