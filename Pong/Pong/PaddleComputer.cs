using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
   }
}
