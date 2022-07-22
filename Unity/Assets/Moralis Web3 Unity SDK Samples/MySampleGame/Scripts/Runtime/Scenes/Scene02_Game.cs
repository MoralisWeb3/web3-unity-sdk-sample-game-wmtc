using UnityEngine;

namespace MoralisUnity.Samples.MySampleGame.Scenes
{
	/// <summary>
	/// Replace with comments...
	/// </summary>
	public class Scene02_Game : MonoBehaviour
	{
		//  Properties ------------------------------------
		public string SamplePublicText { get { return _samplePublicText; } set { _samplePublicText = value; }}

		
		//  Fields ----------------------------------------
		private string _samplePublicText;

		
		//  Unity Methods----------------------------------
		protected void Start()
		{
			Debug.Log("Scene02_Game.Start()");
		}


		//  General Methods -------------------------------
		public string SamplePublicMethod(string message)
		{
			return message;
		}

		
		//  Event Handlers --------------------------------
		public void Target_OnCompleted(string message)
		{

		}
	}
}
