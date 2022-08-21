using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMelodyCreator.Data;

public class MidiService
{
    public void GenerateMidiFile(MarkovChain markovChain, List<NoteName> noteNames, int notes)
    {
        var count = 0;
        var currentNoteIdx = 0;

        PatternBuilder pb = new();


        pb = pb.SetNoteLength(MusicalTimeSpan.Eighth)
                .SetOctave(Octave.Get(5));

        while (count < notes)
        {
            var idx = GetNextNote(markovChain.AdjacencyMatrix[currentNoteIdx]);

            if (idx < 0)
                throw new Exception($"Failed when attempting to find next note after {currentNoteIdx} ({noteNames[currentNoteIdx]})");

            currentNoteIdx = idx;

            pb = pb.Note(noteNames[currentNoteIdx]);
            count++;
        }

        Pattern pattern = pb.Build();
        MidiFile file = pattern.ToFile(TempoMap.Default);

        file.Write($"C:\\Users\\sheve\\source\\repos\\MarkovMelodyCreator\\MarkovMelody_{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.mid", overwriteFile: true);
    }

    private int GetNextNote(List<float> neighbors)
    {
        float total = neighbors.Sum();
        float randomNumber = Convert.ToSingle(rand.NextDouble()) * total;

        float sum = 0;

        for (int i = 0; i < neighbors.Count; i++)
        {
            sum += neighbors[i];

            if (randomNumber < sum)
                return i;
        }

        return -1;
    }

    private static Random rand = new();
}
