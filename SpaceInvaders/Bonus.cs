using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
   
    /// <summary>
    /// This represent the bonus 
    /// </summary>
    internal class Bonus : SimpleObject
    {
        #region Fields
        /// <summary>
        /// Speed of bonus
        /// </summary>
        private readonly int bonusSpeed = 120;
        #endregion

       #region constructors

        /// <summary>
        /// Initializes a new instance of the bonus class
        /// </summary>
        /// <param name="position">Initial position of the bonus</param>
        /// <param name="image">Image representing the bonus</param>
        public Bonus(Vecteur2D position , Bitmap image) : base(position,1,image,Side.Neutral)
        {
            //No specific initialization
        }
        #endregion

        #region methods

        /// <summary>
        /// Update the bonus position and checks for collision with the player
        /// </summary>
        /// <param name="gameInstance">The current instance of the game </param>
        /// <param name="deltaT">Time elpased since the last update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            Position.y += bonusSpeed * deltaT;
            if (BonusCollision(gameInstance.playerShip))
            {
                // Collision with player, give an extra life and remove the bonus
                gameInstance.playerShip.Lives++;

                // Mark the bonus as not alive
                Lives = 0; 
            }
        }


        /// <summary>
        /// Checks collision with the player's ship
        /// </summary>
        /// <param name="player">Player's spaceship</param>
        /// <returns>True if there is a collision, false otherwise  </returns>
        private bool BonusCollision(PlayerSpaceShip player)
        {
            Rectangle bonusRect = new Rectangle((int)Position.x, (int)Position.y, Image.Width, Image.Height);
            Rectangle playerRect = new Rectangle((int)player.Position.x, (int)player.Position.y, player.Image.Width, player.Image.Height);
            return bonusRect.IntersectsWith(playerRect);
        }

        /// <summary>
        /// Implementation of the OnCollision method from the base class.
        /// </summary>
        /// <param name="m">Missile involved in the collision</param>
        /// <param name="numberOfPixelsInCollision"> Number of Pixels involved in the collision </param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
        }
        #endregion

    }
}
