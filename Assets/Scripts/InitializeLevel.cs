using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    //Attributes
    //References
    //OpenSpace
    [SerializeField]
    private List<GameObject> _prefabOSAngularDesk;

    [SerializeField]
    private Transform _transformOSAngularDesk;

    [SerializeField]
    private List<GameObject> _prefabOSStandardDesk;

    [SerializeField]
    private Transform _transformOSStandardDesk;

    //LunchRoom
    [SerializeField]
    private List<GameObject> _prefabLunchRoom;

    [SerializeField]
    private Transform _transformLunchRoom;

    //BreakRoom
    [SerializeField]
    private List<GameObject> _prefabBRTables;

    [SerializeField]
    private Transform _transformBRTables;

    [SerializeField]
    private List<GameObject> _prefabBRTennisTables;

    [SerializeField]
    private Transform _transformBRTennisTables;

    [SerializeField]
    private List<GameObject> _prefabBRKitchen;

    [SerializeField]
    private Transform _transformBRKitchen;

    [SerializeField]
    private List<GameObject> _prefabBRCoffeeTables;

    [SerializeField]
    private Transform _transformBRCoffeeTables;

    //Meeting room
    [SerializeField]
    private List<GameObject> _prefabMRDesk;

    [SerializeField]
    private Transform _transformMRDesk;

    [SerializeField]
    private List<GameObject> _prefabMRTables;

    [SerializeField]
    private Transform _transformMRTables;

    //Entrance
    [SerializeField]
    private List<GameObject> _prefabECoffeeTables;

    [SerializeField]
    private Transform _transformECoffeeTables;

    [SerializeField]
    private List<GameObject> _prefabEOffice;

    [SerializeField]
    private Transform _transformEOffice;

    //Waiting room
    [SerializeField]
    private List<GameObject> _prefabWRCoffeeTables;

    [SerializeField]
    private Transform _transformWRCoffeeTables;

    [SerializeField]
    private List<GameObject> _prefabWRFloor;

    [SerializeField]
    private Transform _transformWRFloor;

    //Director office
    [SerializeField]
    private List<GameObject> _prefabDOShelves;

    [SerializeField]
    private Transform _transformDOShelves;

    [SerializeField]
    private List<GameObject> _prefabDODesk;

    [SerializeField]
    private Transform _transformDODesk;

    //Toilets
    [SerializeField]
    private List<GameObject> _prefabWC;

    [SerializeField]
    private Transform _transformWC;

    //Win object
    [SerializeField]
    private GameObject _prefabWin;

    [SerializeField]
    private Transform _transformWinList;



    //Functions
    public void Initialize()
    {
        //Open space
        SpawnRandomPrefabsAtPoints(_prefabOSAngularDesk, _transformOSAngularDesk);
        SpawnRandomPrefabsAtPoints(_prefabOSStandardDesk, _transformOSStandardDesk);

        //Lunch room
        SpawnRandomPrefabsAtPoints(_prefabLunchRoom, _transformLunchRoom);

        //Break room
        SpawnRandomPrefabsAtPoints(_prefabBRCoffeeTables, _transformBRCoffeeTables);
        SpawnRandomPrefabsAtPoints(_prefabBRKitchen, _transformBRKitchen);
        SpawnRandomPrefabsAtPoints(_prefabBRTables, _transformBRTables);
        SpawnRandomPrefabsAtPoints(_prefabBRTennisTables, _transformBRTennisTables);

        //Meeting room
        SpawnRandomPrefabsAtPoints(_prefabMRDesk, _transformMRDesk);
        SpawnRandomPrefabsAtPoints(_prefabMRTables, _transformMRTables);

        //Entrance
        SpawnRandomPrefabsAtPoints(_prefabEOffice, _transformEOffice);
        SpawnRandomPrefabsAtPoints(_prefabECoffeeTables, _transformECoffeeTables);

        //Waiting room
        SpawnRandomPrefabsAtPoints(_prefabWRCoffeeTables, _transformWRCoffeeTables);
        SpawnRandomPrefabsAtPoints(_prefabWRFloor, _transformWRFloor);

        //Director office
        SpawnRandomPrefabsAtPoints(_prefabDODesk, _transformDODesk);
        SpawnRandomPrefabsAtPoints(_prefabDOShelves, _transformDOShelves);

        //Toilets
        SpawnRandomPrefabsAtPoints(_prefabWC, _transformWC);

        //Win object
        SpawnPrefabAtRandomPoint(_prefabWin, _transformWinList);
    }

    //Make the given object spawn at a random point
    private void SpawnPrefabAtRandomPoint(GameObject prefab, Transform transformList)
    {
        //Get a transform
        int rand = transformList.childCount == 0 ? -1 : Random.Range(0, transformList.childCount);
        Transform trans = rand == -1 ? transformList :  transformList.GetChild(rand).transform;
        Instantiate(prefab, trans.position, trans.rotation);
    }

    //Make one random object of the given prefab list spawn at every given transform 
    private void SpawnRandomPrefabsAtPoints(List<GameObject> prefabtList, Transform transformList)
    {
        //Get a random number for the prefab list
        int rand;

        //Get number of child
        int numberOfChild = transformList.childCount;

        //Make one prefab spawn at the spawn point
        if(numberOfChild == 0)
        {
            Transform trans = transformList;
            rand = Random.Range(0, prefabtList.Count);
            GameObject prefab = prefabtList[rand];
            Instantiate(prefab, trans.position, trans.rotation);
        }


        //Make a prefab spawn at every points
        else
        {
            for (int i = 0; i < transformList.childCount; i++)
            {
                Transform trans = transformList.GetChild(i);
                rand = Random.Range(0, prefabtList.Count);
                GameObject prefab = prefabtList[rand];
                Instantiate(prefab, trans.position, trans.rotation);
            }
        }
       
    }
}
