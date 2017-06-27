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
			Unit = DistanceValue;
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
			return new Inch(Unit * 39.3701f);
		}
		public override Foot ToFoot()
		{
			return new Foot(Unit * 3.28084f);
		}
		public override Mile ToMile()
		{
			return new Mile(Unit / 1609.34f);
		}
		public Meter() { }
	}

	public class Inch : AbstractDistance
	{
		public Inch(float DistanceValue)
		{
			Unit = DistanceValue;
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
			return new Meter(Unit / 39.3701f);
		}
		public override Inch ToInch()
		{
			return this;
		}
		public override Foot ToFoot()
		{
			return new Foot(Unit / 12);
		}
		public override Mile ToMile()
		{
			return new Mile(Unit / 63360);
		}

	}
	public class Foot : AbstractDistance
	{

		public Foot(float DistanceValue)
		{
			Unit = DistanceValue;
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
			return new Meter(Unit / 3.28084f);
		}
		public override Inch ToInch()
		{
			return new Inch(Unit * 12);
		}
		public override Foot ToFoot()
		{
			return this;
		}
		public override Mile ToMile()
		{
			return new Mile(Unit / 5280);
		}
	}
	public class Mile : AbstractDistance
	{
		public Mile(float DistanceValue)
		{
			Unit = DistanceValue;
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
			return new Meter(Unit * 1609.34f);
		}
		public override Inch ToInch()
		{
			return new Inch(Unit * 63360);
		}
		public override Foot ToFoot()
		{
			return new Foot(Unit * 5280);
		}
		public override Mile ToMile()
		{
			return this;
		}
	}
}