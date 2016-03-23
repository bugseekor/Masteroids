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


namespace MVSPFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScrollingBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 stage;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private float transparency;
        private Vector2 position1, position2;

        public ScrollingBackground(Game game,
            SpriteBatch spriteBatch,
            Vector2 stage,
            Texture2D tex,
            Vector2 speed,
            float transparency)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.stage = stage;
            this.tex = tex;
            this.position = new Vector2(0, 0);
            this.speed = speed;
            this.position1 = position;
            this.position2 = new Vector2(position1.X, -tex.Height);
            this.transparency = transparency;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            if (position1.Y < tex.Height)
            {
                position1.Y += speed.Y;
            }
            else
            {
                position1.Y = position2.Y - tex.Height + speed.Y;
            }
            if (position2.Y < tex.Height)
            {
                position2.Y += speed.Y;
            }
            else
            {
                position2.Y = position1.X - tex.Height + speed.Y * 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, Color.White * transparency);
            spriteBatch.Draw(tex, position2, Color.White * transparency);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}