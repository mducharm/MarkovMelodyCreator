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

	public void UpdateAllNodeNeighbors(int from, List<int> amounts)
	{
		var amountsAsFloats = amounts.Select(Convert.ToSingle);
		var total = amountsAsFloats.Sum();
		var ratios = amountsAsFloats.Select(x => x / total).ToArray();

		for (int i = 0; i < AdjacencyMatrix[from].Count; i++)
		{
			UpdateEdge(from, i, ratios[i]);
		}

	}

	public List<int> GetNeighborsAsIntegers(int from) => AdjacencyMatrix[from]
		.Select(x => x * 100)
		.Select(x => (int)x)
		.ToList();


	public float GetValue(int from, int to) => AdjacencyMatrix[from][to];

	public int Size() => AdjacencyMatrix.Count();

}
