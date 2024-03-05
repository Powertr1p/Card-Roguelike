using System;
using System.Threading.Tasks;

namespace DeckMaster
{
    public static class DelayedExecution
    {
        public static void Call(float delay, Action action)
        {
            Task.Delay(TimeSpan.FromSeconds(delay)).ContinueWith(t => action.Invoke());
        }
        
        public static Task Call(float delaySeconds, Func<Task> asyncAction)
        {
            async Task DelayedExecutionAsync()
            {
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                await asyncAction();
            }

            return DelayedExecutionAsync();
        }
    }
}