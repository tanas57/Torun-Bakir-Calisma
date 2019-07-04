namespace Torun
{
    class Personnel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string getFullName() { return Name + " " + Surname; }
    }
    
}
