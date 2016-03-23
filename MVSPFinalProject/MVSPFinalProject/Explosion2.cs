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
    public class Explosion2 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Ship ship;
        private Vector2 position;
        private Game game;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private Vector2 origin;
        private float scale;
        private Vector2 speed;

        private int delay;
        private int delayCounter;

        public Explosion2(Game game, SpriteBatch spriteBatch, 
            Ship ship, int delay, float scale, Vector2 speed)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = game.Content.Load<Texture2D>("Images/explosion_sprite_2d_by_korzonrocknet-d7dv5b5");
            this.ship = ship;
            this.position = ship.Position;
            this.speed = speed;
            this.delay = delay;
            this.scale = scale;
            this.dimension = new Vector2(64, 64);

            this.Enabled = false;
            this.Visible = false;

            createFrames();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
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
            position += speed;
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex >= 16)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                    ship.Position = new Vector2(-ship.Tex.Width, -ship.Tex.Height);
                    ship.IsHit = true;
                    game.Components.Remove(this);
                }
                delayCounter = 0;
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                Vector2 tempOrigin = new Vector2(dimension.X/2, dimension.Y/2);
                origin = tempOrigin;
                //version 7
                spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(frameIndex), Color.White, 0, origin, scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
