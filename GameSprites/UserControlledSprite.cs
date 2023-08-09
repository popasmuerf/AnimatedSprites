using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace AnimatedSprites.GameSprites
{
    internal class UserControlledSprite : Sprite
    {
        private GamePadState gamePadState;
        private MouseState prevMouseState;
        

        /**
         * 
         * These Constructuros are abasially the same as the constructors for the Sprite class 
         * and will just pas the parameters on the base class:
         * 
         */
        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed) : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed)
        {
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame) : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame)
        {
        }

        //public override Vector2 direction => throw new NotImplementedException();

        /**
         *  Here we overload the direction property.  The direction property will be used in the 
         *  Update method.  Remember....a variable object is still a complex type that allows 
         *  for us to override it's attributes and behaviors. So we do so here.
         *  The Direction property will be used in the Update method to modify the position of the 
         *  sprite.  We move the the sprite in the direction indicated by this property.
         *  
         *  Direction in this case will be defined as a combinantion of the speed member of the base class
         *  andthe direction in which the player is pressing the gamepad's left thumb stick, or the 
         *  arrow keys th euser is pressing.
         *  
         *  Users will also be able to control the sprite using mouse but mouse input will be handled a bit differently
         *  Whe themouse is being used to cotorl the sprite, you are going to move the sprite to th position of the 
         *  mouse on the screen, so there is really no need for a direction property when dealing with mouse
         *  movement.
         *  
         *  Events involving the state of keyboard directional key presses will return an instance of type Vector2
         *  indicating the movement direction in a 2 dimenstional plane, multiplied by the speed attribute of the
         *  base class.  The speed attribute is also an object of type Vector2 and is mutable...so we can adjust it
         *  at will. 
         *  
         *  NOTE: "speed" in reality here should be labled as "velocity" as it is a Vector and thus has both speed and direction.
         *  
         *  Notice that the keyboard and gamepad inputs are combined, allowing the player to control the sprite with 
         *  either input device.  Also, just to be clear....the GamePadState struct provides X and Y axis vaules that 
         *  are clamped between -1.0 and 1.0. So always keep in mind that the GamePadState tracks the state of Unit Vectors
         *  only....
         */
        public override Vector2 direction
        {
            get 
            {
                /*
                 
                 Vector2 inputDirection is basically a Unit Vector  Unit Vecotrs are used to determine the direction
                 only and thus are always equal to  one of the following 
                    
                    v(1,0)
                    v(-1,0)
                    v(0,1)
                    v(0,-1)
                 
                 */
                Vector2 inputDirection = Vector2.Zero;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    inputDirection.Y += 1;

                gamePadState = GamePad.GetState(PlayerIndex.One);
                if (gamePadState.ThumbSticks.Left.X != 0)
                    inputDirection.X = gamePadState.ThumbSticks.Left.X;
                if (gamePadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y = gamePadState.ThumbSticks.Left.Y;
                /*
                 *  We multiply the unit vector (inputDirection) by the speed vector to get 
                 *  the updated diplacement of posistion witch w e then in turn return as Vector2 direction.
                 */
                return inputDirection * speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {

            //Move the sprite based on direction 
            position += direction;

            //If player moved the mouse, move the sprite 
            MouseState currMouseState = Mouse.GetState();
            if(currMouseState.X != prevMouseState.X  || currMouseState.Y!= prevMouseState.Y)
            {
                position = new Vector2(currMouseState.X, currMouseState.Y);
            }

            prevMouseState = currMouseState;

            /*If the sprite is off screen, move it back within the game window*/

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;


            /*
             * Here we use just if statements because calls to Keyboard.GetState() 
             * are expensive and elif statements will at some point lock you into 
             * the elif chain.  We don't need that here.  We just need to know what
             * is in the keyboard buffer. If we are moving diagonally and both X and Y 
             * states need to be updated...using an elif will basically only address the update of either X or Y
             * Independent if statements will allow 
             * 
             * We need to toggle this.
             */


           

            base.Update(gameTime, clientBounds);
        }
    }
}
