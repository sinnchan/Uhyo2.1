using Main.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Main.Domain.Entities.Scene;

namespace Main.Presentation.View
{
    /// <summary>
    ///     Unity起動時に色々初期化する用クラス
    /// </summary>
    public class Main : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Initialization()
        {
            ScenePresenter.Init();
            SceneController.Instance.Init(GetCurrentScene());
        }

        private static Scene GetCurrentScene()
        {
            Scene scene;
            switch (SceneManager.GetActiveScene().name)
            {
                case "MainMenu":
                    scene = Scene.MainMenu;
                    break;
                case "PlaySetting":
                    scene = Scene.OfflinePlaySetting;
                    break;
                default:
                    scene = Scene.MainMenu;
                    Log.Error(typeof(Main).FullName, "Start Unknown Scene.");
                    break;
            }

            return scene;
        }
    }
}
