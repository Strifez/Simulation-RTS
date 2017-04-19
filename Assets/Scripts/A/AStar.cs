using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

	//Make 2 Lists, one is for open nodes and one is for ClOSED nodes

	//add the start node to the OPEN list


	//Create a loop to calculate F cost

	//remove current node and put it in the CLOSED list

	// if the current node is the target node , path has been found

		// foreach loop used to look at neighbour nodes
		// if the node is in the closed list skip to the next node

		// if it is in the OPEN list or if the neightbour is not in the OPEN list, calculated the F cost and G cost, set the parent of the neighbour node to the current
		// if its neighbour is not in the open list add it into the open list

	//REPEAT until path is found

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
