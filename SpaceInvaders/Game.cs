using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Media;

namespace SpaceInvaders
{
    /// <summary>
    /// Enumeration representing different state of the game
    /// </summary>
    enum GameState
    {
        Play , 
        Pause,
        Win,
        Lost,
    }
    /// <summary>
    /// This class represents the entire game, it implements the singleton pattern
    /// </summary>
    class Game
    {
        #region GameObjects management
        /// <summary>
        /// Instance of PlayerSpaceShip
        /// </summary>
        public PlayerSpaceShip playerShip;

        /// <summary>
        /// Current state of the game
        /// </summary>
        public GameState state = GameState.Play;

        /// <summary>
        /// Instance of EnemeyBlock
        /// </summary>
        private EnemyBlock enemies;

        /// <summary>
        /// 
        /// </summary>
        private Image backgroundImage;
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);

        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.GreenYellow);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Agency fb", 32, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion


        #region constructors
        /// <summary>
        /// A public Constructor of game class 
        /// </summary>
        public Game()
        {
        }
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);

            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            ResetGame(gameSize);
            SoundManager.Instance.PlayBackgroundMusic();
            backgroundImage = Properties.Resources.backgroundImage;
        }

        #endregion

        #region methods
        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            string[] message = {"","Pause", "You Win", "Game Over" };

            g.DrawImage(backgroundImage, 0, 0, gameSize.Width, gameSize.Height);

            if (state == GameState.Play)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject != playerShip) 
                    {
                        gameObject.Draw(this, g);
                    }
                }

                if (playerShip != null)
                {
                    playerShip.Draw(this, g);
                    
                }
            }
            else
            {
                g.DrawString(message[(int)state], defaultFont, blackBrush, gameSize.Width / 2 - 50, gameSize.Height / 2);
              
            }
        }

        /// <summary>
        /// Update the game state based on the current game state
        /// </summary>
        /// <param name="deltaT">Time elapsed since the last update</param>
        public void Update(double deltaT)
        {
            if (state == GameState.Play)
            {
                UpdatePlayState(deltaT);
                //playerShip.Update(this,deltaT);
                gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
                CheckObjectStatus();
                HandlekeyPause();

            }
            else if (state == GameState.Pause)
            {
                UpdatePauseState(deltaT);
            }
            else if (state == GameState.Win || state == GameState.Lost)
            {
                UpdateEndGameState();
            }
        }

        /// <summary>
        /// Update game element during the play state
        /// </summary>
        /// <param name="deltaT">Time elapsed since the last update</param>
        private void UpdatePlayState(double deltaT)
        {
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();
            playerShip.Update(this, deltaT);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }
        }

        /// <summary>
        /// Update game element during the pause state
        /// </summary>
        /// <param name="deltaT">Time elapsed since the last update</param>
        private void UpdatePauseState(double deltaT)
        {
            //If 'P' key is pressed during pause, return to playing state
            if (keyPressed.Contains(Keys.P))
            {
                state = GameState.Play;
                ReleaseKey(Keys.P);
            }
            if (keyPressed.Contains(Keys.P))
            {
                state = GameState.Pause;
                ReleaseKey(Keys.P);
            }
        }

        /// <summary>
        /// Check the status of the gaem objects , and update the game state accordingly
        /// </summary>
        private void CheckObjectStatus()
        {
            //if enemies is not alive , update the game state to win 
            if (!enemies.IsAlive()) 
            {
                state = GameState.Win;

            }

            //if enemies is not alive , update the game state to lost 
            if (!playerShip.IsAlive()) 
            {

                state = GameState.Lost;
            }
            //Check if enemies have reached the PlayerSpaceShip , his live is reset
            if(enemies.Position.y  >= playerShip.Position.y)
            {
                playerShip.Lives = 0;
            }
        }

        /// <summary>
        /// Update game end state , allowing the game to restart if the "Space" key is pressed
        /// </summary>
        private void UpdateEndGameState()
        {
            if (keyPressed.Contains(Keys.Space))
            {
                gameObjects.Clear();
                pendingNewGameObjects.Clear();
                ResetGame(gameSize);
                ReleaseKey(Keys.Space);
            }
        }

        /// <summary>
        /// Handle the 'P' key during game state, allowing the game to be paused.
        /// </summary>
        private void HandlekeyPause()
        {
            //If 'P' key is pressed during pause, return to Pause state
            if (keyPressed.Contains(Keys.P))
            {
                state = GameState.Pause;
                ReleaseKey(Keys.P);
            }

        }

        /// <summary>
        /// Reset the game to its initial state with the given game size
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        public void ResetGame(Size gameSize)
        {
            this.gameSize = gameSize;
            AddPlayerShip();
          
            AddBunker();
            AddEnemyBlock();
            state = GameState.Play;
            PlayBackgroundMusic();
        }

        /// <summary>
        /// Add the player ship to the game
        /// </summary>
        private void AddPlayerShip()
        {
            Bitmap image = Properties.Resources.ship3;
            double initialX = (gameSize.Width - image.Width) / 2;
            double initialY = (gameSize.Height - image.Height) -10;
            Vecteur2D initPos = new Vecteur2D(initialX, initialY);
            playerShip = new PlayerSpaceShip(initPos,3, image);
            AddNewGameObject(playerShip);
        }

        /// <summary>
        /// Add the bunkers to the game
        /// </summary>
        private void AddBunker()
        {
            int espacementBunker = gameSize.Width / 3;
            int bunkerY = gameSize.Height - 120;
            for (int i = 1; i < 4; i++)
            {
                Vecteur2D bunkerPos = new Vecteur2D(espacementBunker * i - 1.6 * Properties.Resources.bunker.Width, bunkerY);
                Bunker bunker = new Bunker(bunkerPos);
                AddNewGameObject(bunker);
            }
        }

        /// <summary>
        /// Add Enemy Block to the game
        /// </summary>
        private void AddEnemyBlock()
        {
            int baseWidth = gameSize.Width - 200;
            enemies = new EnemyBlock(new Vecteur2D(0, 50), baseWidth);
            enemies.AddLine(4, 1, Properties.Resources.ship2);
            enemies.AddLine(7, 1, Properties.Resources.ship8);
            enemies.AddLine(7, 1, Properties.Resources.ship8);
            enemies.AddLine(8, 1, Properties.Resources.ship9);
            enemies.AddLine(8, 1, Properties.Resources.ship9);
            AddNewGameObject(enemies);

        }

        /// <summary>
        /// Add Background Music to the game
        /// </summary>
        private void PlayBackgroundMusic()
        {
            SoundManager.Instance.PlayBackgroundMusic();
        }
        #endregion
    }
}
