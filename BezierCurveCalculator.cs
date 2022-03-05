using System;
using System.Collections.Generic;
using System.Drawing;

namespace BezierCurveCalculator
{
    /*
    This class calculates Bezier curves based on a set of given points. The class contains several internal private functions,
    but the primary function intended for external use is calculateBezierCurve.
    */
    public class BezierCurveCalculator
    {
        /*
        This internal function takes in a given set of two points (PointF's) as well as the degree of accuracy we want 
        (that is, the number of output points to calculate), and calculates the bezier curve for those given points.
        Returns a list of numPointsToCalulate number of points, which is, in essence, our Bezier curve.
        */
        private List<PointF> calculate_bezier_curve_with_two_control_points(List<PointF> givenPoints, int numPointsToCalulate)
        {            
            float P1_x = givenPoints[0].X;
            float P1_y = givenPoints[0].Y;
            float P2_x = givenPoints[1].X;
            float P2_y = givenPoints[1].Y;
            float amountToIncrement = 1 / numPointsToCalulate;
            List<PointF> bezierCurveToReturn = new List<PointF>();            

            for(int t_counter = 0; t_counter < numPointsToCalulate; t_counter++)
            {
                float t = t_counter * amountToIncrement;
                float xAnalysis = (1 - t) * P1_x + t * P2_x;
                float yAnalysis = (1 - t) * P1_y + t * P2_y;

                bezierCurveToReturn.Add(new PointF(xAnalysis, yAnalysis));
            }

            return bezierCurveToReturn;
        }

        /*
        This internal function takes in a given set of three points (PointF's) as well as the degree of accuracy we want 
        (that is, the number of output points to calculate), and calculates the bezier curve for those given points.
        Returns a list of numPointsToCalulate number of points, which is, in essence, our Bezier curve.
        */
        private List<PointF> calculate_bezier_curve_with_three_control_points(List<PointF> givenPoints, int numPointsToCalulate)
        {
            float P1_x = givenPoints[0].X;
            float P1_y = givenPoints[0].Y;
            float P2_x = givenPoints[1].X;
            float P2_y = givenPoints[1].Y;
            float P3_x = givenPoints[2].X;
            float P3_y = givenPoints[2].Y;
            float amountToIncrement = 1 / numPointsToCalulate;
            List<PointF> bezierCurveToReturn = new List<PointF>();

            for (int t_counter = 0; t_counter < numPointsToCalulate; t_counter++)
            {
                float t = t_counter * amountToIncrement;
                float one_minus_t = 1 - t;
                float xAnalysis = one_minus_t * one_minus_t * P1_x + 2 * one_minus_t * t * P2_x + t * t * P3_x;
                float yAnalysis = one_minus_t * one_minus_t * P1_y + 2 * one_minus_t * t * P2_y + t * t * P3_y;

                bezierCurveToReturn.Add(new PointF(xAnalysis, yAnalysis));
            }

            return bezierCurveToReturn;
        }

        /*
        This internal function takes in a given set of four points (PointF's) as well as the degree of accuracy we want 
        (that is, the number of output points to calculate), and calculates the bezier curve for those given points.
        Returns a list of numPointsToCalulate number of points, which is, in essence, our Bezier curve.
        */
        private List<PointF> calculate_bezier_curve_with_four_control_points(List<PointF> givenPoints, int numPointsToCalulate)
        {
            float P1_x = givenPoints[0].X;
            float P1_y = givenPoints[0].Y;
            float P2_x = givenPoints[1].X;
            float P2_y = givenPoints[1].Y;
            float P3_x = givenPoints[2].X;
            float P3_y = givenPoints[2].Y;
            float P4_x = givenPoints[3].X;
            float P4_y = givenPoints[3].Y;
            float amountToIncrement = 1 / numPointsToCalulate;
            List<PointF> bezierCurveToReturn = new List<PointF>();

            for (int t_counter = 0; t_counter < numPointsToCalulate; t_counter++)
            {
                float t = t_counter * amountToIncrement;
                float one_minus_t = 1 - t;
                float xAnalysis = one_minus_t * one_minus_t * one_minus_t * P1_x + 3 * one_minus_t * one_minus_t * t * P2_x + 3 *one_minus_t * t * t * P3_x + t * t * t * P4_x;
                float yAnalysis = one_minus_t * one_minus_t * one_minus_t * P1_y + 3 * one_minus_t * one_minus_t * t * P2_y + 3 * one_minus_t * t * t * P3_y + t * t * t * P4_y;

                bezierCurveToReturn.Add(new PointF(xAnalysis, yAnalysis));
            }

            return bezierCurveToReturn;
        }


        /*
        This function takes in a given set points (PointF's) as well as the degree of accuracy we want (that is, the number of output points to calculate),
        and calculates the bezier curve for those given points. The function first determines what calculations need to be made based off of how many control
        points are given; then calculates accordingly. Returns a list of numPointsToCalulate number of points, which is, in essence, our Bezier curve.
        */
        public List<PointF> calculateBezierCurve(List<PointF> givenPoints, int numPointsToCalulate)
        {
            //First, determine if we need to calculate multiple Bezier curves and put them together, or if we can just return one of our internal functions:

            int numControlPoints = givenPoints.Count;

            //We cannot make a bezier curve out of fewer than two points; therefore return null if an improper number of points has been given.
            if(numControlPoints < 2)
            {
                return null;
            }
            //Two control points.
            else if (numControlPoints == 2)
            {
                return calculate_bezier_curve_with_two_control_points(givenPoints, numPointsToCalulate);
            }
            //Three control points.
            else if (numControlPoints == 3)
            {
                return calculate_bezier_curve_with_three_control_points(givenPoints, numPointsToCalulate);
            }
            //Four control points.
            else if (numControlPoints == 4)
            {
                return calculate_bezier_curve_with_four_control_points(givenPoints, numPointsToCalulate);
            }

            //Else if there are more than four control points, we have to calculate multiple Bezier curves and attach them together:

            //First, divide the given control points into proper sets.
            List<List<PointF>> setsOfControlPoints = new List<List<PointF>>();
            for(int i = 0; i < numControlPoints; i += 3)
            {
                List<PointF> controlPointsSet = new List<PointF>();
                for (int j = i; j <= i+3 && j < numControlPoints; j++)
                {
                    controlPointsSet.Add(givenPoints[j]);
                }
                setsOfControlPoints.Add(controlPointsSet);
            }
            int distributedNumPointsToCalulate = (int)MathF.Ceiling(numPointsToCalulate / setsOfControlPoints.Count);

            //Now, analyze each set, and combine all the control points together.            
            List<PointF> bezierCurveToReturn = new List<PointF>();
            foreach (List<PointF> controlPointsSet in setsOfControlPoints)
            {
                switch (controlPointsSet.Count)
                {
                    case 2:
                        bezierCurveToReturn.AddRange(calculate_bezier_curve_with_two_control_points(controlPointsSet, distributedNumPointsToCalulate));
                        break;
                    case 3:
                        bezierCurveToReturn.AddRange(calculate_bezier_curve_with_three_control_points(controlPointsSet, distributedNumPointsToCalulate));
                        break;
                    case 4:
                        bezierCurveToReturn.AddRange(calculate_bezier_curve_with_four_control_points(controlPointsSet, distributedNumPointsToCalulate));
                        break;                                           
                }
            }

            //Finally, return the combined Bezier curves.
            return bezierCurveToReturn;
        }
    }
}
