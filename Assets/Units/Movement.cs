using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Distance
{
	public abstract class AbstractVelocity : AbstractUnit
	{
		//Since Velocity is a combination of a Distance Unit and a Time unit, we use each of these
		//Parent/Child lets us assign different variables to this. We can't strongly check what something is made of, but the names MeterPerSecond will generally help
		public AbstractDistance MovementDistance;
		public AbstractTime TimeInterval;

		public override string Name
		{
			get { return "AbstractMovement"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return MovementDistance.DisplayUnit + "/" + TimeInterval.DisplayUnit;
			}
		}
		public override string Category
		{
			get { return "Movement"; }
		}
		//This is to make the unit easily debuggable.
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToMetersPerSecond().Display + "   " + ToFeetPerSecond().Display + "   " + ToMilesPerHour().Display + "   ";
		}

		//There will be some objective to add an Integrate vs Differentiate

		//This is a grouping of the available conversions
		public abstract MetersPerSecond ToMetersPerSecond();
		public abstract FeetPerSecond ToFeetPerSecond();
		public abstract MilesPerHour ToMilesPerHour();
	}

	public class MetersPerSecond : AbstractVelocity
	{
		public override string Name
		{
			get { return "Meters Per Second"; }
		}
		public override string Category
		{
			get { return "Velocity"; }
		}

		public override MetersPerSecond ToMetersPerSecond()
		{
			return this;
		}
		public override FeetPerSecond ToFeetPerSecond()
		{
			return new FeetPerSecond(MovementDistance.ToFoot(), TimeInterval.ToSecond());
		}
		public override MilesPerHour ToMilesPerHour()
		{
			return new MilesPerHour(MovementDistance.ToMile(), TimeInterval.ToHour());
		}

		public MetersPerSecond(Meter distance, Second timeInterval)
		{
			MovementDistance = distance;
			TimeInterval = timeInterval;
			Value = MovementDistance.Value / TimeInterval.Value;
		}
	}

	public class FeetPerSecond : AbstractVelocity
	{
		public override string Name
		{
			get { return "Feet Per Second"; }
		}
		public override string Category
		{
			get { return "Velocity"; }
		}

		public override MetersPerSecond ToMetersPerSecond()
		{
			return new MetersPerSecond(MovementDistance.ToMeter(), TimeInterval.ToSecond());
		}
		public override FeetPerSecond ToFeetPerSecond()
		{
			return this;
		}
		public override MilesPerHour ToMilesPerHour()
		{
			return new MilesPerHour(MovementDistance.ToMile(), TimeInterval.ToHour());
		}

		public FeetPerSecond(Foot distance, Second timeInterval)
		{
			MovementDistance = distance;
			TimeInterval = timeInterval;
			Value = MovementDistance.Value / TimeInterval.Value;
		}
	}
	public class MilesPerHour : AbstractVelocity
	{
		public override string Name
		{
			get { return "Miles Per Hour"; }
		}
		public override string Category
		{
			get { return "Velocity"; }
		}

		public override MetersPerSecond ToMetersPerSecond()
		{
			return new MetersPerSecond(MovementDistance.ToMeter(), TimeInterval.ToSecond());
		}
		public override FeetPerSecond ToFeetPerSecond()
		{
			return new FeetPerSecond(MovementDistance.ToFoot(), TimeInterval.ToSecond());
		}
		public override MilesPerHour ToMilesPerHour()
		{
			return this;
		}

		public MilesPerHour(Mile distance, Hour timeInterval)
		{
			MovementDistance = distance;
			TimeInterval = timeInterval;
			Value = MovementDistance.Value / TimeInterval.Value;
		}
	}

	public abstract class AbstractAcceleration : AbstractUnit
	{
		public AbstractDistance MovementDistance;
		//public abstract Pow<AbstractTime, Squared> TimeInterval;
		//public Pow<AbstractTime, Squared> TimeInterval;
		//public virtual Pow<IAbstractUnit, Squared> TimeInterval
		//{
		//	get { return null; }
		//	set { }
		//}

		public override string Name
		{
			get { return "AbstractMovement"; }
		}
		public override string DisplayUnit
		{
			get
			{
				Debug.LogError("BROKEN\n");
				return MovementDistance.DisplayUnit + "/" + "MISSING TIME INTERVAL";
			}
		}
		public override string Category
		{
			get { return "Movement"; }
		}
		public string AllUnits()
		{
			return Name + " " + Category + ": " + ToMetersPerSecondSqrd().Display + "   ";
		}
		public abstract MetersPerSecondSqrd ToMetersPerSecondSqrd();
	}

	public class MetersPerSecondSqrd : AbstractAcceleration
	{
		//public override Pow<Second, Squared> TimeInterval
		//{
		//	get { return base.TimeInterval; }
		//	set { }
		//}
		public Pow<Second, Squared> TimeInterval;

		public override string Name
		{
			get { return "Meters Per Second^2"; }
		}
		public override string Category
		{
			get { return "Acceleration"; }
		}
		public override string DisplayUnit
		{
			get
			{
				return MovementDistance.DisplayUnit + "/" + TimeInterval.DisplayUnit;
			}
		}

		public override MetersPerSecondSqrd ToMetersPerSecondSqrd()
		{
			return this;
		}

		public MetersPerSecondSqrd(Meter distance, Pow<Second, Squared> timeInterval)
		{
			MovementDistance = distance;
			TimeInterval = timeInterval;
			Value = MovementDistance.Value / timeInterval.ResolvedUnit();
		}
	}
}