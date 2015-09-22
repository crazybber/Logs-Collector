using System;
using System.Runtime.CompilerServices;
using ICSharpCode.SharpZipLib.Zip;

namespace LogsCollections.EC
{
    class FileCollectionMgr : ContextBoundObject
    {

        private static readonly FileCollectionMgr Instance = SingletonProvider<FileCollectionMgr>.GetInstance();
        private static readonly FastZip FastZiper = new FastZip();


        public static FileCollectionMgr GetInstance()
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
