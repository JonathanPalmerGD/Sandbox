using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//Have a single [Influences]
//[Influences]
//	[List<Outcomes>]
//		[Outcome]
//			[List<Condition>]
//				[Condition]
//					[EnvFactor I watch]
//					[What I require]
//			[List<Effect>]
//				[Effect]
//					[EnvFactor I effect]
//					[What happens to it]

[System.Serializable]
public class FactorRule
{
	public string RuleName;

	[SerializeField]
	public List<Requirement> requirements;
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
	public bool EvaluateRequirements()
	{
		bool areAllRequirementsMet = true;
		for (int i = 0; i < requirements.Count; i++)
		{
			areAllRequirementsMet = areAllRequirementsMet && requirements[i].Evaluate();
		}
		return areAllRequirementsMet;
	}
	public void ApplyResults()
	{
		for (int i = 0; i < results.Count; i++)
		{
			Debug.Log("Applying " + RuleName + "\n");
			results[i].Apply();
		}
	}
	public void Simulate()
	{
		for (int i = 0; i < requirements.Count; i++)
		{

		}
	}
	public void PrintRequirements()
	{
		Debug.Log(requirements.Count + "\n");
	}
}

public class RuleComponent
{

}
[System.Serializable]
public class Requirement : RuleComponent
{
	[SerializeField]
	public FactorValues Observed;
	[SerializeField]
	public Condition IfObservedIs;
	public bool Not = false;
	public Requirement(ref FactorValues FactorToObserve, Condition ConditionOnFactor)
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
public abstract class Condition
{
	public abstract bool IsConditionMet(ref FactorValues value);
}
[System.Serializable]
public class GreaterThan : Condition
{
	public float GatewayValue;
	public GreaterThan(float TrueIfGreaterThanThisValue)
	{
		GatewayValue = TrueIfGreaterThanThisValue;
	}
	public override bool IsConditionMet(ref FactorValues value)
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
	public override bool IsConditionMet(ref FactorValues value)
	{
		return value.Strength < GatewayValue;
	}
}
[System.Serializable]
public class WithinRange : Condition
{
	public Vector2 Range;
	public WithinRange(float MinBoundary, float MaxBoundary)
	{
		Range = new Vector2(MinBoundary, MaxBoundary);
	}
	public override bool IsConditionMet(ref FactorValues value)
	{
		return value.Strength > Range.x && value.Strength < Range.y;
	}
}

#endregion
[System.Serializable]
public class Result : RuleComponent
{
	[SerializeField]
	public FactorValues Target;
	[SerializeField]
	public Modification TargetModified;
	public Result(ref FactorValues TargetFactor, Modification newModification)
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
	public abstract void ModifyTarget(ref FactorValues value);
}
[System.Serializable]
public class AdjustValue : Modification
{
	public float Adjustment;
	public AdjustValue(float adjustment)
	{
		Adjustment = adjustment;
	}
	public override void ModifyTarget(ref FactorValues value)
	{
		value.Strength += Adjustment;
	}
}
[System.Serializable]
public class AdjustValueByDeltaTime : Modification
{
	public float Adjustment;
	public AdjustValueByDeltaTime(float adjustment)
	{
		Adjustment = adjustment;
	}
	public override void ModifyTarget(ref FactorValues value)
	{
		value.Strength += Adjustment * Time.deltaTime;
	}
}
//[System.Serializable]
//public class RefAdjustValueByDeltaTime : Modification
//{
//	public float Adjustment;
//	public EnvFactor Factor;
//	public RefAdjustValueByDeltaTime(float adjustment, ref FactorValues factor)
//	{
//		Adjustment = adjustment;
//		Factor = factor;
//	}
//	public override void ModifyTarget(ref FactorValues value)
//	{
//		value.Strength += Factor.Stats.Strength * Adjustment * Time.deltaTime;
//	}
//}
[System.Serializable]
public class MultiplyByPercentAndDeltaTime : Modification
{
	public float PercentChangePerSecond;
	public MultiplyByPercentAndDeltaTime(float percentChangePerSecond)
	{
		PercentChangePerSecond = percentChangePerSecond;
	}
	public override void ModifyTarget(ref FactorValues value)
	{
		//Debug.Log("Applying " + value.Key + " Mult by % & dt  " + value.Strength + " *  " + PercentChangePerSecond + "  /  " + Time.deltaTime + "\n");
		value.Strength = value.Strength * (PercentChangePerSecond * Time.deltaTime);
	}
}
[System.Serializable]
public class Floor : Modification
{
	public Floor()
	{
	}
	public override void ModifyTarget(ref FactorValues value)
	{
		value.Strength = value.StrengthRange.x;
	}
}
[System.Serializable]
public class Ceiling : Modification
{
	public Ceiling()
	{
	}
	public override void ModifyTarget(ref FactorValues value)
	{
		value.Strength = value.StrengthRange.y;
	}
}
#endregion
