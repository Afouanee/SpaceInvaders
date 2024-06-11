using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpaceInvaders
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new GameForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une exception s'est produite : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }









        }
    }
}
