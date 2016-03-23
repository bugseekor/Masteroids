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
using System.IO;
using System.Xml.Serialization;


namespace MVSPFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class HighScoreScreen : GameScreen
    {
        SpriteBatch spriteBatch;

        SimpleString title;
        SimpleString body;

        string highScores = "";

        // high score
        public struct HighScoreData
        {
            public int[] scores;

            public int Count;

            public HighScoreData(int count)
            {
                scores = new int[count];

                Count = count;
            }
        }

        HighScoreData data;
        public string HighScoreFileName = "highscores.dat";

        private void hideAllScenes()
        {
            GameScreen gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScreen)
                {
                    gs = (GameScreen)item;
                    gs.hide();
                }
            }
        }

        public HighScoreScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            GetHighScores();

            string screenTitle = "High Score";
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/titleFont");
            title = new SimpleString(game, spriteBatch, font, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y / 6), screenTitle, Color.LimeGreen);
            this.Components.Add(title);

            SpriteFont bodyFont = game.Content.Load<SpriteFont>("Fonts/bodyFont");
            body = new SimpleString(game, spriteBatch, bodyFont, new Vector2(Shared.stage.X / 2 - font.MeasureString(screenTitle).X / 2, Shared.stage.Y / 2), highScores, Color.LimeGreen);
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

        public void GetHighScores()
        {
            HighScoreData data;
            // Get the path of the save game
            string fullpath = "highscores.dat";
            
            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                data = (HighScoreData)serializer.Deserialize(stream);
                for (int i = 0; i < data.Count; i++)
                {
                    highScores += "High Score " + (i + 1) + ": " + data.scores[i] + "\n\n";
                }
            }
            finally
            {
                // Close the file
                stream.Close();
            }
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
