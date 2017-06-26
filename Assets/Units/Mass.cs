using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Things
{
	public abstract class AbstractMass : AbstractUnit
	{
		public AbstractMass Mass;

		public override string Name
		{
			get { return "AbstractMass"; }
		}
		public override string Category
		{
			get { return "Mass"; }
		}
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToGrams().Display + "   " + ToKilograms().Display + "   " + ToPounds().Display;
		}
		public abstract Gram ToGrams();
		public abstract Pound ToPounds();
		public abstract Kilogram ToKilograms();
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
		public override Gram ToGrams()
		{
			return this;
		}
		public override Pound ToPounds()
		{
			return new Pound(Unit / 453.592f);
		}
		public override Kilogram ToKilograms()
		{
			return new Kilogram(Unit / 1000);
		}

		public Gram(float Mass)
		{
			Unit = Mass;
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
		public override Gram ToGrams()
		{
			return new Gram(Unit * 1000);
		}
		public override Pound ToPounds()
		{
			return new Pound(Unit * 2.20462f);
		}
		public override Kilogram ToKilograms()
		{
			return this;
		}

		public Kilogram(float Mass)
		{
			Unit = Mass;
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
		public override Gram ToGrams()
		{
			return new Gram(Unit * 453.592f);
		}
		public override Pound ToPounds()
		{
			return this;
		}
		public override Kilogram ToKilograms()
		{
			return new Kilogram(Unit * .45392f);
		}

		public Pound(float Mass)
		{
			Unit = Mass;
		}
	}
}