using Main.Domain.Entities;

namespace Main.Presentation.ViewModel
{
    public class MainMenuVm
    {
        private readonly SceneController _sceneController = SceneController.Instance;

        public void OnClickPlayButton()
        {
            _sceneController.ShowPlaySetting();
        }

        public void OnClick2PlayerButton()
        {
            _sceneController.ShowPlayScene(PlayMode.TwoPlayer);
        }

        public void OnClickOnlinePlayButton()
        {
            _sceneController.ShowMatching();
        }
    }
}
