using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static partial class GFunc
{
    //!카메라 사이즈를 리턴하는 함수
    public static Vector2 GetCameraSize()
    {
        Vector2 cameraSize = Vector2.zero;
        cameraSize.y = Camera.main.orthographicSize * 2.0f;
        cameraSize.x = cameraSize.y * Camera.main.aspect;

        //카메라 사이즈 구하는 공식

         /*
        //orthographicSize 는 원근법이 적용되지 않은 카메라 시점. 
        //수직크기의 절반
        //2D에서 쓰임 - 멀리 가도 그대로
        //수직 크기의 절반
        //Perspective 는 원근법이 적용된것
        //3D에서 쓰임 - 멀리 가면 더 많이 보임.
        //Size의 정확한 의미
        //
        */
        return cameraSize;
    }
    //! ??????????? ?????? ????????? ?????? ??????? ???
    public static void SetTmpText(this GameObject obj_, string text_)
    {
        TMP_Text tmpTxt = obj_.GetComponent<TMP_Text>();
        if (tmpTxt == null || tmpTxt == default(TMP_Text))
        {
            return;
        }       // if: ?????? ??????? ????????? ???? ???

        // ?????? ??????? ????????? ??????? ???
        tmpTxt.text = text_;
    }       // SetTextMeshPro()
}
