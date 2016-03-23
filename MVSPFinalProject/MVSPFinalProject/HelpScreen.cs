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
    public class HelpScreen : GameScreen
    {
        private SpriteBatch spriteBatch;

        SimpleString title;
        SimpleString body;

        public HelpScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;

            string screenTitle = "Help";
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/titleFont");
            title = new SimpleString(game, spriteBatch, font, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y / 6), screenTitle, Color.LimeGreen);
            this.Components.Add(title);

            string helpInfo = "The purpose of this game is to have the most fun experience of your entire life.\nYou control a lone space ship and must achieve milestone scores" +
                " by destroying\nasteroids. Once you reach a milestone score, you will advance to another level\nof asteroid chaos. Achieve a high score and we will send you a free t-shirt";
            SpriteFont bodyFont = game.Content.Load<SpriteFont>("Fonts/bodyFont");
            body = new SimpleString(game, spriteBatch, bodyFont, new Vector2(0, Shared.stage.Y / 2), helpInfo, Color.LimeGreen);
            this.Components.Add(body);
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

            base.Update(gameTime);
        }
    }
}
