﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;

namespace RocksmithDLCCreator.Manifest
{
    public class MyContractResolver : DefaultContractResolver
    {
        public static readonly MyContractResolver Instance = new MyContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(Attributes))
            {
                var ignoredInts = new List<string>() { "TargetScore", "ToneUnlockScore", "MaxPhraseDifficulty", "SongPartition", "TargetScore", "ToneUnlockScore" };
                var ignoredFloats = new List<string>() { "Score_MaxNotes", "Score_PNV", "SongLength" };
                if (property.PropertyName == "ChordTemplates")
                {
                    property.ShouldSerialize =
                      instance =>
                      {
                          return (instance as List<ChordTemplate>).Count > 0;
                      };
                }
                else if (property.PropertyName == "PhraseIterations")
                {
                    property.ShouldSerialize =
                      instance =>
                      {
                          return (instance as List<PhraseIteration>).Count > 0;
                      };
                }
                else if (property.PropertyName == "Phrases")
                {
                    property.ShouldSerialize =
                    instance =>
                    {
                        return (instance as List<Phrase>).Count > 0;
                    };
                }
                else if (property.PropertyName == "Sections")
                {
                    property.ShouldSerialize =
                    instance =>
                    {
                        return (instance as List<Section>).Count > 0;
                    };
                }
                else if (ignoredInts.Contains(property.PropertyName))
                {
                    property.ShouldSerialize =
                    instance =>
                    {
                        return ((int)instance) > 0;
                    };
                }
                else if (ignoredFloats.Contains(property.PropertyName))
                {
                    property.ShouldSerialize =
                    instance =>
                    {
                        return ((float)instance) > 0.01;
                    };
                }
            }

            return property;
        }
    }
}
