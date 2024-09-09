using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapColliderController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tilesWithCollider;

    private void Start()
    {
        AddCollidersToTiles();
    }

    void AddCollidersToTiles()
    {
        if (tilemap == null) return;

        TilemapCollider2D tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
            tilemapCollider = tilemap.gameObject.AddComponent<TilemapCollider2D>();
        }

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(position);
            if (Array.Exists(tilesWithCollider, t => t == tile))
            {
                tilemap.SetColliderType(position, Tile.ColliderType.Grid);
            }
            else
            {
                tilemap.SetColliderType(position, Tile.ColliderType.None);
            }
        }
    }

}
