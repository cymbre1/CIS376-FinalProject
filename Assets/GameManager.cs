using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Terrain terrain;
    protected GameObject fox;
    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
        fox = GameObject.Find("Fox");
    }

    // Update is called once per frame
    void Update()
    {
        // int treeCount = terrain.terrainData.treeInstanceCount;
        // for (int i = 0; i < treeCount; i ++){
        //     TreeInstance tree = terrain.terrainData.treeInstances[i];

        //     if (tree.prototypeIndex == 4){
        //         var treePos = tree.position;
        //         var xTree = treePos.x * terrain.terrainData.size.x;
        //         var zTree = treePos.z * terrain.terrainData.size.z;
        //         var yTree = treePos.y * terrain.terrainData.size.y;
        //         treePos = new Vector3(xTree, yTree, zTree);

        //         if(Vector3.Distance(fox.transform.position, treePos) <= 2.0f){
        //             fox.hidden = True;
        //             break;
        //         }
        //         else{
        //             fox.hidden = False;
        //         }
        //     }
        // }
    }
}
