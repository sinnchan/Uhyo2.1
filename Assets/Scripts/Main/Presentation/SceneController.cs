using System;
using System.Collections;
using System.Collections.Generic;
using Main.Domain.Entities;
using Main.Util;
using UniRx;

namespace Main.Presentation
{
    public sealed class SceneController
    {
        public static readonly SceneController Instance = new SceneController();

        private readonly Stack<Scene> _backStack = new Stack<Scene>();

        private readonly Subject<Scene> _sceneStream = new Subject<Scene>();

        private Scene _currentScene = Scene.MainMenu; // 初期値

        // singleton
        private SceneController()
        {
        }

        /// <summary>
        ///     Debug時、開始時のシーンをセットしないと動かんよ
        /// </summary>
        /// <param name="scene"></param>
        public void Init(Scene scene)
        {
            _currentScene = scene;
        }

        public void ResetBackStack()
        {
            _backStack.Clear();
        }

        public IList<Scene> GetBackStackList()
        {
            return _backStack.ToArray();
        }

        public IObservable<Scene> GetScenesStream()
        {
            return _sceneStream;
        }

        public void Show(Scene scene, bool backStack = true)
        {
            _sceneStream.OnNext(scene);
            Log.Info(GetType().FullName, $"Show -> {scene}");

            if (backStack)
                _backStack.Push(_currentScene);

            _currentScene = scene;
        }

        public void Back()
        {
            if (_backStack.Count != 0)
            {
                var targetScene = _backStack.Pop();
                _sceneStream.OnNext(targetScene);
                _currentScene = targetScene;
                Log.Info(GetType().FullName, $"Pop BackStack -> {targetScene}");
            }
            else
            {
                Log.Info(GetType().FullName, "BackStack is Empty!!");
            }
        }
    }
}
