using System;

namespace MobileGame
{
    public sealed class UnitM
    {
        private ControlLeak _controlLeak = new ControlLeak("UnitMData");

        internal event Action evtKill = delegate { };
        internal event Action evtLives = delegate { };
        internal event Action evtDecLives = delegate { };

        private int _hp = -1;
        internal int HP
        {
            get => _hp;
            set
            {
                if (_hp != value && (_hp > -1 || value > 0 || value == -1000))
                {
                    if (_hp < value)
                    {
                        _hp = value;
                        evtLives.Invoke();
                    }

                    if (_hp > value)
                    {
                        if ((_hp > 0 && value <= 0) || value == -1000)
                        {
                            evtKill();
                        }
                        _hp = value;
                        _hp = _hp < 0 ? 0 : _hp;

                        evtDecLives.Invoke();
                        evtLives.Invoke();
                    }

                }
            }
        }

        internal SubscriptionField<float> maxSpeed { get; }
        internal SubscriptionField<float> powerJump { get; }
        internal SubscriptionField<PackInteractiveData> packInteractiveData { get; }
        internal SubscriptionField<bool> isOnGround { get; }
        internal SubscriptionField<TypeAnimation> typeAnimation { get; }
        internal SubscriptionField<Commands> command { get; }

        internal SubscriptionField<(TypeUnit typeUnit,int cfg)> killTypeItem { get; }

        public UnitM()
        {
            packInteractiveData = new SubscriptionField<PackInteractiveData>();
            command = new SubscriptionField<Commands>();
            typeAnimation = new SubscriptionField<TypeAnimation>();
            typeAnimation.Value = TypeAnimation.Idle;
            isOnGround = new SubscriptionField<bool>();
            maxSpeed = new SubscriptionField<float>();
            powerJump = new SubscriptionField<float>();
            killTypeItem = new SubscriptionField<(TypeUnit typeUnit, int cfg)>();
        }

    }

}