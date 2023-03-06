using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    private const string TERRAIN_MAP_OJB_NAME = "TerrainMap";

    public Vector2Int MapCellSize { get; private set; } = default;
    public Vector2 MapCellGap { get; private set; } = default;

    private TerrainMap terrainMap = default;
    private void Awake()
    {
        //{각종 매니저를 모두 초기화 한다.
        ResManager.Instance.Create();

        //}각종 매니저를 모두 초기화 한다.

        //맵에 지형을 초기화하여 배치한다.
        terrainMap = gameObject.FindChildComponent<TerrainMap>(TERRAIN_MAP_OJB_NAME);
        terrainMap.InitAwake(this);
        //함수들이 선언되는 순서가 없어서
        //순서를 정하는 것인데
        //awake에서 ininawake(this )로 하면 순서가 빨라질 일은 없다.
        MapCellSize = terrainMap.GetCellSize();
        MapCellGap = terrainMap.GetCellGap();

    }

}
