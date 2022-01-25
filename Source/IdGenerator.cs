namespace Source
{
    public class IdGenerator
    {
        /*
        Since we need to select meetings somehow, a guid is needed but would take too long to specify via command line.
        So I thought of a "short guid" class to make things work.
        */
        static Random random = new Random();
        public static string Generate()
        {
            char[] id = new char[5];
            for (int i = 0; i < 5; i++)
            {
                id[i] = (char)random.Next(65, 91); /*capital letter*/
                /*Should also check whether an existing meeting already has the same id*/
            }
            return new string(id);

        }
    }
}