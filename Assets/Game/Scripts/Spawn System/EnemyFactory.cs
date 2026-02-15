using Zenject;

namespace Game.SpawnSystem
{
    using Characters;
    public enum EnemyType { Basic, Intermediate, MiniBoss, Boss }
    public class EnemyFactory  : PlaceholderFactory<EnemyType, EnemyController>
    {

    }
}
