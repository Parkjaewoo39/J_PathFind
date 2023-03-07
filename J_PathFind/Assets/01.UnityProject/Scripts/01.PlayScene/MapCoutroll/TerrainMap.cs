using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMap : TileMapController
{
    private const string TERRAIN_TILEMAP_OBJ_NAME = "TerrainTilemap";
    private Vector2Int mapCellSize = default;
    private Vector2 mapCellGap = default;

    private List<TerrainController> allTerrains = default;

    //! Awake Ÿ�ӿ� �ʱ�ȭ �� ������ �������Ѵ�.
    public override void InitAwake(MapBoard mapController_)
    {
        this.tileMapObjName = TERRAIN_TILEMAP_OBJ_NAME;
        base.InitAwake(mapController_);

        allTerrains = new List<TerrainController>();
        //{Ÿ���� x�� ������ ��ü Ÿ���� ���� ���� ����, ���� ����� �����Ѵ�.

        mapCellSize = Vector2Int.zero;
        float tempTileY = allTileObj[0].transform.localPosition.y;
       // Debug.Log(allTileObj.Count);

        for (int i = 0; i < allTileObj.Count; i++)
        {
            // Debug.Log(tempTileY.IsEquals(allTileObj[i].transform.localPosition.y));
            // Debug.Log(mapCellSize.x);
            // Debug.Log(i);

            if (tempTileY.IsEquals(allTileObj[i].transform.localPosition.y) == false)
            {
                mapCellSize.x = i;
                //Debug.Log(i);
                break;
            }   //if : ù��° Ÿ���� y ��ǥ�� �޶����� ���� �������� ���� ���� �� ũ���̴�.

            //��ü Ÿ���� ���� ���� ���� �� ũ��� ���� ���� ���� ���� �� ũ���̴�.
        }
        mapCellSize.y = Mathf.FloorToInt(allTileObj.Count / mapCellSize.x);
        //{Ÿ���� x�� ������ ��ü Ÿ���� ���� ���� ����, ���� ����� �����Ѵ�.

        //{ x ���� ���� �� Ÿ�ϰ�, y �� ���� �� Ÿ�� ������ ���� ���������� Ÿ�� ���� �����Ѵ�.
        mapCellGap = Vector2.zero;

        mapCellGap.x =
        allTileObj[1].transform.localPosition.x - allTileObj[0].transform.localPosition.x;

        mapCellGap.y =
        allTileObj[mapCellSize.x].transform.localPosition.y - allTileObj[0].transform.localPosition.y;

        //} x ���� ���� �� Ÿ�ϰ�, y �� ���� �� Ÿ�� ������ ���� ���������� Ÿ�� ���� �����Ѵ�.
    }   //InitAwake()

    private void Start()
    {
        // { Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ����
        GameObject changeTilePrefabs = ResManager.Instance.terrainPrefabs[RDefine.TERRAIN_PREF_OCEAN];
        //Ÿ�ϸ� �߿� ��� ������ �ٴٷ� ��ü�� ������ �����Ѵ�.
        const float CHANGE_PERCENTAGE = 15.0f;
        float correctChangePercentage =
        allTileObj.Count * (CHANGE_PERCENTAGE / 100.0f);
        //�ٴٷ� ��ü�� Ÿ���� ������ ����Ʈ ���·� �����ؼ� ���´�.
        List<int> changeTileResult = GFunc.CreateList(allTileObj.Count, 1);
        changeTileResult.Shuffle();

        GameObject tempChangeTile = default;
        for (int i = 0; i < allTileObj.Count; i++)
        {
            if (correctChangePercentage <= changeTileResult[i]) { continue; }
            //�������� �ν��Ͻ�ȭ�ؼ� ��ü�� Ÿ���� Ʈ�������� ī���Ѵ�.
            tempChangeTile = Instantiate(changeTilePrefabs, tileMap.transform);
            tempChangeTile.name = changeTilePrefabs.name;
            // Debug.Log(allTileObj[i].transform.localPosition);
            tempChangeTile.SetLocalScale(allTileObj[i].transform.localScale);
            tempChangeTile.SetLocalPos(allTileObj[i].transform.localPosition);

            allTileObj.Swap(ref tempChangeTile, i);
            tempChangeTile.DestoryObj();
        }   //loop: ������ ������ ������ ���� Ÿ�ϸʿ� �ٴٸ� �����ϴ� ����
        // } Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ����

        //{ ������ �����ϴ� Ÿ���� ������ �����ϰ�, ��Ʈ�ѷ��� ĳ���ϴ� ����

        TerrainController tempTerrain = default;
        TerrainType terrainType = TerrainType.NONE;

        int loopCnt = 0;
        foreach (GameObject tile_ in allTileObj)
        {
            tempTerrain = tile_.GetComponentMust<TerrainController>();
            switch (tempTerrain.name)
            {
                case RDefine.TERRAIN_PREF_PLAIN:
                    terrainType = TerrainType.PLAIN_PASS;
                    break;
                case RDefine.TERRAIN_PREF_OCEAN:
                    terrainType = TerrainType.OCEAN_N_PASS;
                    break;
                default:
                    terrainType = TerrainType.NONE;
                    break;
            }   //switch : �������� �ٸ� ������ �Ѵ�.

            tempTerrain.SetupTerrain(mapController, terrainType, loopCnt);
            tempTerrain.transform.SetAsFirstSibling();
            allTerrains.Add(tempTerrain);

            loopCnt += 1;

        }   //loop : Ÿ���� �̸��� ������ ������� �����ϴ� ����

        //} ������ �����ϴ� Ÿ���� ������ �����ϰ�, ��Ʈ�ѷ��� ĳ���ϴ� ����
    }   //Start()


    //! �ʱ�ȭ�� Ÿ���� ������ ������ ���� ����, ���� ũ�⸦ �����Ѵ�.
    public Vector2Int GetCellSize() { return mapCellSize; }

    //! �ʱ�ȭ�� Ÿ���� ������ ������ Ÿ�� ������ ���� �����Ѵ�.
    public Vector2 GetCellGap() { return mapCellGap; }

    //! �ε����� �ش��ϴ� Ÿ���� �����Ѵ�.
    public TerrainController GetTile(int tileIdx1D)
    {
        if (allTerrains.IsValid(tileIdx1D))
        {
            return allTerrains[tileIdx1D];
        }
        return default;
    }   //GetTile()

}
