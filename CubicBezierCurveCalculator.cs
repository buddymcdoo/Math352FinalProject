using System;
using System.Collections.Generic;
using System.Drawing;

namespace CubicBezierCurveCalculator
{
    /*
    This class calculates Cubic Bezier Curves based on a given set of four (x,y) coordinate points. 
    */
    public class CubicBezierCurveCalculator
    {
        /*
        This function takes in a given set of four points (PointD's) as well as the degree of accuracy we want 
        (that is, the number of output points to calculate), and calculates the bezier curve for those given points.
        Returns a list of numPointsToCalulate number of points, which is, in essence, our Bezier curve.
        */
        public List<PointF> CalculateCubicBezierCurve(PointF[] givenPoints, int numPointsToCalulate)
        {
            //More info on this algorithm can be found at https://www.geeksforgeeks.org/cubic-bezier-curve-implementation-in-c/

            float x_zero = givenPoints[0].X;
            float y_zero = givenPoints[0].Y;
            float x_one = givenPoints[1].X;
            float y_one = givenPoints[1].Y;
            float x_two = givenPoints[2].X;
            float y_two = givenPoints[2].Y;
            float x_three = givenPoints[3].X;
            float y_three = givenPoints[3].Y;

            float amountToIncrement = 1 / numPointsToCalulate;
            List<PointF> bezierPointCalculations = new List<PointF>();

            for (int u = 0; u < numPointsToCalulate; u++)
            {
                float current_u = u * amountToIncrement;
                float one_minus_current_u = 1 - current_u;
                float xAnalysis = MathF.Pow(one_minus_current_u, 3) * x_zero + 3 * current_u * MathF.Pow(one_minus_current_u, 2) * x_one + 3 * one_minus_current_u * MathF.Pow(current_u, 2) * x_two + MathF.Pow(current_u, 3) * x_three;
                float yAnalysis = MathF.Pow(one_minus_current_u, 3) * y_zero + 3 * current_u * MathF.Pow(one_minus_current_u, 2) * y_one + 3 * one_minus_current_u * MathF.Pow(current_u, 2) * y_two + MathF.Pow(current_u, 3) * y_three;

                bezierPointCalculations.Add(new PointF(xAnalysis, yAnalysis));
            }

            return bezierPointCalculations;
        }
    }
}
