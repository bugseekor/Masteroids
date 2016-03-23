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
    public class LevelCaption : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private int delay = 100;
        private int delayCounter;
        private Vector2 stage;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed = new Vector2(-20, 0);
        private Vector2 dimension;

        public LevelCaption(Game game, SpriteBatch spriteBatch, Vector2 stage, string text)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.font = game.Content.Load<SpriteFont>("Fonts/stageFont");
            this.stage = stage;
            this.text = text;
            this.dimension = new Vector2(font.MeasureString(text).X, font.MeasureString(text).Y);
            this.position = new Vector2(stage.X, stage.Y / 4);
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

            if (position.X > stage.X / 2 - dimension.X / 2)
            {
                position += speed;
            }
            else
            {
                delayCounter++;
                if (delayCounter > delay)
                {
                    position += speed;
                }
                if (position.X < -dimension.X)
                {
                    speed = new Vector2(0, 0);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, position, Color.GreenYellow);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
