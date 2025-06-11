using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemsData", menuName = "Scriptable Objects/Gems Data", order = 3)]
public class GemsDataSO : ScriptableObject
{
    public List<Gem> gemDatas;

}

[Serializable]
public struct Gem
{
    public string Name;
    public int Value;
}