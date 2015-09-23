using System;
using System.Runtime.CompilerServices;
using ICSharpCode.SharpZipLib.Zip;

namespace LogsCollections.EC
{
    class FileCollectZipMgr : ContextBoundObject
    {

        private static readonly FileCollectZipMgr Instance = SingletonProvider<FileCollectZipMgr>.GetInstance();
        private static readonly FastZip FastZiper = new FastZip();


        public static FileCollectZipMgr GetInstance()
        {
            return Instance;
        }

        public static void CollectFilesAndZipThem(string dirpath)
        {

            FastZiper.CreateZip("test.zip", dirpath, false, null);

            //throw new NotImplementedException();
        }
    }
}
