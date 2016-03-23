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
    public class SplashScreen : GameScreen
    {
        private MenuItem menu;

        public MenuItem Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        private SpriteBatch spriteBatch;
        string[] menuItems = {"Start Game",
                             "Help",
                             "High Score",
                             "About/Credit",
                             "How to play",
                             "Quit"};

        SimpleString title;

        KeyItem menuImage;

        public SplashScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;

            string screenTitle = "Masteroids";
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/titleFont");
            title = new SimpleString(game, spriteBatch, font, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X/2 - 200, Shared.stage.Y / 6), screenTitle, Color.LimeGreen);
            this.Components.Add(title);

            menu = new MenuItem(game, spriteBatch,
                game.Content.Load<SpriteFont>("Fonts/regularFont"),
                game.Content.Load<SpriteFont>("Fonts/hilightFont"), new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X/2 + font.MeasureString(screenTitle).X/6 + 200, Shared.stage.Y / 2 + 100),
                menuItems);
            this.Components.Add(menu);

            menuImage = new KeyItem(game, spriteBatch, game.Content.Load<Texture2D>("Images/menuScreen"), Shared.stage/2, Shared.stage/2);
            this.Components.Add(menuImage);

            
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
