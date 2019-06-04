using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idocNet.Core.Configuration;

namespace idocNet.Core.Localization
{
    public class Localizer
    {
        #region Static Methods
        private static object lockCurrentLoad = new object();
        private static Dictionary<string, Localizer> _current;
        private static Localizer _currents;
        public static Localizer GetCurrent(string cultureName)
        {
            cultureName = cultureName.ToLower().Trim();
            if (_current == null || !_current.ContainsKey(cultureName))
            {
                lock (lockCurrentLoad)
                {
                    SetCulture(cultureName, SiteConfiguration.Current.LocalizationFilePath(cultureName));
                }
            }
            return _current[cultureName];
        }

        //public static Localizer Current
        //{
        //    get
        //    {
        //        if (_currents == null)
        //        {
        //            lock (lockCurrentLoad)
        //            {
        //                var cultureName = SiteConfiguration.Current.CultureName;
        //                SetCulture(cultureName, SiteConfiguration.Current.LocalizationFullPath + cultureName.ToLowerInvariant() + ".po", "");
        //            }
        //        }
        //        return _currents;
        //    }
        //}

        /// <summary>
        /// Loads the translations of a culture and sets as current
        /// </summary>
        /// <param name="cultureName"></param>
        //public static void SetCulture(string cultureName, string filePath, string path="")
        //{
        //    var manager = new Localizer(cultureName, filePath);
        //    manager.LoadCulture();

        //    _currents = manager;
        //}

        /// <summary>
        /// Loads the translations of a culture and sets as current
        /// </summary>
        /// <param name="cultureName"></param>
        public static void SetCulture(string cultureName, string filePath)
        {
            if (_current == null) _current = new Dictionary<string, Localizer>();
            var manager = new Localizer(cultureName, filePath);
            manager.LoadCulture();

            if (_current.ContainsKey(cultureName)) _current.Remove(cultureName);
            _current.Add(cultureName, manager);
        }


        #endregion



        private Dictionary<string, string> _translations;

        /// <summary>
        /// Gets or sets the name of the culture.
        /// </summary>
        public string CultureName
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }

        public Localizer()
        {
            _translations = new Dictionary<string, string>();
        }

        public Localizer(string cultureName, string filePath)
            : this()
        {
            CultureName = cultureName;
            FilePath = filePath;
        }

        public void LoadCulture()
        {
            if (FilePath == null)
            {
                throw new NullReferenceException("FilePath can not be null");
            }
            _translations = LocalizationParser.ParseFile(FilePath);
        }

        public virtual string Get(string neutralValue)
        {
            if (neutralValue == null)
            {
                throw new ArgumentNullException("neutralValue");
            }
            if (_translations.ContainsKey(neutralValue) && !String.IsNullOrEmpty(_translations[neutralValue]))
            {
                return _translations[neutralValue];
            }
            return neutralValue;
        }

        public virtual string Get(string neutralValue, params object[] args)
        {
            return String.Format(Get(neutralValue), args);
        }

        public virtual string this[string neutralValue]
        {
            get
            {
                return Get(neutralValue);
            }
        }

        /// <summary>
        /// Gets the number of loaded translations
        /// </summary>
        public virtual int Count
        {
            get
            {
                return _translations.Count;
            }
        }
    }
}
