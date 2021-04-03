using System;
using Main.Domain.Entities;
using Main.Util;
using UniRx;
using UnityEngine.SceneManagement;

namespace Main.Presentation.View
{
    public static class ScenePresenter
    {
        private static IObservable<Scenes> _sceneStream;

        public static void Init(IObservable<Scenes> sceneStream = null)
        {
            _sceneStream = sceneStream ?? SceneController.Instance.GetScenesStream();
            _sceneStream.Subscribe(
                scenes =>
                {
                    Log.Info(typeof(ScenePresenter).FullName, "showScene: " + scenes);
                    switch (scenes)
                    {
                        case Scenes.HomeMenu:
                            break;
                        case Scenes.MainMenuSetting:
                            break;
                        case Scenes.OfflinePlaySetting:
                            SceneManager.LoadScene("PlaySetting");
                            break;
                        case Scenes.OfflinePlay:
                            break;
                        case Scenes.PlayFinish:
                            break;
                        case Scenes.OnlineMatching:
                            break;
                        case Scenes.OnlinePlayView:
                            break;
                        case Scenes.OnlineFinish:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(scenes), scenes, null);
                    }
                }
            );
        }
    }
}
