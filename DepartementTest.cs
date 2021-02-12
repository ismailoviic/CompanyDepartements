using System;
using Xunit;
using System.Collections.Generic;
using CompanyDepartements;
using Xunit.Abstractions;
using System.Linq;

namespace DepartementsTests
{
    public class DepartementTest : IClassFixture<DepartementsFixture>
    {
        private readonly DepartementsFixture _departementsFixture;
        private readonly ITestOutputHelper _output;

        public readonly List<Recru> NewRecrus;

        public DepartementTest(DepartementsFixture departementsFixture, ITestOutputHelper output)
        {
            _departementsFixture = departementsFixture;
            _output = output;
            NewRecrus = new List<Recru>()
            { new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev),
                new Recru("Ismailovic", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev),
                new Recru("Yassine", 23, "Yassine@alphorm.com", Position.Front, DepartementsType.Dev),
                new Recru("Said", 28, "Said@alphorm.com", Position.Marketer, DepartementsType.Marketing),
                new Recru("Choukri", 25, "Yassine@alphorm.com", Position.Marketer, DepartementsType.Marketing),
                new Recru("Aymen", 26, "Aymen@alphorm.com", Position.Chef, DepartementsType.Rh),
                new Recru("Aymane", 25, "Aymane@alphorm.com", Position.Saler, DepartementsType.Sales),
                new Recru("Manal", 23, "Manal@alphorm.com", Position.Front, DepartementsType.Dev) };
        }


        [Fact]
        public void Affect1Recru()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _output.WriteLine("Test Works ");
            var Ismail = new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev);
            _departementsFixture.Departement.Affect(ListRecrus, Ismail);
            Assert.NotEmpty(ListRecrus);
            Assert.Equal("Ismail", _departementsFixture.Departement.GetRecruByName(ListRecrus, "Ismail").Name);
        }


        [Fact]
        public void AffectListRecrus()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _output.WriteLine("Test Works ");
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            Assert.NotEmpty(ListRecrus);
            Assert.Equal(NewRecrus.Count, ListRecrus.Count);
            Assert.Equal("Yassine", _departementsFixture.Departement.GetRecruByName(ListRecrus, "Yassine").Name);
        }
        [Fact]
        public void DeleteRecruFromDev()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _output.WriteLine("Test Works ");
            Assert.Empty(ListRecrus);
            var Ismail = new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev);
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            _departementsFixture.Departement.DeleteRecrus(ListRecrus, Ismail);
            Assert.False(_departementsFixture.Departement.DepartementContainsRecru(ListRecrus, Ismail.Name));
        }
        [Fact]
        public void DeleteManyRecrusFromDev()
        {
            List<Recru> ListRecrus = new List<Recru>();
            Assert.Empty(ListRecrus);
            var IsmailManal = new List<Recru>() {
                new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev),
                new Recru("Manal", 23, "Manal@alphorm.com", Position.Front, DepartementsType.Dev) };
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            _departementsFixture.Departement.DeleteRecrus(ListRecrus, IsmailManal);
            Assert.False(_departementsFixture.Departement.DepartementContainsRecru(ListRecrus, "Manal"));
            Assert.True(_departementsFixture.Departement.DepartementContainsRecru(ListRecrus, "Yassine"));
        }

        [Fact]
        public void SearchTakesName()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "isma");
            var recrusNamedIsma = NewRecrus.Where(recru => recru.Name.Contains("isma")).ToList();
            Assert.Equal(recrusNamedIsma.Count, searchResult.Count);
        }
        [Fact]
        public void SearchTakesNullReturnsNull()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, null);
            Assert.Null(searchResult);
        }
        [Fact]
        public void SearchTakesEmptyReturnsEmpty()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "");
            Assert.Empty(searchResult);
        }
        [Fact]
        public void SearchTakesInvalidString()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "ff15");
            Assert.Empty(searchResult);
        }
        [Fact]
        public void SearchTakesDepartement()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "Dev");
            var recrusInDev = NewRecrus.Where(recru => recru.Departements == DepartementsType.Dev).ToList();
            Assert.Equal(recrusInDev.Count, searchResult.Count);
        }
        [Fact]
        public void SearchTakesEmail()
        {
            List<Recru> ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "Ismail@");
            var recrusEmail = NewRecrus.Where(recru => recru.Email.Contains("Ismail@")).ToList();
            Assert.Equal(recrusEmail.Count, searchResult.Count);
        }
    }
}
