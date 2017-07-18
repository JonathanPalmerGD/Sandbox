# Sandbox

This project contains two different samples. 
1. A simulation technology for governing factors and how they affect one another.
2. A forray into solving math mistakes in programming.

## Simulating Factors

This demo is made up of three things:
1. Planet
2. EnvFactor (Environment Factor)
3. FactorRules

Planet has several data structures. With it, you can register environmental factors (stored in a Dictionary for easy access, and a list for fast iteration). You can also create rules for those environmental factors. When a rule's requirements are met, it will apply its results, modifying the environmental factors.

Collectively, you can mathematically simulate many factors and their relations to one another.
Simulations happen on a per-planet basis, so you could have multiple planets running at once.

### Efficient?
Overall, my initial code is pretty fast.
I ran some deep profiling and found on the following:
1. The vertices setup content I was planning for the planet visuals (and backburnered due to not being that related) is expensive.
2. The Name display used on each of Planet Earth's children is expensive (blame the string concatenations, lots of property method calls, ToStrings)
3.  Running 8 steps per frame (via the Planet 'Simulation Rate' slider) shows only a .73 ms frame for 16 factors and 13 rules. This is also in a deep profile on unoptimized C# code running 8x more than it needs to for game-time. This would get faster from C++ DLLs or inlining (which Unity isn't always great at). 

I could also see improvements from typical physics programming (such as detecting rules or factors as sleeping, or adding early-outs when one requirement fails).

### Cut Features

I was originally planning visual simulations, but I didn't think that was as critical or interesting as the factor-rule relationships.
I did not finish support for location/distance/size of the Factors as well as only applying rules to factors that overlap and where they overlap. It got into far more collision code than I felt was necessary to explore the factor-rule relationship.
I don't have as many Conditions as I could have. I can't have a condition that watches if the target is less than a separate target.
I don't have rules where factors grow/shrink based on other factors. I was close to finishing implementation on this but it wasn't the focus or necessary to convey the concepts.

### Future Concepts

The first feature I would implement would be redefining location and how factors interact when they overlap. This would tie nicely into a density system, which was missing from my initial implementation. Many environment simulations care significantly about density of certain factors (such as weather).

Better conditions/results that care about other existing factors would be helpful, althought not completely necessary.

## Strongly Typed Units

What if units in math were strongly typed?

The purpose of this project is for some sandbox style simulations.
However, If you wish to make apple pie from scratch, you must first create the universe.

So I went ahead and decided to create strongly typed units and variables.
This means that your compiler won't let you get your math wrong. The objective is that if you want Meters Per Second, there is a variable for that.

I'm still exploring but I'm leaving comments and tests along to explain what I'm doing.

The goal is that Units can be converted to other units in the same category, and turned into other types of units.
For instance, you could combine a Distance and a Time to get an abstract velocity.

There is some complex generic programming inside of the RaiseToPower class (necessary for Area/Acceleration and other things with powers)

Check out the Tests folder (Test.Unity) to see samples of what can be done.

### Goal

The fundamental goal is this:
		If your math compiles, it is correct.

Units crossing out and converting is totally compiler-able, lets make the compiler do the heavy lifting.

Support plenty of different equations, math, science and more.

### Limitations

* This likely isn't super efficient. It creates a lot of new variables, has hard coded values.
* It is early days. As my father always tells me 'Make it work, make it work well, make it work fast'.

* There are a lot of classes necessary to make this work.
* Whenever I want some new unit, I'll usually to write new classes or functions to allow conversion.
* This is something C++ could provide better support for, but it'll take me a bit till I feel ready to reimplement into C++.

* The syntax can get a bit verbose as you create a specific acceleration, providing new values.
* It isn't very visuals yet