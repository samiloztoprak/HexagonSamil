using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject baseHexagon;
    public Color[] colors;
    public int colorNumber;
    public float size;
    public float offset;
    public Vector2Int gridSize;
    private GameObject[] _hexagonList;
    private HexagonModel[] _hexagonModels;
    void Start()
    {
        Vector2 screenDimensions = CalculateScreenSizeInWorldCoords();
        Vector2 startPosition = new Vector2((screenDimensions.x/8),screenDimensions.y/8);
        
        _hexagonList = new GameObject[gridSize.x*gridSize.y];
        _hexagonModels = new HexagonModel[gridSize.x*gridSize.y];
        //_hexagonList[0] = Instantiate(baseHexagon);
        //_hexagonList[0].GetComponent<HexagonModel>().position=startPosition;

        float xposition=startPosition.x;
        float yposition=startPosition.y;

        size=5f/gridSize.x;
        offset=(5*0.9f)/gridSize.x;

        for(int i=0;i<_hexagonList.Length;i++){

            _hexagonList[i] = Instantiate(baseHexagon);
            _hexagonList[i].GetComponent<HexagonModel>().position=new Vector2(xposition,yposition);
            _hexagonList[i].GetComponent<HexagonModel>().neighbor = NeighborCalculate(i);
            _hexagonList[i].transform.localScale=new Vector2(size,size);
            yposition+=offset;
            
            if((i%gridSize.y)+1==gridSize.y){
                xposition+=offset;
                if((i/gridSize.y)%2==0)
                    yposition=startPosition.y+offset/2;
                else
                    yposition=startPosition.y;
            }
            _hexagonModels[i]=_hexagonList[i].GetComponent<HexagonModel>();
            
            //_hexagonList[i].GetComponent<HexagonModel>().color=colors[Random.Range(0,colorNumber)];
        }
        for(int i=0;i<_hexagonList.Length;i++){
            PaintColor(i);
        }

        
    }

    int[] NeighborCalculate(int i){
        //üstü ve sonu görmüyor ondan dolayı sıkıntılı

        int leftbottom=0,lefttop=0,top=0,righttop=0,rightbottom=0,bottom=0;
        
        //far left 
        if(i<gridSize.y){
            leftbottom=-1;
            lefttop=-1;
        }
        //far right
        if(i>=(gridSize.y*(gridSize.x-1))){
            rightbottom=-1;
            righttop=-1;
        }
        //far top
        if(i%gridSize.y==gridSize.y-1){
            top=-1;
            if((i/gridSize.y)%2==1){
                righttop=-1;
                lefttop=-1;
            }
        }
        //far bottom
        if(i%gridSize.y==0){
            bottom=-1;
            if((i/gridSize.y)%2==0){
                rightbottom=-1;
                leftbottom=-1;
            }
        }

        if(leftbottom!=-1)leftbottom=i-gridSize.y;
        if(lefttop!=-1)lefttop=(i-gridSize.y)+1;
        if(top!=-1)top=i+1;
        if(righttop!=-1)righttop=i+gridSize.y;
        if(rightbottom!=-1)rightbottom=(i+gridSize.y)-1;
        if(bottom!=-1)bottom=i-1;

        return new int[]{leftbottom,lefttop,top,righttop,rightbottom,bottom};
    }

    void Update()
    {
        
    }

    Vector2 CalculateScreenSizeInWorldCoords(){
        Camera cam = Camera.main;
        var p1 = cam.ViewportToWorldPoint(new Vector3(0,0,cam.nearClipPlane));
        var p2 = cam.ViewportToWorldPoint(new Vector3(1,0,cam.nearClipPlane));
        var p3 = cam.ViewportToWorldPoint(new Vector3(1,1,cam.nearClipPlane));

        var width = (p2 - p1).magnitude;
        var height = (p3 - p2).magnitude;
        return new Vector2(width,height);
    }

    void PaintColor(int i){
        if(i%gridSize.y==0){
            _hexagonModels[i].color=colors[Random.Range(0,colorNumber)];
        }
        else{
            Color[] newColors = new Color[colors.Length];
            for(int j=0;j<newColors.Length;j++){
                newColors[j]=colors[j];
            }
            for(int j=0;j<newColors.Length;j++){
                if(newColors[j] == _hexagonModels[i-1].color){
                    if(j!=0){
                        newColors[j]=newColors[j-1];
                    }
                    else{
                        newColors[j]=newColors[j+1];
                    }
                }
            }
            _hexagonModels[i].color=newColors[Random.Range(0,colorNumber)];
        }
    }
}
