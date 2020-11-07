# ESJ2 Level Format Specification

Currently, the level editor opens only 1 file, which is CurrentLevel.butt, this will be in ESJ2_Data/StreamingAssets/

You can open it with any text editor, such as Notepad, Text Edit, Visual Studio, or... anything that can open a .txt file.

The level format is made up of *entities*, each of which have *parameters*. Each parameter has a name and a value.

## Entities
As a best practice, entities should begin with a `type` parameter, and must end with a semi-colon `;` separator. This entity has 3 parameters: `type`, `x`, and `y`.
`type:block,x:3,y:0;`

## Parameters
Parameters are made up of two parts: a name, and a value. Each entity can be defined by many parameters, each separated by a comma `,`. Parameter names and values are separated by a colon `:`.
`type:block`
`x:3`
`y:-38`

**Default Values**
All parameters have default values, omitting them will use that default value. For example, the following entity creates a block at position (5, 0), since the y-position is set to zero by default.
`type:block,x:5;`

This example will create a block, at world position (3, 4) that is 3 units tall (that is to say, 3 times the height of a "normal" block.) In this scenario, `xScale` and `yScale` have a default value of 1, and we are overriding the value of `yScale`.
`type:block,x:3,y:4,yScale:3;`

## Prefabs
You can define your own prefabs by adding the `prefab` parameter to an entity definition. As a best practice, define prefabs with the prefab parameter first.
`prefab:42,type:NinjaStar,speed:3;`

