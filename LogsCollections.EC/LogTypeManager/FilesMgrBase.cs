
namespace LogsCollections.EC.LogTypeManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Template;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Diagnostics;

    public class FilesMgrBase : IFilesOperate
    {
        protected const string Pattern1 = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(";

        protected const string Pattern2 = @"\d{1,3}))?$";

        private string _filename = string.Empty;
        private int _logfiledircount = -1;
        public void CopyLogfileByDirTree(string despath, string srcpath, string extStringPattern)
        {
            CopyLogfileByDir(despath, srcpath, extStringPattern);


            var subdirlist = Directory.GetDirectories(srcpath);
            var logcatalogname = GetLastDirname(srcpath);

            Debug.Assert(logcatalogname != null, "logcatalogname==null");

            var subdespath = Path.Combine(despath, logcatalogname);

            foreach (var item in subdirlist)
            {
                CopyLogfileByDir(subdespath, item, extStringPattern);
            }

        }

        protected string GetLastDirname(string fullDirpath)
        {

            if (string.IsNullOrWhiteSpace(fullDirpath)) return null;

            var retStr = Path.GetFileName(fullDirpath.EndsWith("\\") ? Path.GetDirectoryName(fullDirpath) : fullDirpath);

            return retStr;
        }

        public void CopyLogfileByDir(string despath, string srcpath, string extStringPattern)
        {

            var dirname = GetLastDirname(srcpath);

            Debug.Assert(dirname != null, "dirname!=null");
            var curdirNameInDesDir = Path.Combine(despath, dirname);

            CreateIfNotExist(curdirNameInDesDir);

            var loglistincur = IsFileTypeExit(srcpath, extStringPattern);
            if (loglistincur == null || loglistincur.Count == 0) return;

            foreach (var item in loglistincur)
            {
                var filename = Path.GetFileName(item);
                if (filename == null) return;
                curdirNameInDesDir = Path.Combine(curdirNameInDesDir, filename);
                File.Copy(item, curdirNameInDesDir, true);
            }

        }


        public void CreateIfNotExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            // throw new NotImplementedException();
        }

        public void DelDirAndFilesInDir(string rootdir)
        {

            if (Directory.Exists(rootdir))
            {
                Directory.Delete(rootdir, true);
            }
            // throw new NotImplementedException();
        }

        virtual public string GetFileExtStr()
        {
            throw new NotImplementedException();
        }

        virtual public string GetCurrentTypeLogRootDir()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentWorkingDir()
        {
            return Directory.GetCurrentDirectory();
        }

        public string GetNextAllLogTypeRootDirFullPath()
        {
            var dirname = GetLogdirName();

            var rootdir = GetCurrentWorkingDir();

            var fullpath = Path.Combine(rootdir, dirname);

            if (Directory.Exists(fullpath)) return fullpath;
            Directory.CreateDirectory(fullpath);
            _logfiledircount++;
            return fullpath;
        }

        /// <summary>
        ///   //check if exist some kind of  files
        /// </summary>
        /// <param name="dirPath">dir path</param>
        /// <param name="extStringPattern"> file extesion string "*.log.bak|*.txt|*.log|*.log.1|*.1"</param>
        /// <returns>full file path list</returns>
        public ICollection<string> IsFileTypeExit(string dirPath, string extStringPattern)
        {

            var filelist = Directory.EnumerateFiles(dirPath).Where(s => Regex.IsMatch(s, extStringPattern, RegexOptions.IgnoreCase)).ToList();
            return filelist.Count > 0 ? filelist : null;
        }

        private string GetLogdirName()
        {
            _filename = DateTime.Now.ToString("yyy_MM_dd");

            if (_logfiledircount == -1)
            {
                return _filename;
            }
            return _filename + "_" + _logfiledircount;
        }
    }

}
