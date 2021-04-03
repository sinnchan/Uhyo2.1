using Main.Domain.Entities;
using Main.Domain.UseCase;
using Main.Util;

namespace Main.Data.Reposiory
{
    public sealed class PlaySettingRepository: IPlaySettingRepository
    {
        public static readonly PlaySettingRepository Instance = new PlaySettingRepository();

        private PlaySettingRepository()
        {
        }

        // todo ↓こいつら永続化する
        private Turn _turn = Turn.Black;
        private int _cpuStr = 1;
        private static readonly object LockTurn = new object();
        private static readonly object LockCpuStr = new object();

        public void SaveTurn(Turn turn)
        {
            lock (LockTurn)
            {
                _turn = turn;
                Log.Info(GetType().FullName, $"save turn -> {_turn}");
            }
        }

        public Turn LoadTurn()
        {
            lock (LockTurn)
            {
                return _turn;
            }
        }

        public void SaveCpuStr(int strength)
        {
            lock (LockCpuStr)
            {
                _cpuStr = strength;
                Log.Info(GetType().FullName, $"save cpu strength -> {strength}");
            }
        }

        public int LoadCpuStr()
        {
            lock (LockCpuStr)
            {
                return _cpuStr;
            }
        }
    }
}
