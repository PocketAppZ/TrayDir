﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrayDir
{
	public class TrayInstancePlugin
	{
		[XmlAttribute]
		public int id = -1;
		[XmlAttribute]
		public string alias;
		[XmlAttribute]
		public bool visible = true;
		public List<TrayInstancePluginParameter> parameters = new List<TrayInstancePluginParameter>();
		[XmlIgnore]
		public TrayPlugin plugin
		{
			get
			{
				if (id >= 0 && id < ProgramData.pd.plugins.Count)
				{
					return ProgramData.pd.plugins[id];
				}
				return null;
			}
		}
		public TrayInstancePlugin Copy()
		{
			TrayInstancePlugin tip = new TrayInstancePlugin();
			tip.id = id;
			tip.alias = alias;
			foreach(TrayInstancePluginParameter tipp in parameters)
			{
				tip.parameters.Add(tipp.Copy());
			}
			return tip;
		}

		internal bool isValid() {
			TrayPlugin p = plugin;
			bool valid = true;
			if (p != null) {
				for(int i = 0; i < p.parameters.Count; i++) {
					TrayPluginParameter tpp = p.parameters[i];
					valid &= !(tpp.required || tpp.isBoolean) || ((parameters.Count > i) && parameters[i].value != "");
				}
			} else {
				valid = false;
			}
			return valid;
		}
	}
}
