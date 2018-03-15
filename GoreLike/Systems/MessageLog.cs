using RLNET;
using System;
using System.Collections.Generic;

namespace GoreLike.Systems {
    public class MessageLog {
        private static readonly int max_lines = 9;

        private readonly Queue<String> lines;

        public MessageLog() {
            lines = new Queue<string>();
        }

        public void Add(string message) {
            lines.Enqueue(message);

            if(lines.Count > max_lines)
                lines.Dequeue();
        }

        public void Draw(RLConsole console) {
            console.Clear();
            string[] lines_to_array = lines.ToArray();

            for(int i = 0 ; i < lines_to_array.Length ; i++)
                console.Print(1, i + 1, lines_to_array[i], RLColor.White);
        }
    }
}
