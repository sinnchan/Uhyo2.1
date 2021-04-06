using Main.Domain.Entities;
using Main.Domain.UseCase;

namespace Main.Presentation.ViewModel
{
    public class PlaySettingVm
    {
        private readonly SceneController _sceneController = SceneController.Instance;
        private readonly GameSettingUseCase _gameSettingUseCase = new GameSettingUseCase();

        public void OnSelectTurn(Turn turn)
        {
            _gameSettingUseCase.SaveTurn(turn);
            _gameSettingUseCase.SetTurn(turn);
        }

        public void OnSelectCpuStrength(int strength)
        {
            _gameSettingUseCase.SaveCpuStr(strength);
            _gameSettingUseCase.SetCpuStr(strength);
        }

        public void OnGameStart()
        {
            _sceneController.Show(Scene.CpuPlayGameView);
        }

        public void OnBack()
        {
            _sceneController.Back();
        }
    }
}
