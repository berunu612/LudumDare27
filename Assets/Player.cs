using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	bool isStopping, isFound;
	float CurrentVolume;
	
	// Use this for initialization
	void Start () {
		GameContext.player = this;
	}
	
	// Update is called once per frame
	void Update () {
		if ( isFound )
		{
			if ( audio.isPlaying )
			{
				float shakeTime = (float)Music.mtUnit/2;
				if ( (int)( audio.time / shakeTime ) > (int)( ( audio.time - Time.deltaTime ) / shakeTime ) )
				{
					Vector2 next = Random.insideUnitCircle*(audio.clip.length - audio.time)/10;
					transform.position = new Vector3( next.x/2, 1+next.y, transform.position.z );
				}
			}
			else
			{
				transform.position = new Vector3( 0, 1, 0 );
				isFound = false;
				GameContext.eye.Restart();
			}
		}
		else
		{
			Walk();
		}
	}

	void Walk()
	{
		if ( Music.IsPlaying() )
		{
			if ( Input.GetKeyUp( KeyCode.UpArrow ) )
			{
				isStopping = true;
			}

			if ( isStopping )
			{
				if ( CurrentVolume > 0 )
				{
					CurrentVolume -= Time.deltaTime*2.0f;
					if ( CurrentVolume <= 0 )
					{
						Music.Pause();
					}
					else
					{
						Music.SetVolume( CurrentVolume );
					}
				}
			}
			else
			{
				if ( CurrentVolume < 1 )
				{
					CurrentVolume += Time.deltaTime*2.0f;
					if ( CurrentVolume >= 1 ) CurrentVolume = 1.0f;
				}
				Music.SetVolume( CurrentVolume );
			}
			transform.position = new Vector3( 0, 1, (float)Music.MusicalTime/4 );
		}
		else
		{
			if ( Input.GetKeyDown( KeyCode.UpArrow ) )
			{
				Music.Resume();
				isStopping = false;
			}
		}
	}

	public void Restart()
	{
		isFound = true;
		audio.Play();
	}
}
