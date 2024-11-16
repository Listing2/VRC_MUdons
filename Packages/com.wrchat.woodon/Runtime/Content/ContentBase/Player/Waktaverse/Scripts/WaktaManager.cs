using UdonSharp;
using UnityEngine;

namespace WRC.Woodon
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
	public class WaktaManager : MEventSender
	{
		[field: Header("_" + nameof(WaktaManager))]
		[field: SerializeField] public WaktaMemberData[] Datas { get; set; }

		public static WaktaManager GetInstance()
		{
			return GameObject.Find(nameof(WaktaManager)).GetComponent<WaktaManager>();
		}

		public WaktaMemberData GetData(WaktaMember waktaMember)
		{
			foreach (WaktaMemberData data in Datas)
				if (data.Member == waktaMember)
					return data;

			return null;
		}

		public WaktaMember[] GetEnabledMembersByType(WaktaMemberType type)
		{
			WaktaMember[] members = WaktaUtil.GetMembersByType(type);
			
			int enabledMemberCount = 0;
			foreach (WaktaMember member in members)
				if (Datas[(int)member].RuntimeBool)
					enabledMemberCount++;

			WaktaMember[] enabledMembers = new WaktaMember[enabledMemberCount];

			int enabledMemberIndex = 0;
			foreach (WaktaMember member in members)
				if (Datas[(int)member].RuntimeBool)
					enabledMembers[enabledMemberIndex++] = member;

			return enabledMembers;
		}
	}
}