﻿using System;

namespace SGM.GEP.Infra.Data.Mongo.Utils
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}