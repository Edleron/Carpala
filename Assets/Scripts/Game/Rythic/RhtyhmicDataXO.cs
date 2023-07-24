namespace Game.Rhythmic
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class RhtyhmicDataXO
    {
        public string RhythmicName;
        public bool Status;
        public GameObject Kratus;
    }

    [System.Serializable]
    public class RhtyhmicDataAO
    {
        public List<RhtyhmicDataXO> list;
    }
}