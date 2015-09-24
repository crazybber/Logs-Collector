

namespace LogsCollections.EC.Template
{
    internal class SingletonProvider<T> where T : new()
    {
        // object _synLock = new object();

        private static readonly T Instance = new T();

        static public T GetInstance()
        {
            return Instance;

        }

    }

}
