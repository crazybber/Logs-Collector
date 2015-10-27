

using System.Collections.Generic;

namespace LogsCollections.EC.Template
{

    interface IFilesOperate
    {
        ICollection<string> IsFileTypeExit(string dirPath, string extString);

        void CopyLogfileByDirTree(string despath, string srcpath, string extStringPattern);

        void CreateIfNotExist(string dir);

        void DelDirAndFilesInDir(string rootdir);

        string GetFileExtStr();
        string GetCurrentTypeLogRootDir();

    }
}
