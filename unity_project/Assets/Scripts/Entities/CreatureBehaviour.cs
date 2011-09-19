using System;
using System.Collections.Generic;
using HappyPenguin.Effects;
using HappyPenguin.Entities;
using HappyPenguin;

public class CreatureBehaviour : TargetableEntityBehaviour{
	
	public float Points;
	public float Damage;
	
	protected override void AwakeOverride()
	{
		base.AwakeOverride();
		AttackEffects = new List<Effect>();
		KillEffects = new List<Effect>();
		init();
	}
	
	private void init(){
		KillEffects.Add(new PointEffect(Points));
		AttackEffects.Add(new LifeEffect(Damage));
	}

	public List<Effect> AttackEffects { get; protected set; }

}


