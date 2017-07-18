using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is how we define the rules the simulation must follow.


[System.Serializable]
public class FactorRule
{
	//Rules want names for human readability
	public string RuleName;

	/// <summary>
	/// Requirements govern what is needed before a rule can be followed. They observe factors in the simulation
	/// </summary>
	[SerializeField]
	public List<Requirement> requirements;
	/// <summary>
	/// Results govern what happen when a rule is followed. They adjust Factors in the simulation.
	/// </summary>
	[SerializeField]
	public List<Result> results;

	public FactorRule()
	{
		requirements = new List<Requirement>();
		results = new List<Result>();
	}
	public FactorRule(params RuleComponent[] arguments)
	{
		requirements = new List<Requirement>();
		results = new List<Result>();

		for (int i = 0; i < arguments.Length; i++)
		{
			AddRuleComponent(arguments[i]);
		}
	}
	public FactorRule(string name, params RuleComponent[] arguments)
	{
		RuleName = name;
		requirements = new List<Requirement>();
		results = new List<Result>();

		for (int i = 0; i < arguments.Length; i++)
		{
			AddRuleComponent(arguments[i]);
		}
	}
	/// <summary>
	/// Requirements and Results are both RuleComponents. This only serves to make nicer params constructors.
	/// </summary>
	/// <param name="component"></param>
	public void AddRuleComponent(RuleComponent component)
	{
		if (component.GetType() == typeof(Requirement))
		{
			AddRequirement((Requirement)component);
		}
		if (component.GetType() == typeof(Result))
		{
			AddResult((Result)component);
		}
	}
	public void AddRequirement(Requirement newRequirement)
	{
		requirements.Add(newRequirement);
	}
	public void AddResult(Result newEffect)
	{
		results.Add(newEffect);
	}

	/// <summary>
	/// For if all the requirements are met.
	/// </summary>
	/// <returns></returns>
	public bool EvaluateRequirements()
	{
		bool areAllRequirementsMet = true;
		for (int i = 0; i < requirements.Count; i++)
		{
			areAllRequirementsMet = areAllRequirementsMet && requirements[i].Evaluate();
		}
		return areAllRequirementsMet;
	}
	/// <summary>
	/// For applying the results to all relevant factors.
	/// Will not violate ranges on factors.
	/// </summary>
	public void ApplyResults()
	{
		for (int i = 0; i < results.Count; i++)
		{
			//Debug.Log("Applying " + RuleName + "\n");
			results[i].Apply();
		}
	}
}

public class RuleComponent
{

}
[System.Serializable]
public class Requirement : RuleComponent
{
	/// <summary>
	/// The factor that must meet our condition for this requirement to be met.
	/// </summary>
	[SerializeField]
	public FactorData Observed;
	/// <summary>
	/// A condition lets us govern HOW we want to require the observed to behave. GreaterThan, LessThan, WithinRange, etc
	/// </summary>
	[SerializeField]
	public Condition IfObservedIs;
	//Decided not to support this. I'd use a XOR on Evaluate to get the intended behavior.
	//public bool Not = false;
	public Requirement(ref FactorData FactorToObserve, Condition ConditionOnFactor)
	{
		Observed = FactorToObserve;
		IfObservedIs = ConditionOnFactor;
	}
	public bool Evaluate()
	{
		if (Observed == null)
			Debug.LogError("Observed by Requirement was null!\n");
		return IfObservedIs.IsConditionMet(ref Observed);
	}
}
#region Types of Requirement Condition
/// <summary>
/// A condition must be evaluated related to a Factor.
/// Use the GreaterThan, LessThan and WithinRange Conditions to evaluate requirements
/// </summary>
public abstract class Condition
{
	public abstract bool IsConditionMet(ref FactorData value);
}
[System.Serializable]
public class GreaterThan : Condition
{
	public float GatewayValue;
	/// <summary>
	/// We store the value at time of condition creation. Then use it to evaluate.
	/// </summary>
	/// <param name="TrueIfGreaterThanThisValue"></param>
	public GreaterThan(float TrueIfGreaterThanThisValue)
	{
		GatewayValue = TrueIfGreaterThanThisValue;
	}
	public override bool IsConditionMet(ref FactorData value)
	{
		return value.Strength > GatewayValue;
	}
}
[System.Serializable]
public class LessThan : Condition
{
	public float GatewayValue;
	public LessThan(float TrueIfLessThanThisValue)
	{
		GatewayValue = TrueIfLessThanThisValue;
	}
	public override bool IsConditionMet(ref FactorData value)
	{
		return value.Strength < GatewayValue;
	}
}
[System.Serializable]
public class WithinRange : Condition
{
	public Vector2 Range;
	/// <summary>
	/// We store two different values. If the observed factor's value is BETWEEN them, it will be true.
	/// </summary>
	/// <param name="MinBoundary"></param>
	/// <param name="MaxBoundary"></param>
	public WithinRange(float MinBoundary, float MaxBoundary)
	{
		Range = new Vector2(MinBoundary, MaxBoundary);
	}
	public override bool IsConditionMet(ref FactorData value)
	{
		return value.Strength > Range.x && value.Strength < Range.y;
	}
}

