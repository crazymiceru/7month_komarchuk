﻿using UnityEngine;

namespace MobileGame
{
    internal sealed class HomingController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("HomingController");
        private IUnitView _iUnitView;
        private IUnitView _playerView;
        private HomingCfg _homingCfg;
        private SubscriptionField<Vector2> _control;
        private iReadOnlySubscriptionField<float> _maxSpeedPlayer;
        private iReadOnlySubscriptionField<float> _maxSpeed;
        private bool _isHoming;

        internal HomingController(iReadOnlySubscriptionField<float> maxSpeedPlayer, iReadOnlySubscriptionField<float> maxSpeed, SubscriptionField<Vector2> control, IUnitView iUnitView, IUnitView playerView, HomingCfg homingCfg)
        {
            _maxSpeedPlayer = maxSpeedPlayer;
            _maxSpeed = maxSpeed;
            _iUnitView = iUnitView;
            _playerView = playerView;
            _homingCfg = homingCfg;
            _control = control;
            if (_homingCfg.IsHomingCondition) _maxSpeedPlayer.Subscribe(changeMaxSpeed);
        }

        protected override void OnDispose()
        {
            _maxSpeedPlayer.UnSubscribe(changeMaxSpeed);
        }

        public void Execute(float deltaTime)
        {
            if (!_isHoming && _homingCfg.IsHomingCondition) return;

            var homeVector = _playerView.objectTransform.position - _iUnitView.objectTransform.position;
            if (homeVector.sqrMagnitude < _homingCfg.MinDistanceHoming * _homingCfg.MinDistanceHoming)
            {
                _control.Value = homeVector.normalized
                    * _homingCfg.PowerMove;
            }
        }

        private void changeMaxSpeed(float maxSpeedValue)
        {
            if (_maxSpeed.Value > maxSpeedValue)
            {
                _isHoming = true;
            }
            else _isHoming = false;
        }
    }
}
