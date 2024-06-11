using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the bunker in the SpaceInvaders game 
    /// </summary>
    internal class Bunker : SimpleObject
    {
        #region constructor
        /// <summary>
        /// Public constructor of the bunker
        /// </summary>
        /// <param name="position">The initial position of the bunker</param>
        public Bunker(Vecteur2D position) : base(position, 1, Properties.Resources.bunker,Side.Neutral)
        {
            // No specific initialization 
        }
        #endregion


        #region methods
        /// <summary>
        /// Update the state of th bunker in the game
        /// </summary>
        /// <param name="gameInstance">The current instance of the game</param>
        /// <param name="deltaT">The time passed since the last Update</param>

        public override void Update(Game gameInstance, double deltaT)
        {
            // The bunker has not a specification about the Update
        }

        /// <summary>
        /// Manage the collision between the missile and the bunker
        /// </summary>
        /// <param name="m">The missile the bunker collided with </param>
        /// <param name="numberOfPixelsInCollision">The number of pixels in Collision </param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            //The number of missile lives is decremented by the number of pixels in Collision
            m.Lives-=numberOfPixelsInCollision;
        }
        #endregion
    }
}
