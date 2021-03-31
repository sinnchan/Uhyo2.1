using Main.Domain.Entities;

namespace Main.Presentation.ViewModel
{
    public class PlaySettingVm
    {
        private readonly SceneController _sceneController = SceneController.GetInstance();

        public void SelectTurn(Turn turn)
        {
            // TODO usecase
        }

        public void SelectCpuStrength(int strength)
        {
            // TODO usecase
        }

        public void OnClickStartButton()
        {
            // TODO usecase
        }

        public void OnClickBackButton()
        {
            _sceneController.Back();
        }
    }
}
