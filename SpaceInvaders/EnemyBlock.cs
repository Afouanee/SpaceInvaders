using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represent the Enemy Block in the  SpaceInvaders game 
    /// </summary>
    internal class EnemyBlock : GameObject
    {
        #region Fields
        /// <summary>
        /// All  ships in the block
        /// </summary>
        private HashSet<SpaceShip> enemyShips;

        /// <summary>
        /// Width of the block at the time of its creation
        /// </summary>
        private int baseWidth;

        /// <summary>
        /// Probability of random shooting by the enemyShips
        /// </summary>
        private double randomShootProbability;

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random rnd = new Random();

        /// <summary>
        /// Block size (width, height), adapted as the game progresses.
        /// </summary>
        #endregion

        #region properties
        public Size size {  get;  set; }

        /// <summary>
        /// Position du bloc (coin supérieur gauche)
        /// </summary>
        public Vecteur2D Position { get;  set; }
        #endregion

        #region EnemyBlock technical elements
        /// <summary>
        /// This temporary list will store the ships to delete
        /// </summary>
        List<SpaceShip> toRemove = new List<SpaceShip>();
        #endregion

        #region constructors
        /// <summary>
        /// Public constructor for the EnemyBlock class
        /// </summary>
        /// <param name="position">The initial position of the EnemyBlock</param>
        /// <param name="basewidth">The initial width of the block</param>
        public EnemyBlock(Vecteur2D position,int basewidth):base(Side.Enemy)
        {
            Position = position;
            baseWidth = basewidth;
            enemyShips = new HashSet<SpaceShip>();
            size = new Size(0,0);
            randomShootProbability = 0;

        }
        #endregion

        #region methods
        /// <summary>
        /// Add a new line of enemies to the block
        /// </summary>
        /// <param name="nbShips">Number of ships in the line</param>
        /// <param name="nbLives">Number of lives of each ship </param>
        /// <param name="shipImage">The image representing the ship</param>
        public void AddLine(int nbShips,int nbLives,Bitmap shipImage)
        {
            //Calculate total spacing and spacing between enemyShips
            double totalSpacing = baseWidth - nbShips * shipImage.Width;
            int enemySpacing = (int) (totalSpacing / (nbShips-1));
            //Calculate the initial position for the first ship in the line
            double firstShipPlacement = Position.x + (baseWidth - nbShips * shipImage.Width - totalSpacing) /1.0;
            
            //Calculate the lowest y-position for the new line of ships
            double lowestY = CalculateLowestY(shipImage);
            //Create the new line of enemyShips
            CreateEnemyShip(nbShips, nbLives, shipImage, firstShipPlacement, enemySpacing, lowestY);

            //Update the size of the enemy block
            UpdateSize();
        }

        /// <summary>
        /// Calculate the lowest y-position for the new line of ships
        /// </summary>
        /// <param name="shipImage">The image representing the ship</param>
        /// <returns>calculate the lowest y-position </returns>
        private double CalculateLowestY(Bitmap shipImage)
        {
            double maxY = Position.y -30;
           // Iterate existing enemy ships to find the maximum y-position
            foreach (SpaceShip ship in enemyShips)
            {
                if (ship.Position.y> maxY)
                {
                    maxY = ship.Position.y; 
                }
            }

            //Returns the lowest Y position for the new ship line
            return maxY + shipImage.Height+5;
        }

        /// <summary>
        /// Create the new line of enemyShips
        /// </summary>
        /// <param name="nbShips">Number of ships in the line</param>
        /// <param name="nbLives">Number of lives of each ship</param>
        /// <param name="shipImage">The image representing the ship</param>
        /// <param name="firstShipPlacement">The x-position for the first ship in the line</param>
        /// <param name="enemySpacing">Spacing between enemyShips</param>
        /// <param name="lowestY">The lowest y-position for the first ship in the line</param>
        private void CreateEnemyShip(int nbShips, int nbLives, Bitmap shipImage,double firstShipPlacement,int enemySpacing,double lowestY)
        {
            for (int i = 0; i < nbShips; i++)
            {
                double positionEnemyX = firstShipPlacement + i * (shipImage.Width + enemySpacing);
                Vecteur2D positionEnemy = new Vecteur2D(positionEnemyX, lowestY);
                SpaceShip enemy = new SpaceShip(positionEnemy, nbLives, shipImage, Camp);

                //Add the enemy ship to the HashSet
                enemyShips.Add(enemy);
            }

        }

        /// <summary>
        /// Update the size of enemy block
        /// </summary>
        public void UpdateSize()
        {
            int minX = int.MaxValue;
            int maxX= 0;

            //Update the position based on alive ships
            UpdatePosition(ref minX, ref maxX);

            //Check if all ships are destroyed
            if (minX == int.MaxValue || maxX == 0)
            {
                //if true set the size to zero
                size = new Size(0,0);
            }
            else
            {
                // Find the bottom position of the lowest living ship.
                int maxY = enemyShips.Where (s=>s.IsAlive()).Max(s => (int)s.Position.y);
                // Update the size
                size = new Size(maxX-minX,maxY);

                //Update the block position
                Position.x = minX;
            }


        }

        /// <summary>
        /// Update the position of alive ships
        /// </summary>
        /// <param name="minX">Reference to the leftmost position </param>
        /// <param name="maxX">Reference to the rightmost position</param>
        private void UpdatePosition(ref int minX,ref int maxX)
        {
            //Update the position based on alive ships
            foreach (SpaceShip ship in enemyShips.Where(s => s.IsAlive()))
            {
                minX = Math.Min(minX, (int)ship.Position.x);
                maxX = Math.Max(maxX, (int)ship.Position.x + ship.Image.Width);
            }
        }

        /// <summary>
        /// Handle collision with a missile 
        /// </summary>
        /// <param name="missile">The missile object</param>
        public override void Collision(Missile missile)
        {
            // Iterate through each enemy ship in the block and handle collision.
            foreach (SpaceShip ship in enemyShips)
            {
                ship.Collision(missile);
                     
            }
            //Remove destroyed ships
            RemoveShip();                  

        }

        /// <summary>
        /// Draw the EnemyBlock
        /// </summary>
        /// <param name="gameInstance">The current instance game</param>
        /// <param name="graphics">The graphics object for rendering</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach(SpaceShip ennemie in enemyShips)
            {
                ennemie.Draw(gameInstance, graphics);
            }
            
        }

        /// <summary>
        /// Check if each enemy is alive
        /// </summary>
        /// <returns>True if at list one enemy is Alive, false otherwise </returns>
        public override bool IsAlive()
        {
            foreach(SpaceShip enemy in enemyShips)
            {
                if (enemy.IsAlive())
                {

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update the state of the EnemyBlock
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        /// <param name="deltaT">Time elapsed since the last update </param>
        public override void Update(Game gameInstance, double deltaT)
        {

            double vitesse = (Position.y) * deltaT;
            bool deplacementDroite = (int)Position.y %2 == 0;
            double descente = 0.0;
            double mouvementDirection = deplacementDroite ? 1 : -1;
            Position.x += mouvementDirection*vitesse;
            UpdateBlockMouvement(gameInstance,ref descente, deplacementDroite);
            UpdateEnemyShips(gameInstance, deltaT,vitesse, descente, deplacementDroite);
            BonusDisplay(gameInstance,  deltaT);
            RemoveShip();

        }

        /// <summary>
        /// Display bonus if the ship is not Alive and handle collision with it
        /// </summary>
        /// <param name="gameInstance">The Current Instance of the game</param>
        /// <param name="deltaT">Time elapsed since the last update</param>
        private void BonusDisplay(Game gameInstance, double deltaT) 
        {
            double probability = 0.2;
            double randomValue = rnd.NextDouble();
            foreach (SpaceShip ship in enemyShips)
            {
                ship.Update(gameInstance, deltaT);
                if (!ship.IsAlive())
                {

                    if (randomValue < probability)
                    {
                        AddBonus(gameInstance);
                    }
                    toRemove.Add(ship);
                }

            }
        }

        /// <summary>
        /// Add and Create the Bonus 
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        private void AddBonus(Game gameInstance )
        {
            Bitmap image = Properties.Resources.bonus;
            Vecteur2D position = new Vecteur2D(rnd.Next(gameInstance.gameSize.Width - image.Width), 0);
            GameObject bonus = new Bonus(position, image);
            gameInstance.AddNewGameObject(bonus);

        }
        /// <summary>
        /// Remove destroyed ships and 
        /// </summary>
        private void RemoveShip() 
        {
            //Remove destroyed ships
            foreach (SpaceShip ship in toRemove)
            {
                enemyShips.Remove(ship);
            }
            //Calculate the size after handling collisions
            if (toRemove.Count > 0)
            {
                UpdateSize();
            }
        }
        /// <summary>
        /// Updates the block's movement based on its position and game boundaries
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        /// <param name="descente">Descent variable</param>
        /// <param name="deplacementDroite">Direction of movement</param>
        private void UpdateBlockMouvement(Game gameInstance, ref double descente, bool deplacementDroite)
        {
            if(deplacementDroite && (Position.x + size.Width) >= gameInstance.gameSize.Width  || (!deplacementDroite && Position.x <= 0))
            {
                int maxY = enemyShips.Where(s => s.IsAlive()).Max(s => (int)s.Position.y);
                Position.y += 7;
                descente = 7;
                randomShootProbability += 0.01;
                if (maxY >= gameInstance.playerShip.Position.y - gameInstance.playerShip.Image.Height)
                {
                    gameInstance.playerShip.Lives = 0;
                }

            }

        }

        /// <summary>
        /// Updates the position of enemy ships and handles random shoot.
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        /// <param name="deltaT">Time elapsed since the last update</param>
        /// <param name="vitesse">The speed of the block</param>
        /// <param name="descente">Descent variable</param>
        /// <param name="deplacementDroite">Direction of movement</param>
        private void UpdateEnemyShips(Game gameInstance, double deltaT, double vitesse,double descente, bool deplacementDroite)
        {
            foreach (SpaceShip ship in enemyShips)
            {
                double r = rnd.NextDouble();
                ship.Position.x += deplacementDroite ? vitesse : -vitesse;
                ship.Position.y += descente;
                if (r <= randomShootProbability * deltaT)
                {
                    ship.Shoot(gameInstance);
                }

            }

        }
        #endregion 


    }
}

