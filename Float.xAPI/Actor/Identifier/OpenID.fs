﻿// <copyright file="OpenID.cs" company="Float">
// Copyright (c) 2018 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

namespace Float.xAPI.Actor.Identifier

open System
open Float.xAPI.Interop

/// <summary>
/// An openID that uniquely identifies the Agent.
/// </summary>
type public IOpenID =
    /// <summary>
    /// An openID that uniquely identifies the Agent.
    /// </summary>
    abstract member OpenID: Uri

    inherit IInverseFunctionalIdentifier

[<CustomEquality;NoComparison>]
type public OpenID =
    struct
        /// <inheridoc />
        val OpenID: Uri

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Float.xAPI.Actor.Identifier.OpenID"/> class.
        /// </summary>
        /// <param name="openID">An openID that uniquely identifies the Agent.</param>
        new (openID) =
            nullArg openID "openID"
            { OpenID = openID }

        /// <inheritdoc />
        override this.GetHashCode() = hash this.OpenID

        /// <inheritdoc />
        override this.ToString() = sprintf "<%O: %A>" (this.GetType().Name) this.OpenID

        /// <inheritdoc />
        override this.Equals(other) = 
            match other with
            | :? IOpenID as mailbox -> this.OpenID = mailbox.OpenID
            | _ -> false

        interface IEquatable<IOpenID> with
            member this.Equals other = this.Equals other

        interface IOpenID with
            member this.OpenID = this.OpenID
    end
