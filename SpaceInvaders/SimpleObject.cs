using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Abstract class representing a simple object of the game
    /// class parent of SpaceShip,Missile,Bunker,PlayerSpaceShip
    /// </summary>
    internal abstract class SimpleObject : GameObject
    {
        #region properties
        /// <summary>
        /// The position of the object
        /// </summary>
        public Vecteur2D Position { get; set; }

        /// <summary>
        /// Number of lives of the object
        /// </summary>
        public int Lives { get; set; } 

        /// <summary>
        /// The image representing the object
        /// </summary>
        public Bitmap Image { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor for the SimpleObject
        /// </summary>
        /// <param name="position">The initial position of the simple object</param>
        /// <param name="lives">The initial lives of the simple object</param>
        /// <param name="image">The image of the simple object</param>
        /// <param name="camp">The side of the simple object </param>
        public SimpleObject(Vecteur2D position, int lives, Bitmap image, Side camp) : base(camp)
        {
            Position = position;
            Lives = lives;
            Image = image;
        }
        #endregion 

        #region methods
        /// <summary>
        /// Draw method to render the SimpleObject on the screen
        /// </summary>
        /// <param name="gameInstance">The current instance of the game being played</param>
        /// <param name="graphics">The graphics object used for rendering</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            //Check if the object is alive before drawing
            if (IsAlive()) 
            {
                //Convert the position to float for drawing
                float PositionX = (float)Position.x;
                float PositionY = (float)Position.y;

                //Draw the SimpleObject
                graphics.DrawImage(Image, PositionX, PositionY, Image.Width, Image.Height);
            }
            
        }

        /// <summary>
        /// Check if the SimpleObject is Alive based on number of lives
        /// </summary>
        /// <returns>True if the number of lives is strictly positive , false otherwise</returns>
        public override bool IsAlive()
        {
            
            if (Lives > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Handles collision with a missile
        /// </summary>
        /// <param name="m">The missile involved in the collision</param>
        public override void Collision(Missile m)
        {
            //Create rectangles defining missile and bunker areas
            Rectangle missileRect = new Rectangle((int)m.Position.x, (int)m.Position.y, m.Image.Width, m.Image.Height);
            Rectangle bunkerRect = new Rectangle((int)Position.x, (int)Position.y, Image.Width, Image.Height);
            //Checks for an intersection between the rectangles and if the entities are from the same side
            if (!missileRect.IntersectsWith(bunkerRect) || Camp==m.Camp)
            {
                return; // No intersection, No Collision possible
            }
            //Gets the number of colliding pixels and triggers the collision event
            int numberOfPixelsInCollision = PixelInCollision(m);
            OnCollision(m, numberOfPixelsInCollision);
        }

        /// <summary>
        /// Counts the number of pixel in collision between the current object and the missile
        /// </summary>
        /// <param name="m">The missile object to check for collision</param>
        /// <returns>The number of pixel in collision</returns>
        private int PixelInCollision(Missile m)
        {
            int numberOfPixelsInCollision = 0;
            for(int missileX = 0; missileX < m.Image.Width;missileX++)
            {
                for(int missileY = 0;missileY < m.Image.Height;missileY++)
                {
                    int bunkerX = (int)(m.Position.x - Position.x + missileX);
                    int bunkerY = (int)(m.Position.y - Position.y + missileY);
                    //Check if coordinates are within bunker boundaries and if pixel is colliding
                    if (IsWithinPixelBounds(bunkerX,bunkerY) && IsCollisionPixel(bunkerX, bunkerY))
                    {
                        //Handle the pixel in collision
                        HandleCollisionPixel(bunkerX, bunkerY);
                        //Increment number of pixel in collision
                        numberOfPixelsInCollision++;
                    }

                }
            }
            return numberOfPixelsInCollision;
        }

        /// <summary>
        /// Checks if the given coordinates are within the image bounds
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel</param>
        /// <param name="y">The y-coordinate of the pixel</param>
        /// <returns>True if the coordinates are within bounds , fals otherwise</returns>
        private bool IsWithinPixelBounds(int x, int y)
        {
            return x >=0 && x < Image.Width && y>=0 && y < Image.Height;
        }

        /// <summary>
        /// Checks if the given coordinate pixel is in collision
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel</param>
        /// <param name="y">The y-coordinate of the pixel</param>
        /// <returns>True if the pixel is in collision , false otherwise</returns>
        private bool IsCollisionPixel(int x, int y)
        {
            Color bunkerPixel = Image.GetPixel(x, y);
            return bunkerPixel == Color.FromArgb(255, 109, 199, 66);
        }

        /// <summary>
        /// Handles the pixel in collision by making it transparent
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel</param>
        /// <param name="y">The y-coordinate of the pixel</param>
        private void HandleCollisionPixel (int x, int y)
        {
            if (this is Bunker)
            {
                Image.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
            }

        }
        
        /// <summary>
        /// Abstract method to be implemented in subclass to handle collision actions
        /// </summary>
        /// <param name="m">The missile involved in the collision</param>
        /// <param name="numberOfPixelsInCollision">The number of pixel in collision</param>
        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);

        #endregion

    }
}