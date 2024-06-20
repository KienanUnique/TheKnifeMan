using Game.Object.Part;
using Game.Utils.Directions;
using Services.Input;
using Services.ScreenPosition;
using UniRx;
using UnityEngine;

namespace Game.Player.Parts.LookDirection.Impl
{
    public class PlayerLookDirectionPart : AObjectPart<PlayerData>, IPlayerLookDirectionPart
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IScreenPositionService _screenPositionService;
        private readonly IInputService _inputService;

        private readonly ReactiveProperty<EDirection1D> _lookDirection1D = new();

        public IReactiveProperty<EDirection1D> LookDirection1D => _lookDirection1D;

        private Vector2 _lookDirection;
        private Rigidbody2D _rigidbody;

        public PlayerLookDirectionPart(
            IScreenPositionService screenPositionService,
            IInputService inputService
        )
        {
            _screenPositionService = screenPositionService;
            _inputService = inputService;
        }

        public override void Initialize()
        {
            _rigidbody = Data.MainRigidbody;
            Observable.EveryUpdate().Subscribe(_ => OnUpdate()).AddTo(_compositeDisposable);
        }

        public override void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public EDirection2D CalculateLookDirection2D()
        {
            if (Mathf.Abs(_lookDirection.x) > Mathf.Abs(_lookDirection.y))
                return _lookDirection.x > 0 ? EDirection2D.Right : EDirection2D.Left;

            return _lookDirection.y > 0 ? EDirection2D.Up : EDirection2D.Down;
        }

        private void OnUpdate()
        {
            var mouseScreenPosition = _inputService.MousePosition;
            var mouseWorldPosition = _screenPositionService.ConvertScreenPositionToWorld(mouseScreenPosition);
            var thisPosition = _rigidbody.position;

            _lookDirection = (mouseWorldPosition - thisPosition).normalized;

            var needRotateRight = _lookDirection.x > 0;
            _lookDirection1D.Value = needRotateRight ? EDirection1D.Right : EDirection1D.Left;
        }
    }
}