﻿using System;

namespace Contracts
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long ItemsCount { get; set; }        
    }
}