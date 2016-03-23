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
    public class HowToPlayScreen : GameScreen
    {
        SpriteBatch spriteBatch;

        SimpleString title;

        SimpleString wDesc;
        SimpleString aDesc;
        SimpleString dDesc;
        SimpleString spaceDesc;
        SimpleString escDesc;

        KeyItem wKey;
        KeyItem aKey;
        KeyItem dKey;
        KeyItem spaceKey;
        KeyItem escKey;

        public HowToPlayScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;

            string screenTitle = "How To Play";
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/titleFont");
            title = new SimpleString(game, spriteBatch, font, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y / 6), screenTitle, Color.LimeGreen);
            this.Components.Add(title);

            Vector2 stage = new Vector2(Shared.stage.X, Shared.stage.Y);

            // draw keys
            Texture2D tex = game.Content.Load<Texture2D>("Images/WKey1");
            Vector2 pos = new Vector2(stage.X / 4, stage.Y - 550);
            wKey = new KeyItem(game, spriteBatch, tex, pos, Shared.stage);
            this.Components.Add(wKey);

            Texture2D tex1 = game.Content.Load<Texture2D>("Images/AKey1");
            Vector2 pos1 = new Vector2(stage.X / 4, stage.Y - 500);
            aKey = new KeyItem(game, spriteBatch, tex1, pos1, Shared.stage);
            this.Components.Add(aKey);

            Texture2D tex2 = game.Content.Load<Texture2D>("Images/DKey1");
            Vector2 pos2 = new Vector2(stage.X / 4, stage.Y - 450);
            dKey = new KeyItem(game, spriteBatch, tex2, pos2, Shared.stage);
            this.Components.Add(dKey);

            Texture2D tex3 = game.Content.Load<Texture2D>("Images/SpaceKey1");
            Vector2 pos3 = new Vector2(stage.X / 4 + 90, stage.Y - 400);
            spaceKey = new KeyItem(game, spriteBatch, tex3, pos3, Shared.stage);
            this.Components.Add(spaceKey);

            Texture2D tex4 = game.Content.Load<Texture2D>("Images/EscKey1");
            Vector2 pos4 = new Vector2(stage.X / 4, stage.Y - 350);
            escKey = new KeyItem(game, spriteBatch, tex4, pos4, Shared.stage);
            this.Components.Add(escKey);

            // draw key descriptions
            SpriteFont bodyFont = game.Content.Load<SpriteFont>("Fonts/bodyFont");

            string desc = "Move Forward";
            wDesc = new SimpleString(game, spriteBatch, bodyFont, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y - 560), desc, Color.LimeGreen);
            this.Components.Add(wDesc);

            string desc1 = "Turn Left";
            aDesc = new SimpleString(game, spriteBatch, bodyFont, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y - 510), desc1, Color.LimeGreen);
            this.Components.Add(aDesc);

            string desc2 = "Turn Right";
            dDesc = new SimpleString(game, spriteBatch, bodyFont, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y - 460), desc2, Color.LimeGreen);
            this.Components.Add(dDesc);

            string desc3 = "Fire Weapon";
            spaceDesc = new SimpleString(game, spriteBatch, bodyFont, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2 + 100, Shared.stage.Y - 410), desc3, Color.LimeGreen);
            this.Components.Add(spaceDesc);

            string desc4 = "Back to Menu";
            escDesc = new SimpleString(game, spriteBatch, bodyFont, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y - 360), desc4, Color.LimeGreen);
            this.Components.Add(escDesc);
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
