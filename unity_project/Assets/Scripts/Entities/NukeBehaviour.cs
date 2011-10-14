using System;
using UnityEngine;
using HappyPenguin.Effects;
using System.Collections.Generic;
using HappyPenguin.Spawning;
using HappyPenguin.Entities;


public sealed class NukeBehaviour : PerkBehaviour
{
	
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		Init ();
		
	}

	private void Init ()
	{
		CollectedEffects.Add (new NukeKillEffect (this));
		NotCollectedEffects.Add (new PerkVanishEffect (this));
	}
	
}