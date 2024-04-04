﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace Mascari4615
{
	// [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class ObjectActive : MEventSender
	{
		[Header("_" + nameof(ObjectActive))]
		[SerializeField] private GameObject[] activeObjects;
		[SerializeField] private GameObject[] disableObjects;
		[SerializeField] private Image[] buttonUIImages;
		[SerializeField] private Sprite[] buttonUISprites;
		[SerializeField] private bool defaultActive;
		[SerializeField] private CustomBool customBool;

		private bool active;
		public bool Active
		{
			get => active;
			private set
			{
				active = value;
				OnActiveChange();
			}
		}

		private void Start()
		{
			if (!customBool)
			{
				Active = defaultActive;
			}
			else
			{

			}

			OnActiveChange();
		}

		public void SetActive(bool targetActive)
		{
			MDebugLog($"{nameof(SetActive)}({targetActive})");

			Active = targetActive;
		}

		public void ToggleActive() => SetActive(!Active);
		public void SetActiveTrue() => SetActive(true);
		public void SetActiveFalse() => SetActive(false);

		public void UpdateValue()
		{
			if (customBool)
				SetActive(customBool.Value);
		}

		public void SetCustomBool(CustomBool customBool)
		{
			this.customBool = customBool;
			UpdateValue();
		}

		private void OnActiveChange()
		{
			MDebugLog($"{nameof(OnActiveChange)}");

			foreach (var i in buttonUIImages)
			{
				if (buttonUISprites != null && buttonUISprites.Length > 0)
					i.sprite = buttonUISprites[Active ? 0 : 1];
				else
					i.color = GetGreenOrRed(Active);
			}

			foreach (var o in activeObjects)
				if (o)
				{
					MDebugLog(o.name);
					o.SetActive(Active);
				}

			foreach (var o in disableObjects)
				if (o)
				{
					MDebugLog(o.name);
					o.SetActive(!Active);
				}

			SendEvents();
		}
	}
}