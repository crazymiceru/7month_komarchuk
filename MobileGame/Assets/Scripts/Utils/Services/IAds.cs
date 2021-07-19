using System;

namespace MobileGame
{
    internal interface IAds
    {
        public void ShowVideoReward(Action<VideoResult> successShow);
        public void ShowVideo();
    }
}