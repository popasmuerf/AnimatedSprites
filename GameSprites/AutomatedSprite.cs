using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedSprites.GameSprites
{
    internal class AutomatedSprite : Sprite
    {
        bool movementFlagX = false;
        bool movementFlagY = false;
        bool plusMovementFlagX = false;
        bool plusMovementFlagY = false;
        int clientBoundX ;
        int clientBoundY ;


        /**
         * The automated sprite will use the sped member of the base class
         * to move around the screen.  This will be done through an overridden
         * direction property because that property is abstract in the base class 
         * and therefore must be defined in this class.  Create the override for 
         * the direction property as we have here.
         * 
         * 
         * 
         * 
         * 
         */
        public override Vector2 direction
        {
            get { return speed; }
        }

        public void SetClientBounds(GameWindow gameWindow)
        {
            clientBoundX = gameWindow.ClientBounds.Width;
            clientBoundY = gameWindow.ClientBounds.Height;
        }

        /*
         * We need to add the code that will make your automated sprite move.
         * Because the direction property is represented by a Vector2 value, this propety
         * represents direction and spedd for your automated sprite.  Any direction in 2D space can be reprsented
         * by a Vector2 value, and the magnitude that indicates the speed of the object.
         * The greater the length(magnitude) of tha vecot is, the faster the automated sprite will move.
         * 
         * We just need to add the vector represented by the direction vector to the posistion vector at 
         * the speed indicated by the magnitude of the vector.
         */

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed) : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed)
        {
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame) : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame)
        {
        }


        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
    
            animate(clientBounds);
            base.Update(gameTime, clientBounds);
        }

        public void animate(Rectangle clientBounds )
        {
            if (movementFlagX)
            {
                position.X -= 1;
            }
            else
            {
                position.X += 1;
            }

            if (movementFlagY)
            {
                position.Y -= 1;
            }
            else
            {
                position.Y += 1;
            }



            if (position.X < 0)
            {
                position.X = 0;
                movementFlagX = false;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                movementFlagY = false;
            }
            if (position.X > clientBounds.X - frameSize.X)
            {
                position.X = clientBounds.X - frameSize.X;
                movementFlagX = true;
            }
            if (position.Y > clientBounds.Y - frameSize.Y)
            {
                position.Y = clientBounds.Y - frameSize.Y;
                movementFlagY = true;
            }
        }
    }
}
