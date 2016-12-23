﻿// Copyright (c) 2015-2016, Saritasa. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Saritasa.Tools.Messages.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Abstractions;

    /// <summary>
    /// Simple in memory message repository.
    /// </summary>
    public class InMemoryMessageRepository : IMessageRepository
    {
        /// <summary>
        /// All stored messages.
        /// </summary>
        public IList<IMessage> Messages { get; }

        readonly object objLock = new object();

        /// <summary>
        /// .ctor
        /// </summary>
        public InMemoryMessageRepository()
        {
            Messages = new List<IMessage>();
        }

        /// <inheritdoc />
        public void Add(IMessage message)
        {
            lock (objLock)
            {
                Messages.Add(message);
            }
        }

        /// <inheritdoc />
        public IEnumerable<IMessage> Get(MessageQuery messageQuery)
        {
            lock (objLock)
            {
                return Messages.Where(messageQuery.Match).ToList();
            }
        }

        /// <inheritdoc />
        public void SaveState(IDictionary<string, object> dict)
        {
            // no need to implement since repository does not have state
        }

        /// <summary>
        /// Dump messages into string.
        /// </summary>
        /// <returns>String with data.</returns>
        public string Dump()
        {
            var sb = new StringBuilder((Messages.Count + 1) * 250);
            foreach (var message in Messages)
            {
                sb.AppendLine(JsonConvert.SerializeObject(message, Formatting.Indented));
                sb.AppendLine("----------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Create repository from dictionary.
        /// </summary>
        /// <param name="dict">Properties.</param>
        /// <returns>Message repository.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            Justification = "Need only for general purpose", MessageId = "dict")]
        public static IMessageRepository CreateFromState(IDictionary<string, object> dict)
        {
            return new InMemoryMessageRepository();
        }
    }
}
