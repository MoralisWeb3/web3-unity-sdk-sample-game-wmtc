using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Samples.Shared.Audio;
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
			InitDuringAutomaticBouncing();

		}

		/// <summary>
		/// The animator automatically plays through to a bounce animation. So just time
		/// this by coincidence to happen every scene
		/// </summary>
		private void InitDuringAutomaticBouncing()
		{
			_bubblesParticleSystem.Stop();
			_raysParticleSystem.gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);

			PlayAudioClipOpen();

		}

		private async void PlayAudioClipOpen()
		{
			TheGameSingleton.Instance.TheGameController.PlayAudioClip(TheGameHelper.GetAudioClipIndexChestHit01());
			await UniTask.Delay((500));
			
			TheGameSingleton.Instance.TheGameController.PlayAudioClip(TheGameHelper.GetAudioClipIndexChestHit02());
			await UniTask.Delay((500));
			
			TheGameSingleton.Instance.TheGameController.PlayAudioClip(TheGameHelper.GetAudioClipIndexChestHit01());
			await UniTask.Delay((500));

		}

		// General Methods --------------------------------
		public async UniTask TakeDamageAsync()
		{
			_bubblesParticleSystem.Play();

			_animator.SetTrigger("TakeDamage");

			await UniTask.Delay(1000);
			return;
		}

		public async UniTask OpenAsync()
		{
			PlayAudioClipOpen();
			_animator.SetTrigger("Open");
			await UniTask.Delay(2000 );
			
			if (_raysParticleSystem == null) return;
			TweenHelper.TransformDoScale(_raysParticleSystem.gameObject, new Vector3(.1f, .1f, .1f), new Vector3(1, 1, 1), 1, 0.1f);

			await UniTask.Delay(500);

			return;
		}

		public async UniTask BounceWhileOpenAsync()
		{
			_animator.SetTrigger("BounceWhileOpen");
			await UniTask.Delay(2000);
		}

		// Event Handlers ---------------------------------

	}
}
