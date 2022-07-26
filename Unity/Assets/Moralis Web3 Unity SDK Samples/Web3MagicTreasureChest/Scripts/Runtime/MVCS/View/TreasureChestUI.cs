using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Helpers;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the TreasureChest for the game
	/// </summary>
	public class TreasureChestUI : MonoBehaviour
	{
		// Properties -------------------------------------
		public Animator Animator { get { return _animator;}}
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private Animator _animator = null;

		[SerializeField]
		private ParticleSystem _raysParticleSystem = null;

		[SerializeField]
		private ParticleSystem _bubblesParticleSystem = null;

		//[Header("References (Project)")] 

		//  Unity Methods----------------------------------
		protected void Start()
		{
			_bubblesParticleSystem.Stop();
			_raysParticleSystem.gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
		}
		

		// General Methods --------------------------------
		public async UniTask TakeDamage()
		{
			_bubblesParticleSystem.Play();

			_animator.SetTrigger("TakeDamage");

			await UniTask.Delay(1000);
			return;
		}

		public async UniTask Open(bool willSkipWaiting)
		{
			_animator.SetTrigger("Open");

			int m = 1;
			if (willSkipWaiting)
            {
				m = 0;
            }
	
			await UniTask.Delay(2000 * m);

			if (_raysParticleSystem == null) return;
			TweenHelper.TransformDoScale(_raysParticleSystem.gameObject, new Vector3(.1f, .1f, .1f), new Vector3(1, 1, 1), 1, 0.1f);

			await UniTask.Delay(500 * m);

			return;
		}

		public async UniTask BounceWhileOpen()
		{
			_animator.SetTrigger("BounceWhileOpen");
			await UniTask.Delay(2000);

	

			return;
		}

		// Event Handlers ---------------------------------

	}
}
