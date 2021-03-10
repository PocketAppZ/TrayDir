﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrayDir
{
    public class ProgramData
    {
        [XmlElement(ElementName = "Settings")]
        public Settings settings;
        [XmlElement(ElementName = "Instances")]
        public List<TrayInstance> trayInstances;
        private static string config = "config2.xml";
        public ProgramData()
        {
            settings = new Settings();
            trayInstances = new List<TrayInstance>();
        }
        public static ProgramData Load()
        {
            ProgramData pd = XMLUtils.LoadFromFile<ProgramData>(config);
            if (pd is null)
            {
                pd = new ProgramData();
            }
            return pd;
        }
        public void CreateDefaultInstance()
        {
            TrayInstance ti = new TrayInstance();
            trayInstances.Add(ti);
        }
        public void Save()
        {
            XMLUtils.SaveToFile(this, config);
        }
        public void UpdateAllMenus()
        {
            if (trayInstances != null)
            {
                foreach (TrayInstance instance in trayInstances)
                {
                    instance.UpdateTrayMenu();
                }
            }
        }
        public void FormHidden()
        {
            if (trayInstances != null)
            {
                foreach (TrayInstance instance in trayInstances)
                {
                    instance.SetFormHiddenMenu();
                }
            }
        }
        public void FormShowed()
        {
            if (trayInstances != null)
            {
                foreach (TrayInstance instance in trayInstances)
                {
                    instance.SetFormShownMenu();
                }
            }
        }


    }
}
