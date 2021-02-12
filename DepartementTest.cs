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

        public readonly List<Recru> NewRecrus;

        public DepartementTest(DepartementsFixture departementsFixture)
        {
            _departementsFixture = departementsFixture;
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
            var ListRecrus = new List<Recru>();
            var Ismail = new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev);
            _departementsFixture.Departement.Affect(ListRecrus, Ismail);
            Assert.NotEmpty(ListRecrus);
            var sut = _departementsFixture.Departement.GetRecruByName(ListRecrus, "Ismail").Name;
            Assert.Equal("Ismail", sut);
        }


        [Fact]
        public void AffectListRecrus()
        {
            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            Assert.NotEmpty(ListRecrus);
            Assert.Equal(NewRecrus.Count, ListRecrus.Count);
            var sut = _departementsFixture.Departement.GetRecruByName(ListRecrus, "Yassine").Name;
            Assert.Equal("Yassine", sut);
        }
        [Fact]
        public void DeleteRecruFromDev()
        {
            var ListRecrus = new List<Recru>();
            var Ismail = new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev);
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            _departementsFixture.Departement.DeleteRecrus(ListRecrus, Ismail);
            var sut = _departementsFixture.Departement.DepartementContainsRecru(ListRecrus, Ismail.Name);
            Assert.False(sut);
        }
        [Fact]
        public void DeleteManyRecrusFromDev()
        {
            var ListRecrus = new List<Recru>();
            var IsmailManal = new List<Recru>() {
                new Recru("Ismail", 22, "Ismail@alphorm.com", Position.Back, DepartementsType.Dev),
                new Recru("Manal", 23, "Manal@alphorm.com", Position.Front, DepartementsType.Dev) };
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            _departementsFixture.Departement.DeleteRecrus(ListRecrus, IsmailManal);
            var sut = _departementsFixture.Departement.DepartementContainsRecru(ListRecrus, "Manal");
            Assert.False(sut);
            sut = _departementsFixture.Departement.DepartementContainsRecru(ListRecrus, "Yassine");
            Assert.True(sut);
        }

        [Fact]
        public void SearchTakesName()
        {
            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "isma");
            var recrusNamedIsma = NewRecrus.Where(recru => recru.Name.Contains("isma")).ToList();
            Assert.Equal(recrusNamedIsma.Count, searchResult.Count);
        }
        [Fact]
        public void SearchTakesNullReturnsEmpty()
        {
            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, null);
            Assert.Empty(searchResult);
        }
        [Fact]
        public void SearchTakesEmptyReturnsEmpty()
        {
            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "");
            Assert.Empty(searchResult);
        }
        [Fact]
        public void SearchTakesInvalidString()
        {
            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "ff15");
            Assert.Empty(searchResult);
        }
        [Fact]
        public void SearchTakesDepartement()
        {
            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "Dev");
            var recrusInDev = NewRecrus.Where(recru => recru.Departements == DepartementsType.Dev).ToList();
            Assert.Equal(recrusInDev.Count, searchResult.Count);
        }

        [Fact]
        public void SearchTakesEmail()
        {

            var ListRecrus = new List<Recru>();
            _departementsFixture.Departement.Affect(ListRecrus, NewRecrus);
            var searchResult = _departementsFixture.Departement.Search(ListRecrus, "Ismail@");
            var recrusEmail = NewRecrus.Where(recru => recru.Email.Contains("Ismail@")).ToList();
            Assert.Equal(recrusEmail.Count, searchResult.Count);
        }
    }
}
