using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[Range(0, 15)]
	public int SimulationRate = 1;
	private Mesh Sphere;
	public List<Vector3> vertices;
	private float waitDuration = .01f;
	public bool SlowlyPopulate = false;
	public float planetaryRadius = 15;

	[SerializeField]
	private List<string> FactorNames;

	private List<EnvFactor> AllFactors;
	[SerializeField]
	public List<FactorRule> AllFactorRules;
	public Dictionary<string, EnvFactor> factors;

	#region String Keys
	public string Temperature = "Temperature";
	public string H2 = "H2";
	public string O2 = "O2";
	public string N2 = "N2";
	public string CH4 = "CH4";
	public string Ar = "Ar";
	public string CO2 = "CO2";
	public string SolidCarbon = "Solid Carbon";
	public string LiquidH20 = "Liquid H20";
	public string SolidH20 = "Solid H20";
	public string GaseousH20 = "Gaseous H20";
	public string SingleCellLife = "Single Cell Life";
	public string Anaerobes = "Anaerobes";
	public string Cyanobacteria = "Cyanobacteria";
	public string Methanogens = "Methanogens";

	#endregion

	void Start()
	{
		AllFactors = new List<EnvFactor>();
		factors = new Dictionary<string, EnvFactor>();
		Sphere = GetComponent<MeshFilter>().mesh;
		vertices = new List<Vector3>();
		StartCoroutine(DisplayVerts());

		PopulateEnvFactorsForEarlyEarth();
		PopulateRules();
	}

	public void PopulateEnvFactorsForCurrentDay()
	{
		FactorValues newValue = new FactorValues(O2, 21, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(N2, 78, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(CH4, 0.00017f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Ar, 0.93f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(CO2, 0.038f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(SolidCarbon, 0.038f, new Vector2(0, 100000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Temperature, 284, new Vector2(0, 5778), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//This+Solid+gaseous = all water
		newValue = new FactorValues(LiquidH20, 69.74f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//70% of the earth is covered with water. 2% is fresh water. 90% of that is frozen.
		newValue = new FactorValues(SolidH20, 1.26f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(GaseousH20, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(SingleCellLife, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Anaerobes, 1, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);
	}

	//This is digging into the https://en.wikipedia.org/wiki/Great_Oxygenation_Event
	//Purpose here is to simulate the early starts.
	//We want to see:
	//Early H2+CO2 environment
	//Methanogenesis harnesses H2+CO2 into CH4 and water
	//
	//Heavy creation of O2
	//Create of CO2/CH4 (Anaerobic processes)
	//Death of Obligate Anaerobes.
	//Huronian Glaciation with freezing for potential snowball earth.
	public void PopulateEnvFactorsForEarlyEarth()
	{
		FactorValues newValue = new FactorValues(H2, 5, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(O2, 2, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(N2, 90, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(CH4, 1f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Ar, 1.93f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(CO2, 4.038f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(SolidCarbon, 40f, new Vector2(0, 100000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Temperature, 284, new Vector2(0, 5778), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//This+Solid+gaseous = all water
		newValue = new FactorValues(LiquidH20, 69.74f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//70% of the earth is covered with water. 2% is fresh water. 90% of that is frozen.
		newValue = new FactorValues(SolidH20, 1.26f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(GaseousH20, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(SingleCellLife, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Anaerobes, 1, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Cyanobacteria, .02f, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorValues(Methanogens, .02f, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);
	}

	public void PopulateRules()
	{
		FactorRule rule;
		#region Water Boiling
		rule = new FactorRule("Water Boiling",

			//Hotter than boiling point
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(373.2f)),
			//There's 1% surface water left
			new Requirement(ref factors[LiquidH20]._stats, new GreaterThan(1)),

			//When there is that much heat, it evaporates the water into gaseous form
			new Result(ref factors[Temperature]._stats, new AdjustValue(-5)),
			new Result(ref factors[LiquidH20]._stats, new AdjustValue(-1)),
			new Result(ref factors[GaseousH20]._stats, new AdjustValue(1))
		);
		AddRule(rule);
		#endregion

		#region Water Freezing
		rule = new FactorRule("Water Freezing",

			//Hotter than boiling point
			new Requirement(ref factors[Temperature]._stats, new LessThan(280.0f)),
			//There's 1% surface water left
			new Requirement(ref factors[LiquidH20]._stats, new GreaterThan(1)),

			//When there is that much heat, it evaporates the water into gaseous form
			new Result(ref factors[Temperature]._stats, new AdjustValue(-5)),
			new Result(ref factors[LiquidH20]._stats, new AdjustValueByDeltaTime(-.001f)),
			new Result(ref factors[SolidH20]._stats, new AdjustValueByDeltaTime(.001f))
		);
		AddRule(rule);
		#endregion

		#region Sun Heating the Planet
		rule = new FactorRule("Sun Heating Planet",

			//The sun will attempt to heat us to the temperature of the sun.
			new Requirement(ref factors[Temperature]._stats, new LessThan(5778)),

			//When there is that much heat, it evaporates the water into gaseous form
			new Result(ref factors[Temperature]._stats, new AdjustValueByDeltaTime(1))
		);
		AddRule(rule);
		#endregion

		#region Huronian Glaciation
		//Oxygen + Methane = CO2 + Water - Temperature
		//https://en.wikipedia.org/wiki/Huronian_glaciation
		rule = new FactorRule("Huronian Glaciation",
			//If too much O2 and CH4
			new Requirement(ref factors[O2]._stats, new GreaterThan(20.78f)),
			new Requirement(ref factors[CH4]._stats, new GreaterThan(.05f)),

			//CO2 and O2 form into CO2
			new Result(ref factors[O2]._stats, new AdjustValueByDeltaTime(-.1f)),
			new Result(ref factors[CH4]._stats, new AdjustValueByDeltaTime(-.1f)),
			new Result(ref factors[CO2]._stats, new AdjustValueByDeltaTime(.1f)),

			//We see temperature drops
			new Result(ref factors[Temperature]._stats, new AdjustValueByDeltaTime(-.5f))
		);
		AddRule(rule);
		#endregion

		#region Methanogenesis
		//CO2 + 4 H2 = CH4 + 2 H20
		//https://en.wikipedia.org/wiki/Methanogen
		rule = new FactorRule("Methanogenesis",
			//We don't want lots of O2 to do this.
			new Requirement(ref factors[CO2]._stats, new GreaterThan(5.0f)),
			//We need some carbon. (specifically glucose, but we aren't simulating that fine grain)
			new Requirement(ref factors[H2]._stats, new GreaterThan(1.0f)),
			//We need some heat (a bit above freezing)
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(295f)),
			//Methanogens are required for this process
			new Requirement(ref factors[Methanogens]._stats, new GreaterThan(2)),

			//More Methanogens.
			new Result(ref factors[Methanogens]._stats, new AdjustValueByDeltaTime(.4f)),
			new Result(ref factors[CH4]._stats, new AdjustValueByDeltaTime(.1f)),
			//Make some H20
			new Result(ref factors[CO2]._stats, new AdjustValueByDeltaTime(.1f))
		);
		AddRule(rule);
		#endregion

		#region Cyanobacteria Photosynthesis
		//Cyanobacteria Photosynthesis works in high CO2 environments
		//https://en.wikipedia.org/wiki/Obligate_anaerobe
		rule = new FactorRule("Cyanobacteria Photosynthesis",

			//Eat that CO2
			new Requirement(ref factors[CO2]._stats, new GreaterThan(6.0f)),
			//We need some carbon. (specifically glucose, but we aren't simulating that fine grain)
			new Requirement(ref factors[O2]._stats, new LessThan(60.0f)),
			//We need some heat (a bit above freezing)
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(295f)),

			//More Cyanobacteria.
			new Result(ref factors[Cyanobacteria]._stats, new AdjustValueByDeltaTime(2f)),
			//Absorb JUST a bit of heat
			new Result(ref factors[Temperature]._stats, new AdjustValueByDeltaTime(-0.002f)),
			//Eat some CO2
			new Result(ref factors[O2]._stats, new AdjustValueByDeltaTime(.6f)),
			//Make O2
			new Result(ref factors[CO2]._stats, new AdjustValueByDeltaTime(-.45f))
		);

		AddRule(rule);
		#endregion

		#region Anaerobic Fermentation
		//Obligate Anaerobes die if they hit 20% oxygen
		//https://en.wikipedia.org/wiki/Obligate_anaerobe
		rule = new FactorRule("Anaerobic Fermentation",
			//We don't want lots of O2 to do this.
			new Requirement(ref factors[O2]._stats, new LessThan(14.0f)),
			//We need some carbon. (specifically glucose, but we aren't simulating that fine grain)
			new Requirement(ref factors[SolidCarbon]._stats, new GreaterThan(2.0f)),
			//We need some heat (a bit above freezing)
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(275f)),

			//More Anaerobes.
			new Result(ref factors[Anaerobes]._stats, new AdjustValueByDeltaTime(.4f)),
			//new Result(ref factors[CH4]._stats, new AdjustValueByDeltaTime(.1f)));
			//Make some CO2
			new Result(ref factors[CO2]._stats, new AdjustValueByDeltaTime(.75f))
		);
		AddRule(rule);
		#endregion

		#region Obligate Anaerobes Death
		//Obligate Anaerobes die if they hit 20% oxygen
		//https://en.wikipedia.org/wiki/Obligate_anaerobe
		rule = new FactorRule("Obligate Anaerobes Death",

			//Too much O2
			new Requirement(ref factors[O2]._stats, new GreaterThan(20.78f)),

			//And Anaerobes die off (not changing the constraints of what is going on)
			new Result(ref factors[Anaerobes]._stats, new MultiplyByPercentAndDeltaTime(.15f))

		//Maybe they'd add to the 'carbo-matter' available?
		);
		AddRule(rule);
		#endregion
	}

	private void AddRule(FactorRule rule)
	{
		AllFactorRules.Add(rule);
	}

	private void RegisterNewEnvironmentFactor(FactorValues newFactor)
	{
		EnvFactor newEnvFactor = AddNewEnvironmentFactor(newFactor);
		factors.Add(newFactor.Key, newEnvFactor);
		FactorNames.Add(newFactor.Key);
		AllFactors.Add(newEnvFactor);
	}

	private EnvFactor AddNewEnvironmentFactor(FactorValues values)
	{
		GameObject go = new GameObject();

		EnvFactor newFactor = go.AddComponent<EnvFactor>();

		go.transform.SetParent(transform);

		newFactor.AssignFactorValues(values);
		return newFactor;
	}

	void Update()
	{
		for (int k = 0; k < SimulationRate; k++)
		{
			for (int i = 0; i < AllFactors.Count; i++)
			{
				AllFactors[i].Simulate();
			}
			for (int i = 0; i < AllFactorRules.Count; i++)
			{
				SimulateRule(AllFactorRules[i]);
			}
		}
	}

	public void SimulateRule(FactorRule rule)
	{
		if (rule.EvaluateRequirements())
		{
			rule.ApplyResults();
		}
	}

	IEnumerator DisplayVerts()
	{
		WaitForSeconds wait = new WaitForSeconds(waitDuration);
		for (int i = 0; i < Sphere.vertices.Length; i++)
		{
			vertices.Add(Sphere.vertices[i]);
			if (SlowlyPopulate)
			{
				yield return wait;
			}
		}
		SortVertices();
	}

	void SortVertices()
	{
		vertices = vertices.OrderBy(x => x.y).ThenBy(x => x.x).ToList();
	}
	void OnDrawGizmos()
	{
		if (vertices == null)
		{
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Count; i++)
		{
			//Gizmos.color = new Color((vertices[i].x) * 2, (vertices[i].y) * 2, (vertices[i].z) * 2);
			Gizmos.DrawSphere(transform.TransformPoint(vertices[i]), 0.1f);
		}
	}
}
