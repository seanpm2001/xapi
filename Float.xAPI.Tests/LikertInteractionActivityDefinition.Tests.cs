﻿// <copyright file="LikertInteractionActivityDefinition.Tests.cs" company="Float">
// Copyright (c) 2018 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using System.Collections.Generic;
using Float.xAPI.Activities;
using Float.xAPI.Activities.Definitions;
using Float.xAPI.Languages;
using Xunit;

namespace Float.xAPI.Tests
{
    public class LikertInteractionActivityDefinitionTests : IInitializationTests<LikertInteractionActivityDefinition>
    {
        [Fact]
        public LikertInteractionActivityDefinition TestValidInit()
        {
            var scale = new List<IInteractionComponent>
            {
                new InteractionComponent("likert_0", LanguageMap.EnglishUS("It's OK")),
                new InteractionComponent("likert_1", LanguageMap.EnglishUS("It's Pretty Cool")),
                new InteractionComponent("likert_2", LanguageMap.EnglishUS("It's Damn Cool")),
                new InteractionComponent("likert_3", LanguageMap.EnglishUS("It's Gonna Change the World"))
            };

            var definition1 = new LikertInteractionActivityDefinition(
                LanguageMap.EnglishUS("Likert"),
                LanguageMap.EnglishUS("How awesome is Experience API?"),
                new ResponsePattern(new CharacterString("likert_3")),
                scale);

            var definition2 = new LikertInteractionActivityDefinition(
                LanguageMap.EnglishUS("Likert"),
                LanguageMap.EnglishUS("How awesome is Experience API?"),
                new ResponsePattern(new CharacterString("likert_3")),
                scale,
                new Uri("http://www.example.com/more"));

            return new LikertInteractionActivityDefinition(
                LanguageMap.EnglishUS("Likert"),
                LanguageMap.EnglishUS("How awesome is Experience API?"),
                new ResponsePattern(new CharacterString("likert_3")),
                scale,
                new Uri("http://www.example.com/more"),
                new List<KeyValuePair<Uri, string>> { new KeyValuePair<Uri, string>(new Uri("http://www.example.com/ext"), "ext") });
        }

        [Fact]
        public void TestInvalidInit()
        {
            var scale1 = new List<IInteractionComponent>
            {
                new InteractionComponent("likert_0", LanguageMap.EnglishUS("It's OK"))
            };

            var scale2 = new List<IInteractionComponent>();

            Assert.Throws<ArgumentNullException>(() => new LikertInteractionActivityDefinition(null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new LikertInteractionActivityDefinition(LanguageMap.EnglishUS("a"), null, null, null));
            Assert.Throws<ArgumentNullException>(() => new LikertInteractionActivityDefinition(LanguageMap.EnglishUS("a"), LanguageMap.EnglishUS("b"), null, null));
            Assert.Throws<ArgumentException>(() => new LikertInteractionActivityDefinition(LanguageMap.EnglishUS("a"), LanguageMap.EnglishUS("b"), new ResponsePattern("c"), scale2));
            Assert.Throws<ArgumentException>(() => new LikertInteractionActivityDefinition(LanguageMap.EnglishUS("a"), LanguageMap.EnglishUS("b"), new ResponsePattern("c"), scale2, new Uri("a://b.c"), new Dictionary<Uri, string>()));
        }

        [Fact]
        public void TestProperties()
        {
            var scale = new List<IInteractionComponent>
            {
                new InteractionComponent("likert_0", LanguageMap.EnglishUS("It's OK")),
                new InteractionComponent("likert_1", LanguageMap.EnglishUS("It's Pretty Cool")),
                new InteractionComponent("likert_2", LanguageMap.EnglishUS("It's Damn Cool")),
                new InteractionComponent("likert_3", LanguageMap.EnglishUS("It's Gonna Change the World"))
            };

            var ext = new List<KeyValuePair<Uri, string>> { new KeyValuePair<Uri, string>(new Uri("http://www.example.com/ext"), "ext") };

            var definition = new LikertInteractionActivityDefinition(
                LanguageMap.EnglishUS("Likert"),
                LanguageMap.EnglishUS("How awesome is Experience API?"),
                new ResponsePattern("likert_3"),
                scale,
                new Uri("http://www.example.com/more"),
                new List<KeyValuePair<Uri, string>> { new KeyValuePair<Uri, string>(new Uri("http://www.example.com/ext"), "ext") });

            Assert.Equal(new ResponsePattern("likert_3"), definition.CorrectResponsesPattern);
            Assert.Equal(LanguageMap.EnglishUS("How awesome is Experience API?"), definition.Description);
            Assert.Equal(ext, definition.Extensions.Value);
            Assert.Equal(Interaction.Likert, definition.InteractionType);
            Assert.Equal(new Uri("http://www.example.com/more"), definition.MoreInfo);
            Assert.Equal(LanguageMap.EnglishUS("Likert"), definition.Name);
            Assert.Equal(scale, definition.Scale);
            Assert.Equal(new Uri("http://adlnet.gov/expapi/activities/cmi.interaction"), definition.Type);

            var idefinition = definition as ILikertInteractionActivityDefinition;
            Assert.Equal(new ResponsePattern("likert_3"), idefinition.CorrectResponsesPattern);
            Assert.Equal(LanguageMap.EnglishUS("How awesome is Experience API?"), idefinition.Description);
            Assert.Equal(ext, idefinition.Extensions.Value);
            Assert.Equal(Interaction.Likert, idefinition.InteractionType);
            Assert.Equal(new Uri("http://www.example.com/more"), idefinition.MoreInfo);
            Assert.Equal(LanguageMap.EnglishUS("Likert"), idefinition.Name);
            Assert.Equal(scale, idefinition.Scale);
            Assert.Equal(new Uri("http://adlnet.gov/expapi/activities/cmi.interaction"), idefinition.Type);
        }
    }
}
