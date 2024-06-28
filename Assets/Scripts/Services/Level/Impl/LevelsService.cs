using System;
using Db.Scenes;
using Game.Utils;
using KoboldUi.Utils;
using PostProcessing;
using Ui.Loading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Services.Level.Impl
{
    public class LevelsService : IInitializable, ILevelsService, IDisposable
    {
        private readonly IScenesParameters _scenesParameters;
        private readonly IPostProcessingController _processingController;
        private readonly SignalBus _signalBus;
        
        private readonly ReactiveProperty<float> _loadingProgress = new();
        private readonly ReactiveProperty<bool> _isLoadingCompleted = new(true);

        private int _currentLevelIndex;
        private AsyncOperation _loadingOperation;
        private IDisposable _updateLoadingDisposable;

        public IReactiveProperty<float> LoadingProgress => _loadingProgress;
        public IReactiveProperty<bool> IsLoadingCompleted => _isLoadingCompleted;
        public LevelSceneData CurrentLevelData => _scenesParameters.Levels[0];

        public LevelsService(
            IScenesParameters scenesParameters, 
            IPostProcessingController processingController,
            SignalBus signalBus
        )
        {
            _scenesParameters = scenesParameters;
            _processingController = processingController;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            _currentLevelIndex = 0;
            for (var i = 0; i < _scenesParameters.Levels.Count; i++)
            {
                var levelData = _scenesParameters.Levels[i];
                
                if (currentSceneName != levelData.LevelName)
                    continue;
                
                _currentLevelIndex = i;
                break;
            }
        }
        
        public void Dispose()
        {
            _loadingProgress?.Dispose();
            _updateLoadingDisposable?.Dispose();
        }
        
        public void LoadNextLevel()
        {
            var nextSceneName = _scenesParameters.MainMenuSceneName;
            
            _currentLevelIndex++;
            
            if (_currentLevelIndex < _scenesParameters.Levels.Count)
            {
                nextSceneName = _scenesParameters.Levels[_currentLevelIndex].LevelName;
            }
            
            LoadScene(nextSceneName);
        }

        public void ReloadLevel()
        {
            LoadScene(_scenesParameters.Levels[_currentLevelIndex].LevelName);
        }

        public void LoadMainMenu()
        {
            LoadScene(_scenesParameters.MainMenuSceneName);
        }

        public void LoadFirstLevel()
        {
            _currentLevelIndex = 0;
            LoadScene(_scenesParameters.Levels[_currentLevelIndex].LevelName);
        }

        private void LoadScene(string sceneName)
        {
            if(!_isLoadingCompleted.Value)
                return;
            
            _isLoadingCompleted.Value = false;
            
            _loadingProgress.Value = 0f;
            
            _signalBus.OpenWindow<LoadingWindow>(EWindowLayer.Project);
            
            _processingController.EnterFade(() =>
            {
                _loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            
                _loadingOperation.completed += OnLoadingCompleted;
            
                _updateLoadingDisposable = Observable.EveryUpdate().Subscribe(_ => OnUpdateDuringLoading());
            });
        }

        private void OnLoadingCompleted(AsyncOperation obj)
        {
            _loadingOperation.completed -= OnLoadingCompleted;
            _loadingOperation = null;
            
            _updateLoadingDisposable.Dispose();
            _loadingProgress.Value = 1f;
            
            _signalBus.BackWindow(EWindowLayer.Project);
            
            _processingController.ExitFade(() => _isLoadingCompleted.Value = true);
        }

        private void OnUpdateDuringLoading()
        {
            _loadingProgress.Value = _loadingOperation.progress;
        }
    }
}