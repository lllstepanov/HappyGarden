using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace HappyGarden
{
    internal class LocalizationManager : EternalSingleton<LocalizationManager>
    {
        private List<string> languages = new List<string>(new string[] { "English", "Russian" });

        private List<LocalizationString> localization = new List<LocalizationString>();

        [SerializeField] private string currentLanguageLong = "English";
        [SerializeField] private string currentLanguageShort;

        [SerializeField] private bool oneLanguage;

        private void Start()
        {
            if (ProfileManager.Instance.GetLocalization() != "")
            {
                currentLanguageShort = ProfileManager.Instance.GetLocalization();
                currentLanguageLong = GetLanguageLongById(currentLanguageShort);
                ChangeLocalization(currentLanguageShort);
            }
            else
            {
                currentLanguageLong = Application.systemLanguage.ToString();

                currentLanguageShort = GetLanguageShort(currentLanguageLong);

                ChangeLocalization(currentLanguageShort);
            }

            if (oneLanguage)
            {
                ChangeLocalization("en");
            }

            Debug.Log($"Current Language is {currentLanguageLong}");
        }

        private void ChangeLocalization(string value)
        {
            LocalizationParser parser = new LocalizationParser();
            localization = parser.ParseLocalization(value);

            StartCoroutine(WaitAndChange());
        }

        IEnumerator WaitAndChange()
        {
            yield return new WaitForSeconds(0.2f);
            UpdateAllObjects();

            ProfileManager.Instance.SetLocalization(currentLanguageShort);
        }

        internal string GetLanguageShort()
        {
            return currentLanguageShort;
        }

        internal string GetLanguageLong()
        {
            return currentLanguageLong;
        }

        internal string GetString(string value)
        {
            if (localization.Count > 0)
            {
                LocalizationString tempString = localization.Find(x => x.placement == value);
                return tempString.value;
            }

            return "";
        }

        internal void NextLanguage()
        {
            int index = languages.IndexOf(currentLanguageLong);

            if (index != languages.Count - 1)
            {
                index++;
                currentLanguageLong = languages[index];
            }
            else
            {
                currentLanguageLong = languages[0];
            }

            currentLanguageShort = GetLanguageShort(currentLanguageLong);

            ChangeLocalization(currentLanguageShort);
        }

        internal void PreviousLanguage()
        {
            int index = languages.IndexOf(currentLanguageLong);

            if (index != 0)
            {
                index--;
                currentLanguageLong = languages[index];
            }
            else
            {
                currentLanguageLong = languages[languages.Count - 1];
            }

            currentLanguageShort = GetLanguageShort(currentLanguageLong);
            ChangeLocalization(currentLanguageShort);
        }

        private string GetLanguageShort(string value)
        {
            string result = "en";

            if (value == "German")
            {
                result = "en";
            }
            else if (value == "English")
            {
                result = "en";
            }
            else if (value == "Spanish")
            {
                result = "en";
            }
            else if (value == "French")
            {
                result = "en";
            }
            else if (value == "Italian")
            {
                result = "en";
            }
            else if (value == "Japanese")
            {
                result = "en";
            }
            else if (value == "Korean")
            {
                result = "en";
            }
            else if (value == "Portuguese")
            {
                result = "en";
            }
            else if (value == "Russian")
            {
                result = "ru";
            }
            else if (value == "Ukrainian")
            {
                result = "en";
            }
            else if (value == "Chinese (Simplified)")
            {
                result = "en";
            }
            else
            {
                result = "en";
            }

            return result;
        }

        private string GetLanguageLongById(string value)
        {
            string result = "English";

            if (value == "en")
            {
                result = "English";
            }
            else if (value == "ru")
            {
                result = "Russian";
            }
            else
            {
                result = "English";
            }

            return result;
        }

        internal string GetLanguageName()
        {
            string result = "English";

            if (currentLanguageShort == "en")
            {
                result = "English";
            }
            else if (currentLanguageShort == "ru")
            {
                result = "Русский";
            }
            else
            {
                result = "English";
            }

            return result;
        }

        private void UpdateAllObjects()
        {
            LocalizatedObject[] localizatedObjects = FindObjectsOfType(typeof(LocalizatedObject)) as LocalizatedObject[];
            foreach (LocalizatedObject localizatedObject in localizatedObjects)
            {
                localizatedObject.UpdateLocalization();
            }
        }

    }

    internal class LocalizationParser
    {
        List<LocalizationString> tempCollection = new List<LocalizationString>();
        XmlDocument xml;

        public List<LocalizationString> ParseLocalization(string language)
        {
            TextAsset localizationXML = (TextAsset)Resources.Load("XML/Localization/" + language, typeof(TextAsset));

            xml = new XmlDocument();
            xml.LoadXml(localizationXML.text);

            foreach (XmlNode xmlLZ in xml.ChildNodes)
            {
                if (xmlLZ.Name == "localization")
                {
                    foreach (XmlNode xmlArea in xmlLZ.ChildNodes)
                    {
                        if (xmlArea.Name == "string")
                        {
                            string placement = xmlArea.Attributes["placement"].Value;
                            string value = xmlArea.Attributes["value"].Value;

                            LocalizationString singleString = new LocalizationString();
                            singleString.CreateItem(placement, value);

                            tempCollection.Add(singleString);
                        }
                    }
                }
            }

            return tempCollection;
        }
    }

    internal class LocalizationString
    {
        internal string placement;
        internal string value;

        internal void CreateItem(string placement, string value)
        {
            this.placement = placement;
            this.value = value;
        }
    }
}

