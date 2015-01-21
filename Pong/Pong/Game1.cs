using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
   /// <summary>
   /// This is the main type for your game
   /// </summary>
   public class Game1 : Microsoft.Xna.Framework.Game
   {
      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;

      Texture2D ballSprite;

      Vector2 ballPosition = Vector2.Zero;

      Vector2 ballSpeed = new Vector2(150, 150);

      Texture2D paddleSprite;

      Vector2 paddlePosition;

      SoundEffect swishSoundEffect;

      SoundEffect crashSoundEffect;
      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         // TODO: Add your initialization logic here
         IsMouseVisible = true;
         base.Initialize();

         paddlePosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - paddleSprite.Width / 2,
            graphics.GraphicsDevice.Viewport.Height - paddleSprite.Height);
      }

      /// <summary>
      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      /// </summary>
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures.
         spriteBatch = new SpriteBatch(GraphicsDevice);

         // TODO: use this.Content to load your game content here

         ballSprite = Content.Load<Texture2D>("basketball");

         paddleSprite = Content.Load<Texture2D>("hand");

         swishSoundEffect = Content.Load<SoundEffect>("swish");

         crashSoundEffect = Content.Load<SoundEffect>("crash");
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// all content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         // Allows the game to exit
         if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            this.Exit();

         // TODO: Add your update logic here

         ballPosition += ballSpeed*(float) gameTime.ElapsedGameTime.TotalSeconds;

         int maxX = GraphicsDevice.Viewport.Width - ballSprite.Width;
         int maxY = GraphicsDevice.Viewport.Height - ballSprite.Height;

         KeyboardState keyState = Keyboard.GetState();
         if (keyState.IsKeyDown(Keys.Right))
            paddlePosition.X += 5;
         else if (keyState.IsKeyDown(Keys.Left))
            paddlePosition.X -= 5;

         if (ballPosition.X > maxX || ballPosition.X < 0)
         {
            ballSpeed.X *= -1;
         }

         if (ballPosition.Y < 0)
         {
            ballSpeed.Y *= -1;
         }
         else if (ballPosition.Y > maxY)
         {
            crashSoundEffect.Play();

            ballPosition.Y = 0;
            ballSpeed.X = 150;
            ballSpeed.Y = 150;
         }

         Rectangle ballRectangle = new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
            ballSprite.Width, ballSprite.Height);

         Rectangle handRectangle = new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y, 
            paddleSprite.Width, paddleSprite.Height);

         if (ballRectangle.Intersects(handRectangle) && ballSpeed.Y > 0)
         {
            swishSoundEffect.Play();

            ballSpeed.Y += 50;
            if (ballSpeed.X < 0)
            {
               ballSpeed.X -= 50;
            }
            else
            {
               ballSpeed.X += 50;
            }

            ballSpeed.Y *= -1;
         }
         base.Update(gameTime);
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         // TODO: Add your drawing code here

         spriteBatch.Begin();
         spriteBatch.Draw(ballSprite, ballPosition, Color.White);
         spriteBatch.Draw(paddleSprite, paddlePosition, Color.White);
         spriteBatch.End();

         base.Draw(gameTime);
      }
   }
}
