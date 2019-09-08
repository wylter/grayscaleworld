using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

namespace UnityEditor {
    [CustomGridBrush(true, false, false, "Platform Brush")]
    public class PlatformBrush : GridBrush {

        //Tilemaps holder
        private Tilemap neutralTilemap;
        private Tilemap blackTilemap;
        private Tilemap whiteTilemap;

        private void Awake() {
            initTilemaps();
        }

        //Tilemaps assignations
        private void initTilemaps() {
            neutralTilemap = GameObject.Find("Neutral").GetComponent<Tilemap>();
            blackTilemap = GameObject.Find("Black").GetComponent<Tilemap>();
            whiteTilemap = GameObject.Find("White").GetComponent<Tilemap>();
        }

        //Checks if the tilemaps are assigned
        public void checkTilemaps() {
            if (!neutralTilemap || !blackTilemap || !whiteTilemap) {
                initTilemaps();
            }
        }

        //Finds the destination tilemap of out tile
        //Checkflag true if you want it to launch errors
        public Tilemap findDestinationTilemap(bool checkFlag) {

            if (!(cells[0].tile is PlatformTile)) {
                if (checkFlag) {
                    Debug.LogError("What you're trying to paint is not a PlatformTile. You cannot use this brush to paint such object");
                }
                return null;
            }

            PlatformTile tile = (PlatformTile)cells[0].tile; //Gets the tile to be painted

            if (checkFlag) {
                //Checks if all tiles selected are part of the same destination. If not, the system doesnt allow it
                for (int i = 1; i < cellCount; i++) {
                    PlatformTile tileToCheck = (PlatformTile)cells[i].tile;
                    if (tile.getDestination() != tileToCheck.getDestination()) {
                        Debug.LogError("Cannot Paint tiles that have different destinations");
                        return null;
                    }
                }
            }

            //Finds the destination Tilemap
            Tilemap destinationTilemap = neutralTilemap;
            switch (tile.getDestination()) {
                case destinationOption.Neutral:
                    destinationTilemap = neutralTilemap;
                    break;
                case destinationOption.Black:
                    destinationTilemap = blackTilemap;
                    break;
                case destinationOption.White:
                    destinationTilemap = whiteTilemap;
                    break;
                default:
                    Debug.Log("Errore, Destination non corretta");
                    break;
            }

            //Returns the destination Tilemap
            return destinationTilemap;
        }

        public Tilemap findDestinationTilemap() {
            return findDestinationTilemap(true);
        }

        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position) {
            checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic. 
            if (brushTarget.layer == 31) {
                base.Paint(grid, brushTarget, position);
                return;
            }

            //Find the destination tilemap
            Tilemap destinationTilemap = findDestinationTilemap();

            //Checks for errors
            if (!destinationTilemap) {
                return;
            }

            //Erases from all Tilemaps before doing the Paint, to sovrascrive the square
            Erase(grid, brushTarget, position);
            //Paints in the destination Tilemap
            base.Paint(grid, destinationTilemap.gameObject, position);
           
        }

        //Erases from all 3 of our tilemaps
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) {
            checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic. 
            if (brushTarget.layer == 31) {
                base.Erase(gridLayout, brushTarget, position);
                return;
            }

            base.Erase(gridLayout, neutralTilemap.gameObject, position);
            base.Erase(gridLayout, blackTilemap.gameObject, position);
            base.Erase(gridLayout, whiteTilemap.gameObject, position);
        }

        //Box Erases from all 3 Tilemaps
        public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) {
            checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic. 
            if (brushTarget.layer == 31) {
                base.BoxErase(gridLayout, brushTarget, position);
                return;
            }

            base.BoxErase(gridLayout, neutralTilemap.gameObject, position);
            base.BoxErase(gridLayout, blackTilemap.gameObject, position);
            base.BoxErase(gridLayout, whiteTilemap.gameObject, position);
        }

        //Box fills the correct destination tilemap
        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) {
            checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic.
            if(brushTarget.layer == 31) {
                base.BoxFill(gridLayout, brushTarget, position);
                return;
            }

            //Find the destination tilemap
            Tilemap destinationTilemap = findDestinationTilemap();

            //Checks for errors
            if (!destinationTilemap) {
                return;
            }

            //Erases from all Tilemaps before doing the Paint, to sovrascrive the box
            BoxErase(gridLayout, brushTarget, position);

            base.BoxFill(gridLayout, destinationTilemap.gameObject, position);
        }

        //Tries to pick from every tilemaps until it find the one that is filled
        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pickStart) {
            checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic. 
            if (brushTarget.layer == 31) {
                base.Pick(gridLayout, brushTarget, position, pickStart);
                return;
            }

            base.Pick(gridLayout, neutralTilemap.gameObject, position, pickStart);
            if (cells[0].tile is PlatformTile) {
                return;
            }

            base.Pick(gridLayout, blackTilemap.gameObject, position, pickStart);
            if (cells[0].tile is PlatformTile) {
                return;
            }

            base.Pick(gridLayout, whiteTilemap.gameObject, position, pickStart);
            if (cells[0].tile is PlatformTile) {
                return;
            }
        }

        //TODO: Solve the problem of the erasure
        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) {
            checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic. 
            if (brushTarget.layer == 31) {
                base.FloodFill(gridLayout, brushTarget, position);
                return;
            }

            //Find the destination tilemap
            Tilemap destinationTilemap = findDestinationTilemap();

            //Checks for errors
            if (!destinationTilemap) {
                return;
            }

            base.FloodFill(gridLayout, destinationTilemap.gameObject, position);

            //TODO: Implement a Floodfill algorithm
            Debug.LogWarning("The Floodfill doesnt takes in consideration other tilemaps nor sovriscribes from them. Be wary when using this functionality");
        }

        //Needed to create the brush for unity
        [MenuItem("Assets/Create/Platform Brush")]
        public static void CreateBrush() {
            string path = EditorUtility.SaveFilePanelInProject("Save Line Brush", "New Line Brush", "Asset", "Save Line Brush", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlatformBrush>(), path);
        }
    }

    //Needed to modify how the editor works 
    [CustomEditor(typeof(PlatformBrush))]
    public class PlatformBrushEditor : GridBrushEditor {
        private PlatformBrush PlatformBrush { get { return target as PlatformBrush; } }

        //Brush Preview viewed only for the correct tilemap
        public override void OnPaintSceneGUI(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing) {
            PlatformBrush.checkTilemaps();

            //If the tilemap target is the palette, it has to work normally and without our logic. 
            if (brushTarget.layer == 31) {
                base.OnPaintSceneGUI(gridLayout, brushTarget, position, tool, executing);
                return;
            }

            //Find the destination tilemap. We dont want errors in this case
            Tilemap destinationTilemap = PlatformBrush.findDestinationTilemap(false);

            if (destinationTilemap) {
                base.OnPaintSceneGUI(gridLayout, destinationTilemap.gameObject, position, tool, executing);
            }
        }

    }
}