using System;
namespace Pux
{
	public struct Range
	{
		public Range (float @from, float to)
		{
			From = @from;
			To = to;
		}
		
		public float From;
		public float To;
	}
}

