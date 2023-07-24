namespace Game.Rhythmic
{
    using UnityEngine;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "RhythmicData", menuName = "Custom/Rhythmic Data")]
    public class RhythmicDataScriptableObject : ScriptableObject
    {
        public List<RhtyhmicDataSO> rhytmicData = new List<RhtyhmicDataSO>();
    }
}