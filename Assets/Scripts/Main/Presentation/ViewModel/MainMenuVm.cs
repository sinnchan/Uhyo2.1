using Main.Domain.Entities;

namespace Main.Presentation.ViewModel
{
    public class MainMenuVm
    {
        private readonly SceneController _sceneController = SceneController.Instance;

        public void OnClickPlayButton()
        {
            _sceneController.Show(Scene.OfflinePlaySetting);
        }

        public void OnClick2PlayerButton()
        {
            _sceneController.Show(Scene.TwoPlayerGameView);
        }

        public void OnClickOnlinePlayButton()
        {
            _sceneController.Show(Scene.OnlineMatching);
        }
    }
}
