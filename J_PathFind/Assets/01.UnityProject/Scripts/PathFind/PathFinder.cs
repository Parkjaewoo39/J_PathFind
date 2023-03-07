using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    #region 지형 탐색을 위한 변수
    public GameObject sourceObj = default;
    public GameObject destinationObj = default;
    public MapBoard mapBoard = default;
    #endregion  //지형 탐색을 위한 변수

    #region A star 알고리즘으로 최단거리를 찾기 위한 변수
    private List<AstarNode> aStarResultPath = default;
    private List<AstarNode> aStarOpenPath = default;
    private List<AstarNode> aStarClosePath = default;
    #endregion  //A star 알고리즘으로 최단거리를 찾기 위한 변수

    //! 비용을 설정한 노드를 Open 리스트에 추가한다.
    private void Add_AstarOpenList(AstarNode targetTerrain_, AstarNode prevNode = default)
    {
        //Open 리스트에 추가하기 전에 알고리즘 비용을 설정한다.
        Update_AstarCostToTerrain(targetTerrain_, prevNode);

        //AstarNode

    }   //Add_AstarOpenList()

    //! Target 지형 정보와 Destination 지형 정보로 Distance 와 Heuristic 을 설정하는 함수
    private void Update_AstarCostToTerrain(
        AstarNode targetNode, AstarNode prevNode)
    {
        //{Target 지형에서 Destination 까지의 2D 타일 거리를 계산하는 로직
        Vector2Int distance2D = mapBoard.GetDistance2D(
            targetNode.Terrain.gameObject, destinationObj);

        int totalDistance2D = distance2D.x + distance2D.y;

        //Heuristic 은 직선거리로 고정한다.
        Vector2 localDistance = destinationObj.transform.localPosition - 
        targetNode.Terrain.transform.localPosition;

        float heuristic = Mathf.Abs(localDistance.magnitude);
        //}Target 지형에서 Destination 까지의 2D 타일 거리를 계산하는 로직

        //{ 이전 노드가 존재하는 경우 이전 노드의 코스트를 추가해서 연산한다.
        if(prevNode == default || prevNode == null){/*Do nothing*/}
        else
        {
            totalDistance2D = Mathf.RoundToInt(prevNode.AstarG + 1.0f );
        }
        targetNode.UpdateCost_Astar(totalDistance2D, heuristic, prevNode);

        //} 이전 노드가 존재하는 경우 이전 노드의 코스트를 추가해서 연산한다.

    }   //Update_AstarCostToTerrain()
}
