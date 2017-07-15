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
		private float _value = 0.0f;
		public float Value
		{
			get { return _value; }
			set { _value = value; }
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
		/// <summary>
		/// Display the Value + Unit for human readability.
		/// </summary>
		public virtual string Display
		{
			get { return (Value + " " + ComplexDisplayUnit); }
		}
		public override string ToString()
		{
			return Display;
		}
		public string ComplexDisplayUnit
		{
			get { return (DisplayUnit + ""); }
		}
		public virtual string DisplayUnit
		{
			get { return "?"; }
		}
	}
}