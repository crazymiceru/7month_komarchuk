using UnityEngine;

namespace MobileGame
{
    public sealed class ControlM
    {
        private ControlLeak _controlLeak = new ControlLeak("ControlM");

        public ControlM()
        {
            positionCursor = new SubscriptionField<Vector2>();
            isNewTouch = new SubscriptionField<bool>();
            control = new SubscriptionField<Vector2>();
            isJump = new SubscriptionField<bool>();
        }

        public SubscriptionField<Vector2> positionCursor { get; }
        public SubscriptionField<bool> isNewTouch { get; }
        public SubscriptionField<Vector2> control { get; }        
        public SubscriptionField<bool> isJump { get; }
    }
}