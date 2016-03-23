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
    public class ActionScreen : GameScreen
    {
        SpriteBatch spriteBatch;

        // ship
        private Ship ship;

        public Ship Ship
        {
            get { return ship; }
            set { ship = value; }
        }

        //score and level display
        private SimpleString inGameDisplay;

        //game over message
        private SimpleString gameOverMessage;

        private LevelCaption caption;

        // meteor
        private float t = 0;
        private const int asteroidInterval = 1;
        private const int ASTEROID_NUM_MAX = 20;
        private const int ASTEROID_POS_MAX = 16;
        private const int ASTEROID_SPEED_RANGE = 3;
        private const int NUMBER_OF_ASTEROIDS_IMG = 5;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private int level = 1;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private Random r = new Random();

        // Collision Manager
        private CollisionManager cm;

        private int score = 0;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

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

        public string HighScoreFileName = "highscores.dat";

        public ActionScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;

            SoundEffect explosionSound = game.Content.Load<SoundEffect>("Sounds/explosionSound");

            Texture2D tex = game.Content.Load<Texture2D>("Images/spaceShipIcon");
            Vector2 stage = new Vector2(Shared.stage.X,
                Shared.stage.Y);
            Vector2 pos = new Vector2(stage.X / 2, stage.Y / 2);
            Vector2 speed = new Vector2(1, 1);
            ship = new Ship(game, spriteBatch, tex, pos, speed, Shared.stage);
            this.Components.Add(ship);

            cm = new CollisionManager(game, spriteBatch, asteroids, ship, explosionSound);
            this.Components.Add(cm);

            caption = new LevelCaption(Game, spriteBatch, Shared.stage, "Level 1");
            Components.Add(caption);

            string scoreText = "Score: " + score + "\nLevel: " + level;
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/regularFont");
            inGameDisplay = new SimpleString(game, spriteBatch, font, new Vector2(0, 0), scoreText, Color.LimeGreen);
            this.Components.Add(inGameDisplay);
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
            createRemoveAsteroid(gameTime);
            boundAsteroid();
            removeHitComponent();
            score = level*10 + cm.Score - 10;
            inGameDisplay.Title = "Score: " + score + "\nLevel: " + level;
            GameOver();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
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

        public static HighScoreData LoadHighScores(string fileName)
        {
            HighScoreData data;

            string highScorePath = "highscores.dat";

            FileStream stream = File.Open(highScorePath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                data = (HighScoreData)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }

            return data;
        }

        private void SaveHighScore()
        {
            HighScoreData data = LoadHighScores(HighScoreFileName);

            int scoreIndex = -1;
            for (int i = data.Count - 1; i > -1; i--)
            {
                if (score >= data.scores[i])
                {
                    scoreIndex = i;
                }
            }

            if (scoreIndex > -1)
            {
                for (int i = data.Count - 1; i > scoreIndex; i--)
                {
                    data.scores[i] = data.scores[i];
                }

                data.scores[scoreIndex] = score;

                SaveHighScores(data, HighScoreFileName);
            }
        }

        public void GameOver()
        {
            if (ship.IsHit == true)
            {
                Game.Components.Remove(ship.Bullet);
                SaveHighScore();
                string gameOverText = "Game Over... Score: " + score + "\npress esc key to return to menu";
                SpriteFont font = Game.Content.Load<SpriteFont>("Fonts/regularFont");
                gameOverMessage = new SimpleString(Game, spriteBatch, font, new Vector2(Shared.stage.X/2 - font.MeasureString(gameOverText).X/2, Shared.stage.Y/2), gameOverText, Color.LimeGreen);
                this.Components.Add(gameOverMessage);
                cm.Enabled = false;
            }
        }

        public void createRemoveAsteroid(GameTime gameTime)
        {
            t += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            if (t >= asteroidInterval)
            {
                t = 0;

                if (asteroids.Count < ASTEROID_NUM_MAX)
                {
                    //create asteroid
                    float speedX = (1f / (float)ASTEROID_SPEED_RANGE) * (float)r.Next(1, ASTEROID_SPEED_RANGE + 1) * (float)level;
                    float speedY = (1f / (float)ASTEROID_SPEED_RANGE) * (float)r.Next(1, ASTEROID_SPEED_RANGE + 1) * (float)level;
                    Vector2 speed = new Vector2(speedX, speedY);

                    Texture2D tex;
                    switch (r.Next(0, NUMBER_OF_ASTEROIDS_IMG))
                    {
                        case 0:
                            tex = Game.Content.Load<Texture2D>("Images/asteroid1");
                            break;
                        case 1:
                            tex = Game.Content.Load<Texture2D>("Images/asteroid2");
                            break;
                        case 2:
                            tex = Game.Content.Load<Texture2D>("Images/asteroid3");
                            break;
                        case 3:
                            tex = Game.Content.Load<Texture2D>("Images/asteroid4");
                            break;
                        case 4:
                            tex = Game.Content.Load<Texture2D>("Images/asteroid5");
                            break;
                        default:
                            tex = Game.Content.Load<Texture2D>("Images/asteroid1");
                            break;
                    }
                    Asteroid a = new Asteroid(Game, spriteBatch, Shared.stage, tex, r.Next(0, ASTEROID_POS_MAX), speed, r.Next(0, 2));
                    Components.Add(a);
                    asteroids.Add(a);
                }

                //remove asteroid
                for (int i = 0; i < asteroids.Count; i++)
                {
                    Asteroid a = asteroids.ElementAt(i);
                    if (a.CollisionTime > 0)
                    {
                        a.CollisionTime--;
                    }
                    if (a.Position.X < -a.Tex.Width / 2 ||
                        a.Position.Y < -a.Tex.Height / 2 ||
                        a.Position.X > Shared.stage.X + a.Tex.Width / 2 ||
                        a.Position.Y > Shared.stage.Y + a.Tex.Height / 2)
                    {
                        Components.Remove(asteroids.ElementAt(i));
                        asteroids.RemoveAt(i);
                    }
                }
            }
        }

        public void boundAsteroid()
        {
            for (int i = 0; i < asteroids.Count - 1; i++)
            {
                for (int j = i + 1; j < asteroids.Count; j++)
                {
                    Asteroid a = asteroids.ElementAt(i);
                    Asteroid b = asteroids.ElementAt(j);
                    if (a.getActiveBounds().Intersects(b.getActiveBounds()))
                    {
                        Vector2 tempSpeed = a.Speed;
                        a.Speed = b.Speed;
                        b.Speed = tempSpeed;
                        a.CollisionTime = 3;
                        b.CollisionTime = 3;
                    }
                }
            }
        }

        public void removeHitComponent()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                if(asteroids.ElementAt(i).IsHit)
                {
                    Components.Remove(asteroids.ElementAt(i));
                    asteroids.RemoveAt(i);
                }
            }
            if(ship.IsHit)
            {
                Components.Remove(ship);
            }
        }

        public void DisplayLevel()
        {
            caption.Text = "Level " + level;
            caption.Position = new Vector2(Shared.stage.X, Shared.stage.Y / 4);
            
        }
    }
}
