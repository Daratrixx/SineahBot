using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Data
{
    public class KnowledgeBase
    {
        private Dictionary<string, string> knowledge = new Dictionary<string, string>();
        private Dictionary<string, string> knowledgeCompiled = new Dictionary<string, string>();

        public KnowledgeBase SetKnowledge(IEnumerable<string> keys, string value)
        {
            foreach (var key in keys)
            {
                knowledge[key] = value;
            }
            return this;
        }
        public KnowledgeBase SetKnowledge(string key, string value)
        {
            knowledge[key] = value;
            return this;
        }

        public KnowledgeBase MergeKnowledge(KnowledgeBase knowledgeBase)
        {
            foreach (var key in knowledgeBase.knowledge.Keys)
            {
                knowledge[key] = knowledgeBase.knowledge[key];
            }
            return this;
        }

        public KnowledgeBase CompileKnowledge()
        {
            knowledgeCompiled.Clear();
            foreach (var key in knowledge.Keys)
            {
                var stringVal = knowledge[key];
                foreach (var key2 in knowledge.Keys)
                {
                    if (key != key2 && stringVal.Contains(key2, StringComparison.OrdinalIgnoreCase)
                    && !stringVal.Contains("[" + key2, StringComparison.OrdinalIgnoreCase)
                    && !stringVal.Contains(key2 + "]", StringComparison.OrdinalIgnoreCase))
                        stringVal = stringVal.Replace(key2, $"[{key2}]", StringComparison.OrdinalIgnoreCase);
                }
                var key3 = key.ToLowerInvariant();
                if (knowledgeCompiled.ContainsKey(key3))
                    Logging.Log($"Error compiling knowledge: {key3} replaced.");
                knowledgeCompiled[key3] = stringVal;
            }
            if (knowledgeCompiled.Count != knowledge.Count)
            {
                var missing = $"[MISSING: {String.Join(',', knowledge.Keys.Where(x => !knowledgeCompiled.ContainsKey(x.ToLowerInvariant())))}]";
                Logging.Log($"Error compiling knowledge: {knowledgeCompiled.Count}/{knowledge.Count} compiled. {missing}");
            }
            return this;
        }

        public string GetKnowledge(string key)
        {
            if (knowledgeCompiled.TryGetValue(key, out string val))
                return val;
            if (key.Last() != 's')
                return GetKnowledge(key + "s");
            return null;
        }

        public string GetRandomKnowledge()
        {
            return knowledgeCompiled.Values.GetRandom();
        }

        public bool needCompile { get => knowledgeCompiled.Count == 0 || knowledgeCompiled.Count != knowledge.Count; }
    }

    public class MultiDictionnary<Tkey, Tvalue> where Tkey : notnull
    {
        private Dictionary<Tkey, List<Tvalue>> data = new Dictionary<Tkey, List<Tvalue>>();

        public MultiDictionnary<Tkey, Tvalue> Add(Tkey key, params Tvalue[] values)
        {
            return Add(key, values);
        }
        public MultiDictionnary<Tkey, Tvalue> Add(Tkey key, IEnumerable<Tvalue> values)
        {
            List<Tvalue> list;
            if (!data.TryGetValue(key, out list))
            {
                list = new List<Tvalue>();
                data[key] = list;
            }
            list.AddRange(values);
            return this;
        }

        public List<Tvalue> this[Tkey key] { get => data[key]; }

        public int Count { get { return data.Count; } }

        public IEnumerable<Tkey> Keys { get => data.Keys; }
        public bool ContainsKey(Tkey key)
        {
            return data.ContainsKey(key);
        }
    }
}
