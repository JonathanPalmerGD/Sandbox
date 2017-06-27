using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Distance
{
	public abstract class AbstractTime : AbstractUnit
	{
		public override string Name
		{
			get { return "AbstractTime"; }
		}
		public override string Category
		{
			get { return "Time"; }
		}
		
		//This is to make the unit easily debuggable.
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToSecond().Display + "   " + ToHour().Display + "   ";
		}

		//This is a grouping of the available conversions
		public abstract Second ToSecond();
		public abstract Minute ToMinute();
		public abstract Hour ToHour();
		public abstract Year ToYear();
	}

	public class Second : AbstractTime
	{
		public override string Name
		{
			get { return "Seconds"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "s";
			}
		}
		public override string Category
		{
			get { return "Time"; }
		}

		public override Second ToSecond()
		{
			return this;
		}
		public override Minute ToMinute()
		{
			return new Minute(Unit / (60));
		}
		public override Hour ToHour()
		{
			return new Hour(Unit / (60 * 60));
		}
		public override Year ToYear()
		{
			return new Year(Unit / (60 * 60 * 24 * 365.25f));
		}

		public Second(float timeInterval)
		{
			Unit = timeInterval;
		}
		public Second() { }
	}

	public class Minute : AbstractTime
	{
		public override string Name
		{
			get { return "Minute"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "min";
			}
		}
		public override string Category
		{
			get { return "Time"; }
		}

		public override Second ToSecond()
		{
			return new Second(Unit / 60);
		}
		public override Minute ToMinute()
		{
			return this;
		}
		public override Hour ToHour()
		{
			return new Hour(Unit / (60 * 60));
		}
		public override Year ToYear()
		{
			return new Year(Unit / (60 * 60 * 24 * 365.25f));
		}

		public Minute(float timeInterval)
		{
			Unit = timeInterval;
		}
	}

	public class Hour : AbstractTime
	{
		public override string Name
		{
			get { return "Hour"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "hr";
			}
		}
		public override string Category
		{
			get { return "Time"; }
		}

		public override Second ToSecond()
		{
			return new Second(Unit * (60 * 60));
		}
		public override Minute ToMinute()
		{
			return new Minute(Unit / 60);
		}
		public override Hour ToHour()
		{
			return this;
		}
		public override Year ToYear()
		{
			return new Year(Unit * (24 * 365.25f));
		}

		public Hour(float timeInterval)
		{
			Unit = timeInterval;
		}
	}

	public class Year : AbstractTime
	{
		public override string Name
		{
			get { return "Year"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return "yr";
			}
		}
		public override string Category
		{
			get { return "Time"; }
		}

		public override Second ToSecond()
		{
			return new Second(Unit / (60 * 60 * 24 * 365.25f));
		}
		public override Minute ToMinute()
		{
			return new Minute(Unit / (60 * 24 * 365.25f));
		}
		public override Hour ToHour()
		{
			return new Hour(Unit / (24 * 365.25f));
		}
		public override Year ToYear()
		{
			return this;
		}

		public Year(float timeInterval)
		{
			Unit = timeInterval;
		}
	}
}