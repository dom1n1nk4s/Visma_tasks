using Source;
string loginUser()
{

    string x = "";
    while (!x.Any())
    {
        Console.WriteLine("Enter your name");
        x = Console.ReadLine() ?? "";
    }
    return x;
}
void printMeetings(IEnumerable<Meeting> meetings)
{
    /*Used in listing/filtering meetings*/

    if (!meetings.Any())
    {
        Console.WriteLine("No such meetings found");
        return;
    }
    foreach (Meeting m in meetings)
    {
        Console.WriteLine($@"
        Id: {m.Id}
        Name: {m.Name}
        ResponsiblePerson: {m.ResponsiblePerson}
        Description: {m.Description}
        Category: {m.Category}
        Type: {m.Type}
        StartDate: {m.StartDate}
        EndDate: {m.EndDate}");

        Console.Write("        Attendees: ");
        foreach (var attendee in m.Attendees)
        {
            Console.Write($"{attendee}; ");
        }
        Console.WriteLine();

    }
}
void Menu()
{
    string currentPerson = loginUser();
    MeetingManager.Initialize();
    while (true)
    {
        Console.WriteLine(@"
        1. Create a new meeting.
        2. Delete a meeting.
        3. Add a person to a meeting.
        4. Remove a person from a meeting.
        5. List all meeting by filter.
        6. Log in as different user.
        7. Exit
        Choose an option:
        ");
        var choice = Console.ReadLine() ?? "";
        if (choice == "") continue;
        switch (choice[0])
        {
            case '1':

                Console.WriteLine("Enter meeting name:");
                var name = Console.ReadLine() ?? "";
                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty");
                    continue;
                }
                Console.WriteLine("Enter meeting description:");
                var description = Console.ReadLine() ?? "";
                if (description == "")
                {
                    Console.WriteLine("Description cannot be empty");
                    continue;
                }

                Console.WriteLine("Choose meeting category (1-CodeMonkey; 2-Hub; 3-Short; 4-TeamBuilding):");
                ECategory category;
                var categoryChoice = Console.ReadLine() ?? "";
                if (categoryChoice == "")
                {
                    Console.WriteLine("Invalid choice");
                    continue;
                }
                switch (categoryChoice[0])
                {
                    case '1':
                        category = ECategory.CodeMonkey;
                        break;
                    case '2':
                        category = ECategory.Hub;
                        break;
                    case '3':
                        category = ECategory.Short;
                        break;
                    case '4':
                        category = ECategory.TeamBuilding;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        continue;
                }

                Console.WriteLine("Choose meeting type (1-Live; 2-InPerson)");
                EType type;
                var typeChoice = Console.ReadLine() ?? "";
                if (typeChoice == "")
                {
                    Console.WriteLine("Invalid choice");
                    continue;
                }
                switch (typeChoice[0])
                {
                    case '1':
                        type = EType.Live;
                        break;
                    case '2':
                        type = EType.InPerson;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        continue;
                }
                DateTime startDate, endDate;
                try
                {
                    Console.WriteLine("Enter start date (format - MM/DD/YYYY HH:MM:SS)");
                    var sStartDate = Console.ReadLine() ?? "";
                    startDate = DateTime.Parse(sStartDate);
                    Console.WriteLine("Enter end date (format - MM/DD/YYYY HH:MM:SS)");
                    var sEndDate = Console.ReadLine() ?? "";
                    endDate = DateTime.Parse(sEndDate);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                MeetingManager.CreateMeeting(new Meeting(name, currentPerson, description, category, type, startDate, endDate));
                Console.WriteLine("New meeting created");
                break;
            case '2':
                Console.WriteLine("Enter meeting id");
                var id = Console.ReadLine() ?? "";
                if (id == "")
                {
                    Console.WriteLine("Id cannot be empty");
                    continue;
                }
                try
                {
                    MeetingManager.RemoveMeeting(id, currentPerson);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine("Meeting removed");
                break;
            case '3':
                Console.WriteLine("Enter meeting Id");
                id = Console.ReadLine() ?? "";
                if (id == "")
                {
                    Console.WriteLine("Id cannot be empty");
                    continue;
                }
                Console.WriteLine("Enter person's name");
                name = Console.ReadLine() ?? "";
                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty");
                    continue;
                }

                try
                {
                    MeetingManager.AddPerson(id, name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine("Person added");
                break;
            case '4':
                Console.WriteLine("Enter meeting Id");
                id = Console.ReadLine() ?? "";
                if (id == "")
                {
                    Console.WriteLine("Id cannot be empty");
                    continue;
                }
                Console.WriteLine("Enter person's name");
                name = Console.ReadLine() ?? "";
                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty");
                    continue;
                }

                try
                {
                    MeetingManager.RemovePerson(id, name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine("Person removed");
                break;
            case '5':
                Console.WriteLine("Choose filter type (1-Description; 2-ResponsiblePerson; 3-Category; 4-Type; 5-Dates; 6-NumberOfAttendees)");

                var filterChoice = Console.ReadLine() ?? "";
                if (filterChoice == "")
                {
                    Console.WriteLine("Invalid choice");
                    continue;
                }
                switch (filterChoice[0])
                {
                    case '1':
                        Console.WriteLine("Enter text to filter by");
                        var text = Console.ReadLine() ?? "";
                        if (text == "")
                        {
                            Console.WriteLine("Text cannot be empty");
                            continue;
                        }
                        printMeetings(MeetingManager.ListMeetings(m => m.Description.Contains(text)));

                        break;
                    case '2':
                        Console.WriteLine("Enter person to filter by");
                        text = Console.ReadLine() ?? "";
                        if (text == "")
                        {
                            Console.WriteLine("Text cannot be empty");
                            continue;
                        }
                        printMeetings(MeetingManager.ListMeetings(m => m.ResponsiblePerson.Contains(text)));

                        break;
                    case '3':
                        Console.WriteLine("Choose meeting category (1-CodeMonkey; 2-Hub; 3-Short; 4-TeamBuilding):");
                        categoryChoice = Console.ReadLine() ?? "";
                        if (categoryChoice == "")
                        {
                            Console.WriteLine("Invalid choice");
                            continue;
                        }
                        switch (categoryChoice[0])
                        {
                            case '1':
                                category = ECategory.CodeMonkey;
                                break;
                            case '2':
                                category = ECategory.Hub;
                                break;
                            case '3':
                                category = ECategory.Short;
                                break;
                            case '4':
                                category = ECategory.TeamBuilding;
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                continue;
                        }
                        printMeetings(MeetingManager.ListMeetings(m => m.Category == category));
                        break;
                    case '4':

                        Console.WriteLine("Choose meeting type (1-Live; 2-InPerson)");

                        typeChoice = Console.ReadLine() ?? "";
                        if (typeChoice == "")
                        {
                            Console.WriteLine("Invalid choice");
                            continue;
                        }
                        switch (typeChoice[0])
                        {
                            case '1':
                                type = EType.Live;
                                break;
                            case '2':
                                type = EType.InPerson;
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                continue;
                        }
                        printMeetings(MeetingManager.ListMeetings(m => m.Type == type));
                        break;

                    case '5':
                        Console.WriteLine("Enter start date (can be empty, format - MM/DD/YYYY HH:MM:SS)");
                        var sStartDate = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter end date (can be empty, format - MM/DD/YYYY HH:MM:SS)");
                        var sEndDate = Console.ReadLine() ?? "";
                        startDate = new DateTime();
                        endDate = new DateTime();

                        if (sStartDate == "")
                        {
                            startDate = DateTime.MinValue;
                        }
                        else
                        {
                            try
                            {
                                startDate = DateTime.Parse(sStartDate);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid choice");
                            }
                        }

                        if (sEndDate == "")
                        {
                            startDate = DateTime.MaxValue;
                        }
                        else
                        {
                            try
                            {
                                endDate = DateTime.Parse(sEndDate);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid choice");
                            }
                        }
                        printMeetings(MeetingManager.ListMeetings(m => m.StartDate >= startDate && m.EndDate <= endDate));

                        break;

                    case '6':
                        Console.WriteLine("Choose filter method (1-MoreThanX; 2-LessThanX; 3-EqualToX)");
                        var attendeeFilterChoice = Console.ReadLine() ?? "";
                        if (attendeeFilterChoice == "")
                        {
                            Console.WriteLine("Invalid choice");
                            continue;
                        }

                        Console.WriteLine("Enter X");
                        var sNumber = Console.ReadLine() ?? "";
                        int X;
                        try
                        {
                            X = int.Parse(sNumber);
                            if (X < 0) throw new Exception();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid number");
                            continue;
                        }
                        Func<Meeting, bool> predicate = m => true;
                        switch (attendeeFilterChoice[0])
                        {
                            case '1':
                                predicate = m => m.Attendees.Count > X;
                                break;
                            case '2':
                                predicate = m => m.Attendees.Count < X;
                                break;
                            case '3':
                                predicate = m => m.Attendees.Count == X;
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        printMeetings(MeetingManager.ListMeetings(predicate));
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        continue;
                }
                break;
            case '6':
                currentPerson = loginUser();
                break;
            case '7':
                MeetingManager.SaveMeetings();
                return;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
}

Menu();
