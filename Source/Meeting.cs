namespace Source
{
    public class Meeting
    {
        public string Id { get; }
        public string Name { get; }
        public string ResponsiblePerson { get; }
        public string Description { get; }
        public ECategory Category { get; }
        public EType Type { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public List<string> Attendees { get; }
        /*
        Ideally, each 'attendee' or 'person' would have it's own class with a guid, a name, etc.
        But for this example, I imagine using plain strings is the correct way to go.
        */

        public Meeting(string name, string responsiblePerson, string description, ECategory category, EType type, DateTime startDate, DateTime endDate, string id = "", List<string>? attendees = null)
        {
            Id = id == "" ? IdGenerator.Generate() : id;
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Attendees = attendees ?? new List<string> { responsiblePerson };
        }
    }

    public enum ECategory
    {
        CodeMonkey,
        Hub,
        Short,
        TeamBuilding
    }
    public enum EType
    {
        Live,
        InPerson
    }
}