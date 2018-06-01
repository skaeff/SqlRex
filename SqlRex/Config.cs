using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRex
{
    public class Config
    {
        public static bool UseLargeFiles
        {
            get
            {
                return ConfigurationManager.AppSettings["large_files"]?.ToString() == "1";
            }
            set
            {
                //if (value)
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    if (config.AppSettings.Settings["large_files"] != null)
                    {
                        config.AppSettings.Settings["large_files"].Value = value ? "1" : "0";
                    }
                    else
                    {
                        config.AppSettings.Settings.Add("large_files", value ? "1" : "0");
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static bool RegexOnLoad
        {
            get
            {
                return ConfigurationManager.AppSettings["regex_on_load"]?.ToString() == "1";
            }
            set
            {
                //if (value)
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    if (config.AppSettings.Settings["regex_on_load"] != null)
                    {
                        config.AppSettings.Settings["regex_on_load"].Value = value ? "1" : "0";
                    }
                    else
                    {
                        config.AppSettings.Settings.Add("regex_on_load", value ? "1" : "0");
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static bool ReadOnlySql
        {
            get
            {
                return ConfigurationManager.AppSettings["readonly_sql"]?.ToString() == "1";
            }
            set
            {
                //if (value)
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    if (config.AppSettings.Settings["readonly_sql"] != null)
                    {
                        config.AppSettings.Settings["readonly_sql"].Value = value ? "1" : "0";
                    }
                    else
                    {
                        config.AppSettings.Settings.Add("readonly_sql", value ? "1" : "0");
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string Encoding
        {
            get
            {
                var enc = ConfigurationManager.AppSettings["encoding"]?.ToString();
                return string.IsNullOrEmpty(enc) ? "Auto" : enc;
            }
            set
            {
                //if (value)
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    if (config.AppSettings.Settings["encoding"] != null)
                    {
                        config.AppSettings.Settings["encoding"].Value = value;
                    }
                    else
                    {
                        config.AppSettings.Settings.Add("encoding", value);
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

    }
}
