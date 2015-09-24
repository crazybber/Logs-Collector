

namespace LogsCollections.EC.LogTypeManager
{

 
    using Template;

    using ICSharpCode.SharpZipLib.Zip;

    class FileCollectZipMgr : MgrBase
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
