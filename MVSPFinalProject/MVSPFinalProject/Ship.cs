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
    public class Ship : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;

        private Vector2 initial_speed;
        private const float MAX_SPEED = 10.0F;

        private float rotationFactor = 0;

        public float RotationFactor
        {
            get { return rotationFactor; }
            set { rotationFactor = value; }
        }

        private float rotationChange = 0.03f;

        private Rectangle srcRect;
        private Vector2 origin;
        private float scaleFactor = 1.0f;

        private Bullet bullet;

        public Bullet Bullet
        {
            get { return bullet; }
            set { bullet = value; }
        }
        private Vector2 bulletDirection;
        private float bulletRotationFactor;
        private int bulletCount = 0;
        private bool isHit = false;

        public bool IsHit
        {
            get { return isHit; }
            set { isHit = value; }
        }
        private Thrust thrust1;
        private Thrust thrust2;
        Texture2D texFlaim;

        public Ship(Game game, SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage)
            : base(game) 
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.initial_speed = speed;
            this.rotationFactor = 0f;

            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            texFlaim = game.Content.Load<Texture2D>("Images/flamealpha");
            thrust1 = new Thrust(game, spriteBatch, texFlaim, this.position, 3, MathHelper.ToRadians(180), 1, Vector2.Zero,
                new Vector2(texFlaim.Width / 8 , texFlaim.Height / 4));
            game.Components.Add(thrust1);
            thrust2 = new Thrust(game, spriteBatch, texFlaim, this.position, 3, MathHelper.ToRadians(180), 1, Vector2.Zero,
                new Vector2(texFlaim.Width / 8, texFlaim.Height / 4));
            game.Components.Add(thrust2);
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
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyUp(Keys.W))
            {
                Vector2 direction = new Vector2((float)Math.Cos(rotationFactor + (float)Math.PI / 2), (float)Math.Sin(rotationFactor + (float)Math.PI / 2));
                direction.Normalize();
                if (speed.X > initial_speed.X)
                {
                    speed.X *= 0.95f;
                    position.X -= direction.X * speed.X;
                    if (speed.X < initial_speed.X)
                    {
                        speed.X = initial_speed.X;
                    }
                }
                if (speed.Y > initial_speed.Y)
                {
                    speed.Y *= 0.95f;
                    position.Y -= direction.Y * speed.Y;
                    if (speed.Y < initial_speed.Y)
                    {
                        speed.Y = initial_speed.Y;
                    }
                }
                keepShip();
            }
            if (ks.IsKeyDown(Keys.W))
            {
                Vector2 direction = new Vector2((float)Math.Cos(rotationFactor + (float)Math.PI / 2), (float)Math.Sin(rotationFactor + (float)Math.PI / 2));

                direction.Normalize();

                position -= direction * speed;

                if (speed.X < MAX_SPEED)
                    speed.X *= 1.03f;
                if (speed.Y < MAX_SPEED)
                    speed.Y *= 1.03f;

                keepShip();
                thrust1.Position = position +
                    new Vector2(tex.Height / 4 * -(float)Math.Sin(rotationFactor + 0.5),
                    tex.Height / 4 * (float)Math.Cos(rotationFactor + 0.5));
                thrust1.Enabled = true;
                thrust1.Visible = true;
                thrust2.Position = position +
                    new Vector2(tex.Height / 4 * -(float)Math.Sin(rotationFactor - 0.5),
                    tex.Height / 4 * (float)Math.Cos(rotationFactor - 0.5));
                thrust2.Enabled = true;
                thrust2.Visible = true;
            }
            else
            {
                thrust1.Enabled = false;
                thrust1.Visible = false;
                thrust2.Enabled = false;
                thrust2.Visible = false;
            }
            if (ks.IsKeyDown(Keys.A))
            {
                rotationFactor -= rotationChange;
                thrust1.RotationFactor -= rotationChange;
                thrust2.RotationFactor -= rotationChange;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                rotationFactor += rotationChange;
                thrust1.RotationFactor += rotationChange;
                thrust2.RotationFactor += rotationChange;

            }
            if (ks.IsKeyDown(Keys.Space) && bulletCount == 0)
            {
                bulletCount++;
                Vector2 direction = new Vector2((float)Math.Cos(rotationFactor + (float)Math.PI / 2), (float)Math.Sin(rotationFactor + (float)Math.PI / 2));
                direction.Normalize();
                bulletDirection = direction;
                bulletRotationFactor = rotationFactor;

                Texture2D tex = Game.Content.Load<Texture2D>("Images/bullet");
                Vector2 stage = new Vector2(Shared.stage.X,
                    Shared.stage.Y);
                Vector2 pos = position;
                Vector2 speed = new Vector2(6, 6);
                bullet = new Bullet(Game, spriteBatch, tex, pos, speed, Shared.stage);
                Game.Components.Add(bullet);
            }
            try
            {
                bullet.RotationFactor = bulletRotationFactor;
                bullet.Position -= bulletDirection * bullet.Speed;
                if (bullet.Position.X > Shared.stage.X - tex.Width * scaleFactor / 2 || 
                    bullet.Position.Y > Shared.stage.Y - tex.Height * scaleFactor / 2 || 
                    bullet.Position.X < tex.Width * scaleFactor / 2 || 
                    bullet.Position.Y < tex.Height * scaleFactor / 2 ||
                    bullet.IsHit)
                {
                    Game.Components.Remove(bullet);
                    bullet.Dispose();
                    bullet = null;
                    bulletCount = 0;
                }
            }
            catch (Exception)
            {
                
            }
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotationFactor,
                origin, scaleFactor, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)(position.X - tex.Width / 2), (int)(position.Y - tex.Height / 2),
                                (int)tex.Width, (int)tex.Height);
        }

        public void keepShip()
        {
            if (position.X > Shared.stage.X - tex.Width * scaleFactor / 2)
            {
                position.X = Shared.stage.X - tex.Width * scaleFactor / 2;
            }
            if (position.X < tex.Width * scaleFactor / 2)
            {
                position.X = tex.Width * scaleFactor / 2;
            }
            if (position.Y > Shared.stage.Y - tex.Height * scaleFactor / 2)
            {
                position.Y = Shared.stage.Y - tex.Height * scaleFactor / 2;
            }
            if (position.Y < tex.Height * scaleFactor / 2)
            {
                position.Y = tex.Height * scaleFactor / 2;
            }
        }

    }
}
