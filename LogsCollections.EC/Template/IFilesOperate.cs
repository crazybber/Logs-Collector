

using System.Collections.Generic;

namespace LogsCollections.EC.Template
{

    interface IFilesOperate
    {
        ICollection<string> IsFileTypeExit(string dirPath,string extString);

        void Copyfile(string despath, string srcpath);

        void CreateIfNotExist(string dir);

        void DelDirAndFilesInDir(string rootdir);

    }
}
