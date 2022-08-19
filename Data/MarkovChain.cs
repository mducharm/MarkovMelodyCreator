using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMelodyCreator.Data;

public class MarkovChain
{
	public List<List<float>> AdjacencyMatrix;

	public MarkovChain(int size)
	{
		var newRow = () => Enumerable.Range(0, size)
									.Select(num => 0f)
									.ToList();

        AdjacencyMatrix = Enumerable.Range(0, size)
			.Select(_ => newRow())
			.ToList();
	}

	public void UpdateEdge(int from, int to, float newValue)
	{
		AdjacencyMatrix[from][to] = newValue;
	}

	public float GetValue(int from, int to) => AdjacencyMatrix[from][to];

}
