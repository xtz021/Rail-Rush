using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rank
{
    public RankTier rankTier;
    public string rankName; // 0 to 9
    public int pickAxesRequired; // Based on rank tier
    public int NuggetReward => (int)rankTier * 300;

}

[System.Serializable]
public enum RankTier
{
    Passerby,
    Novice,
    Entrant,
    PitWorker,
    StoneHunter,
    RailRunner,
    CartRider,
    GoldDigger,
    MasterCarter,
    KingOfRails
}
