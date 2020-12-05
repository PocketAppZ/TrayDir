﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace TrayDir
{
    class Option
    {
        public string name;
        public string sValue;
        public bool bValue
        {
            get { return sValue == "1"; }
            set { sValue = value ? "1" : "0"; }
        }
        public Option(string name, string value)
        {
            this.name = name;
            sValue = value;
        }
        public Option(string name, bool value) : this(name, value ? "1" : "0") { }
    }
    class Settings
    {
        public static string config = "config.xml";
        public static string iconPath = "";
        public static string iconText = "TrayDir";

        private static Dictionary<string, Option> options;
        public static List<string> paths;
        private static bool _altered;
        public static void Init()
        {
            options = new Dictionary<string, Option>();
            setOptionBool("RunAsAdmin", false);
            setOptionBool("ShowFileExtensions", true);
            setOptionBool("MinimizeOnClose", true);
            setOptionBool("StartMinimized", false);
            setOptionBool("ExploreFoldersInTrayMenu", false);
            paths = new List<string>();
            paths.Add(".");
            Load();
            _altered = false;
        }
        public static void Load()
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try
            {
                doc.Load(config);
                XmlElement root = doc.DocumentElement;
                LoadOptions(root);
                LoadPaths(root);
                LoadAppConfig(root);
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error Loading Config:" + e.Message);
            }
        }
        private static void LoadOptions(XmlElement root)
        {
            try
            {
                XmlElement options = root.GetElementsByTagName("options")[0] as XmlElement;
                foreach (XmlElement option in options.GetElementsByTagName("option"))
                {
                    try
                    {
                        string name = option.GetAttribute("Name");
                        bool value = StrToBool(option.GetAttribute("Value"));
                        Option o = new Option(name, value);
                        Settings.options[name] = o;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error Parsing Option: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error Parsing Options: " + e.Message);
            }
        }
        private static void LoadPaths(XmlElement root)
        {
            try
            {
                XmlElement paths = root.GetElementsByTagName("paths")[0] as XmlElement;
                XmlNodeList list = paths.GetElementsByTagName("path");
                if (list.Count > 0)
                {
                    Settings.paths.Clear();
                    foreach (XmlElement path in list)
                    {
                        try
                        {
                            string value = path.GetAttribute("Value");
                            Settings.paths.Add(value);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error Parsing Path: " + e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error Parsing Paths: " + e.Message);
            }
        }
        private static void LoadAppConfig(XmlElement root)
        {
            try
            {
                XmlElement appConfig = root.GetElementsByTagName("appconfig")[0] as XmlElement;
                XmlElement trayicon = appConfig.GetElementsByTagName("trayicon")[0] as XmlElement;
                iconPath = trayicon.GetAttribute("Value");
                if (trayicon.Attributes != null && trayicon.Attributes["Text"] != null)
                {
                    iconText = trayicon.GetAttribute("Text");
                }
            }
            catch
            {
                //MessageBox.Show("Error Parsing Paths: " + e.Message);
            }
        }
        public static void Save()
        {
            try
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = ("    ");
                xmlSettings.CloseOutput = true;
                xmlSettings.OmitXmlDeclaration = false;
                XmlWriter writer = XmlWriter.Create(config, xmlSettings);
                writer.WriteStartElement("TrayDir");
                writer.WriteStartElement("appconfig");
                writer.WriteStartElement("trayicon");
                writer.WriteAttributeString("Value", iconPath);
                writer.WriteAttributeString("Text", iconText);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("options");
                foreach (KeyValuePair<string, Option> option in options)
                {
                    writer.WriteStartElement("option");
                    writer.WriteAttributeString("Name", option.Key);
                    writer.WriteAttributeString("Value", option.Value.sValue);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("paths");
                writer.WriteAttributeString("count", paths.Count.ToString());
                foreach (string path in paths)
                {
                    writer.WriteStartElement("path");
                    writer.WriteAttributeString("Value", path);
                    writer.WriteEndElement();
                    Console.WriteLine(path);
                };
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
                _altered = false;
            }
            catch (System.UnauthorizedAccessException e)
            {
                MessageBox.Show("Exception caught: " + e.Message);
            }
        }
        static string BoolToStr(bool value)
        {
            return value ? "1" : "0";
        }
        static bool StrToBool(string value)
        {
            return value == "1" ? true : false;
        }
        public static bool getOptionBool(string input)
        {
            if (options.ContainsKey(input))
            {
                return options[input].bValue;
            }
            else
            {
                return false;
            }
        }
        public static void setOptionBool(string input, bool value)
        {
            if (options.ContainsKey(input))
            {
                options[input].bValue = value;
            }
            else
            {
                Option o = new Option(input, value);
                options[input] = o;
            }
            _altered = true;
        }
        public static string getOptionStr(string input)
        {
            if (options.ContainsKey(input))
            {
                return options[input].sValue;
            }
            else
            {
                return "";
            }
        }
        public static void setOptionStr(string input, string value)
        {
            if (options.ContainsKey(input))
            {
                options[input].sValue = value;
            }
            else
            {
                Option o = new Option(input, value);
                options[input] = o;
            }
            _altered = true;
        }
        public static Dictionary<string, Option>.ValueCollection getOptions()
        {
            return options.Values;
        }
        public static bool ConfirmClose()
        {
            if (_altered)
            {
                return (MessageBox.Show("Changes have not been saved. Continue closing?", "TrayDir", MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            else
            {
                return true;
            }
        }
        public static bool isAltered()
        {
            return _altered;
        }
        public static void Alter()
        {
            _altered = true;
        }
    }
}
