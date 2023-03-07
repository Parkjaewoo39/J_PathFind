using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    protected string tileMapObjName = default;    
    protected MapBoard mapController = default;
    protected Tilemap tileMap = default;
    protected List<GameObject> allTileObj = default;

    //! Awake 타임에 초기화 할 내용을 상속받은 클래스별로 재정의한다.
    public virtual void InitAwake(MapBoard mapController_)
    {
        mapController = mapController_;
        tileMap = gameObject.FindChildComponent<Tilemap>(tileMapObjName);

        //직사각형 형태로 초기화 된 타일을 캐싱해서 가지고 있는다.
        allTileObj = tileMap.gameObject.GetChildrenObjs();
        if(allTileObj.IsValid())
        {
           allTileObj.Sort(GFunc.CompareTileObjToLocalPos2D);
        }
        else
        {
            allTileObj = new List<GameObject>();
        }
        /* To Do*/
    }   //InitAwake()
}   //class TileMapController
