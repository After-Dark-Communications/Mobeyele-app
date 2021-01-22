namespace Mobeye.Logic
    {
        public class LanguageHandler
        {
            public Language Language { get; set; }

            public void ChangeLanguage(Language lang)
            {
                Language = lang;
            }
        }

        public enum Language
        {
            English,
            Dutch,
            German
        }
    }
