using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactorValues
{
	public bool Dirty = false;
	[SerializeField]
	private string _key = "Unassigned Key";
	public string Key
	{
		get { return _key; }
		set { _key = value; }
	}
	[SerializeField]
	private Vector3 _location = Vector3.zero;
	public Vector3 Location
	{
		get { return _location; }
		set
		{
			_location = value;
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

	private float _radius = 15;
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

	public FactorValues(string key, float strength, Vector2 strengthRange, Vector3 location, float radius)
	{
		Key = key;
		StrengthRange = strengthRange;
		Radius = radius;
		Strength = strength;
		Location = location;
	}
}

public class EnvFactor : MonoBehaviour
{
	[SerializeField]
	public FactorValues _stats;
	public FactorValues Stats
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
	public void UpdateName()
	{
		gameObject.name = _stats.Key + " [" + Strength + "] - [" + PercentStrength * 100 + "%]     Range - " + StrengthRange.ToString();// + "  Location - " + Location.ToString() + "";
	}

	/// <summary>
	/// Construct the Stats with it's new constructor. Then pass it in.
	/// </summary>
	/// <param name="values"></param>
	public void AssignFactorValues(FactorValues values)
	{
		if (values != null && _stats == null)
		{
			_stats = values;
			UpdateName();
		}
	}

	public virtual void Simulate()
	{
		if (Stats.Dirty)
		{
			UpdateName();
			Stats.Dirty = false;
		}
	}

	//We have consequences of this factor going to different levels.
}
