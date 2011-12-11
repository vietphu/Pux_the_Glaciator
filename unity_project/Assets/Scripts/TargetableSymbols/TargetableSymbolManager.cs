using System;
using Pux.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Pux
{
	public sealed class TargetableSymbolManager
	{
		private Dictionary<string, TargetableEntityBehaviour> targets;
		private readonly System.Random random;
		
		public TargetableSymbolManager ()
		{
			targets = new Dictionary<string, TargetableEntityBehaviour>();
			random = new System.Random();
		}
		
		public bool IsTargetableRegistered(string symbolChain){
			if (string.IsNullOrEmpty(symbolChain)) {
				return false;
			}
			return targets.ContainsKey(symbolChain);
		}
		
		public void RegisterTargetable(TargetableEntityBehaviour entity)
		{
			if (entity == null) {
				EditorDebug.Log("perk still null");
				return;
			}
			
			entity.SymbolChain = GenerateSymbolChain(entity.SymbolRange);
			targets.Add(entity.SymbolChain, entity);
		}
		
		public IEnumerable<TargetableEntityBehaviour> Targetables {
			get{ return targets.Values; }
		}
		
		public TargetableEntityBehaviour GetTargetable(string symbolChain){
			return targets[symbolChain];	
		}
		
		public void VoidTargetable(TargetableEntityBehaviour entity)
		{
			if (targets.ContainsKey(entity.SymbolChain)) {
				targets.Remove(entity.SymbolChain);	
				return;
			}
		}
		
		internal string GenerateSymbolChain(Range range)
		{
			var misses = -1;
			string chain = string.Empty; 
			int rnd1;
			do {
				chain = string.Empty;
				rnd1 = random.Next((int)range.From, (int)range.To+1);
				for (int i = 1; i <= rnd1; i++) {
					if (misses == range.To) {
						range = new Range(range.From, range.To + 1);
					}
					misses++;
					int rnd = random.Next(1, 5);
				
					switch (rnd) {
						case(1):
							chain+= "Q";
							break;
						case(2):
							chain+= "E";
							break;
						case(3):
							chain+= "Y";
							break;
						case(4):
							chain+= "C";
							break;
					}
				}
			} while (targets.ContainsKey(chain));
			return chain;	
		}
	}
}