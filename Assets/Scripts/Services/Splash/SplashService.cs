using System;
using Db.Splash;
using KoboldUi.Utils;
using Services.Input;
using Services.Level;
using Services.Sound;
using Ui.Splash;
using UniRx;
using Utils.Sounds;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Services.Splash
{
    public class SplashService : IInitializable, IDisposable
    {
        private readonly ILevelsService _levelsService;
        private readonly IUiSoundFxService _uiSoundFxService;
        private readonly SignalBus _signalBus;
        private readonly ISplashParameters _splashParameters;
        private readonly IInputService _inputService;

        private readonly CompositeDisposable _compositeDisposable = new();


        public SplashService(
            ILevelsService levelsService, 
            IUiSoundFxService uiSoundFxService, 
            SignalBus signalBus,
            ISplashParameters splashParameters, 
            IInputService inputService
        )
        {
            _levelsService = levelsService;
            _uiSoundFxService = uiSoundFxService;
            _signalBus = signalBus;
            _splashParameters = splashParameters;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _signalBus.OpenWindow<SplashWindow>();
            _uiSoundFxService.Play(EUiSoundFxType.KitchenInTheDungeon, () => _signalBus.BackWindow());
            
            _inputService.SwitchToAnyKeyInput();
            _inputService.AnyKeyPressed.Subscribe(_ => OnAnyKeyPressed()).AddTo(_compositeDisposable);

            Observable.Timer(TimeSpan.FromSeconds(_splashParameters.SplashDuration)).Subscribe(_ => HandleSplashEnd())
                .AddTo(_compositeDisposable);
            Observable.Timer(TimeSpan.FromSeconds(_splashParameters.CloseLogoDelay)).Subscribe(_ => _signalBus.BackWindow())
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void OnAnyKeyPressed()
        {
            _signalBus.BackWindow();
            HandleSplashEnd();
        }

        private void HandleSplashEnd()
        {
            _compositeDisposable?.Dispose();
            
            _uiSoundFxService.Interrupt();
            _levelsService.LoadMainMenu();
        }
    }
}