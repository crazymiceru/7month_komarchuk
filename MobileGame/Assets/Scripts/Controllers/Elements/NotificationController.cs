using System;
using UnityEngine;

#if  UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif


namespace MobileGame
{
    internal sealed class NotificationController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("NotificationController");
        private GameModel _gameModel;
        private const string _NotifierId = "test";
        private const string _largeIconIDReward = "id_reward";
        private AndroidNotificationChannel _androidNotificationChanel;

        internal NotificationController(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.currentDayReward.Subscribe(GetRewards);

#if UNITY_ANDROID
            _androidNotificationChanel = new AndroidNotificationChannel
            {
                Id = _NotifierId,
                Name = "Game Notifier",
                Importance = Importance.Default,
                CanBypassDnd = false,
                CanShowBadge = true,
                Description = "Get Reward",
                EnableLights = true,
                EnableVibration = true,
                LockScreenVisibility = LockScreenVisibility.Public
            };
            AndroidNotificationCenter.RegisterNotificationChannel(_androidNotificationChanel);
#endif
        }

        protected override void OnDispose()
        {
            _gameModel.currentDayReward.UnSubscribe(GetRewards);
        }

        private void GetRewards(int currentReward)
        {
            if (currentReward == 0) return;
            CreateNotification("Награда", "Героя ждёт награда", _largeIconIDReward);            
        }

        private void CreateNotification(string title,string text,string largeIcon)
        {
#if UNITY_ANDROID
            var androidSettingsNotification = new AndroidNotification
            {
                Color = Color.white,
                FireTime = DateTime.UtcNow.AddSeconds(_gameModel.durationNextReward.Value),
                Title = title,
                Text = text,
                LargeIcon = largeIcon
            };
            AndroidNotificationCenter.SendNotification(androidSettingsNotification, _NotifierId);
#elif UNITY_IOS
       var iosSettingsNotification = new iOSNotification
       {
           Identifier = _NotifierId,
           Title = title,
           Subtitle = title,
           Body = text,
           Badge = 1,
           Data = (DateTime.UtcNow + TimeSpan.FromSeconds(_gameModel.durationNextReward.Value)).ToString(),
           ForegroundPresentationOption = PresentationOption.Alert,
           ShowInForeground = false
       };
      
       iOSNotificationCenter.ScheduleNotification(iosSettingsNotification);
#endif
        }

    }
}
