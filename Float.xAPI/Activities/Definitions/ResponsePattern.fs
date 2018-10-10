﻿// <copyright file="ResponsePattern.fs" company="Float">
// Copyright (c) 2018 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

namespace Float.xAPI.Activities.Definitions

open System.Runtime.InteropServices
open Float.xAPI.Interop

/// <summary>
/// The Correct Responses Pattern contains an array of response patterns.
/// A learner's response will be considered correct if it matches any of the response patterns in that array.
/// Where a response pattern is a delimited list, the learner's response is only considered correct if all of the items in that list match the learner's response.
/// </summary>
type IResponsePattern =
    /// <summary>
    /// An exhaustive list of possible correct responses.
    /// </summary>
    abstract member CharacterStrings: ICharacterString seq

    /// <summary>
    /// Whether or not the case of items in the list matters.
    /// </summary>
    abstract member CaseMatters: bool option

    /// <summary>
    /// Whether or not the order of items in the list matters.
    /// </summary>
    abstract member OrderMatters: bool option

    /// <summary>
    /// Returns true if the given string matches this response pattern.
    /// </summary>
    abstract member Match: string -> bool

    /// <summary>
    /// Returns true if the given strings match this response pattern.
    /// </summary>
    abstract member Match: string seq -> bool

[<NoEquality;NoComparison;Struct>]
type ResponsePattern =
    /// <inheritdoc />
    val CharacterStrings: ICharacterString seq

    /// <inheritdoc />
    val CaseMatters: bool option

    /// <inheritdoc />
    val OrderMatters: bool option
    
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Float.xAPI.Activities.Definitions.ResponsePattern"/> struct.
    /// Use this to create a response pattern with a single character string item.
    /// </summary>
    /// <param name="item">A single character string item.</param>
    /// <param name="caseMatters>Whether or not the case of items in the list matters.</param>
    /// <param name="orderMatters">Whether or not the order of items in the list matters.</param>
    new (item: string, [<Optional;DefaultParameterValue(null)>] ?caseMatters, [<Optional;DefaultParameterValue(null)>] ?orderMatters) =
        nullArg item "item"
        invalidStringArg item "item"
        { CharacterStrings = [ CharacterString(item) ]; CaseMatters = caseMatters; OrderMatters = orderMatters }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Float.xAPI.Activities.Definitions.ResponsePattern"/> struct.
    /// </summary>
    /// <param name="characterStrings">An exhaustive list of possible correct responses.</param>
    /// <param name="caseMatters>Whether or not the case of items in the list matters.</param>
    /// <param name="orderMatters">Whether or not the order of items in the list matters.</param>
    new (characterStrings: ICharacterString seq, [<Optional;DefaultParameterValue(null)>] ?caseMatters: bool, [<Optional;DefaultParameterValue(null)>] ?orderMatters: bool) =
        nullArg characterStrings "characterStrings"
        emptySeqArg characterStrings "characterStrings"
        { CharacterStrings = characterStrings; CaseMatters = caseMatters; OrderMatters = orderMatters }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Float.xAPI.Activities.Definitions.ResponsePattern"/> struct.
    /// Use this to create a simple true/false character string.
    /// </summary>
    /// <param name="characterString">A single correct response.</param>
    /// <param name="caseMatters>Whether or not the case of items in the list matters.</param>
    /// <param name="orderMatters">Whether or not the order of items in the list matters.</param>
    new (characterString: ICharacterString, [<Optional;DefaultParameterValue(null)>] ?caseMatters: bool, [<Optional;DefaultParameterValue(null)>] ?orderMatters: bool) =
        nullArg characterString "characterString"
        { CharacterStrings = [ characterString ]; CaseMatters = caseMatters; OrderMatters = orderMatters }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Float.xAPI.Activities.Definitions.ResponsePattern"/> struct.
    /// Use this to create a simple true/false character string.
    /// </summary>
    /// <param name="correct">The correct response.</param>
    new (correct: bool) =
        { CharacterStrings = [ CharacterString(correct.ToString().ToLower()) ] ; CaseMatters = Some false ; OrderMatters = None }

    /// <inheritdoc />
    member this.Match(str: string) =
        this.CharacterStrings |> Seq.exists(fun (cs) -> cs.Match(str))

    /// <inheritdoc />
    member this.Match(strseq: string seq) =
        this.CharacterStrings |> Seq.exists(fun (cs) -> cs.Match(strseq))
        
    /// <inheritdoc />
    override this.ToString() =
        let caseMattersString = match this.CaseMatters with
                                | Some cm -> sprintf "{case_matters=%O}"(toLowerString(toString cm))
                                | None -> ""
        let orderMattersString = match this.OrderMatters with
                                 | Some om -> sprintf "{order_matters=%O}" (toLowerString(toString om))
                                 | None -> ""
        let eachString = this.CharacterStrings |> Seq.map(fun x -> sprintf "\"%O%O%O\"" caseMattersString orderMattersString x)
        sprintf  "[%O]" (seqToString eachString)

    interface IResponsePattern with
        member this.CaseMatters = this.CaseMatters
        member this.OrderMatters = this.OrderMatters
        member this.CharacterStrings = this.CharacterStrings
        member this.Match(str: string) = this.Match str
        member this.Match(strseq: string seq) = this.Match strseq
