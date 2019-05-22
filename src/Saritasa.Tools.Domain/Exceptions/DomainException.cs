﻿// Copyright (c) 2015-2019, Saritasa. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
#if NET40
using System.Runtime.Serialization;
#endif

namespace Saritasa.Tools.Domain.Exceptions
{
    /// <summary>
    /// Exception occurs in domain part of application. It can be business logic or validation exception.
    /// The message can be used as display messages to end user. InnerException should contain actual system exception.
    /// </summary>
#if NET40
    [Serializable]
#endif
    public class DomainException : Exception
    {
        /// <summary>
        /// Optional description code for this exception.
        /// </summary>
        public string Code { get; protected set; } = string.Empty;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DomainException() : base(DomainErrorDescriber.Default.Error())
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">Optional description code for this exception.</param>
        public DomainException(int code) : base(DomainErrorDescriber.Default.Error())
        {
            this.Code = code.ToString();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="code">Optional description code for this exception.</param>
        public DomainException(string message, int code) : base(message)
        {
            this.Code = code.ToString();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="code">Optional description code for this exception.</param>
        public DomainException(string message, string code) : base(message)
        {
            this.Code = code;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a
        /// null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DomainException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a
        /// null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        /// <param name="code">Optional description code for this exception.</param>
        public DomainException(string message, Exception innerException, int code) :
            base(message, innerException)
        {
            this.Code = code.ToString();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a
        /// null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        /// <param name="code">Optional description code for this exception.</param>
        public DomainException(string message, Exception innerException, string code) :
            base(message, innerException)
        {
            this.Code = code;
        }

#if NET40
        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or deserialize an object.</param>
        /// <param name="context">Describes the source and destination of a given serialized stream,
        /// and provides an additional caller-defined context.</param>
        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Code = info.GetString("code");
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("code", Code);
        }
#endif
    }
}
