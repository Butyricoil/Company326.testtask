using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cifkor.Karpusha
{
    [Serializable]
    public class Attributes
    {
        public string name { get; set; }
        public string description { get; set; }
        public Life life { get; set; }
        public MaleWeight male_weight { get; set; }
        public FemaleWeight female_weight { get; set; }
        public bool hypoallergenic { get; set; }
    }
    [Serializable]
    public class Data
    {
        public string id { get; set; }
        public string type { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }
    }
    [Serializable]
    public class FemaleWeight
    {
        public int max { get; set; }
        public int min { get; set; }
    }
    [Serializable]
    public class Group
    {
        public Data data { get; set; }
    }
    [Serializable]
    public class Life
    {
        public int max { get; set; }
        public int min { get; set; }
    }
    [Serializable]
    public class Links
    {
        public string self { get; set; }
        public string current { get; set; }
        public string next { get; set; }
        public string last { get; set; }
    }
    [Serializable]
    public class MaleWeight
    {
        public int max { get; set; }
        public int min { get; set; }
    }
    [Serializable]
    public class Meta
    {
        public Pagination pagination { get; set; }
    }
    [Serializable]
    public class Pagination
    {
        public int current { get; set; }
        public int next { get; set; }
        public int last { get; set; }
        public int records { get; set; }
    }
    [Serializable]
    public class Relationships
    {
        public Group group { get; set; }
    }
    [Serializable]
    public class Root
    {
        public List<Data> data { get; set; }
        public Meta meta { get; set; }
        public Links links { get; set; }
    }
}
