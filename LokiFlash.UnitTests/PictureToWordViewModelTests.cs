using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using LokiFlash.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace LokiFlash.UnitTests
{
    [TestFixture]
    public class PictureToWordViewModelTests
    {
        private IWordsList SetupTestSource()
        {
            var words = new[]
                            {
                                new Word("the", new FileInfo("the.wmf")), new Word("and", new FileInfo("and.wmf")),
                                new Word("is", new FileInfo("is.wmf")), new Word("it", new FileInfo("it.wmf"))
                            };

            return new Words("test",words);
        }

        private IScoreTracker GetFakeTracker()
        {
            return MockRepository.GenerateMock<IScoreTracker>();
        }

        [Test]
        public void Constructs_From_Source()
        {
            var model = new PictureToWordViewModel(SetupTestSource(), GetFakeTracker());

            // we should have only one picture
            model.Target.Should().NotBeNull();

            // we should have MaximumWord words
            model.PossibleAnswers.Should().HaveCount(model.MaximumWords);

            // all words should be unique
            model.PossibleAnswers.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Constructs_From_Source_1000_Times()
        {
            var source = SetupTestSource();

            for (int i = 0; i != 1000; i++)
            {
                var model = new PictureToWordViewModel(source, GetFakeTracker());

                // we should have only one picture
                model.Target.Should().NotBeNull();

                // we should have MaximumWord words
                model.PossibleAnswers.Should().HaveCount(model.MaximumWords);

                // all words should be unique
                model.PossibleAnswers.Select(a=>a.Word).Should().OnlyHaveUniqueItems();
            }
        }

        [Test]
        public void Next_Changes_State()
        {
            var model = new PictureToWordViewModel(SetupTestSource(), GetFakeTracker());

            var pp = model.Target;

            model.Next();

            model.Target.Should().NotBe(pp);

            // we should have only one picture
            model.Target.Should().NotBeNull();

            // we should have MaximumWord words
            model.PossibleAnswers.Should().HaveCount(model.MaximumWords);

            // all words should be unique
            model.PossibleAnswers.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Should_Cycle_Through_All_Pictures_Without_Repeats()
        {
            var model = new PictureToWordViewModel(SetupTestSource(), GetFakeTracker());

            var paths = new List<WordViewModel> {model.Target};

            for(var i=0;i!=3;i++)
            {
                model.Next();
                paths.Add(model.Target);
            }

            paths.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Moving_Next_Should_Never_Show_A_Repeat()
        {
            var model = new PictureToWordViewModel(SetupTestSource(), GetFakeTracker());
            var last = model.Target;

            for(var i=0;i!=100;i++)
            {
                model.Next();
                model.Target.Should().NotBe(last);
                last = model.Target;
            }
        }
    }


}
