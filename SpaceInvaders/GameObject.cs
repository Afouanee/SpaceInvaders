﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Enum representing the side of a game entity
    /// </summary>
    enum Side
    {
        Ally,
        Enemy,
        Neutral,
    }

    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {
        /// <summary>
        /// Property to get or set the side of the game object
        /// </summary>
        public Side Camp { get; set; }

        /// <summary>
        /// Constructor to initialize the game object with a specified side
        /// </summary>
        /// <param name="camp"></param>
        public GameObject(Side camp)
        {
            Camp = camp;

        }

        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public abstract void Draw(Game gameInstance, Graphics graphics);

        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();


        /// <summary>
        /// Handles collision with a missile
        /// </summary>
        /// <param name="missile">missile object that collied with this game object</param>
        public abstract void Collision(Missile missile); 


    }
}
