using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the player ship in the SpaceInvaders game 
    /// </summary>
    internal class PlayerSpaceShip : SpaceShip
    {
        #region parameters
        /// <summary>
        /// This is the player's movement speed 
        /// </summary>
        private readonly double speedPixelPerSecond;
        #endregion

        #region constructor
        /// <summary>
        /// public constructor of PlayerSpaceShip class
        /// Initialize a new instance of PlayerSpaceShip class with specified position, lives and image
        /// </summary>
        /// <param name = "position"> The initial position of the player's spaceShip</param>
        /// <param name="lives"> The number of lives the player's spaceShip starts with</param>
        /// <param name="image"> The image representing the player's spaceShip</param>  
        public PlayerSpaceShip(Vecteur2D position, int lives, Bitmap image) : base(position, lives, image,Side.Ally)
        {
            speedPixelPerSecond = 85;
        }
        #endregion

        #region methods
        /// <summary>
        ///Handle the displacement of PlayerSpaceShip
        /// </summary>
        /// <param name = "gameInstance">instance of current object</param> 
        /// <param name = "deltaT">time ellapsed in seconds since last call to Update</param> 
        public override void Update(Game gameInstance, double deltaT)
        {
            //Check if the left arrow key is pressed and the PlayerSpaceShip is not in the left edge
            if ((gameInstance.keyPressed.Contains(Keys.Left)) && (Position.x > 0))
            {
                //Move the PlayerSpaceShip to the left without going out of the game area on the left
                Position.x -= (speedPixelPerSecond * deltaT);

                //Check if the space key is pressed
                if (gameInstance.keyPressed.Contains(Keys.Space))
                {
                    //Call the Shoot methode to fire a missile from de PlayerSpaceShip
                    Shoot(gameInstance);

                }
            }
            //Check if the right arrow key is pressed and the PlayerSpaceShip is not in the left edge
            else if ((gameInstance.keyPressed.Contains(Keys.Right)) && (Position.x < gameInstance.gameSize.Width - Image.Width))
            {
                //Move the PlayerSpaceShip to the right without going out of the game area on the right
                Position.x += (speedPixelPerSecond * deltaT);

                //Check if the space key is pressed
                if (gameInstance.keyPressed.Contains(Keys.Space))
                    {
                        //Call the Shoot methode to fire a missile from de PlayerSpaceShip
                        Shoot(gameInstance);
                    }
            }
            //Check if the space key is pressed
            else if (gameInstance.keyPressed.Contains(Keys.Space))
            {
                //Call the Shoot methode to fire a missile from de PlayerSpaceShip
                Shoot(gameInstance);

            }
           
        }
        

       
        /// <summary>
        /// Draw the PlayerSpaceShip on its position
        /// </summary>
        /// <param name="gameInstance">instance of current object</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            //Call the Draw method of the SpaceShip parent class to draw the PlayerSpaceShip
            base.Draw(gameInstance, graphics);

            //Display the number of lives of the PlayerSpaceShip
            string nombreVie = " Vie : " + Lives.ToString();

            //Set the font and brush to draw the number of lives
            Brush blackbrush = new SolidBrush(Color.GreenYellow);
            Font defaultFont = new Font("Agency fb", 34, FontStyle.Bold, GraphicsUnit.Pixel);
            //Set the position to show number of lives 
            float nombreVieX = 30;
            float nombreVieY = (float)gameInstance.gameSize.Height - 50;
            PointF point = new PointF(nombreVieX, nombreVieY);

            //Draw the number of lives on the screen
            graphics.DrawString(nombreVie, defaultFont, blackbrush, point);
           
        }

        public void handlekeyinput(Keys key , double deltaT)
        {
            if (( key == Keys.Left) && (Position.x > 0))
            {
                //Move the PlayerSpaceShip to the left without going out of the game area on the left
                Position.x -= (speedPixelPerSecond * deltaT);
            }
        }
        #endregion

    }
}
