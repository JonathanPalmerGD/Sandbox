using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
	/// <summary>
	/// The empty, but core interface of how Units can behave. Will fill this in as necessarily.
	/// Some other aspects might be required in the interface.
	/// </summary>
	public interface IAbstractUnit
	{

	}

	/// <summary>
	/// This is our Abstract Unit class. Abstract so nobody uses it but we can force default behavior with virtuals.
	/// </summary>
	public abstract class AbstractUnit : IAbstractUnit
	{
		private float _unit = 0.0f;
		public float Unit
		{
			get { return _unit; }
			set { _unit = value; }
		}

		public AbstractUnit()
		{

		}

		public virtual string Name
		{
			get { return "Units"; }
		}
		public virtual string Category
		{
			get { return "Raw"; }
		}
		public virtual string AllRelatedUnits()
		{
			return Name + " " + Category + ": " + Display;
		}
		public virtual string Display
		{
			get { return (Unit + " " + Value); }
		}
		public string Value
		{
			get { return (DisplayUnit + ""); }
		}
		public virtual string DisplayUnit
		{
			get { return "?"; }
		}
	}
}