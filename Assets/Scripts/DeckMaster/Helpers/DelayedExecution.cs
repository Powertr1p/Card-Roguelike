using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DeckMaster
{
    public static class DelayedExecution
    {
        public static IEnumerator Call(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
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