# Sandbox
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

=========
GOAL
=========

The fundamental goal is this:
		If your math compiles, it is correct.

Units crossing out and converting is totally compiler-able, lets make the compiler do the heavy lifting.

Support plenty of different equations, math, science and more.

==LIMITATIONS===
This likely isn't super efficient. It creates a lot of new variables, has in-line coded values.
It is early days. As my father always tells me 'Make it work, make it work well, make it work fast'.

There are a lot of classes necessary to make this work.
When I want something new, I have to write new classes or functions to allow conversion.
This is something C++ could provide better support for, but it'll take me a bit till I feel ready to reimplement into C++.
Trying to shore up the design/implementation details before I do that.

The syntax can get a bit verbose as you create a specific acceleration, providing new values.

It isn't very visuals yet