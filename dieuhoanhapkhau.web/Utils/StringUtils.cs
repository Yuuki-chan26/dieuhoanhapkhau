using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.VisualBasic.CompilerServices;

namespace dieuhoanhapkhau.web.Utils
{
    public class StringUtils : System.Web.UI.Page
    {
        public static string ShowNameLevelDot(string Name, string Level)
        {
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = 1; i < strLevel; i++)
            {
                strReturn = strReturn + ".....";
            }
            strReturn += Name;
            return strReturn;
        }

        public static string ShowNameLevel(string Name, string Level)
        {
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = 1; i < strLevel - 1; i++)
            {
                strReturn = strReturn + "|    ";
            }
            strReturn += "|----";
            strReturn += Name;
            return strReturn;
        }

        public static string ShowLevel(string Level)
        {
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = 1; i < strLevel - 1; i++)
            {
                strReturn = strReturn + "|    ";
            }
            strReturn += "|----";
            return strReturn;
        }

        public static void File_Write(string sFileName, string sContent)
        {
            bool flag = sFileName.StartsWith("~");
            if (flag)
            {
                sFileName = HttpContext.Current.Server.MapPath(sFileName);
            }
            flag = !Directory.Exists(Path.GetDirectoryName(sFileName));
            if (flag)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(sFileName));
            }
            File.WriteAllText(sFileName, sContent, Encoding.UTF8);
        }

        public static string File_Read(string sFileName)
        {
            bool flag = sFileName.StartsWith("~");
            if (flag)
            {
                sFileName = HttpContext.Current.Server.MapPath(sFileName);
            }
            flag = File.Exists(sFileName);
            string result;
            if (flag)
            {
                result = File.ReadAllText(sFileName);
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static void DeleteFolder(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                if (System.IO.Directory.Exists(path))
                {
                    var drInfo = new DirectoryInfo(path);
                    DirectoryInfo[] folders = drInfo.GetDirectories(); // lay cac folder
                    FileInfo[] files = drInfo.GetFiles(); //lay cac files
                    if (folders != null)
                    {
                        foreach (DirectoryInfo fol in folders)
                        {
                            DeleteFolder(fol.FullName);
                        }

                    }
                    if (files != null)
                    {
                        foreach (FileInfo f in files)
                        {
                            f.Delete();
                        }
                    }
                    Directory.Delete(path);
                }
            }

        }

        public static string[] strArrPathImage(string subPath, HttpPostedFileBase file, int sizeMB)
        {
            var myarray = new string[] { "", "" };
            var path = "";
            if (file != null && file.ContentLength > 0 && file.ContentLength <= sizeMB)
            {
                myarray[0] = file.FileName;
                if (subPath.Length > 0)
                {
                    var isExists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(subPath));
                    if (!isExists)
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(subPath));
                    }
                    path = Path.Combine(HttpContext.Current.Server.MapPath(subPath), file.FileName);
                }
            }
            myarray[1] = path;
            return myarray;
        }

        public static string strPathImage(string subPath, HttpPostedFileBase file, int sizeMB)
        {
            var path = "";
            if (file != null && file.ContentLength > 0 && file.ContentLength <= sizeMB)
            {
                if (subPath.Length > 0)
                {
                    var isExists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(subPath));
                    if (!isExists)
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(subPath));
                    }
                    path = Path.Combine(HttpContext.Current.Server.MapPath(subPath), file.FileName);
                }
            }
            return path;
        }

        public static void deleteImageofUpdate(string currentPath, string oldPath)
        {
            if (currentPath == null || currentPath.Length <= 0) return;
            if (oldPath.ToLower().Equals(currentPath.ToLower())) return;
            // delete folder, image
            if (oldPath.Length <= 0) return;
            var index = oldPath.LastIndexOf("/", System.StringComparison.Ordinal);
            if (index <= 0) return;
            oldPath = oldPath.Substring(0, index).Replace("/", "\\");
            DeleteFolder(oldPath);
        }

        public static void deleteImageofUpdate(string currentPath, string oldPath, HttpPostedFileBase file)
        {
            if (currentPath == null || currentPath.Length <= 0) return;
            if (oldPath.ToLower().Equals(currentPath.ToLower())) return;
            //save image
            if (file != null)
            {
                file.SaveAs(currentPath);
            }
            // delete folder, image
            if (oldPath.Length <= 0) return;
            var index = oldPath.LastIndexOf("\\", System.StringComparison.Ordinal);
            if (index <= 0) return;
            oldPath = oldPath.Substring(0, index);
            DeleteFolder(oldPath);
        }

        public static string returnPathImage(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                var index = path.IndexOf("\\tempfiles\\", System.StringComparison.Ordinal);
                return index < 0 ? "" : path.Substring(index).Replace("\\", "/");
            }
            return "";
        }

        public static string URLForHTML(string sServerURL)
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            bool flag = sServerURL.StartsWith("~");
            string result;
            if (flag)
            {
                bool flag2 = Operators.CompareString(applicationPath, "/", false) == 0;
                if (flag2)
                {
                    result = applicationPath + sServerURL.Substring(2);
                }
                else
                {
                    result = applicationPath + sServerURL.Substring(1);
                }
            }
            else
            {
                result = sServerURL;
            }
            return result;
        }

        public static string galleryItem(string sRoot, string sClass, string name)
        {
            var sReturn = "";
            var i = 1;

            if (!(Directory.Exists(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"))))
            {
                return "";
            }

            foreach (var sFile in Directory.GetFiles(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"), "*_sum*.*"))
            {
                var fileText = HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/" + Path.GetFileNameWithoutExtension(sFile) + "l.txt");
                if (File.Exists(fileText))
                {
                    string[] arrLine = File.ReadAllLines(fileText);
                    sReturn += "<li class='" + sClass + "' type='movie'><a onclick=\"return showGalleryLargeLayer(this,'" + arrLine[0] + "','" + arrLine[1] + "')\" href='#'>";
                }
                else
                {
                    sReturn += "<li class='fix-galery-li " + sClass + "' style='display: none;' index=\"" + i + "\" ><a onclick='return showGalleryLargeLayer(this)' href='#'>";
                }
                sReturn += "<img src='" + URLForHTML(sRoot + sClass + "/" + Path.GetFileName(sFile)) + "'>" + "<div class='cover'>" + name + "</div>" + "</a></li>";
                i++;
            }
            return sReturn;
        }
        public static string galleryItemMB(string sRoot, string sClass, string name)
        {
            var sReturn = "";
            var i = 1;

            if (!(Directory.Exists(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"))))
            {
                return "";
            }

            foreach (var sFile in Directory.GetFiles(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"), "*_sum*.*"))
            {
                var fileText = HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/" + Path.GetFileNameWithoutExtension(sFile) + "l.txt");
                if (File.Exists(fileText))
                {
                    string[] arrLine = File.ReadAllLines(fileText);
                    sReturn += "<li class='" + sClass + "' type='movie'><a onclick=\"return showGalleryLargeLayer(this,'" + arrLine[0] + "','" + arrLine[1] + "')\" href='#'>";
                }
                else
                {
                    var s = Path.GetFileName(sFile);
                    var fileName = "";
                    if (s != null)
                    {
                        fileName = s.Replace("_sum", "_");
                        if (fileName.IndexOf("gallery_") == -1)
                        {
                            fileName = fileName.Replace(".png", ".jpg");
                            if (fileName.IndexOf("exterior_") == 0)
                            {
                                fileName = fileName.Replace("exterior_", "exterior_4door_");
                            }
                        }
                    }
                    sReturn += "<li class='fix-galery-li col-md-3 col-sm-4 col-xs-6 " + sClass + "' style='display: none;' index=\"" + i + "\" ><a class='fancybox' rel='" + sClass + "' href='" + URLForHTML(sRoot + sClass + "/" + fileName) + "'>";
                }
                //sReturn += "<img width='193' height='136' src='" + URLForHTML(sRoot + sClass + "/" + Path.GetFileName(sFile)) + "'>" + "<div class='cover'>" + name + "</div>" + "</a></li>";
                sReturn += "<img width='193' height='136' src='" + URLForHTML(sRoot + sClass + "/" + Path.GetFileName(sFile)) + "'>" + "<div class='cover'>&nbsp</div>" + "</a></li>";
                i++;
            }
            return sReturn;
        }
        public static string galleryItemMobile(string sRoot, string sClass, string name)
        {
            var sReturn = "";
            var i = 1;

            if (!(Directory.Exists(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"))))
            {
                return "";
            }

            foreach (var sFile in Directory.GetFiles(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"), "*_sum*.*"))
            {
                var fileText = HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/" + Path.GetFileNameWithoutExtension(sFile) + "l.txt");
                if (File.Exists(fileText))
                {
                    //string[] arrLine = File.ReadAllLines(fileText);
                    //sReturn += "<li class='" + sClass + "' type='movie'><a onclick=\"return showGalleryLargeLayer(this,'" + arrLine[0] + "','" + arrLine[1] + "')\" href='#'>";
                    var s = Path.GetFileName(sFile);
                    var fileName = "";
                    if (s != null)
                    {
                        fileName = s.Replace("_sum", "_");
                        if (!sClass.ToLower().Equals("others"))
                        {
                            fileName = fileName.Replace(".png", ".jpg");
                        }
                    }
                    sReturn += "<li data-src=\"" + URLForHTML(sRoot + sClass + "/" + fileName) + "\">";
                    sReturn += "<a href=\"#\">";
                }
                else
                {
                    //sReturn += "<li class='fix-galery-li " + sClass + "' style='display: block;' index=\"" + i + "\" ><a onclick='return showGalleryLargeLayer(this)' href='#'>";
                    var s = Path.GetFileName(sFile);
                    var fileName = "";
                    if (s != null)
                    {
                        fileName = s.Replace("_sum", "_");
                        if (!sClass.ToLower().Equals("others"))
                        {
                            fileName = fileName.Replace(".png", ".jpg");
                        }
                    }
                    sReturn += "<li data-src=\"" + URLForHTML(sRoot + sClass + "/" + fileName) + "\">";
                    sReturn += "<a href=\"#\">";
                }
                sReturn += "<img src='" + URLForHTML(sRoot + sClass + "/" + Path.GetFileName(sFile)) + "'>" + "</a></li>";
                i++;
            }
            return sReturn;
        }

        public static string galleryItemMobile1(string sRoot, string sClass, string name)
        {
            var sReturn = "";

            if (!(Directory.Exists(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"))))
            {
                return "";
            }

            foreach (var sFile in Directory.GetFiles(HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/"), "*_sum*.*"))
            {
                var fileText = HttpContext.Current.Server.MapPath(sRoot + "/" + sClass + "/" + Path.GetFileNameWithoutExtension(sFile) + "l.txt");
                if (File.Exists(fileText))
                {
                    var s = Path.GetFileName(sFile);
                    var fileName = "";
                    if (s != null)
                    {
                        fileName = s.Replace("_sum", "_");
                        if (!sClass.ToLower().Equals("others"))
                        {
                            fileName = fileName.Replace(".png", ".jpg");
                        }
                    }
                    //sReturn += "<div class=\"col-lg-3 col-sm-4 col-xs-6\" data-src=\"" + URLForHTML(sRoot + sClass + "/" + fileName) + "\">";
                    sReturn += "<div class=\"col-lg-3 col-sm-4 col-xs-6\" data-src=\"" + URLForHTML(sRoot + sClass + "/" + fileName) + "\"> <br />";
                    sReturn += "<a href=\"#\" class=\"\"> <br />";
                }
                else
                {
                    var s = Path.GetFileName(sFile);
                    var fileName = "";
                    if (s != null)
                    {
                        fileName = s.Replace("_sum", "_");
                        if (!sClass.ToLower().Equals("others"))
                        {
                            fileName = fileName.Replace(".png", ".jpg");
                        }
                    }
                    sReturn += "<div class=\"col-lg-3 col-sm-4 col-xs-6\" data-src=\"" + URLForHTML(sRoot + sClass + "/" + fileName) + "\"> <br />";
                    sReturn += "<a href=\"#\" class=\"\"> <br />";
                }
                //sReturn += "<img style=\"img-responsive col-xs-12 col-lg-12 fix-img fix-col\" src='" + URLForHTML(sRoot + sClass + "/" + Path.GetFileName(sFile)) + "'>" + "</a></div>";
                sReturn += "<img src=\'" + URLForHTML(sRoot + sClass + "/" + Path.GetFileName(sFile)) + "' style=\"\" class=\"img-responsive\" />" + "</a></div>"; //width: 270px; height: 176px;
            }
            return sReturn;
        }

        #region["Ham loc chuoi co dau thanh khong dau"]
        public static string ChuyenCoDauThanhKhongDau(string s)
        {
            var stFormD = s.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var t in from t in stFormD let uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(t) where uc != System.Globalization.UnicodeCategory.NonSpacingMark select t)
            {
                sb.Append(t);
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
        #endregion

        #region["SystemController"]

        #endregion
    }
}