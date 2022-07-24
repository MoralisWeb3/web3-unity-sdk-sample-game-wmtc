using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene01_Intro : MonoBehaviour
    {
        //  Properties ------------------------------------
        public string SamplePublicText { get { return _samplePublicText; } set { _samplePublicText = value; }}

		
        //  Fields ----------------------------------------
        private string _samplePublicText;

		
        //  Unity Methods----------------------------------
        protected void Start()
        {
            Debug.Log("Scene01_Intro.Start()");
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