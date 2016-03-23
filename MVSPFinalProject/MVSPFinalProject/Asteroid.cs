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
    public class Asteroid : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        private Vector2 stage;

        private Texture2D tex;

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }

        private int positionIndex;

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 speed;

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private float rotationFactor = 0;
        private float rotationChange = 0.01f;
        private Vector2 origin;
        private int sign;
        private int collisionTime;

        public int CollisionTime
        {
            get { return collisionTime; }
            set { collisionTime = value; }
        }

        private bool isHit = false;

        public bool IsHit
        {
            get { return isHit; }
            set { isHit = value; }
        }

        private int scoreValue = 1;

        public int ScoreValue
        {
            get { return scoreValue; }
            set { scoreValue = value; }
        }

        public Asteroid(Game game, SpriteBatch spriteBatch, Vector2 stage, Texture2D tex, int positionIndex, Vector2 speed, int sign)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.stage = stage;
            this.tex = tex;
            this.positionIndex = positionIndex;
            this.position = Vector2.Zero;
            this.speed = speed;
            this.origin = new Vector2(tex.Width / 2, tex.Height / 2);
            this.sign = sign;

            if (positionIndex == 0)
            {
                this.position.X = -tex.Width / 2;
                this.position.Y = -tex.Height / 2;
            }
            if (positionIndex >= 1 && positionIndex <= 3)
            {
                this.position.X = stage.X / 4 * positionIndex;
                this.position.Y = -tex.Height / 2;
                if (sign == 0)
                {
                    this.speed.X *= -1;
                }
            }
            else if (positionIndex == 4)
            {
                this.position.X = stage.X + tex.Width / 2;
                this.position.Y = -tex.Height / 2;
                this.speed.X *= -1;
            }
            else if (positionIndex >= 5 && positionIndex <= 7)
            {
                this.position.X = stage.X + tex.Width / 2;
                this.position.Y = stage.Y / 4 * positionIndex % 4;
                this.speed.X *= -1;
                if (sign == 0)
                {
                    this.speed.Y *= -1;
                }
            }
            else if (positionIndex == 8)
            {
                this.position.X = stage.X + tex.Width / 2;
                this.position.Y = stage.Y + tex.Width / 2;
                this.speed.X *= -1;
                this.speed.Y *= -1;
            }
            else if (positionIndex >= 9 && positionIndex <= 11)
            {
                this.position.X = stage.X - ((stage.X / 4) * (positionIndex % 4));
                this.position.Y = stage.Y + tex.Height / 2;
                this.speed.Y *= -1;
                if (sign == 0)
                {
                    this.speed.X *= -1;
                }
            }
            else if (positionIndex == 12)
            {
                this.position.X = -tex.Width / 2;
                this.position.Y = stage.Y + tex.Height / 2;
                this.speed.Y *= -1;
            }
            else if (positionIndex >= 13 && positionIndex <= 15)
            {
                this.position.X = -tex.Width / 2;
                this.position.Y = stage.Y - ((stage.Y / 4) * (positionIndex % 4));
                if (sign == 0)
                {
                    this.speed.Y *= -1;
                }
            }
            if (sign == 0)
            {
                this.rotationChange *= -1;
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
            // TODO: Add your update code here
            rotationFactor += rotationChange;
            position.X += speed.X;
            position.Y += speed.Y;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, null, Color.White, rotationFactor, origin,
                1, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getActiveBounds()
        {
            if (collisionTime > 0)
            {
                return new Rectangle((int)position.X - (tex.Width / 8), (int)position.Y - (tex.Height / 8),
                    (int)((tex.Width / 8) * 2), (int)((tex.Height / 8) * 2));
            }
            else
            {
                return new Rectangle((int)position.X - (tex.Width / 2), (int)position.Y - (tex.Height / 2), (int)tex.Width, (int)tex.Height);
            }

        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X - ((tex.Width / 8) * 3), (int)position.Y - ((tex.Height / 8) * 3),
                    (int)((tex.Width / 8) * 6), (int)((tex.Height / 8) * 6));
        }

    }
}
