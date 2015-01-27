using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
   class PaddleComputer : Paddle
   {
      public PaddleComputer(Game game) : base(game)
      {
      }

      protected override void LoadContent()
      {
         spriteBatch = new SpriteBatch(GraphicsDevice);

         paddleSprite = contentManager.Load<Texture2D>(@"Content\Images\blueHand");
      }

      public override void Initialize()
      {
         base.Initialize();

         // Make sure base.Initialize() is called before this or handSprite will be null
         X = GraphicsDevice.Viewport.Width;
         Y = (GraphicsDevice.Viewport.Height - Height) / 2;

         Speed = DEFAULT_X_SPEED;
      }

      public override void Update(GameTime gameTime)
      {
         // Scale the movement based on time
         float moveDistance = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

         // Move paddle, but don't allow movement off the screen

         KeyboardState newKeyState = Keyboard.GetState();
         if (newKeyState.IsKeyDown(Keys.Down) && Y + paddleSprite.Height
             + moveDistance <= GraphicsDevice.Viewport.Height)
         {
            Y += moveDistance;
         }
         else if (newKeyState.IsKeyDown(Keys.Up) && Y - moveDistance >= 0)
         {
            Y -= moveDistance;
         }
         else if (paddlePosition.Y < 0)
         {
            paddlePosition.Y = 0;
         }

         base.Update(gameTime);
      }

   }
}
