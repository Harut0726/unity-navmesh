using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	public Camera cam;
	public NavMeshAgent agent;
	public GameObject door;

	void Start () {
		GameObject newPlayer = CreatePlayer();
		agent = newPlayer.GetComponent<NavMeshAgent>();

		Debug.Log(agent.transform.position);
		door = GameObject.Find("Door");
		Debug.Log(door.transform.position);
		// agent.nextPosition(door.transform.position);
		agent.transform.position = door.transform.position;
		Debug.Log(agent.transform.position);
	}	

	void Update() {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				// MOVE OUR AGENT
				agent.SetDestination(hit.point);
			}
		}
	}

	GameObject CreatePlayer() {
		GameObject newPlayer = new GameObject();
		newPlayer.name = "Player2";
		newPlayer.transform.position = new Vector3(-9.3f, 1.25f, -4.94f);
		newPlayer.AddComponent<NavMeshAgent>();
		return newPlayer;
	}

	// void Update() {
	// 	if (Input.GetMouseButtonDown(0))
	// 	{
	// 		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	// 		RaycastHit hit;

	// 		if (Physics.Raycast(ray, out hit))
	// 		{
	// 			NavMeshPath path = new NavMeshPath();
	// 			var watch = System.Diagnostics.Stopwatch.StartNew();
	// 	        agent.CalculatePath(hit.point, path);
	// 			watch.Stop();
	// 			var elapsedMs = watch.ElapsedMilliseconds;
	// 			Debug.Log(elapsedMs);
	// 			Debug.Log(path.status);
	// 	        // if (path.status == NavMeshPathStatus.PathPartial)
	// 	        // {
	// 	        // 	Debug.Log("True");
	// 	        // }
	// 	        // else 
	// 	        // {
	// 	        // 	Debug.Log("False");
	// 	        // }
	// 		}
	// 	}
	// }
}
