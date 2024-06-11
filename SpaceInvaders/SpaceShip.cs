using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceInvaders
{

    /// <summary>
    /// This class inherits from simple object represent a SpaceShip in the SpaceInvaders game 
    /// </summary>
    internal class SpaceShip : SimpleObject
    {
        #region fields
        /// <summary>
        /// Private field to represent the missile associated with SpaceShip
        /// </summary>
        private Missile missile;
        #endregion

        #region constructors
        /// <summary>
        /// Public constructor for the SpaceShip class
        /// </summary>
        /// <param name="position">The initial position of the SpaceShip</param>
        /// <param name="lives">The initial number of lives of the SpaceShip</param>
        /// <param name="image">The image representing the SpaceShip</param>
        /// <param name="camp">The side or camp of the SpaceShip</param>
        public SpaceShip (Vecteur2D position,int lives,Bitmap image,Side camp):base(position,lives,image,camp)
        {
           // No specific initialisation because it was done by the parent class (SimpleObject)
        }
        #endregion

        #region methods
        /// <summary>
        /// Update method for the SpaceShip
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
          // The method is empty because it will be defined in the PlayerSpaceShip class
        }
        
        /// <summary>
        /// Shoot method for firing a missile from the SpaceShip
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        public void Shoot(Game gameInstance)
        {
            //Check if there is no missile or the existing one is not alive
            if(missile == null || !missile.IsAlive()) 
            {
                //Create a new missile
                Bitmap image = Properties.Resources.shoot1;
                double possitionMissileX = Position.x + (Image.Width / 2) - (image.Width / 2);
                double positionMissileY = Position.y;
                Vecteur2D positionMissile = new Vecteur2D(possitionMissileX, positionMissileY);
                missile = new Missile(positionMissile, 1, image,Camp);

                //Add the new missile to game object
                gameInstance.AddNewGameObject(missile);
                
            }
        }

        /// <summary>
        /// Handles collision between missiles and SpaceShip object
        /// </summary>
        /// <param name="m">The missile involved in the collision</param>
        /// <param name="numberOfPixelsInCollision">The number of pixels in collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            //Determine the minimum value between missile lives and SpaceShip lives
            int minimumValue = Math.Min(m.Lives, Lives);
            //Decrement of lives of both missile and SpaceShip by the minimum value 
            m.Lives -= minimumValue;
            Lives -= minimumValue;
            
        }
        #endregion
    }
}
