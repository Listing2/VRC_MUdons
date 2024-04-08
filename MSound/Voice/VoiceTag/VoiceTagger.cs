using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
	public class VoiceTagger : MBase
	{
		[Header("_" + nameof(VoiceTagger))]
		[SerializeField] protected VoiceManager voiceManager;
		[field: SerializeField] public VoiceAreaTag Tag { get; private set; }
		[SerializeField] private float updateTerm = .5f;

		private bool isLocalPlayerIn;
		private bool isSomeoneIn;

		protected virtual void Start() => UpdateVoiceLoop();
		public void UpdateVoiceLoop()
		{
			SendCustomEventDelayedSeconds(nameof(UpdateVoiceLoop), updateTerm);
			UpdateAllTag();
		}

		protected virtual void UpdateAllTag()
		{
			if (NotOnline)
				return;

			if (voiceManager.PlayerApis == null ||
				voiceManager.PlayerApis.Length != VRCPlayerApi.GetPlayerCount())
			{
				isLocalPlayerIn = false;
				isSomeoneIn = false;
				return;
			}

			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
			{
				bool isIn = IsPlayerIn(voiceManager.PlayerApis[i]);

				UpdatePlayerTag(voiceManager.PlayerApis[i], isIn);

				isSomeoneIn = isSomeoneIn || isIn;
				if (voiceManager.PlayerApis[i].isLocal)
					isLocalPlayerIn = isIn;
			}
		}

		public virtual bool IsPlayerIn(VRCPlayerApi player) { return true; }

		private bool UpdatePlayerTag(VRCPlayerApi player, bool isIn)
		{
			// MDebugLog($"{playerID}{Tag}" + (isin ? TRUE_STRING : FALSE_STRING));
			Networking.LocalPlayer.SetPlayerTag($"{player.playerId}{Tag}", isIn ? TRUE_STRING : FALSE_STRING);
			return isIn;
		}
	}
}