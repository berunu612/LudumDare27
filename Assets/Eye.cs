using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {

	enum State
	{
		Closed,
		Opening,
		Opened,
		Catched,
	}
	State state;
	Vector3 initialScale;
	public Material cellMaterial;
	
	// Use this for initialization
	void Start () {
		GameContext.eye = this;
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
				cellMaterial.color = Color.red;
			}
			break;
		case State.Opened:
			if ( Music.IsPlaying() )
			{
				Music.Stop();
				GameContext.player.Restart();
				state = State.Catched;
			}
			if ( Random.Range( 0, 30 ) == 0 )
			{
				state = State.Closed;
				cellMaterial.color = Color.white;
				transform.localScale = initialScale;
			}
			break;
		case State.Catched:
			break;
		}
		
	}

	public void Restart()
	{
		state = State.Closed;
		cellMaterial.color = Color.white;
		transform.localScale = initialScale;
	}
}
