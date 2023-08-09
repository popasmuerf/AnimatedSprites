## Game Components


1. XNA has a really nice way to integrate different logical pieces of code
2. The Game Component class
	--> Allows you to modularly plug any code into your application an dautomatically wires
	--> The GameComponent 

3. Class GameComponent
	--> An object that can be attached to a Game and have it's Update(GameTime) method called when 
	Update(GameTime) is called.

Object
|-> GameComponent
	|-> DrawableGameComponent

Implementes
	- IGameComponent
	- IUpdateable
	- IDisposable

Namespace: Microsoft.Xna.Framework
Assembly: MonoGame.Framework.dll

//Signature
public class GameCoponent : object, IGameComponent, IUpdateable

//Declaration
public GameComponent(Game game)


