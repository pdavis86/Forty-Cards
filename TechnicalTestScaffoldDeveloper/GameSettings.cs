using System;
using System.Configuration;

namespace TechnicalTestScaffoldDeveloper
{
    public class GameSettings
    {
        private static GameSettings _instance;

        private GameSettings() { }

        public static GameSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSettings();
                    _instance.LoadSettings();
                }
                return _instance;
            }
        }

        private static int LoadIntSetting(System.Collections.Specialized.NameValueCollection settings, string settingName)
        {
            int parsedInt;
            if (!int.TryParse(settings[settingName], out parsedInt))
            {
                throw new Exception($"Invalid value presemt for '{settingName}' setting");
            }
            return parsedInt;
        }

        public void LoadSettings()
        {
            var appSettings = ConfigurationManager.AppSettings;
            _firstCard = LoadIntSetting(appSettings, "firstCard");
            _lastCard = LoadIntSetting(appSettings, "lastCard");
            _repeatLimit = LoadIntSetting(appSettings, "repeatLimit");
        }

        private int _firstCard;
        public int FirstCard
        {
            get { return _firstCard; }
        }

        private int _lastCard;
        public int LastCard
        {
            get { return _lastCard; }
        }

        private int _repeatLimit;
        public int RepeatLimit
        {
            get { return _repeatLimit; }
        }

    }
}
