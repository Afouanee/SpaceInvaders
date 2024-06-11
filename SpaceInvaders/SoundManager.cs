using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    /// <summary>
    /// SoundManager class handles sound-related functionalities in the SpaceInvaders game    
    /// </summary>
    public class SoundManager
    {
        #region Fields
        /// <summary>
        /// Insatance of SoundManager
        /// </summary>
        private static SoundManager instance;

        /// <summary>
        /// SoundPlayer for background Music
        /// </summary>
        private SoundPlayer backgroundMusic;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the SoundManager class
        /// </summary>
        private SoundManager()
        {
            backgroundMusic = new SoundPlayer(Properties.Resources.main);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance of SoundManager class
        /// </summary>
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }
                return instance;
            }
        }
        #endregion
        #region methods
        /// <summary>
        /// Plays the background music in a loop
        /// </summary>
        public void PlayBackgroundMusic()
        {
            backgroundMusic.PlayLooping();
        }

        /// <summary>
        /// Stops the background music
        /// </summary>
        public void StopBackgroundMusic()
        {
            backgroundMusic.Stop();
        }

        /// <summary>
        /// Stop and release sound ressources
        /// </summary>
        public void Dispose()
        {
            backgroundMusic.Stop();
            backgroundMusic.Dispose();
        }
        #endregion
    }
}
