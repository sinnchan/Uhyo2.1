using System;
using UniRx;
using UnityEngine.SceneManagement;
using Scene = Main.Domain.Entities.Scene;

namespace Main.Presentation.View
{
    public static class ScenePresenter
    {
        private static IObservable<Scene> _sceneStream;

        public static void Init(IObservable<Scene> sceneStream = null)
        {
            _sceneStream = sceneStream ?? SceneController.Instance.GetScenesStream();
            _sceneStream.Subscribe(
                scenes =>
                {
                    switch (scenes)
                    {
                        case Scene.MainMenu:
                            SceneManager.LoadScene("MainMenu");
                            break;
                        case Scene.MainMenuSetting:
                            break;
                        case Scene.OfflinePlaySetting:
                            SceneManager.LoadScene("PlaySetting");
                            break;
                        case Scene.CpuPlayGameView:
                            break;
                        case Scene.TwoPlayerGameView:
                            break;
                        case Scene.OnlinePlayGameView:
                            break;
                        case Scene.PlayFinish:
                            break;
                        case Scene.OnlineMatching:
                            break;
                        case Scene.OnlineFinish:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(scenes), scenes, null);
                    }
                }
            );
        }
    }
}
