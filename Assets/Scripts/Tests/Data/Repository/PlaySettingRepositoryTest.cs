using Main.Data.Reposiory;
using Main.Domain.Entities;
using Main.Domain.UseCase;
using NUnit.Framework;

namespace Tests.Data.Repository
{
    public class PlaySettingRepositoryTest
    {
        [TestCase(Turn.Black)]
        [TestCase(Turn.White)]
        public void Save_Load_TurnTest(Turn turn)
        {
            IPlaySettingRepository repository = PlaySettingRepository.Instance;
            repository.SaveTurn(turn);
            Assert.AreEqual(turn, repository.LoadTurn());
        }
    }
}
