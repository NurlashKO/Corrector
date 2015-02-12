using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Priority_Queue;

namespace WindowsFormsApplication1
{
    public class Dictionary
    {

        public List<string> words = new List<string>();
        int[,] d = new int[50, 50];
        HashSet <string> HS = new HashSet<string>();

        public class Word : PriorityQueueNode
        {
            public string Name { get; private set; }
            public Word(string name)
            {
                Name = name;
            }
        }

        HeapPriorityQueue<Word> priorityQueue = new HeapPriorityQueue<Word> (20);

        public bool isAvailable(string word)
        {
            return HS.Contains(word);
        }

        public void load(string fileName)
        {
            string word;
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            do
            {
                word = theReader.ReadLine();
                if (word != null)
                {
                    words.Add(word);
                    HS.Add(word);
                }
            } while (word != null);
        }

        public int wordDist(string s, string t)
        {
            int n = s.Length, m = t.Length;
            if (Math.Min(n, m) == 0)
                return Math.Max(n, m);
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                    if (Math.Min(i, j) == 0)
                        d[i, j] = Math.Max(i, j);
                    else
                    {
                        d[i, j] = Math.Min(d[i - 1, j], d[i, j - 1]) + 1;
                        d[i, j] = Math.Min(d[i, j], d[i - 1, j - 1] + (s[i - 1] == t[j - 1] ? 0 : 1));
                    }
            return d[n, m];
        }

        public List<string> findIdentical(string word, int cnt = 5)
        {
            priorityQueue.Clear();
            int cur = 0;
            foreach (string s in words)
            {
                cur = -wordDist(s, word);
                if (priorityQueue.Count() < cnt)
                    priorityQueue.Enqueue(new Word(s), cur);
                else
                {
                    if (priorityQueue.First().Priority < cur)
                    {
                        priorityQueue.Dequeue();
                        priorityQueue.Enqueue(new Word(s), cur);
                    }
                }

            }
            List<string> result = new List<string>();
            while (priorityQueue.Count != 0)
            {
                result.Add(priorityQueue.Dequeue().Name);
            }
            result.Reverse();
            return result;
        }

        public void process()
        {
            return;
        }
    }
}
