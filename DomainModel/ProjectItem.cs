﻿using System;

using DomainModel.Common;

namespace DomainModel
{
    public class ProjectItem : IAggregateRootProvider
    {
        private Guid id;

        private string name;

        private Project project;

        public ProjectItem(Guid id, string name, Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            this.id = id;
            this.name = name;
            this.project = project;
        }

        protected ProjectItem()
        {
        }

        public virtual Guid Id
        {
            get
            {
                return this.id;
            }

            protected internal set
            {
                this.id = value;
            }
        }

        public virtual File File { get; protected set; }

        public virtual string Name
        {
            get
            {
                return this.name;
            }

            protected set
            {
                this.name = value;
            }
        }

        public virtual Project Project
        {
            get
            {
                return this.project;
            }

            protected set
            {
                this.project = value;
            }
        }

        public virtual int Version { get; protected internal set; }

        IAggregateRoot IAggregateRootProvider.AggregateRoot => this.Project;

        public virtual void AppendToName(string itemsufix)
        {
            this.Name += itemsufix;
        }

        public virtual void LinkFile(File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            this.File = file;
        }
    }
}