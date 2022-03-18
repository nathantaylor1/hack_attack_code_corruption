using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeModule : MonoBehaviour
{
    [System.Serializable]
    public class Stat<T>
    {
        public bool statApplies = true;
        public T stat;

        public Stat() { }

        public Stat(T _stat)
        {
            stat = _stat;
        }

        public Stat(bool _statApplies, T _stat)
        {
            statApplies = _statApplies;
            stat = _stat;
        }
    }

    [Header("Base Stats")]

    public Stat<float> moveSpeed = new Stat<float>(1f);
    public Stat<float> jumpSpeed = new Stat<float>(1f);
    [Tooltip("The amount of time between attacks")]
    public Stat<float> reloadTime = new Stat<float>(1f);
    [Tooltip("How quickly projectiles move upon emission from this module")]
    public Stat<float> projectileSpeedMultiplier = new Stat<float>(1f);

    [Header("Code Editor Reference")]

    [SerializeField]
    protected GameObject codeEditor;
}
