using System;
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
		//This could be a static thing? I know these don't want to live as in-lines, but what is more efficient/readable?
		public string Degree { get { return "°"; } }

		public override string Name
		{
			get { return "Unknown"; }
		}
		public override string Category
		{
			get { return "Temperature"; }
		}
		//This is to make the unit easily debuggable.
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToCelsius().Display + "   " + ToFahrenheit().Display + "   " + ToKelvin().Display;
		}

		//We use a custom display here because we do '°' followed by the unit
		public override string Display
		{
			get { return (Value + " "+ Degree + ComplexDisplayUnit); }
		}
		
		//This is a grouping of the available conversions
		public abstract Celsius ToCelsius();
		public abstract Kelvin ToKelvin();
		public abstract Fahrenheit ToFahrenheit();
	}

	public class Celsius : AbstractTemperature
	{
		public Celsius(float TemperatureValue)
		{
			Value = TemperatureValue;
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
			return new Kelvin(Value + 273.15f);
		}
		public override Fahrenheit ToFahrenheit()
		{
			return new Fahrenheit((Value * 1.8f) + 32);
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
			Value = TemperatureValue;
		}
		public override Celsius ToCelsius()
		{
			return new Celsius(Value - 273.15f);
		}
		public override Kelvin ToKelvin()
		{
			return this;
		}
		public override Fahrenheit ToFahrenheit()
		{
			return new Fahrenheit((Value * 1.8f) - 459.67f);
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
			Value = TemperatureValue;
		}
		public override Celsius ToCelsius()
		{
			return new Celsius((Value - 32) * 5 / 9);
		}
		public override Kelvin ToKelvin()
		{
			return new Kelvin((Value + 459.67f) / 1.8f);
		}
		public override Fahrenheit ToFahrenheit()
		{
			return this;
		}
	}
}