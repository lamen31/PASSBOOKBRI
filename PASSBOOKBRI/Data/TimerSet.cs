using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace PASSBOOKBRI.Data
{
    public class TimerSet
    {
        private Timer time;
        private int flag = 1;
        public void SetTimer(double interval)
        {
            time = new Timer(interval);
            time.Elapsed += TimeElapsed;
            time.Enabled = true;
        }
        public void TimeDispose()
        {
            TimeStop();
            time.Dispose();
            Console.WriteLine("TIMER DISPOSE");
        }
        public void TimeStop()
        {
            time.Enabled = false;
            Console.WriteLine("TIMER STOP");
        }
        public event Action onelapsed;
        public void TimeElapsed(object source, ElapsedEventArgs e)
        {
            onelapsed?.Invoke();
            time.Stop();
            time.Enabled = false;
            time.Dispose();
            Console.WriteLine("TIMER END");
            Console.WriteLine(flag);
        }
    }
}