This prefab can now be referenced in certain places by the number 42 (such as when defining a Frog entity.)
See also [Bullet Types](#bullet-types) below.

## Common Parameter Definitions and Default Values

type (string : None)
: Signifies which entity the game will create when loading the level.
For a list of possible types, see [types](#types) below.

x (float : 0.0)
: The x-position (in world space) that the entity will spawn.

y (float : 0.0)
: The y-position (in world space) that the entity will spawn.

[pos|position] (Vector2 : 0.0, 0.0)
: This is a shorthand for setting x and y separately `pos:1/20` will create an entity at world position (1, 20).

zPos (float : type-dependant)
: Set the depth that the entity will be drawn. A value of 0 or less will be drawn behind the camera and invisible to the player.

xScale (float : 1.0)
: Set the width of an entity relative to its default size.

yScale (float : 1.0)
: Set the height of an entity relative to its default size.

scale (Vector2 : 1.0, 1.0)
: Shorthand for setting `xScale` and `yScale`. Example: `scale:3/3` creates an entity 3 times its normal size.

tilt (float : 0.0)
: Number of degrees to rotate an entity. Ignored for Block types.

prefab (integer : -1)
: Contains the prefab's id number (can be any number) to identify it. example: prefab:77

speed (float : type-dependant)
: How fast something moves, if it is supposed to move.

createLate (float : 0.0)
: A time in seconds to delay the creation of an entity after a level loads.

## Types
PlayerSpawn
: Spawns the player.

Block
: A solid thing to stand on. Cannot be tilted.

SpikeBall
: A spiky hurty thing.

Portal
: Creates an end-of-level portal.
This type has custom parameters:

    destinationLevel (string : None)
    : Takes you to a developer-created level, if you know the file name.

    destinationButt (string : None)
    : Overrides `destinationLevel` if present.
    Loads a custom .butt file with the provided name, you may omit the extension.
    
    noSound (bool : false)
    : Determines if the portal makes that tinkling sound. Set to `true` to silence the portal.

SetSky
: An invisible object that configures the sky's colour when it's created.
This type has custom parameters:

    color1 (Color : 0, 0, 0)
    : Sets the colour at the top of the screen. `color1:255/255/255` will be white.
    
    color2 (Color : 0, 0, 0)
    : Sets the colour at the bottom of the screen `color2:0/0/0` will be black.
    
    skyBands (integer : 0)
    : Determines the number of "bands" or "steps" that will divide the sky. A value of 0 or 1 will result in a smooth gradient between `color1` and `color2`.
    Many original ESJ levels use a value or 6 or 8.

SetMusic
: An invisible object that sets the music when it's created.
This type has custom parameters:

    int1 (integer : 0)
    : Selects which track to play.
      1. None
      2. ENV_1
      3. ENV_5
      4. ENV_8222
      5. ENV_AbsoluteZero
      6. ENV_Darkas
      7. ENV_FutureShock
      8. ENV_GenericTechno
      9. ENV_GreenWithMe
      10. ENV_Rokits
      11. ENV_Shakestopper
      12. ENV_ThisSound
      13. ENV_TwoStep
      14. ENV_WobbleWobble
      15. ENV_Valkyrie
      16. GetSix_NoInterruptions
      17. GetSix_Valhalla
      18. Reptiore_Butter
      19. Reptiore_GatorRaid
      20. Reptiore_OmegaHauzer
      21. Reptiore_SomeRegrettablyOffensiveShit

Ability Shrines
: These are likely to change in a future update
  - ShrineDoubleJump
  - ShrineTripleJump
  - ShrineQuintupleJump
  - ShrineButtSmash
  - ShrineAirSword
  - ShrineRemoveAbilities

Frog
: Creates a frog entity.
This type has custom parameters:

    ammo (integer : -1)
    : The prefab id number of the ammo you wish this frog to fire see [Bullet Types](#bullet-types) below.

    firingDelay (float : 2.0)
    : Time in seconds between how slowly a frog fires bullets/missiles

Laser
: Creates a single laser
This type has custom parameters:

    speed (float : 0.0)
    : Speed the laser will spin

QuadLaser
: Creates a 4-way laser in an X shape.
This type has custom parameters:

    speed (float : 15.0)
    : Speed the laser will spin

Coin
: A coin you can collect!

BounceArrow
: An arrow you can bounce on once before it disappears.

Firestick
: A line of rotating spikeballs.
This type has custom parameters:

    speed (float : 200.0)
    : the speed at which the firestick will rotate.

MovingSpikePlatform
: A block with spikes on three sides. It's upsidedown for some reason, so if you want to stand on it, set `tilt:180`.
This type has custom parameters:

    tilt (float : 0.0)
    : Rotates the platform. Setting this to non-90-degree increments will probably break things.
    
    nodePos (Vector2 : 0.0/0.0)
    : Sets the world position that the platform will move to from its starting position.
    
    speed
    : The time in seconds it takes the platform to move from `pos` to `nodePos`.
    
    easing
    : The type of easing to apply to the platform's movement.
        0: Linear movement
        1: Sine easing

Particles
: Creates a particle system.
This type has custom parameters:

    mesh (enumeration : None)
    : Possible values:
      - Cube
      - Square
      - Star
      - FourPointStar
      - Diamond
      - Circle
      - Triangle

    emissionPerSecond (float : 10.0)
    : Number of particles per second

    speedMin (float : 5.0)
    : How fast they move (minimum this amount.)
    If you want all particles to move at speed 5, set both `speedMin` and `speedMax` to 5.

    speedMax (float : 5.0)
    : how fast they move (up to this amount)

    startSizeMin (float : 5.0)
    : the minimum starting size for each particle

    startSizeMax (float : 5.0)
    : the maximum starting size for each particle

    lifespanMin (float : 5.0)
    : the minimum time before each particle dies

    lifespanMax (float : 5.0)
    : the maximum time before each particle dies

    rotationMin (float : 0.0)
    : The minimum amount each particle will rotate

    rotationMax (float : 0.0)
    : The maximum amount each particle will rotate

    shape (enumeration : Box)
    : Shape of the emitter. Possible values:
      - Box
      - Circle
      - Cone
      - Sphere
      - Any other Unity [ParticleSystemShapeType](https://docs.unity3d.com/ScriptReference/ParticleSystemShapeType.html)

    shapeScaleX (float : 1.0)
    : how wide the shape is

    shapeScaleY (float : 1.0)
    : how long the shape is

    shapeScaleZ (float : 1.0)
    : how deep the shape is

    color1 (Color : 0, 0, 0)
    : the first color of the particle. uses an additive material so darker colours will be transparent instead.

    color2 (Color : 0, 0, 0)
    : the second color of the particle. Particles will fade towards this color.
    
    alpha[1-5] (float : 0.0)
    : Five alphas states of the particle.
    Each particle will fade between all of these alphas, valid values between 0.0-1.0.

CameraTrigger
: Configures the camera.
This type has custom parameters:

    cameraState (enumeration : None)
    : Possible Values:
     - HorPush
     - VerPush
     - Pos
     - HorScrolling
     - VerScrolling
     - HorScrollingWithBumpRight
     - VerScrollingWithBumpUp
     - HorPushWideBox
     - SlowPos
     - HorPushLookAhead
     - HorVerPush
     - VerySlowPos
     - VerPushHighBox
     - InstaPos

    nodePosX
    : Used for `cameraState:Pos`, moves the camera to the specified x-position.
    
    nodePosY
    : Used for `cameraState:Pos`, moves the camera to the specified y-position.
    
    nodePos
    : Shorthand for `nodePosX` and `nodePosY`. Example: `nodePos:3/-8`
    
    track
    : The x or y that the camera slides along, in Scrolling or Push modes.
    
    minX (float : -Infinity)
    : The minimum x-position that the camera can be. Allows limiting scrolling or push cameras.
    
    minY (float : -Infinity)
    : The minimum y-position that the camera can be. Allows limiting scrolling or push cameras.

    min (Vector2 : -Infinity, -Infinity)
    : Shorthand for setting `minX` and `minY` Example: `min:20/0` sets minimum x-position to 20, and minimum y-position to 0.
    
    maxX (float : Infinity)
    : The maximum x-position that the camera can be. Allows limiting scrolling or push cameras.
    
    maxY (float : Infinity)
    : The maximum y-position that the camera can be. Allows limiting scrolling or push cameras.

    max (Vector2 : Infinity, Infinity)
    : Shorthand for setting `maxX` and `maxY` Example: `max:100/20` sets maximum x-position to 100, and maximum y-position to 20.

### Bullet Types
These types are most suitable for use as ammo for Frogs.

NinjaStar
: A bullet type
This type has custom parameters:

    lifespan (float : 2.0)
    : Time in seconds the bullet will live before disappearing
    
    speed (float : 5.0)
    : The bullet's speed.

RoundBullet
: A bullet type you can airsword off of
This type has custom parameters:

    lifespan (float : 3.0)
    : Time in seconds the bullet will live before disappearing
    
    speed (float : 5.0)
    : The bullet's speed

HomingMissile
: A bullet type. -- Homing missile that turns to chase the player.
This type has custom parameters:

    lifespan (float : 8.0)
    : Time in seconds the bullet will live before disappearing
    
    speed (float : 7.0)
    : The bullet's speed

    turningSpeed (float : 55.0)
    : How fast homing missiles turn.
    
SpikyHomingMissile
: A bullet type -- Homing missile you can't kill with airsword (It's spiky.)
**BUG:** The player shouldn't be able to kill this type with double jump, but currently can.
This type has custom parameters:

    lifespan (float : 8.0)
    : Time in seconds the bullet will live before disappearing
    
    speed (float : 7.0)
    : The bullet's speed

    turningSpeed (float : 55.0)
    : How fast homing missiles turn.
