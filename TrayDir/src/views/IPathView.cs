﻿using FolderSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayDir
{
    class IPathView
    {
        public GroupBox pathsgb;
        public IPathView(TrayInstance instance)
        {
            // Paths Group
            pathsgb = new GroupBox();
            pathsgb.Text = "Paths";
            ControlUtils.ConfigureGroupBox(pathsgb);

            TableLayoutPanel pathstlp = new TableLayoutPanel();
            ControlUtils.ConfigureTableLayoutPanel(pathstlp);
            pathsgb.Controls.Add(pathstlp);

            for (int i = 0; i < instance.settings.paths.Count; i++)
            {
                string path = instance.settings.paths[i];
                int j = i;
                TextBox textbox = null;
                EventHandler fileSelect = new EventHandler(delegate (object obj, EventArgs args)
                {
                    MainForm.form.fd.DereferenceLinks = false;
                    MainForm.form.fd.InitialDirectory = textbox.Text;
                    DialogResult d = MainForm.form.fd.ShowDialog();
                    if (d == DialogResult.OK)
                    {
                        textbox.Text = MainForm.form.fd.FileName;
                        instance.settings.paths[j] = textbox.Text;
                        instance.UpdateTrayMenu();
                        MainForm.form.pd.Save();
                    }
                });
                EventHandler folderSelect = new EventHandler(delegate (object obj, EventArgs args)
                {
                    FolderSelectDialog fs = new FolderSelectDialog();
                    fs.InitialDirectory = textbox.Text;
                    if (fs.ShowDialog())
                    {
                        textbox.Text = fs.FileName;
                        instance.settings.paths[j] = textbox.Text;
                        instance.UpdateTrayMenu();
                        MainForm.form.pd.Save();
                    }
                });
                textbox = ControlUtils.AddPath(pathstlp, i, path, instance, fileSelect, folderSelect);
            }
        }
        public Control GetControl()
        {
            return pathsgb;
        }
        public int GetHeight()
        {
            int height = 0;
            height += pathsgb.Padding.Bottom + pathsgb.Padding.Top + pathsgb.Margin.Top + pathsgb.Margin.Bottom + pathsgb.Height;
            return height;
        }
    }
}
