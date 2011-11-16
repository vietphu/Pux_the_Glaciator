using System;
using Pux.Controllers;
using UnityEngine;
using Pux.Unity2;

namespace Pux.UI
{
	public sealed class UIElementSlideController : Controller<MonoBehaviour>
	{
		private Func<float, float> function;
		private TimeSpan elapsedTime = TimeSpan.Zero; 

		public UIElementSlideController()
			: this((x) => x) { }
		
		public UIElementSlideController(Func<float, float> function) {
			this.function = function;
		}
		
		public TimeSpan Duration {
			get;
			set;
		}
		
		public Vector2 StartPosition {
			get;
			set;
		}
		
		public Vector2 TargetPosition {
			get;
			set;
		}
		
		protected override void UpdateOverride (MonoBehaviour entity)
		{	
			if (IsFinished || entity == null) {
				return;
			}
			
			elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
			
			if (elapsedTime >= Duration) {
				elapsedTime = TimeSpan.Zero;
				InvokeControllerFinished(entity);
				return;
			}
			
			var relTime = (float)elapsedTime.TotalMilliseconds / (float)Duration.TotalMilliseconds;
			if (relTime > 1.0f) {
				relTime = 1.0f;
			}
			var relDistance = function(relTime); 
			// need more speed for we do operate on a larger scale than in game 
			
			var vec = (TargetPosition - StartPosition);
			
			//HACK for mono bug, something with trampolines
			var ui = (Panel)entity;
			ui.Position = StartPosition + (vec * relDistance); 
		}		
	}
}

