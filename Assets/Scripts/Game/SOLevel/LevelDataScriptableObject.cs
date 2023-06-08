namespace Game.SOLevel
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Level", menuName = "Custom/Level Data Scriptable Object")]
    public class LevelDataScriptableObject : ScriptableObject
    {
        public LevelData levelData;
    }
}
