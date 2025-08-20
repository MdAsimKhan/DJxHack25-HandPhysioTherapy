//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//public class SceneManager : MonoBehaviour
//{
//    [SerializeField] private GameObject objectToSpawn; // Assign your prefab in the Inspector

//    void Start()
//    {
//        TryGetWalls();
//    }

//    public void TryGetWalls()
//    {
//        var planes = FindObjectsByType<ARPlane>(FindObjectsSortMode.None);
//        foreach (var plane in planes)
//        {
//            Debug.Log(plane.name);
//            if (plane.classifications == PlaneClassifications.WallFace)
//            {
//                Debug.Log("Found a wall plane.");
//                SpawnObjectOnWall(plane);
//            }
//        }
//    }

//    private void SpawnObjectOnWall(ARPlane wallPlane)
//    {
//        if (objectToSpawn == null) return;

//        // Get the plane's center and size
//        Vector3 center = wallPlane.center;
//        Vector2 size = wallPlane.size;

//        // Pick a random point within the plane's bounds
//        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
//        float randomY = Random.Range(-size.y / 2f, size.y / 2f);
//        Vector3 randomPoint = wallPlane.transform.TransformPoint(new Vector3(randomX, randomY, 0));

//        // Instantiate the object at the random point, aligned with the wall
//        Instantiate(objectToSpawn, randomPoint, wallPlane.transform.rotation);
//    }
//}
