using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarNode
{
    public TerrainController Terrain {get; private set;}
    public GameObject DestinationObj{get; private set;}
    //A star algorithm
    public float AstarF {get; private set;} = float.MaxValue;
    public float AstarG {get; private set;} = float.MaxValue;
    public float AstarH {get; private set;} = float.MaxValue;
    //MaxValue를 초기화 하는 이유
    //너무 작은 값을 넣으면 없는 길을 가버림

    //이전노드
    public AstarNode AstarPrevNode {get; private set;} = default;
    public AstarNode(TerrainController terrain_, GameObject destinationObj_)
    {
        Terrain = terrain_;
        DestinationObj = destinationObj_;
    }

    //! Astar 알고리즘에 사용할 비용을 설정한다.
    public void UpdateCost_Astar(float gCost, float heuristic, AstarNode prevNode)
    {
        float aStarF = gCost + heuristic;
        if(aStarF < AstarF) //작은경우에만 업데이트
        {
            AstarG = gCost;
            AstarH = heuristic;
            AstarF = aStarF;

            AstarPrevNode = prevNode;
            //if : 비용이 더 작은 경우에만 업데이트
        }
        else{/*Do nothing*/}//비용이 크면 아무것도 안혀

    }   //UpdateCost_Astar()

    //! 설정한 비용을 출력한다.
    public void ShowCost_Astar()
    {
        GFunc.Log($"TileIdx1D: {Terrain.TileIdx1D}, " + 
        $"F: {AstarF}, G: {AstarG}, H: {AstarH}");
    }   //ShowCost_Astar()
    
}
