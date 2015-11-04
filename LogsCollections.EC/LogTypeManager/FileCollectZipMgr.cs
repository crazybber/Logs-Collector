


using System;
using System.IO;
using LogsCollections.EC.Properties;

namespace LogsCollections.EC.LogTypeManager
{


    using Template;

    using ICSharpCode.SharpZipLib.Zip;

    public class FileCollectZipMgr : FilesMgrBase
    {

        private static readonly FileCollectZipMgr Instance = SingletonProvider<FileCollectZipMgr>.GetInstance();
        private static readonly FastZip FastZiper = new FastZip();


        public static FileCollectZipMgr Current
        {

            get { return Instance; }
        }

        public void zipdir_direct(string desdir, string srcdir)
        {
            if (!Directory.Exists(srcdir) || !Directory.Exists(desdir))
            {
                throw new FileNotFoundException(Resources.dir_not_found);
            }
            var filename = GetLastDirname(srcdir);
            filename = filename + ".zip";

            filename = Path.Combine(desdir, filename);

            FastZiper.CreateZip(filename, srcdir, false, null);
        }

        public void zipdir(string dirpath, bool isDeletedir = false)
        {

            if (!Directory.Exists(dirpath))
            {
                throw new FileNotFoundException(Resources.dir_not_found);
            }

            var filename = GetLastDirname(dirpath);
            if (filename == null)
            {
                throw new ArgumentNullException(Resources.last_dirname_not_found);
            }

            filename = filename + ".zip";

            FastZiper.CreateZip(filename, dirpath, false, null);

            if (isDeletedir)
            {
                Directory.Delete(dirpath, true);
            }

            //throw new NotImplementedException();
        }

        public void zipsrcdirtodesdir(string desdir, string srcdir, bool isDeletedir = false)
        {
            if (!Directory.Exists(srcdir))
            {
                throw new FileNotFoundException(Resources.dir_not_found);
            }

            var filename = GetLastDirname(desdir);
            filename = filename + ".zip";

            FastZiper.CreateZip(filename, srcdir, false, null);

            if (isDeletedir)
            {
                Directory.Delete(desdir, true);
            }

        }


    }
}
