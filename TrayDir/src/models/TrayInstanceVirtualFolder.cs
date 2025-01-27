﻿using System.Xml.Serialization;

namespace TrayDir
{
	public class TrayInstanceVirtualFolder
	{
		[XmlAttribute]
		public string alias;
		[XmlAttribute]
		public bool visible = true;
		public TrayInstanceVirtualFolder() : this(string.Empty) { }
		public TrayInstanceVirtualFolder(string alias)
		{
			this.alias = alias;
		}
		public TrayInstanceVirtualFolder Copy()
		{
			TrayInstanceVirtualFolder tivf = new TrayInstanceVirtualFolder();
			tivf.alias = alias;
			return tivf;
		}
	}
}
