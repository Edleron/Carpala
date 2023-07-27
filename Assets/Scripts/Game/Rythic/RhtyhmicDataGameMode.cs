namespace Game.Rhythmic
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class RhtyhmicDataGameMode
    {
        public string RhythmicName;
        public bool Status;
        public GameObject Kratus;
    }

    [System.Serializable]
    public class RhtyhmicDataGM
    {
        public List<RhtyhmicDataGameMode> list;
    }
}