//There is no support as of yet for something being related to ANOTHER factor's value.
//Such as, True if (H2 > O2), otherwise false.

#endregion
[System.Serializable]
public class Result : RuleComponent
{
	/// <summary>
	/// What will be affected by this result.
	/// </summary>
	[SerializeField]
	public FactorData Target;
	/// <summary>
	/// How we will modify our target when the result is applied.
	/// </summary>
	[SerializeField]
	public Modification TargetModified;
	public Result(ref FactorData TargetFactor, Modification newModification)
	{
		Target = TargetFactor;
		TargetModified = newModification;
	}
	public void Apply()
	{
		TargetModified.ModifyTarget(ref Target);
	}
}

#region Types of Result Modification
public abstract class Modification
{
	public abstract void ModifyTarget(ref FactorData value);
}
/// <summary>
/// Adjust by a fixed value
/// </summary>
[System.Serializable]
public class AdjustByFixedValue : Modification
{
	public float Adjustment;
	public AdjustByFixedValue(float adjustment)
	{
		Adjustment = adjustment;
	}
	public override void ModifyTarget(ref FactorData value)
	{
		value.Strength += Adjustment;
	}
}
[System.Serializable]
public class AdjustValueDeltaTime : Modification
{
	public float Adjustment;
	/// <summary>
	/// Adjustment is multiplied by Time.deltaTime
	/// </summary>
	/// <param name="adjustment"></param>
	public AdjustValueDeltaTime(float adjustment)
	{
		Adjustment = adjustment;
	}
	public override void ModifyTarget(ref FactorData value)
	{
		//Later we would want to step away from Time.deltaTime so we could control our simulation better.
		value.Strength += Adjustment * Time.deltaTime;
	}
}
/// <summary>
/// This was one I didn't get working quite yet.
/// The idea is that it adjusts a factor by the value of another factor.
/// Example: Adjust O2 based on how many Cyanobacteria there are
/// </summary>
[System.Serializable]
public class RefAdjustValueByDeltaTime : Modification
{
	public float Adjustment;
	public FactorData Factor;
	public RefAdjustValueByDeltaTime(float adjustment, ref FactorData factor)
	{
		Adjustment = adjustment;
		Factor = factor;
	}
	public override void ModifyTarget(ref FactorData value)
	{
		value.Strength += Factor.Strength * Adjustment * Time.deltaTime;
	}
}
/// <summary>
/// This is for VERY fast adjustment of something over time. It multiplies based on the value.
/// </summary>
[System.Serializable]
public class MultiplyByPercentAndDeltaTime : Modification
{
	public float PercentChangePerSecond;
	public MultiplyByPercentAndDeltaTime(float percentChangePerSecond)
	{
		PercentChangePerSecond = percentChangePerSecond;
	}
	public override void ModifyTarget(ref FactorData value)
	{
		//Debug.Log("Applying " + value.Key + " Mult by % & dt  " + value.Strength + " *  " + PercentChangePerSecond + "  /  " + Time.deltaTime + "\n");
		value.Strength = value.Strength * (PercentChangePerSecond * Time.deltaTime);
	}
}
/// <summary>
/// Modifies the target by setting it to it's floor available value.
/// Example, opening the airlock floors the amount of O2 in the spaceship.
/// </summary>
[System.Serializable]
public class Floor : Modification
{
	public Floor()
	{
	}
	public override void ModifyTarget(ref FactorData value)
	{
		value.Strength = value.StrengthRange.x;
	}
}
/// <summary>
/// Modifies the target by setting it to it's ceiling available value.
/// Example: Filling the room with happiness when you start dancing.
/// </summary>
[System.Serializable]
public class Ceiling : Modification
{
	public Ceiling()
	{
	}
	public override void ModifyTarget(ref FactorData value)
	{
		value.Strength = value.StrengthRange.y;
	}
}
#endregion
