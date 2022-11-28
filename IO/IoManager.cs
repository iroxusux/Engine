using Engine.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.IO
{
    public static class IoManager
    {
        public static string GetFilePath(string sDefaultExtension)
        {
            string sFile = string.Empty;
            using(OpenFileDialog oOpenFileDialog = new())
            {
                oOpenFileDialog.InitialDirectory = @"C\\";
                oOpenFileDialog.DefaultExt = sDefaultExtension;
                if(oOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sFile = oOpenFileDialog.FileName;
                }
            }
            return sFile;
        }
        public static string GetDirectoryPath()
        {
            FolderBrowserDialog oBrowser = new();
            oBrowser.Description = "Select Folder";
            if(oBrowser.ShowDialog() == DialogResult.OK)
            {
                return oBrowser.SelectedPath;
            }
            return string.Empty;
        }
        public static FileLocation SaveAs(string sFileFilter)
        {
            SaveFileDialog oDialog = new()
            {
                Filter = sFileFilter,
                Title = "Save File As",
            };
            if(oDialog.ShowDialog() == DialogResult.OK)
            {
                string[] aPathSlice = oDialog.FileName.Split("\\");
                if (aPathSlice.Count() == 0) return new();
                string sFileName = aPathSlice.Last().Substring(0, aPathSlice.Last().LastIndexOf("."));
                FileLocation oLocation = new()
                {
                    Name = sFileName,
                    Directory = string.Join("\\", aPathSlice.Take(aPathSlice.Length -1)) + "\\",
                };
                return oLocation;
            }
            return new();
        }
    }
}
