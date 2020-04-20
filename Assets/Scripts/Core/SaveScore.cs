using System;
using System.IO;
using UnityEngine;

namespace SaveData
{
    public static class Save
    {
        public static void All(float playTime, Rating playerRating)
        {
            string dirName = DateTime.Today.ToShortDateString();
            dirName.Replace(".", "_");

            string dirPath = Application.dataPath + "/" + dirName;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = DateTime.Now.ToString();
            fileName.Replace(".", "_");

            string contents = string.Format("PlayTime: {0} {1} AveragePlayerRating: {2}", playTime, Environment.NewLine, playerRating);

            File.WriteAllText(dirPath + "/" + fileName + ".txt", contents);
        }
    }
}
