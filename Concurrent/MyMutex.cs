namespace Concurrent
{
    public class MyMutex
    {
        bool _blocking = false;

        public void WaitOne()
        {
            while (_blocking);
            _blocking = true;

        }

        public void ReleaseMutex()
        {
            _blocking = false;
        }
    }
}