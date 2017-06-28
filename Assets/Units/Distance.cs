using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Distance
{
	public abstract class AbstractDistance : AbstractUnit
	{
		public override string Name
		{
			get { return "AbstractDistance"; }
		}
		public override string Category
		{
			get { return "Distance"; }
		}

		//This is to make the unit easily debuggable.
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToMeter().Display + "   " + ToInch().Display + "   " + ToFoot().Display + "   " + ToMile().Display;
		}

		//This is a grouping of the available conversions
		public abstract Meter ToMeter();
		public abstract Inch ToInch();
		public abstract Foot ToFoot();
		public abstract Mile ToMile();
	}

	public class Meter : AbstractDistance
	{
		public Meter(float DistanceValue)
		{
			Value = DistanceValue;
		}
		public override string Name
		{
			get { return "Meter"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "m";
			}
		}
		public override Meter ToMeter()
		{
			return this;
		}
		public override Inch ToInch()
		{
			return new Inch(Value * 39.3701f);
		}
		public override Foot ToFoot()
		{
			return new Foot(Value * 3.28084f);
		}
		public override Mile ToMile()
		{
			return new Mile(Value / 1609.34f);
		}

		public static Meter operator +(Meter c1, AbstractDistance c2)
		{
			return new Meter(c1.Value + c2.ToMeter().Value);
		}
		public static Meter operator -(Meter c1, AbstractDistance c2)
		{
			return new Meter(c1.Value - c2.ToMeter().Value);
		}

		//Using some operator overloading to create a Pow<T,U> of our new unit.
		public Pow<Meter, Squared> Multiply(Meter c1, AbstractDistance c2)
		{
			return Pow<Meter, Squared>.New(c1 + c2);
		}

		public Meter() { }
	}

	public class Inch : AbstractDistance
	{
		public Inch(float DistanceValue)
		{
			Value = DistanceValue;
		}
		public override string Name
		{
			get { return "Inch"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "in";
			}
		}
		public override Meter ToMeter()
		{
			return new Meter(Value / 39.3701f);
		}
		public override Inch ToInch()
		{
			return this;
		}
		public override Foot ToFoot()
		{
			return new Foot(Value / 12);
		}
		public override Mile ToMile()
		{
			return new Mile(Value / 63360);
		}

	}
	public class Foot : AbstractDistance
	{

		public Foot(float DistanceValue)
		{
			Value = DistanceValue;
		}
		public override string Name
		{
			get { return "Foot"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "ft";
			}
		}
		public override Meter ToMeter()
		{
			return new Meter(Value / 3.28084f);
		}
		public override Inch ToInch()
		{
			return new Inch(Value * 12);
		}
		public override Foot ToFoot()
		{
			return this;
		}
		public override Mile ToMile()
		{
			return new Mile(Value / 5280);
		}
	}
	public class Mile : AbstractDistance
	{
		public Mile(float DistanceValue)
		{
			Value = DistanceValue;
		}
		public override string Name
		{
			get { return "Mile"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "M";
			}
		}
		public override Meter ToMeter()
		{
			return new Meter(Value * 1609.34f);
		}
		public override Inch ToInch()
		{
			return new Inch(Value * 63360);
		}
		public override Foot ToFoot()
		{
			return new Foot(Value * 5280);
		}
		public override Mile ToMile()
		{
			return this;
		}
	}
}