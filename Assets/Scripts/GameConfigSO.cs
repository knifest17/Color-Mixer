using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "GameConfigSO", menuName = "GameConfigSO", order = 0)]
    public class GameConfigSO : ScriptableObject
    {
        [SerializeField] LevelSO[] levels;

        public LevelSO[] Levels => levels;
    }
}