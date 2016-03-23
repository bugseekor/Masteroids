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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // declare scenes
        private ActionScreen actionScreen;
        private SplashScreen splashScreen;
        private HelpScreen helpScreen;
        private AboutScreen aboutScreen;
        private HighScoreScreen highScoreScreen;
        private HowToPlayScreen howToPlayScreen;

        private int level = 1;

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

        //music
        protected Song song;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

            string highScorePath = "highscores.dat";

            if (!File.Exists(highScorePath))
            {
                //If the file doesn't exist, make a fake one...
                // Create the data to save
                data = new HighScoreData(5);
                data.scores[0] = 1000000;

                data.scores[1] = 18;

                data.scores[2] = 15;

                data.scores[3] = 10;

                data.scores[4] = 5;

                SaveHighScores(data, highScorePath);
            }

            base.Initialize();
        }

        public void SaveHighScores(HighScoreData data, string fileName)
        {
            string highScorePath = "highscores.dat";

            FileStream stream = File.Open(highScorePath, FileMode.OpenOrCreate);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Vector2 stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

            Texture2D texBack1 = Content.Load<Texture2D>("Images/stars2");
            ScrollingBackground sb1 = new ScrollingBackground(this, spriteBatch, stage, texBack1, new Vector2(0, 1), 1);

            Texture2D texBack2 = Content.Load<Texture2D>("Images/stars");
            ScrollingBackground sb2 = new ScrollingBackground(this, spriteBatch, stage, texBack2, new Vector2(0, 2), 0.5f);

            song = Content.Load<Song>("Sounds/music");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            this.Components.Add(sb1);
            this.Components.Add(sb2);

            actionScreen = new ActionScreen(this, spriteBatch);
            Components.Add(actionScreen);

            splashScreen = new SplashScreen(this, spriteBatch);
            Components.Add(splashScreen);

            helpScreen = new HelpScreen(this, spriteBatch);
            Components.Add(helpScreen);

            aboutScreen = new AboutScreen(this, spriteBatch);
            Components.Add(aboutScreen);

            highScoreScreen = new HighScoreScreen(this, spriteBatch);
            Components.Add(highScoreScreen);

            howToPlayScreen = new HowToPlayScreen(this, spriteBatch);
            Components.Add(howToPlayScreen);

            splashScreen.show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            
            if (splashScreen.Enabled)
            {
                selectedIndex = splashScreen.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    Components.Remove(actionScreen);
                    actionScreen = new ActionScreen(this, spriteBatch);
                    level = 1;
                    Components.Add(actionScreen);
                    hideAllScenes();
                    actionScreen.show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScreen.show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    Components.Remove(highScoreScreen);
                    highScoreScreen = new HighScoreScreen(this, spriteBatch);
                    Components.Add(highScoreScreen);
                    hideAllScenes();
                    highScoreScreen.show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScreen.show();
                }
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    howToPlayScreen.show();
                }
                if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (helpScreen.Enabled || aboutScreen.Enabled || highScoreScreen.Enabled || howToPlayScreen.Enabled || actionScreen.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    Components.Remove(actionScreen.Ship.Bullet);
                    hideAllScenes();
                    splashScreen.show();
                }
            }

            if (actionScreen.Score == level*10)
            {
                Components.Remove(actionScreen);
                actionScreen = new ActionScreen(this, spriteBatch);
                level += 1;
                Components.Add(actionScreen);
                actionScreen.Level = level;
                actionScreen.show();
                actionScreen.DisplayLevel();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void CreateActionScreen()
        {
            Components.Remove(actionScreen);
            actionScreen = new ActionScreen(this, spriteBatch);
            Components.Add(actionScreen);
        }
    }
}
