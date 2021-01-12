using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class Pathfinding : MonoBehaviour {
	public GameObject door;
	public GameObject navMesh;
	public GameObject[] frontPoints;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("Door");
		if (!(IsIgnorableByNavMesh(door))) {
			MakeObjectIgnorableByNavMesh(door);
		}

		navMesh = CreateNavMesh();
		NavMeshSurface navMeshSurface = navMesh.GetComponent<NavMeshSurface>();
		navMeshSurface.BuildNavMesh();

		// GameObject newPlayer = CreatePlayer(door.transform.position);
		GameObject debugStartPoint = GameObject.Find("DebugStartPoint");
		// GameObject newPlayer = CreatePlayer(debugStartPoint.transform.position);

		// agent = newPlayer.GetComponent<NavMeshAgent>();
		frontPoints = GameObject.FindGameObjectsWithTag("frontPoint");
		NavMeshPath path = new NavMeshPath();
		foreach (GameObject frontPoint in frontPoints) {
			if (!(IsIgnorableByNavMesh(frontPoint))) {
				MakeObjectIgnorableByNavMesh(frontPoint);
			}
			// var watch = System.Diagnostics.Stopwatch.StartNew();
	        // agent.CalculatePath(frontPoint.transform.position, path);
	        NavMesh.CalculatePath(
	        	debugStartPoint.transform.position,
	        	// debugStartPoint.transform.position,
	        	frontPoint.transform.position,
	        	NavMesh.AllAreas,
	        	path
	        );
			// watch.Stop();
			// var elapsedMs = watch.ElapsedMilliseconds;
			Debug.Log(frontPoint.name + ": " + path.status);
		}	
	}
	
	// bool isPositionReachable(position) {

	// }

	static GameObject CreateNavMesh() {
		GameObject surface = new GameObject();
		surface.name = "NavMeshGenerated";
		surface.transform.position = new Vector3(0, 0, 0);
		surface.AddComponent<NavMeshSurface>();
		return surface;
	}

	static GameObject CreatePlayer(Vector3 position) {
		GameObject newPlayer = new GameObject();
		newPlayer.name = "Player";
		//newPlayer.transform.position = new Vector3(-9.3f, 1.25f, -4.94f);
		newPlayer.transform.position = position;
		newPlayer.AddComponent<NavMeshAgent>();
		return newPlayer;
	}

	static void MakeObjectIgnorableByNavMesh(GameObject obj) {
		obj.AddComponent<NavMeshModifier>();
		obj.GetComponent<NavMeshModifier>().ignoreFromBuild = true;
	}

	static bool IsIgnorableByNavMesh(GameObject obj) {
		NavMeshModifier navMeshModifier = obj.GetComponent<NavMeshModifier>();
		if (navMeshModifier == null) {
			return false;
		}
		return navMeshModifier.ignoreFromBuild;
	}
}
