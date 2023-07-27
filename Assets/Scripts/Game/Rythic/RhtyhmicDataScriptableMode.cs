namespace Game.Rhythmic
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class RhtyhmicDataScriptableMode
    {
        public string RhythmicName;
        public bool Status;
    }

    [System.Serializable]
    public class RhtyhmicDataSO
    {
        public List<RhtyhmicDataScriptableMode> list;
    }
}