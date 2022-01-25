using System;
using NUnit.Framework;
namespace Source.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        MeetingManager.DeleteAllMeetings();

        MeetingManager.CreateMeeting(new Meeting("a New Meeting",
        "Bob",
        "Bob's meeting",
        ECategory.CodeMonkey
        , EType.Live,
        System.DateTime.Now,
        System.DateTime.Now.AddHours(2),
        "AAAAA"));
    }

    [Test]
    public void PersonAlreadyInMeeting()
    {
        Assert.Throws<Exception>(() => MeetingManager.AddPerson("AAAAA", "Bob"));
    }

    [Test]
    public void NoSuchMeetingFound()
    {
        Assert.Throws<Exception>(() => MeetingManager.AddPerson("", "Peter"));
    }

    [Test]
    public void NotResponsiblePersonRemovingMeeting()
    {
        Assert.Throws<Exception>(() => MeetingManager.RemoveMeeting("AAAAA", "Rob"));
    }
    [Test]
    public void RemoveResponsiblePersonFromMeeting()
    {
        Assert.Throws<Exception>(() => MeetingManager.RemovePerson("AAAAA", "Bob"));
    }
    [Test]
    public void RemovePersonNotInMeeting()
    {
        Assert.Throws<Exception>(() => MeetingManager.RemovePerson("AAAAA", "Dom"));
    }



}