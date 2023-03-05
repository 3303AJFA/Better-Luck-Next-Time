using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.World.Generator 
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField, NaughtyAttributes.Expandable] private WorldGenerationSettingsSO Settings;
        [Space]
        [SerializeField] private Tilemap Map;
        [SerializeField] private Transform VisualObjectsParent;

        private Dictionary<TileBase, GameObject> SelectedBiomes = new Dictionary<TileBase, GameObject>();

        private void Start()
        {
            Generate();
        }

        [NaughtyAttributes.Button]
        private void Generate()
        {
            if(VisualObjectsParent.childCount > 0)
            {
                ClearVisualObjectsParent();
            }

            GenerateBiomes();
            GenerateWorld();
        }

        private void ClearVisualObjectsParent()
        {
            int childCount = VisualObjectsParent.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(VisualObjectsParent.GetChild(i).gameObject);
            }
            SelectedBiomes.Clear();
        }
        private void GenerateBiomes()
        {
            foreach (var biome in Settings.MapBiomes)
            {
                GameObject visual = biome.AviableVisuals[Random.Range(0, biome.AviableVisuals.Length)];
                SelectedBiomes.Add(biome.Tile, visual);
            }
        }
        private void GenerateWorld()
        {
            BoundsInt bounds = Map.cellBounds;

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = Map.GetTile(new Vector3Int(x, y));
                    if (tile != null)
                    {
                        Instantiate(SelectedBiomes[tile], new Vector3(x, SelectedBiomes[tile].transform.position.y, y) + Settings.Offset, Quaternion.identity, VisualObjectsParent);
                    }
                }
            }
        }
    }
}
