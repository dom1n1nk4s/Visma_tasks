

using System.Text.Json;
namespace Source
{
    public class MeetingManager
    {
        /*
        A singleton to manage all the meetings and actions related to them.
        */
        static List<Meeting> meetings = new List<Meeting>();
        public static void Initialize()
        {
            string jsonString = "";
            try
            {
                jsonString = File.ReadAllText("meetings.json");
                if (jsonString == "") throw new Exception();
            }
            catch (Exception)
            {
                return;
            }

            meetings = JsonSerializer.Deserialize<List<Meeting>>(jsonString) ?? new List<Meeting>();
        }
        public static void SaveMeetings()
        {
            string jsonString = JsonSerializer.Serialize(meetings);
            File.WriteAllText("meetings.json", jsonString);
        }
        public static void CreateMeeting(Meeting meeting)
        {
            /*No error checking here, as all of it is done via the menu.*/
            meetings.Add(meeting);
        }
        public static void RemoveMeeting(string id, string responsiblePerson)
        {
            var meeting = meetings.FirstOrDefault(m => m.Id == id);
            if (meeting == null)
            {
                throw new Exception("No such meeting found");
                /*Could've used specific exceptions such as ArgumentNullException, but for this simple console application, I thought this would suffice.*/
            }
            if (meeting.ResponsiblePerson != responsiblePerson)
            {
                throw new Exception("You are not responsible for this meeting");
            }
            meetings.Remove(meeting);

        }
        public static void AddPerson(string id, string person)
        {
            var meeting = meetings.FirstOrDefault(m => m.Id == id);
            if (meeting == null)
            {
                throw new Exception("No such meeting found");
            }
            if (meeting.Attendees.Contains(person))
            {
                throw new Exception("Person already in meeting");
            }
            if (meetings.Where(m => m.Attendees.Contains(person) &&
             ((m.StartDate <= meeting.StartDate && meeting.StartDate <= m.EndDate) ||
               (meeting.StartDate <= m.StartDate && m.StartDate <= meeting.EndDate))).Any())
                Console.WriteLine("Warning. Person already in meeting during new meeting's time.");

            meeting.Attendees.Add(person);
        }
        public static void RemovePerson(string id, string person)
        {
            var meeting = meetings.FirstOrDefault(m => m.Id == id);
            if (meeting == null)
            {
                throw new Exception("No such meeting found");
            }
            if (!meeting.Attendees.Contains(person))
            {
                throw new Exception("Person is not in meeting");
            }
            if (meeting.ResponsiblePerson == person)
            {
                throw new Exception("Person cannot be removed, as he is responsible for the meeting");
            }
            meeting.Attendees.Remove(person);
        }
        public static IEnumerable<Meeting> ListMeetings(Func<Meeting, bool> predicate)
        {
            return meetings.Where(predicate);
        }
        public static void DeleteAllMeetings()
        {
            meetings.Clear();
        }
    }
}