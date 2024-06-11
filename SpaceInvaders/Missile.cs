using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents a missile in the SpaceInvaders game
    /// </summary>
    internal class Missile : SimpleObject
    {
        #region properties
        /// <summary>
        /// The speed of the missile
        /// </summary>
        public double Vitesse { get; set; }
        #endregion


        #region constructor
        /// <summary>
        /// A public constructor of missile class
        /// </summary>
        /// <param name="position">The initial position of the missile</param>
        /// <param name="lives">The number of lives the missile</param>
        /// <param name="image">The image representing the missile</param>
        /// <param name="camp">The side to which the missile belongs</param>
        public Missile(Vecteur2D position,int lives, Bitmap image, Side camp) : base(position,lives,image,camp)
        {
            Vitesse = 400;
        }
        #endregion


        #region methods
        /// <summary>
        /// Updates the missile's position and handles collisions with other game objects
        /// </summary>
        /// <param name="gameInstance"> The current instance game </param>
        /// <param name="deltaT">The time elapsed since the last update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            //Check if the missile is ally
            if (Camp == Side.Ally)
            {
                //The missile moves upward
                Position.y -= deltaT * Vitesse;

            }
            ////Check if the missile is Enemy
            else if (Camp == Side.Enemy)
            {
                //The missile moves downward
                Position.y += deltaT * Vitesse;
            }

            //Destroy the missile if it goeas out of bounds
            if(Camp == Side.Ally && Position.y < -Image.Height || Camp == Side.Enemy && Position.y > gameInstance.gameSize.Height)
            {
                Lives = 0;
            }
            
            //Checks for collisions with other game objects
            foreach (GameObject gameObject in gameInstance.gameObjects.ToList())
            {
                // Avoid collision with itself
                if (gameObject != this) 
                {
                    gameObject.Collision(this);
                }
            }
        }

        /// <summary>
        /// Handles the collisions between two missiles by setting their lives to zero (the missiles destroy each other).
        /// </summary>
        /// <param name="m">The other missile involved in the collision</param>
        /// <param name="numberOfPixelsInCollision">The number of pixels in collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
           Lives = 0;
           m.Lives = 0;
        }
        #endregion
    }
}
