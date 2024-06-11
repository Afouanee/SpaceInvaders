using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents a two- dimensionel vector,
    /// This class facilitates the management of coordinate and movement
    /// </summary>
    internal class Vecteur2D
    {
        /// <summary>
        /// The x coordinate of the vector
        /// </summary>
        public double x;

        /// <summary>
        /// The y coordinate of the vector
        /// </summary>
        public double y;

        /// <summary>
        /// A public parametric constructor to initialize the values of x and y
        /// </summary>
        /// <param name="x">The initial value of the x-coordinate (default is 0 ) </param>
        /// <param name="y">The initial value of the y-coordinate (default is 0 )</param>
        public Vecteur2D(double x = 0,double y=0)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// A public property read-only double type norm 
        /// </summary>
        /// <returns> norm of vector </returns>
        public double Norme
        {
            get 
            {
                return Math.Sqrt((x * x) + (y * y));
            }
        }

        /// <summary>
        /// Redefinition of operators
        /// </summary>

        /// <summary>
        /// 1.Vector addition
        /// </summary>
        /// <param name="v1">The first vector to add </param>
        /// <param name="v2">The second vector to add </param>
        /// <returns> The result of vector addition</returns>
        public static Vecteur2D operator+(Vecteur2D v1 , Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// 2.Vector Substraction
        /// </summary>
        /// <param name="v1">The first vector to subtract </param>
        /// <param name="v2">The second vector to substract </param>
        /// <returns> The result of vector substraction </returns>
        public static Vecteur2D operator-(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y- v2.y);
        }

        /// <summary>
        /// 3.Unary minus
        /// </summary>
        /// <param name="v">The vector to negate </param>
        /// <returns> The negated vector </returns>
        public static Vecteur2D operator-(Vecteur2D v)
        {
            return new Vecteur2D(-v.x , -v.y);
        }

        /// <summary>
        /// 4.Multiplication by a scalar on the right
        /// </summary>
        /// <param name="v">The vector to multiply </param>
        /// <param name="k">The scalar with which we multiply the vector</param>
        /// <returns> The result of the right vector scalar multiplication </returns>
        public static Vecteur2D operator*(Vecteur2D v,double k)
        {
            return new Vecteur2D(v.x * k, v.y * k);
        }


        /// <summary>
        /// 5.Multiplication by a scalar on the left
        /// </summary>
        /// <param name="k">The scalar with which we multiply the vector</param>
        /// <param name="v">The vector to multiply </param>
        /// <returns> The result of the left vector scalar multiplication </returns>
        public static Vecteur2D operator*(double k,Vecteur2D v)
        {
            return new Vecteur2D(k * v.x, k * v.y);
        }

        /// <summary>
        /// 6.Division by scalar
        /// </summary>
        /// <param name="v">The vector to multiply </param>
        /// <param name="d">The scalar by which wedivide the vector</param>
        /// <returns> The result of vector scalar division </returns>
        /// <exception cref="DivideByZeroException"> Thrown when trying to divide by zero</exception> 
        public static Vecteur2D operator /(Vecteur2D v , double d)
        {
            // check if the divisor is zero
            if (d == 0)
            {
                // thrown an exception
                throw new DivideByZeroException("Erreur : Division par zéro est impossible ");
            }
            // Calculate and return The result of vector scalar division
            return new Vecteur2D(v.x / d, v.y / d);
        }


    }
}
