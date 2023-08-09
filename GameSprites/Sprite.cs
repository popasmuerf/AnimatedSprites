using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace AnimatedSprites.GameSprites
{

    /**
     * 
     * Members of the Sprite Class 
     * 
     * Texture2D                   textureImage            Sprite or sprite sheet of image being drawn
     * Vector2                     position                Position at which to draw the sprite
     * Point                       frameSize               Size of each individual fram in the sprite sheet
     * int                         collisionOffset         Offset used to modify frame-size rectangle for collisional checks against this sprite  
     * Point                       currentFrame            Index of current frame in sprite sheet 
     * Point                       sheetSize               Number of columns/rows in sprite sheet
     * int                         timeSinceLastFrame      Number of milliseconds since last fram was drawn
     * int                         millisecondsPerFrame    Number of millisecnds to wait between frame changes
     * Vector2                     speed                   Speed at which the sprite will move in both X and Y directions
     * Sprite(...)                 Constructor             Sprite constructor method
     * Update(GameTime,Rectangle)  void                    Handles all collision checks, movement, user input, and so on      
     * Draw(GameTime, SpriteBatch) void                    Draws the sprite
     *
     * 
     * 
     * 
     * 
     */
    abstract class Sprite
    {

        public abstract Vector2 direction{get;}
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }

        Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        protected Point currentFrame;
        protected Point sheetSize;
        protected int collisionOffset;
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 16;
        protected Vector2 speed;
        
        public Sprite(Texture2D textureImage,
                      Vector2 position,
                      Point frameSize,
                      int collisionOffset,
                      Point currentFrame,
                      Point sheetSize,
                      Vector2 speed): this(textureImage, 
                                           position,
                                           frameSize,
                                           collisionOffset,
                                           currentFrame,
                                           sheetSize,
                                           speed,
                                           defaultMillisecondsPerFrame)
        {
        }//end method definition 
        public Sprite(Texture2D textureImage,
                      Vector2 position,
                      Point frameSize,
                      int collisionOffset,
                      Point currentFrame,
                      Point sheetSize,
                      Vector2 speed,
                      int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }//end method definition 


        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }

        }//end method definition 



        public virtual void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                            position,
                            new Rectangle(currentFrame.X * frameSize.X,
                                          currentFrame.Y * frameSize.Y,
                                          frameSize.X,
                                          frameSize.Y),
                                          Color.White,
                                          0,
                                          Vector2.Zero,
                                          1f,
                                          SpriteEffects.None,
                                          0);
        }
    }//end class definition
}// end namespace definition
