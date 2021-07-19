using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace MobileGame
{
    internal class UnityAds : MonoBehaviour, IAds, IUnityAdsListener
    {

        private const string _rewardVideoIDAndroid = "Rewarded_Android";
        private const string _rewardVideoIDIOS = "Rewarded_iOS";
        private string _rewardVideoID;
        private const string _videoIDAndroid = "Interstitial_Android";
        private const string _videoIDIOS = "Interstitial_iOS";
        private string _videoID;
        private const string _gameIdAndroid = "4222637";
        private const string _gameIdIOS = "4222636";
        private Action<VideoResult> _evtEndVideo;

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            VideoResult result = (VideoResult)showResult;
            _evtEndVideo?.Invoke(result);
        }

        public void OnUnityAdsDidError(string message)
        {
            _evtEndVideo?.Invoke(VideoResult.Failed);
        }

        public void OnUnityAdsDidStart(string placementId)
        {            
        }

        public void OnUnityAdsReady(string placementId)
        {
        }

        public void ShowVideo()
        {
            _evtEndVideo = null;
            Advertisement.Show(_videoID);
        }

        public void ShowVideoReward(Action<VideoResult> successShow)
        {
            _evtEndVideo += successShow;
            Advertisement.Show(_rewardVideoID);
        }

        private void Start()
        {
#if UNITY_IOS
            Advertisement.Initialize(_gameIdIOS, true);
            _rewardVideoID=_rewardVideoIDIOS;
#endif
#if UNITY_ANDROID
            Advertisement.Initialize(_gameIdAndroid, true);
            _rewardVideoID = _rewardVideoIDAndroid;
#endif
        }
    }
}