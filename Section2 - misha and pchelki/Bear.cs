using System.Threading;
using System.Threading.Tasks;

namespace BearAndBees
{
    public interface IAwakable
    {
        void Awake();
    }

    public class Bear<T> : IAwakable
    {
        private AutoResetEvent eatEvent;

        public Bear(IStorage<T> stor)
        {
            eatEvent = new AutoResetEvent(false);

            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    eatEvent.WaitOne();
                    stor.Dequeue();
                    eatEvent.Reset();
                    Thread.Sleep(5000);
                }
            });
        }

        public void Awake()
        {
            eatEvent.Set();
        }
    }
}
