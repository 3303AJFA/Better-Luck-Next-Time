using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.World.Generator
{
    [System.Serializable]
    public struct Biomes
    {
        public TileBase Tile;
        public GameObject[] AviableVisuals;
    }

    [CreateAssetMenu(fileName = "Generation Settings", menuName = "Game/Generator/new Gerneration settings")]
    public class WorldGenerationSettingsSO : ScriptableObject
    {
        public List<Biomes> MapBiomes = new List<Biomes>();
        public Vector3 Offset = new Vector3(1, 0, 1);
    }
}
