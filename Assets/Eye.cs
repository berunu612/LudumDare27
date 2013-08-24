using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {

	enum State
	{
		Closed,
		Opening,
		Opened
	}
	State state;
	Vector3 initialScale;
	
	// Use this for initialization
	void Start () {
		initialScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		switch ( state )
		{
		case State.Closed:
			if ( Random.Range( 0, 30 ) == 0 )
			{
				state = State.Opening;
				audio.Play();
			}
			break;
		case State.Opening:
			if ( audio.isPlaying )
			{
				float t = audio.time;
				float l = audio.clip.length/2;
				transform.localScale = new Vector3( initialScale.x, 1, ( t<l ? initialScale.z : initialScale.z + ( ( initialScale.x-initialScale.z ) * ( t-l )/l ) ) );
			}
			else
			{
				state = State.Opened;
			}
			break;
		case State.Opened:
			if ( Music.IsPlaying() )
			{
				Music.Stop();
				GameContext.player.Restart();
			}
			if ( Random.Range( 0, 30 ) == 0 )
			{
				state = State.Closed;
				transform.localScale = initialScale;
			}
			break;
		}
		
	}
}
