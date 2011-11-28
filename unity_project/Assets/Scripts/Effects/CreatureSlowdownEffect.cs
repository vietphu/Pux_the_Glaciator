using System;
using UnityEngine;
namespace Pux.Effects
{
	public sealed class CreatureSlowdownEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			world.RegisterEffect(this);
			world.ModifyCreatures((x) => x.Speed *= 0.8f);
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.ModifyCreatures((x) => x.Speed = x.DefaultSpeed);
		}
		
		#endregion
		public CreatureSlowdownEffect() {
			Duration = TimeSpan.FromSeconds(4);
			IconResourceUV = new Rect(0,197,144,144);
		}
		
		public override string Description {
			get {
				return "Bullet Time!";
			}
		}
	}
}

