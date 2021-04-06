using System.Threading.Tasks;
using Main.Data.Reposiory;
using Main.Domain.Entities;

namespace Main.Domain.UseCase
{
    public interface IPlaySettingRepository
    {
        void SaveTurn(Turn turn);
        Turn LoadTurn();
        void SaveCpuStr(int strength);
        int LoadCpuStr();
    }

    /// <summary>
    ///     すべて別スレッドで実行されます
    /// </summary>
    public class GameSettingUseCase
    {
        private readonly IPlaySettingRepository _playSettingRepository;

        public GameSettingUseCase(IPlaySettingRepository repository = null)
        {
            _playSettingRepository = repository ?? PlaySettingRepository.Instance;
        }

        /// <summary>
        ///     選択したターンを保存します。
        /// </summary>
        /// <param name="turn"></param>
        public void SaveTurn(Turn turn)
        {
            Task.Run(() => _playSettingRepository.SaveTurn(turn));
        }

        public void SetTurn(Turn turn)
        {
            // todo 開始ターン選択の実装する
        }

        public void SaveCpuStr(int strength)
        {
            Task.Run(() => _playSettingRepository.SaveCpuStr(strength));
        }

        public void SetCpuStr(int strength)
        {
            // todo CPUの強さ設定の実装する
        }
    }
}
