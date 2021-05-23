using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindingNationalityGame.Libraries
{
    public static class Calculations
    {
        private static int totalScore;

        public static int TotalScore
        {
            get { return totalScore; }
            set
            {
                totalScore = value;
            }
        }

        public static Point[] GetPoints(Point point1, Point point2, int quantity)
        {
            var points = new Point[quantity];
            int ydiff = point2.Y - point1.Y, xdiff = point2.X - point1.X;
            double slope = (double)(point2.Y - point1.Y) / (point2.X - point1.X);
            double x, y;

            --quantity;

            for (double i = 0; i < quantity; i++)
            {
                y = slope == 0 ? 0 : ydiff * (i / quantity);
                x = slope == 0 ? xdiff * (i / quantity) : y / slope;
                points[(int)i] = new Point((int)Math.Round(x) + point1.X, (int)Math.Round(y) + point1.Y);
            }

            points[quantity] = point2;
            return points;
        }

        public static double GetDistanceBetweenPoints(Point point1, Point point2)
        {
            return Math.Sqrt((Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2)));
        }

        public static Point GetCenterPointOfObject(System.Windows.Forms.PictureBox pictureBox)
        {
            Point retPoint = new Point();
            retPoint.X = pictureBox.Location.X + (pictureBox.Width / 2);
            retPoint.Y = pictureBox.Location.Y + (pictureBox.Height / 2);

            return retPoint;
        }


    }
}