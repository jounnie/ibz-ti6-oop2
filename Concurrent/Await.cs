namespace Concurrent
{
    public class Await
    {
        bool B = false;
        object l = new object();

        public void await(bool b)
        {
            B = b;
            while (!B) ;
        }

        public void notify(bool b)
        {
            B = b;
        }
    }
}