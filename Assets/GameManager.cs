using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Terrain terrain;
    protected Transform fox;
    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
        fox = GameObject.Find("Fox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(fox.position.normalized);
        int treeCount = terrain.terrainData.treeInstanceCount;
        for (int i = 0; i < treeCount; i ++){
            TreeInstance tree = terrain.terrainData.treeInstances[i];
            Debug.Log(Vector3.Distance(fox.position, tree.position) + " " + tree.prototypeIndex);
            if(Vector3.Distance(fox.position, tree.position) <= 1078.0f){
                Debug.Log(tree.prototypeIndex.ToString());
                if (tree.prototypeIndex == 4){
                    Debug.Log("Red Bush");
                }
            }
        }
    }
}
