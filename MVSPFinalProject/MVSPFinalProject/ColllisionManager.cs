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
    public class ColllisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;

        private List<Asteroid> asteroids = new List<Asteroid>();

        public List<Asteroid> Asteroids
        {
            get { return asteroids; }
            set { asteroids = value; }
        }
        private Ship ship;
        private Bullet bullet;

        public ColllisionManager(Game game, SpriteBatch spriteBatch, List<Asteroid> asteroids, Ship ship)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.asteroids = asteroids;
            this.ship = ship;
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
            bullet = ship.Bullet;

            // TODO: Add your update code here
            for (int i = 0; i < asteroids.Count; i++)
            {
                Rectangle asteroidRect = asteroids.ElementAt(i).getBounds();

                if (bullet != null)
                {
                    Rectangle bulletRect = bullet.getBounds();
                    if (bulletRect.Intersects(asteroidRect))
                    {
                        asteroids.ElementAt(i).IsHit = true;
                        ship.Bullet.IsHit = true;
                        Explosion1 e1 = new Explosion1(game, spriteBatch,
                            asteroids.ElementAt(i).Position, 5, 1, asteroids.ElementAt(i).Speed);
                        e1.Enabled = true;
                        e1.Visible = true;
                        game.Components.Add(e1);
                    }
                }

                Rectangle shipRect = ship.getBounds();

                if (shipRect.Intersects(asteroidRect))
                {
                    Explosion2 e2 = new Explosion2(game, spriteBatch, ship, 1, 3, Vector2.Zero);
                    e2.Enabled = true;
                    e2.Visible = true;
                    game.Components.Add(e2);
                }
            }

            base.Update(gameTime);
        }
    }
}
