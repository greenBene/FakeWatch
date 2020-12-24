using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbteilungF.SNF
{
	public interface INodeFactory
	{
		List<Node> GetNodes();
	}
}
