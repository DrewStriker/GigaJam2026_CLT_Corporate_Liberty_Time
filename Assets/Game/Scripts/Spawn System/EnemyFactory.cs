using Zenject;

namespace Game.SpawnSystem
{
    using Characters;
    public enum EnemyType { Basic, Boss }
    public class EnemyFactory  : PlaceholderFactory<EnemyType, EnemyController>
    {

    }
}
