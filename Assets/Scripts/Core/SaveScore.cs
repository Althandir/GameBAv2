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
            dirName = dirName.Replace(".", "_");

            string dirPath = Application.persistentDataPath + "/" + dirName;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = DateTime.Now.ToString();
            fileName = fileName.Substring(11);
            fileName = fileName.Replace(":", "_"); // FileSystem doesn't like :
            string contents = string.Format("PlayTime: {0} {1}AveragePlayerRating: {2}", playTime, Environment.NewLine, playerRating);

            string filePath = dirPath + "/" + fileName + ".txt";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, contents);
            }
        }
    }
}
