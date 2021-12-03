using System.Windows;

namespace Computer{

   public static class Monitor
   {
        public static double[] GetPrimaryMonitorCenterPosition(double WindowWidth, double WindowHeight)
        {

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double centerVertical = (screenHeight / 2) - (WindowHeight / 2);
            double centerHorizontal = (screenWidth / 2) - (WindowWidth / 2);

            double[] position = new double[2];
            position[0] = centerHorizontal;
            position[1] = centerVertical;
            return position;
          

        }
    }

   


}