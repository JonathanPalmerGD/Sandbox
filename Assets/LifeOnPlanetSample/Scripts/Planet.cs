using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	/// <summary>
	/// This controls how many times we simulate per frame.
	/// This lets us fast-forward and pause our simulation.
	/// </summary>
	[Range(0, 15)]
	public int SimulationRate = 1;

	#region Planetary Details
	//The purpose of these variables is to allow us to create multiple planets at once.
	//The idea was also to define factors to have localized regions.
	private Mesh Sphere;
	public List<Vector3> vertices;
	private float waitDuration = .01f;

	public bool SlowlyPopulate = false;
	public float planetaryRadius = 15;
	#endregion

	[SerializeField]
	[Header("Each of the factors involved")]
	private List<string> FactorNames;

	/// <summary>
	/// This is for each of the constructed rules we have added to the simulation system.
	/// </summary>
	[SerializeField]
	public List<FactorRule> AllFactorRules;
	
	/// <summary>
	/// This is a dictionary for cheap lookup (instead of endlessly iterating through a list)
	/// </summary>
	public Dictionary<string, EnvFactor> factors;

	/// <summary>
	/// This is a list for easy iteration (dictionaries and hash sets aren't well optimized for iteration)
	/// </summary>
	private List<EnvFactor> AllFactors;

	#region String Keys
	public string Temperature = "Temperature";
	public string H2 = "H2";
	public string O2 = "O2";
	public string N2 = "N2";
	public string CH4 = "CH4";
	public string Ar = "Ar";
	public string CO2 = "CO2";
	public string UnrustedMetals = "Fe";
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
		//Set up our data structures.
		AllFactors = new List<EnvFactor>();
		factors = new Dictionary<string, EnvFactor>();
		Sphere = GetComponent<MeshFilter>().mesh;
		vertices = new List<Vector3>();
		StartCoroutine(DisplayVerts());

		//Populate our factors and rules. These would be loaded from a file in the future.
		PopulateEnvFactorsForEarlyEarth();
		PopulateRules();
	}

	/// <summary>
	/// This was my first attempt at simulating the modern day atmosphere and factors on the surface.
	/// I rapidly decided to start with an Early Earth for the Great Oxygenation Extinction.
	/// </summary>
	public void PopulateEnvFactorsForCurrentDay()
	{
		//This code is not used, but a test case for the idea of creating factors and having them be influenced by the planet.
		FactorData newValue = new FactorData(O2, 21, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(N2, 78, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(CH4, 0.00017f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Ar, 0.93f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(CO2, 0.038f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(SolidCarbon, 0.038f, new Vector2(0, 100000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Temperature, 284, new Vector2(0, 5778), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//This+Solid+gaseous = all water
		newValue = new FactorData(LiquidH20, 69.74f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//70% of the earth is covered with water. 2% is fresh water. 90% of that is frozen.
		newValue = new FactorData(SolidH20, 1.26f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(GaseousH20, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(SingleCellLife, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Anaerobes, 1, new Vector2(0, 5000), transform.position, planetaryRadius);
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
		//All of these could be loaded from a file in the future (and saved to files for later simulation/debugging)
		FactorData newValue = new FactorData(H2, 5, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(O2, 2, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(N2, 90, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(CH4, 1f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Ar, 1.93f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//Get some metal in here for Anaerobic Corrosion
		newValue = new FactorData(UnrustedMetals, 35, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(CO2, 4.038f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//This isn't an accurate value, but lets me represent the consumption of available unstable carbon.
		newValue = new FactorData(SolidCarbon, 40f, new Vector2(0, 100000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//This is in Kelvin. Avg temperature is a 287 (14 degrees Celsius)
		newValue = new FactorData(Temperature, 287, new Vector2(0, 5778), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//This+Solid+gaseous = all water
		newValue = new FactorData(LiquidH20, 69.74f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		//70% of the earth is covered with water. 2% is fresh water. 90% of that is frozen.
		newValue = new FactorData(SolidH20, 1.26f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(GaseousH20, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(SingleCellLife, .001f, new Vector2(0, 100), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Anaerobes, 1, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Cyanobacteria, .02f, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);

		newValue = new FactorData(Methanogens, .02f, new Vector2(0, 5000), transform.position, planetaryRadius);
		RegisterNewEnvironmentFactor(newValue);
	}

	/// <summary>
	/// Create the rules that govern our early earth environment
	/// </summary>
	public void PopulateRules()
	{
		//The purpose of these rules is to force several events to occur that simulate approximately what happens in Early Earth during Great Oxygen Extinction.
		//These rules aren't highly accurate or anything. I pulled the data together in a few hours of Wikipedia/other site searching
		//The goal was to create a base system that has factors that influence one another according to defined rules.

		FactorRule rule;
		//All of these could be loaded from a file in the future (and saved to files for later simulation/debugging)

		#region Water Boiling
		rule = new FactorRule("Water Boiling",

			//Hotter than boiling point
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(373.2f)),
			//There's 1% surface water left
			new Requirement(ref factors[LiquidH20]._stats, new GreaterThan(1)),

			//Likely wouldn't be that big of a temperature drop
			//The temperature goes down because now the gaseous H20 is more energetic.
			new Result(ref factors[Temperature]._stats, new AdjustByFixedValue(-.25f)),

			//When there is that much heat, it evaporates the water into gaseous form
			new Result(ref factors[LiquidH20]._stats, new AdjustByFixedValue(-.1f)),
			new Result(ref factors[GaseousH20]._stats, new AdjustByFixedValue(.1f))
		);
		AddRule(rule);
		#endregion

		#region Water Freezing
		rule = new FactorRule("Water Freezing",

			//Colder than freezing point
			new Requirement(ref factors[Temperature]._stats, new LessThan(273.2f)),

			//There's 1% surface water left
			new Requirement(ref factors[LiquidH20]._stats, new GreaterThan(1)),

			//I doubt the temperature raise would be ANYWHERE near this significant.
			//Temperature goes up because the energy is now in the form of the bonds holding the water into a frozen form.
			new Result(ref factors[Temperature]._stats, new AdjustByFixedValue(.25f)),

			//Lets get a little more solid water.
			new Result(ref factors[LiquidH20]._stats, new AdjustValueDeltaTime(-.1f)),
			new Result(ref factors[SolidH20]._stats, new AdjustValueDeltaTime(.1f))
		);
		AddRule(rule);
		#endregion

		#region Ice Melting [Not implemented]
		//I chose not to implement this rule deliberately.
		//MY simulation does not yet support segregation of temperatures or factors.
		//This means most simulations would immediately force the temperature just around freezing point, rather than maintaining a near earth temperature.
		//Earth's ice is usually maintained through uneven distribution of the temperature.

		//Condensation would likely fit in the same boat here.
		#endregion

		#region Sun Heating the Planet
		rule = new FactorRule("Sun Heating Planet",

			//Based on what the University of Tennesse's Agriculture Solar/Sunstainable Energy page says:
			//https://ag.tennessee.edu/solar/Pages/What%20Is%20Solar%20Energy/Sun%27s%20Energy.aspx
			//Earth is soaking up heat by 1368 W/m^2. However only about 1000 of it reaches it to the earth's surface.
			//Earth is 510.1 trillion m^2. Let's cut this in half to represent the 'Day' side of the planet.
			//This means that the earth is soaking up 255.1 PetaWatts of energy
			//Another resource, Wikipedia, gives us a smaller number of 174 PW (https://en.wikipedia.org/wiki/Watt#Petawatt) - Wolfram also backs this number up.
			//Note to self: Read about the Solar Constant, because that looks cool.
		
			//I'd like to dig into this deeper and calculate how much temperature differential is caused by 174 PetaWatts pouring into earth, but I want to tie up these comments.

			//The sun will attempt to heat us to the temperature of the sun.
			new Requirement(ref factors[Temperature]._stats, new LessThan(5778)),

			//So I'll say it is .174 degrees kelvin per 'time interval' of every second of simulation.
			new Result(ref factors[Temperature]._stats, new AdjustValueDeltaTime(.174f))
		);
		AddRule(rule);
		#endregion

		#region Nocturnal Cooling of Earth
		rule = new FactorRule("Sun Heating Planet",

			//Doing some math
			//https://upload.wikimedia.org/wikipedia/commons/8/8f/Erbe.gif
			//Assume we're cooling 225 Watts / m^2.
			//Earth is 510.1 trillion m^2. Let's cut this in half to represent the 'Night' side of the planet.
			//This means we	are losing 57.39 PetaWatts of energy every 'time interval'
			//I'll say a single second is a time interval so our Rule can have some numerical basis.

			//The sun will attempt to heat us to the temperature of the sun.
			new Requirement(ref factors[Temperature]._stats, new LessThan(5778)),

			//When there is that much heat, it evaporates the water into gaseous form
			new Result(ref factors[Temperature]._stats, new AdjustValueDeltaTime(.05739f))
		);
		AddRule(rule);
		#endregion

		#region Huronian Glaciation
		//Oxygen + Methane = CO2 + Water - Temperature
		//https://en.wikipedia.org/wiki/Huronian_glaciation
		rule = new FactorRule("Huronian Glaciation",
			//If too much O2 and CH4, and it isn't too cold
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(270)),
			new Requirement(ref factors[O2]._stats, new GreaterThan(20.78f)),
			new Requirement(ref factors[CH4]._stats, new GreaterThan(.05f)),

			//CH4 and O2 form into CO2
			new Result(ref factors[O2]._stats, new AdjustValueDeltaTime(-.1f)),
			new Result(ref factors[CH4]._stats, new AdjustValueDeltaTime(-.1f)),

			new Result(ref factors[CO2]._stats, new AdjustValueDeltaTime(.05f)),

			//We see high temperature drops (which will drive the freezing of liquid water)
			new Result(ref factors[Temperature]._stats, new AdjustValueDeltaTime(-2.5f))
		);
		AddRule(rule);
		#endregion

		#region Anaerobic corrosion
		//Fe + 2H2O -> Fe(OH)2 + H2
		//https://en.wikipedia.org/wiki/Hydrogen#Anaerobic_corrosion
		//We want to represent (however inaccurately) some introduction of hydrogen into the system.
		//I choose to represent this because it's a good candidate for it's interaction with water.
		//I did chunk both parts of anaerobic corrosion of iron together into one rule.
		rule = new FactorRule("Anaerobic Corrosion",
			//Need some metal to break apart the rust into more stable Iron compounds and H2.
			new Requirement(ref factors[UnrustedMetals]._stats, new GreaterThan(5.0f)),
			//Metal rusts. This involves water (ideally this could be an OR of liquid or gas)
			new Requirement(ref factors[LiquidH20]._stats, new GreaterThan(1.0f)),
			//Anaerobic Corrosion doesn't occur when there's lots of O2
			new Requirement(ref factors[O2]._stats, new LessThan(15f)),

			//We consume some of the un-rusted metals. (I didn't represent a new creation that they make.
			new Result(ref factors[UnrustedMetals]._stats, new AdjustValueDeltaTime(-.1f)),
			//Yield!
			new Result(ref factors[LiquidH20]._stats, new AdjustValueDeltaTime(-.1f)),
			new Result(ref factors[H2]._stats, new AdjustValueDeltaTime(.25f))
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
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(280f)),
			//Methanogens are required for this process
			//new Requirement(ref factors[Methanogens]._stats, new GreaterThan(2)),

			//More Methanogens.
			new Result(ref factors[Methanogens]._stats, new AdjustValueDeltaTime(.25f)),
			//Consume our components.
			new Result(ref factors[H2]._stats, new AdjustValueDeltaTime(-.8f)),
			new Result(ref factors[CO2]._stats, new AdjustValueDeltaTime(-.1f)),
			//Product yield.
			new Result(ref factors[CH4]._stats, new AdjustValueDeltaTime(.5f)),
			new Result(ref factors[LiquidH20]._stats, new AdjustValueDeltaTime(.5f))
		);
		AddRule(rule);
		#endregion

		#region Cyanobacteria Photosynthesis
		//Cyanobacteria Photosynthesis works well in high CO2 environments
		//https://en.wikipedia.org/wiki/Obligate_anaerobe
		rule = new FactorRule("Cyanobacteria Photosynthesis",

			//Eat that CO2
			new Requirement(ref factors[CO2]._stats, new GreaterThan(.5f)),
			//We need some carbon. (specifically glucose, but we aren't simulating that fine grain)
			new Requirement(ref factors[O2]._stats, new LessThan(60.0f)),
			//We need some heat (a bit above freezing)
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(295f)),

			//More Cyanobacteria.
			new Result(ref factors[Cyanobacteria]._stats, new AdjustValueDeltaTime(2f)),
			//Absorb JUST a bit of heat
			new Result(ref factors[Temperature]._stats, new AdjustValueDeltaTime(-0.002f)),
			//Eat some CO2
			new Result(ref factors[CO2]._stats, new AdjustValueDeltaTime(-.75f)),
			//Make O2
			new Result(ref factors[O2]._stats, new AdjustValueDeltaTime(.6f))
		);

		AddRule(rule);
		#endregion

		#region Anaerobic Fermentation
		//Anaerobes like low oxygen environments and produce CO2 off of heat and solid carbon.
		//https://en.wikipedia.org/wiki/Obligate_anaerobe
		rule = new FactorRule("Anaerobic Fermentation",
			//We don't want lots of O2 to do this.
			new Requirement(ref factors[O2]._stats, new LessThan(14.0f)),
			//We need some carbon. (specifically glucose, but we aren't simulating that fine grain)
			new Requirement(ref factors[SolidCarbon]._stats, new GreaterThan(2.0f)),
			//We need some heat (I assume they stop if its freezing)
			new Requirement(ref factors[Temperature]._stats, new GreaterThan(275f)),

			//More Anaerobes.
			new Result(ref factors[Anaerobes]._stats, new AdjustValueDeltaTime(.4f)),
			//new Result(ref factors[CH4]._stats, new AdjustValueByDeltaTime(.1f)));
			//Make some CO2
			new Result(ref factors[CO2]._stats, new AdjustValueDeltaTime(.75f))
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

		#region Cyanobacteria Death
		//Cyanobacteria likely need continuous CO2 and heat to survive. Here's a pair of rules to kill them.
		rule = new FactorRule("Cyanobacteria Death",

			//Too little CO2
			new Requirement(ref factors[CO2]._stats, new LessThan(1)),

			//MASS STARVATION!
			new Result(ref factors[Cyanobacteria]._stats, new MultiplyByPercentAndDeltaTime(.15f))
		);
		AddRule(rule);

		rule = new FactorRule("Cyanobacteria Freeze",

			//Too cold (not enough sun)
			new Requirement(ref factors[Temperature]._stats, new LessThan(260)),

			//MASS DEATH!
			new Result(ref factors[Cyanobacteria]._stats, new MultiplyByPercentAndDeltaTime(.15f))
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
		//Add the rule to the list for updating
		AllFactorRules.Add(rule);
	}

	private void RegisterNewEnvironmentFactor(FactorData newFactor)
	{
		EnvFactor newEnvFactor = AddNewEnvironmentFactor(newFactor);
		
		//We track the dictionary for easy references
		factors.Add(newFactor.Key, newEnvFactor);

		//But we also have two lists to make it cheaper to iterate. Iterating over a dictionary is very expensive.
		FactorNames.Add(newFactor.Key);
		AllFactors.Add(newEnvFactor);
	}

	/// <summary>
	/// Define FactorData separate of the MonoBehavior component. This lets us test/simulate outside of Unity more easily.
	/// </summary>
	/// <param name="values">Just use new FactorData(params)</param>
	/// <returns>The component created on a new game object</returns>
	private EnvFactor AddNewEnvironmentFactor(FactorData values)
	{
		GameObject go = new GameObject();

		EnvFactor newFactor = go.AddComponent<EnvFactor>();

		go.transform.SetParent(transform);

		newFactor.AssignFactorData(values);
		return newFactor;
	}

	void Update()
	{
		for (int k = 0; k < SimulationRate; k++)
		{
			for (int i = 0; i < AllFactors.Count; i++)
			{
				//Some factors could be VERY complicated and want separate logic such as a factor called "Humans"
				//I could see factors having their own rules, so much so you aren't looping through irrelevant rules.
				AllFactors[i].Simulate();
			}
			for (int i = 0; i < AllFactorRules.Count; i++)
			{
				//This checks and then applies the results of met rules.
				SimulateRule(AllFactorRules[i]);
			}
		}
	}

	public void SimulateRule(FactorRule rule)
	{
		//Are the requirements met?
		if (rule.EvaluateRequirements())
		{
			//If they are, do what the rule wants.
			rule.ApplyResults();
		}
	}

	//This was for displaying the planet. I chose to go for data simulation rather than visuals.
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
