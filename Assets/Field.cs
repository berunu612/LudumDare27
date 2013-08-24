using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
	
	public GameObject cellPrefab;
	
	// Use this for initialization
	void Start () {
		GameContext.field = this;
		GameObject tempObj;
		for( int x=-8;x<=8;++x )
		{
			for( int z=0;z<32;++z )
			{
				tempObj = (GameObject)Instantiate( cellPrefab, new Vector3(x,0,z), cellPrefab.transform.rotation );
				tempObj.transform.parent = this.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
