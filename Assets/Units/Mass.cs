using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Things
{
	public abstract class AbstractMass : AbstractUnit
	{
		public override string Name
		{
			get { return "AbstractMass"; }
		}
		public override string Category
		{
			get { return "Mass"; }
		}

		//This is to make the unit easily debuggable.
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToGram().Display + "   " + ToKilogram().Display + "   " + ToPound().Display;
		}

		//This is a grouping of the available conversions
		public abstract Gram ToGram();
		public abstract Milogram ToMilogram();
		public abstract Pound ToPound();
		public abstract Kilogram ToKilogram();
	}

	public class Milogram : AbstractMass
	{
		public override string Name
		{
			get { return "Milogram"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "mg";
			}
		}
		public override Milogram ToMilogram()
		{
			return this;
		}
		public override Gram ToGram()
		{
			return new Gram(Value / 1000);
		}
		public override Pound ToPound()
		{
			return new Pound(Value / 453.592f);
		}
		public override Kilogram ToKilogram()
		{
			return new Kilogram(Value / 1000);
		}

		//Testing out operator overloading
		public static Milogram operator +(Milogram c1, AbstractMass c2)
		{
			return new Milogram(c1.Value + c2.ToMilogram().Value);
		}
		public static Milogram operator -(Milogram c1, AbstractMass c2)
		{
			return new Milogram(c1.Value - c2.ToMilogram().Value);
		}

		public Milogram(float Mass)
		{
			Value = Mass;
		}
	}
	public class Gram : AbstractMass
	{
		public override string Name
		{
			get { return "Gram"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "g";
			}
		}
		public override Milogram ToMilogram()
		{
			return new Milogram(Value * 1000);
		}
		public override Gram ToGram()
		{
			return this;
		}
		public override Pound ToPound()
		{
			return new Pound(Value / 453.592f);
		}
		public override Kilogram ToKilogram()
		{
			return new Kilogram(Value / 1000);
		}

		//Testing out operator overloading
		public static Gram operator +(Gram c1, AbstractMass c2)
		{
			return new Gram(c1.Value + c2.ToGram().Value);
		}
		public static Gram operator -(Gram c1, AbstractMass c2)
		{
			return new Gram(c1.Value - c2.ToGram().Value);
		}
		
		public Gram(float Mass)
		{
			Value = Mass;
		}
	}
	public class Kilogram : AbstractMass
	{
		public override string Name
		{
			get { return "Kilogram"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "kg";
			}
		}
		public override Milogram ToMilogram()
		{
			return new Milogram(Value * 1000 * 1000);
		}
		public override Gram ToGram()
		{
			return new Gram(Value * 1000);
		}
		public override Pound ToPound()
		{
			return new Pound(Value * 2.20462f);
		}
		public override Kilogram ToKilogram()
		{
			return this;
		}

		//Testing out operator overloading
		public static Kilogram operator +(Kilogram c1, AbstractMass c2)
		{
			return new Kilogram(c1.Value + c2.ToGram().Value);
		}
		public static Kilogram operator -(Kilogram c1, AbstractMass c2)
		{
			return new Kilogram(c1.Value - c2.ToGram().Value);
		}

		public Kilogram(float Mass)
		{
			Value = Mass;
		}
	}
	public class Pound : AbstractMass
	{
		public override string Name
		{
			get { return "Pound"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "lb";
			}
		}
		public override Milogram ToMilogram()
		{
			return new Milogram(Value * 453.592f * 1000);
		}
		public override Gram ToGram()
		{
			return new Gram(Value * 453.592f);
		}
		public override Pound ToPound()
		{
			return this;
		}
		public override Kilogram ToKilogram()
		{
			return new Kilogram(Value * .45392f);
		}

		//Testing out operator overloading
		public static Pound operator +(Pound c1, AbstractMass c2)
		{
			return new Pound(c1.Value + c2.ToGram().Value);
		}
		public static Pound operator -(Pound c1, AbstractMass c2)
		{
			return new Pound(c1.Value - c2.ToGram().Value);
		}

		public Pound(float Mass)
		{
			Value = Mass;
		}
	}
}