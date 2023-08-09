using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnimatedSprites.GameSprites
{
    /**
     * We can draw in our sprite Manager's Draw method,just as we can n our 
     * Game1 class.  In vact, to clearly seperate the sprite logic fromt he 
     * rest of your game
     * 
     * XNA has a really neat way to integrate different logical pieces of code(This class right here) into our application.
     * Teh GameComponent class allows us to modularly plug any code into our application and automatically 
     * wires that component into the game loop's update call (only after the game's Update() has been called.
     * 
     * So what does this mean?   There seems to be a bit of reflection going on here....the run time is (I speculate) 
     * searching the manifest for all classes that are of type Component and then calling their 
     * Update() and Draw() methods
     * 
     * 
     * If we want to create a game component that will also be wired into the game
     * loop's method so that our component has the ability to draw itens as well, you can do so by 
     * instead deriving form the DrawableGameComponent class 
     * 
     * If ro example, we built a compnoent to raw the framerate and other peformatnce-related debug inforamtion 
     * on screen, you could add it to any game rather trivially
     * 
     *  Just as the Sprite manager's Update() and Draw() methods are wired up to be called after your 
     *  Game1 class's Update() and Draw() methods are called, the Initialize() and Load() Content  methods 
     *  will also be called after the equivalent Game1   You are going to need to add some code to 
     *  load textures, initialize the SpriteBatch, initialize  your player object, and for testing 
     *  purposes, add some sprites to tyour sprite manager's list of sprites
     * 
     */
    internal class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        UserControlledSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        

        public SpriteManager(Game game) : base(game)
        {
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);


            /** Loading Content **/


            /** 
             * 1. Initialize player object 
             * 2. Add 4 NPCs to the sprite list.
             * 3. We don't add the player object to the sprite list just yet. Why ?
             * 
             * 
             ***/

            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/threerings"),
                Vector2.Zero, new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 6)); 
            
                spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(150, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), Vector2.Zero));

                spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(300, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), Vector2.Zero));
                
                spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(150, 300), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), Vector2.Zero));
                
                spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(600, 400), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), Vector2.Zero));


            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            //Update player 
            //player.Update(gameTime, Game.Window.ClientBounds);
            

            //Update all sprites
            foreach (Sprite sprite in spriteList)
            {
                sprite.Update(gameTime, Game.Window.ClientBounds);

                if (sprite.collisionRect.Intersects(player.collisionRect))
                {
                    Game.Exit();
                }
            }

            player.Update(gameTime,Game.Window.ClientBounds);

            base.Update(gameTime);
        }



        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            foreach (Sprite _sprite in spriteList)
            {
                _sprite.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /*
         * NOTES FOR LATER
         * 
         *      //  int _skullBallFramePositionX = (int) skullBallFramePosition.X + skullBallCollisionRectOffset;
                //   int _skullBallFramePositionY = (int)skullBallFramePosition.Y + skullBallCollisionRectOffset;
                //    int _skullBallFrameSizeX = (int)skullBallFrameSize.X - (ringsCollisionRectOffset * 2); ;
                //     int _skullBallFrameSizeY = (int)skullBallFrameSize.Y - (ringsCollisionRectOffset * 2);
                //  collisionFlag = (skullBallCollisionFlag || plusCollisionFlag);


                    /**
             * Frame animation management for the Skull Ball
             */
            /*
            skullBallTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (skullBallTimeSinceLastFrame > skullBallMillisecondsPerFrame)
            {
                skullBallTimeSinceLastFrame -= skullBallMillisecondsPerFrame;
                ++skullBallCurrentFrame.X;
                if (skullBallCurrentFrame.X >= skullBallSheetSize.X)
                {
                    skullBallCurrentFrame.X = 0;
                    ++skullBallCurrentFrame.Y;
                    if (skullBallCurrentFrame.Y >= skullBallSheetSize.Y)
                        skullBallCurrentFrame.Y = 0;
                }
            }
            */


         

    }
}
