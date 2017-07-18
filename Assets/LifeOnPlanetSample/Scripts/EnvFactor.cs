using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A unity component for storing and accessing FactorData (a pure data class)
/// Stands for EnvironmentFactor (but that's a longer name than necessary)
/// </summary>
public class EnvFactor : MonoBehaviour
{
	[SerializeField]
	public FactorData _stats;
	/// <summary>
	/// Our property for accessing our pure data object FactorData
	/// </summary>
	public FactorData Stats
	{
		get { return _stats; }
	}

	public Vector3 Location
	{
		get { return _stats.Location; }
		set { _stats.Location = value; }
	}
	public float Radius
	{
		get { return _stats.Radius; }
		set { _stats.Radius = value; }
	}
	public Vector2 StrengthRange
	{
		get { return _stats.StrengthRange; }
		set { _stats.StrengthRange = value; }
	}
	public float Strength
	{
		get { return _stats.Strength; }
		set
		{
			LastStrength = _stats.Strength;
			_stats.Strength = Mathf.Clamp(value, StrengthRange.x, StrengthRange.y);
		}
	}
	public float PercentStrength
	{
		get { return (Strength - StrengthRange.x) / StrengthRange.y; }
		set
		{
			Strength = (StrengthRange.y - StrengthRange.x) * value + StrengthRange.x;
		}
	}
	private float LastStrength;
	private float changeCounter = 2.5f;
	public void UpdateName()
	{
		string change = LastStrength > _stats.Strength ? "V" : LastStrength == _stats.Strength ? " " : "^";
		gameObject.name = _stats.Key + " " + change + "[" + Strength + "] - [" + PercentStrength * 100 + "%]     Range - " + StrengthRange.ToString();// + "  Location - " + Location.ToString() + "";
	}

	/// <summary>
	/// Construct the Stats with its new() constructor. Then pass it in.
	/// </summary>
	/// <param name="values"></param>
	public void AssignFactorData(FactorData values)
	{
		if (values != null && _stats == null)
		{
			_stats = values;
			UpdateName();
		}
	}

	/// <summary>
	/// Ideally this would be overridden for very complex factors (like Humans) and would govern their own rules.
	/// </summary>
	public virtual void Simulate()
	{
		if (changeCounter > 0)
		{
			changeCounter -= Time.deltaTime;
			if (changeCounter <= 0)
			{
				changeCounter = 0;
				Stats.Dirty = true;
				LastStrength = Stats.Strength;
			}
		}
		if (Stats.Dirty)
		{
			UpdateName();
			Stats.Dirty = false;
		}
	}
}

/// <summary>
/// A pure data object for simulating rules and factors.
/// One is held by each EnvFactor
/// </summary>
[System.Serializable]
public class FactorData
{
	//Used to update the visual names.
	public bool Dirty = false;
	[SerializeField]
	private string _key = "Unassigned Key";
	public string Key
	{
		get { return _key; }
		set { _key = value; }
	}

	[SerializeField]
	private float _value = 0;
	public float Strength
	{
		get { return _value; }
		set
		{
			_value = Mathf.Clamp(value, StrengthRange.x, StrengthRange.y);
			Dirty = true;
		}
	}
	public float PercentStrength
	{
		get { return (Strength - StrengthRange.x) / StrengthRange.y; }
		set
		{
			Strength = (StrengthRange.y - StrengthRange.x) * value + StrengthRange.x;
			Dirty = true;
		}
	}
	[SerializeField]
	private Vector2 _strRange = new Vector2(float.MinValue, float.MaxValue);
	public Vector2 StrengthRange
	{
		get { return _strRange; }
		set
		{
			_strRange = value;
			Dirty = true;
		}
	}


	//I chose not to focus on the location/area aspects of factors, but rather how they affect each other through the rules.
	private float _radius = 15;
	/// <summary>
	/// How big this factor's area of effect is.
	/// </summary>
	public float Radius
	{
		get { return _radius; }
		set
		{
			_radius = value;
			Dirty = true;
		}
	}
	[SerializeField]
	private Vector3 _location = Vector3.zero;
	/// <summary>
	/// For factors that have location specific effects.
	/// </summary>
	public Vector3 Location
	{
		get { return _location; }
		set
		{
			_location = value;
			Dirty = true;
		}
	}

	public FactorData(string key, float strength, Vector2 strengthRange, Vector3 location, float radius)
	{
		Key = key;
		StrengthRange = strengthRange;
		Radius = radius;
		Strength = strength;
		Location = location;
	}
}