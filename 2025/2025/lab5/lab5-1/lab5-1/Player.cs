using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_1
{
    // helper method for validation 
    public static class PlayerHelper
    {
        public static bool Contains(this PlaybackOptions current, PlaybackOptions flag) => (current & flag) != 0;
        
        // cant have (play and pause), (Play and stop), (nextand prev) at the same time
        public static bool IsInvalid(PlaybackOptions opts)
        {
            bool playAndPause = opts.Contains(PlaybackOptions.Play) && opts.Contains(PlaybackOptions.Pause);
            bool playAndStop = opts.Contains(PlaybackOptions.Play) && opts.Contains(PlaybackOptions.Stop);
            bool nextAndPrev = opts.Contains(PlaybackOptions.Next) && opts.Contains(PlaybackOptions.Previous);
            return playAndPause || playAndStop || nextAndPrev;
        }
    }
    class Player
    {
        public PlaybackOptions CurrentOptions { get; set; } = PlaybackOptions.None;

        public bool TryAdd(PlaybackOptions opts)
        {
            var trail = CurrentOptions | opts;
            if(PlayerHelper.IsInvalid(trail)) 
                return false; // invalid 
            
            CurrentOptions = trail;
            return true; // success 
        }

        public void Remove(PlaybackOptions opts)
        {
            CurrentOptions &= ~opts;
        }

        public bool Has(PlaybackOptions opt) => CurrentOptions.Contains(opt); 
        public void TogglePause() => CurrentOptions ^= PlaybackOptions.Pause;

        public override string ToString()
        {
            if (CurrentOptions == PlaybackOptions.None)
                return "None";

            var parts = new List<string>();
            foreach(PlaybackOptions o in Enum.GetValues(typeof(PlaybackOptions)))
                if(o != PlaybackOptions.None && CurrentOptions.Contains(o))
                    parts.Add(o.ToString());

            return string.Join(", ", parts);
        }
    }
}
