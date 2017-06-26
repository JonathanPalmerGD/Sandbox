using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
	public abstract class AbstractUnit
	{
		private float _unit = 0.0f;
		public float Unit
		{
			get { return _unit; }
			set { _unit = value; }
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