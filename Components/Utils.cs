using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace IWStar.DNN.Modules.IWStarGallery.Components
{
    public class Utils
    {
        public const string SETTINGS_MAX_WIDTH = "MAX_WIDTH";
        public const string SETTINGS_MAX_HEIGHT = "MAX_HEIGHT";
        public const string SETTINGS_SHOW_INDICATOR = "SHOW_INDICATOR";
        public const string SETTINGS_SHOW_THUMBNAIL = "SHOW_THUMBNAIL";
        public const string SETTINGS_SHOW_TITLE = "SHOW_TITLE";
        public const string SETTINGS_SHOW_DESCRIPTION = "SHOW_DESCRIPTION";
        public const string SETTINGS_SHOW_POPUP = "SHOW_POPUP";
        public const string SETTINGS_SHOW_POPUP_TITLE = "SHOW_POPUP_TITLE";
        public const string SETTINGS_SHOW_POPUP_DESCRIPTION = "SHOW_POPUP_DESCRIPTION";

        public const int DEFAULT_MAX_WIDTH = 990;
        public const int DEFAULT_MAX_HEIGHT = 300;
        public const bool DEFAULT_SHOW_INDICATOR = true;
        public const bool DEFAULT_SHOW_THUMBNAIL = true;
        public const bool DEFAULT_SHOW_TITLE = true;
        public const bool DEFAULT_SHOW_DESCRIPTION = true;
        public const bool DEFAULT_SHOW_POPUP = true;
        public const bool DEFAULT_SHOW_POPUP_TITLE = true;
        public const bool DEFAULT_SHOW_POPUP_DESCRIPTION = true;

        public static void GenerateThumbnailImage(int size, string filePath, string saveFilePath)
        {
            float newHeight = 0;
            float newWidth = 0;
            float scale = 0;
            Bitmap curImage = new Bitmap(filePath);

            if (curImage.Height > curImage.Width)
            {
                scale = size / Convert.ToSingle(curImage.Height);
            }
            else
            {
                scale = size / Convert.ToSingle(curImage.Width);
            }

            if (scale < 0 || scale > 1)
            {
                scale = 1;
            }

            newHeight = Convert.ToSingle(curImage.Height) * scale;
            newWidth = Convert.ToSingle(curImage.Width) * scale;

            Bitmap newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
            Graphics imgDest = Graphics.FromImage(newImage);
            imgDest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            imgDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            imgDest.DrawImage(curImage, 0, 0, newImage.Width, newImage.Height);
            newImage.Save(saveFilePath, curImage.RawFormat);
            curImage.Dispose();
            newImage.Dispose();
            imgDest.Dispose();
        }

        public static int CInt(object obj)
        {
            if (obj == null)
            { 
                return 0; 
            }
            int newVal = 0;
            Int32.TryParse(obj.ToString().Trim(), out newVal);
            return newVal;
        }

        public static bool CBool(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            string str = obj.ToString().Trim().ToLower();
            if (str == "true" || str == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}