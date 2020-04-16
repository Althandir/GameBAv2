using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Language
{
    public enum Lang
    {
        en, de
    }

    [System.Serializable]
    public class Container
    {
        public string MainMenuTitle = string.Empty;
        public string MainMenuSubtitle = string.Empty;

        public string PlayButtonText = string.Empty;
        public string TutorialButtonText = string.Empty;
        public string SettingsButtonText = string.Empty;
        public string ExitButtonText = string.Empty;

        public string SettingsOrderText = string.Empty;
        public string SettingsRatDelayText = string.Empty;

        public List<string> zeroStarText = new List<string>();
        public List<string> oneStarText = new List<string>();
        public List<string> twoStarText = new List<string>();
        public List<string> threeStarText = new List<string>();
        public List<string> fourStarText = new List<string>();
        public List<string> fiveStarText = new List<string>();

        public string EndOfGameText = string.Empty;

        public Container()
        {
            zeroStarText.Add("example1");
            zeroStarText.Add("example2");
        }
    }

    

}
